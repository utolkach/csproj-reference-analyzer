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
        private const string SpreadsheetDelimiter = "\r\n\t\t";
        private const string Assemblyref = "\tassemblyref\t";
        private const string Projref = "\tprojref\t";
        private const string RefsHeader = "***REFS***";
        private const string UsagesHeader = "\r\n***USAGES***";
        private const string StatusTextFinished = "Finished";

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

        private void StartAnalysis(object sender, EventArgs e)
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
            var usageList = new List<(string, string[])>();
            var outputUsageList = new List<(string, int)>();
            var projectCollection = new ProjectCollection();
            foreach (var projectPath in projectPaths)
            {
                var project = projectCollection.LoadProject(projectPath);
                var projectName = Path.GetFileNameWithoutExtension(projectPath);
                sb.AppendLine($"{projectName} [{ projectPath}]");

                var assemblyReferences = project.GetItems(ReferenceItemType);
                var filteredAssemblyReferences = FilterReferences(assemblyReferences, customRefs);

                var projectReferences = project.GetItems(ProjectReferenceItemType);
                var filteredProjectReferences = FilterReferences(projectReferences, customRefs);

                foreach (var assemblyRef in filteredAssemblyReferences)
                {
                    sb.AppendLine(Assemblyref + assemblyRef);
                }

                foreach (var projectRef in filteredProjectReferences)
                {
                    var projectRefName = Path.GetFileNameWithoutExtension(projectRef);
                    sb.AppendLine(Projref + projectRefName);
                }

                var allRefs = new List<string>();
                allRefs.AddRange(filteredAssemblyReferences);
                allRefs.AddRange(filteredProjectReferences);
                usageList.Add((projectName, allRefs.ToArray()));
                priorityList.Add((projectName, allRefs.Count(x => x.Contains(_namespaceToCount))));

                projectCollection.UnloadProject(project);
            }

            sbAnalyze.AppendLine(RefsHeader);
            foreach (var tuple in priorityList.OrderBy(x => x.Item2))
            {
                sbAnalyze.AppendLine($"{tuple.Item1}\t{tuple.Item2}");
            }

            sbAnalyze.AppendLine(UsagesHeader);
            foreach (var (projName, _) in usageList)
            {
                var usages = usageList.SelectMany(r => r.Item2.Where(l => l.Contains(projName)));
                var usagesCount = usages.Count();

                outputUsageList.Add((projName, usagesCount));
            }

            foreach (var (projName, referenceCount) in outputUsageList.OrderByDescending(x => x.Item2))
            {
                sbAnalyze.AppendLine($"{projName}\t{referenceCount}");
            }

            analyzeTextBox.Text = sbAnalyze.ToString();
            outputTextBox.Text = sb.ToString();
            globalStatus.Text = StatusTextFinished;
        }

        private List<string> FilterReferences(ICollection<ProjectItem> assemblyReferences, string[] customRefs)
        {
            var filteredAssemblyReferences = assemblyReferences
                .Select(x => fullInfoOutput.Checked
                    ? x.EvaluatedInclude + SpreadsheetDelimiter + (!x.DirectMetadata.Any()
                          ? string.Empty
                          : x.DirectMetadata.Select((a, b) => $"{a.Name}:{a.UnevaluatedValue}")
                              .Aggregate((a, b) => $"{a}, {b}"))
                    : x.EvaluatedInclude.Split(',').FirstOrDefault() ?? string.Empty)
                .Where(x => keepSystemCheckBox.Checked || !x.StartsWith(SystemStr))
                .Where(x => !keepCustomProjectRefsCheckBox.Checked || !customRefs.Any(x.StartsWith)).ToList();
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
