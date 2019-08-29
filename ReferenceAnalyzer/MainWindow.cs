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
        private const string Na = "N/A";
        private const string SystemStr = "System";
        private const string SpreadsheetDelimiter = "\r\n\t\t";
        private const string Assemblyref = "\tassemblyref\t";
        private const string Projref = "\tprojref\t";
        private const string Modules = "Modules";
        private const string RefsHeader = "***REFS***";
        private const string UsagesHeader = "\r\n***USAGES***";

        public MainWindow()
        {
            InitializeComponent();
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
            result.AddRange(files.Where(f => projectNames.Contains(Path.GetFileNameWithoutExtension(f)) && Path.GetExtension(f) == ProjExtension));
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
                sb.AppendLine($"{projectName}");

                var assemblyReferences = project.GetItems(ReferenceItemType);
                var filteredAssemblyReferences = assemblyReferences
                    .Select(x => fullInfoOutput.Checked
                        ? x.EvaluatedInclude + SpreadsheetDelimiter + (!x.DirectMetadata.Any()
                              ? string.Empty
                              : x.DirectMetadata.Select((a, b) => $"{a.Name}:{a.UnevaluatedValue}")
                                  .Aggregate((a, b) => $"{a}, {b}"))
                        : x.EvaluatedInclude.Split(',').FirstOrDefault() ?? Na)
                    .Where(x => keepSystemCheckBox.Checked || !x.StartsWith(SystemStr))
                    .Where(x => !keepCustomProjectRefsCheckBox.Checked || !customRefs.Any(x.StartsWith)).ToList();

                var projectReferences = project.GetItems(ProjectReferenceItemType);
                var filteredProjectReferences = projectReferences
                    .Select(x => fullInfoOutput.Checked
                        ? x.EvaluatedInclude + SpreadsheetDelimiter + (!x.DirectMetadata.Any()
                              ? Na
                              : x.DirectMetadata.Select((a, b) => $"{a.Name}:{a.UnevaluatedValue}")
                                  .Aggregate((a, b) => $"{a}, {b}"))
                        : x.EvaluatedInclude.Split(',').FirstOrDefault() ?? Na)
                    .Where(x => keepSystemCheckBox.Checked || !x.StartsWith(SystemStr))
                    .Where(x => !keepCustomProjectRefsCheckBox.Checked || !customRefs.Any(x.StartsWith)).ToList();

                foreach (var assemblyRef in filteredAssemblyReferences)
                {
                    sb.AppendLine(Assemblyref + assemblyRef);
                }

                foreach (var projectRef in filteredProjectReferences)
                {
                    var projectRefName = Path.GetFileNameWithoutExtension(projectRef);
                    sb.AppendLine($"{projectRef}");
                    sb.AppendLine(Projref + projectRefName);
                }

                var allRefs = new List<string>();
                allRefs.AddRange(filteredAssemblyReferences);
                allRefs.AddRange(filteredProjectReferences);
                usageList.Add((projectName, allRefs.ToArray()));
                priorityList.Add((projectName, allRefs.Count(x => x.Contains(Modules))));
                projectCollection.UnloadProject(project);
            }

            sbAnalyze.AppendLine(RefsHeader);
            foreach (var tuple in priorityList.OrderBy(x => x.Item2))
            {
                sbAnalyze.AppendLine($"{tuple.Item1}\t{tuple.Item2}");
            }

            sbAnalyze.AppendLine(UsagesHeader);
            foreach (var tuple in usageList)
            {
                var usages = usageList.SelectMany(r => r.Item2.Where(l => l.Contains(tuple.Item1)));
                var usagesCount = usages.Count();

                outputUsageList.Add((tuple.Item1, usagesCount));

                //var aggregate = usages.Any() ? usages.Aggregate((a, b) => $"{a}\r\n{b}") : "";
                //sbAnalyze.AppendLine($"*{tuple.Item1}\t{aggregate} usages");
            }

            foreach (var tuple in outputUsageList.OrderByDescending(x => x.Item2))
            {
                sbAnalyze.AppendLine($"{tuple.Item1}\t{tuple.Item2}");
            }

            analyzeTextBox.Text = sbAnalyze.ToString();
            outputTextBox.Text = sb.ToString();
        }

        private void SetDirectoryPathDialogOpen(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog(this);
            var selectedPath = folderBrowserDialog1.SelectedPath;
            pathToScanTextBox.Text = string.IsNullOrEmpty(selectedPath) ? pathToScanTextBox.Text : selectedPath;
        }
    }
}
