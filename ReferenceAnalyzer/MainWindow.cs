using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Build.Evaluation;

namespace ReferenceAnalyzer
{
    public partial class MainWindow : Form
    {
        private const string NewLine = "\r\n";
        private const string NewLineAlt = "\n";
        private const string ProjExtension = ".csproj";
        private const char CustomNamespaceSeparator = ',';
        private const string ReferenceItemType = "Reference";
        private const string ProjectReferenceItemType = "ProjectReference";
        private const string SystemStr = "System";
        private const string ItemDelimiter = "    ";
        private const string RefsHeader = "References in project:";
        private const string UsagesHeader = "\r\nUsages in other projects:";
        private const string StatusTextFinished = "Finished";

        private readonly string _spreadsheetDelimiter = $"\r\n{ItemDelimiter}";
        private readonly string _assemblyref = $"{ItemDelimiter}assemblyref{ItemDelimiter}";
        private readonly string _projref = $"{ItemDelimiter}projref{ItemDelimiter}";
        private string _namespaceToCount = string.Empty;

        public MainWindow()
        {
            InitializeComponent();
            GetDataFromControls();
        }

        private void GetDataFromControls()
        {
            _namespaceToCount = string.IsNullOrEmpty(namespaceToCountTextBox.Text) ? string.Empty : namespaceToCountTextBox.Text;
        }

        private async void StartAnalysis(object sender, EventArgs e)
        {
            var projectList = projectsTextBox.Text.Split(new[] { NewLine, NewLineAlt }, StringSplitOptions.RemoveEmptyEntries);
            var root = pathToScanTextBox.Text;
            var projectsPaths = FindProjectsPaths(root, projectList);
            LoadProject(projectsPaths);
        }

        private string[] FindProjectsPaths(string path, string[] projectNames)
        {
            var files = Directory.GetFiles(path);
            var result = new List<string>();
            result.AddRange(files.Where(f => fullScanCheckBox.Checked
                ? Path.GetExtension(f) == ProjExtension
                : projectNames.Contains(Path.GetFileNameWithoutExtension(f)) && Path.GetExtension(f) == ProjExtension));

            var directories = Directory.GetDirectories(path);
            result.AddRange(directories.SelectMany(x => FindProjectsPaths(x, projectNames)));
            return result.ToArray();
        }

        private void LoadProject(string[] projectPaths)
        {
            var customRefs = string.IsNullOrEmpty(customReferenceTextBox.Text)
                ? new string[] { }
                : customReferenceTextBox.Text.Split(CustomNamespaceSeparator);
            var sb = new StringBuilder();
            var sbAnalyze = new StringBuilder();
            var priorityList = new List<(string, int)>();
            var usageList = new List<(string, ProjectItem[])>();
            var outputUsageList = new List<(string, int)>();
            var projectCollection = new ProjectCollection();

            List<TreeNode<string>> roots = new List<TreeNode<string>>(2);

            foreach (var projectPath in projectPaths)
            {
                var project = projectCollection.LoadProject(projectPath);
                var projectName = Path.GetFileNameWithoutExtension(projectPath);
                sb.AppendLine($"{projectName} [{ projectPath}]");

                var assemblyReferences = project.GetItems(ReferenceItemType);
                var filteredAssemblyReferences = FilterReferences(assemblyReferences, customRefs);

                var projectReferences = project.GetItems(ProjectReferenceItemType);
                var filteredProjectReferences = FilterReferences(projectReferences, customRefs);

                var projectNode = new TreeNode<string>(projectName);

                foreach (var assemblyRef in filteredAssemblyReferences)
                {
                    var projectItemToString = ProjectItemToString(assemblyRef);

                    sb.AppendLine(projectItemToString);
                    projectNode.Insert(projectNode, projectItemToString);
                }

                foreach (var projectRef in filteredProjectReferences)
                {
                    var projectItemToString = ProjectItemToString(projectRef);

                    sb.AppendLine(projectItemToString);
                    projectNode.Insert(projectNode, projectItemToString);
                }

                var insterted = false;
                foreach (var root in roots)
                {
                    insterted = root.Insert(root, projectNode).Item2;
                    if (insterted)
                        break;
                }

                if (!insterted)
                {
                    roots.Add(projectNode);
                }

                var allRefs = new List<ProjectItem>();
                allRefs.AddRange(filteredAssemblyReferences);
                allRefs.AddRange(filteredProjectReferences);

                usageList.Add((projectName, allRefs.ToArray()));
                priorityList.Add((projectName, allRefs.Count(x => x.EvaluatedInclude.Contains(_namespaceToCount))));

                projectCollection.UnloadProject(project);
            }

            sbAnalyze.AppendLine(RefsHeader);
            foreach (var tuple in priorityList.OrderBy(x => x.Item2))
            {
                sbAnalyze.AppendLine($"{tuple.Item1}{ItemDelimiter}{tuple.Item2}");
            }

            sbAnalyze.AppendLine(UsagesHeader);
            foreach (var (projName, _) in usageList)
            {
                var usages = usageList.SelectMany(r => r.Item2.Where(l => l.EvaluatedInclude.Contains(projName)));
                var usagesCount = usages.Count();

                outputUsageList.Add((projName, usagesCount));
            }

            foreach (var (projName, referenceCount) in outputUsageList.OrderByDescending(x => x.Item2))
            {
                sbAnalyze.AppendLine($"{projName}{ItemDelimiter}{referenceCount}");
            }

            sb.Append("DEBUG\r\n\r\n");

            foreach (var root in roots)
            {
                sb.Append(root);
            }

            analyzeTextBox.Text = sbAnalyze.ToString();
            outputTextBox.Text = sb.ToString();
            globalStatus.Text = StatusTextFinished;
        }

        private string ProjectItemToString(ProjectItem projectItem)
        {
            var projectRefName = fullInfoOutput.Checked
                ? projectItem.EvaluatedInclude + _spreadsheetDelimiter + (!projectItem.DirectMetadata.Any()
                      ? string.Empty
                      : ItemDelimiter + projectItem.DirectMetadata
                            .Select((a, b) => $"{a.Name}:{a.UnevaluatedValue}")
                            .Aggregate((a, b) => $"{a}, {b}"))
                : Path.GetFileNameWithoutExtension(projectItem.EvaluatedInclude);
            return projectRefName;
        }

        private List<ProjectItem> FilterReferences(ICollection<ProjectItem> assemblyReferences, string[] customRefs)
        {
            var filteredAssemblyReferences = assemblyReferences
                .Where(x => keepSystemCheckBox.Checked || !Path.GetFileName(x.EvaluatedInclude).StartsWith(SystemStr))
                .Where(x => !keepCustomProjectRefsCheckBox.Checked || !customRefs.Any(Path.GetFileName(x.EvaluatedInclude).StartsWith)).ToList();
            return filteredAssemblyReferences;
        }

        private void SetDirectoryPathDialogOpen(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog(this);
            var selectedPath = folderBrowserDialog1.SelectedPath;
            pathToScanTextBox.Text = string.IsNullOrEmpty(selectedPath) ? pathToScanTextBox.Text : selectedPath;
        }

        private void FullScanCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            projectsTextBox.Enabled = !fullScanCheckBox.Checked;
            StartAnalyzis.Enabled = IsStartAnalyzisButtonEnabled();
        }

        private void ProjectsTextBox_TextChanged(object sender, EventArgs e)
        {
            StartAnalyzis.Enabled = IsStartAnalyzisButtonEnabled();
        }

        private bool IsStartAnalyzisButtonEnabled()
        {
            return !string.IsNullOrEmpty(pathToScanTextBox.Text) && (fullScanCheckBox.Checked || !string.IsNullOrEmpty(projectsTextBox.Text));
        }
    }
}
