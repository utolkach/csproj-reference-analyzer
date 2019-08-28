using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Microsoft.Build.Evaluation;

namespace ReferenceAnalyzer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        private void Button1_Click(object sender, EventArgs e)
        {
            var projectList = projectsTextBox.Text.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            var root = pathToScanTextBox.Text;
            var projectsPaths = FindProjectsPaths(root, projectList);
            LoadProject(projectsPaths);
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            outputTextBox.Text = string.Empty;
            outputTextBox.Text = Clipboard.GetText();
            Button1_Click(sender, e);
        }

        private string[] FindProjectsPaths(string path, string[] projectNames)
        {
            var files = Directory.GetFiles(path);
            var result = new List<string>();
            result.AddRange(files.Where(f => projectNames.Contains(Path.GetFileNameWithoutExtension(f)) && Path.GetExtension(f) == ".csproj"));
            var directories = Directory.GetDirectories(path);
            result.AddRange(directories.SelectMany(x => FindProjectsPaths(x, projectNames)));
            return result.ToArray();
        }
        
        private void LoadProject(string[] projectPaths)
        {
            var customRefs = string.IsNullOrEmpty(customReferenceTextBox.Text) ? new string[] {}  : customReferenceTextBox.Text.Split(',');
            var sb = new StringBuilder();
            var sbAnalyze = new StringBuilder();
            var priorityList = new List<(string, int)>();
            var usageList = new List<(string, string[])>();
            var outputUsageList = new List<(string, int)>();
            using (var sw = new StreamWriter("./references.log", false))
            {
                var projectCollection = new ProjectCollection();
                foreach (var projectPath in projectPaths)
                {
                    var project = projectCollection.LoadProject(projectPath);
                    var projectName = Path.GetFileNameWithoutExtension(projectPath);
                    sb.AppendLine($"{projectName}");

                    var assemblyReferences = project.GetItems("Reference");
                    var filteredAssemblyReferences = assemblyReferences
                        .Select(x => fullInfoOutput.Checked
                            ? x.EvaluatedInclude + "\r\n\t\t" + (!x.DirectMetadata.Any() ? "" : x.DirectMetadata.Select((a, b) => $"{a.Name}:{a.UnevaluatedValue}").Aggregate((a, b) => $"{a}, {b}"))
                            : x.EvaluatedInclude.Split(',').FirstOrDefault() ?? "N/A")
                        .Where(x => keepSystemCheckBox.Checked || !x.StartsWith("System"))
                        .Where(x => !keepCustomProjectRefsCheckBox.Checked || !customRefs.Any(x.StartsWith)).ToList();

                    var projectReferences = project.GetItems("ProjectReference");
                    var filteredProjectReferences = projectReferences
                        .Select(x => fullInfoOutput.Checked
                            ? x.EvaluatedInclude + "\r\n\t\t" + (!x.DirectMetadata.Any() ? "N/A" : x.DirectMetadata.Select((a, b) => $"{a.Name}:{a.UnevaluatedValue}").Aggregate((a, b) => $"{a}, {b}"))
                            : x.EvaluatedInclude.Split(',').FirstOrDefault() ?? "N/A")
                        .Where(x => keepSystemCheckBox.Checked || !x.StartsWith("System"))
                        .Where(x => !keepCustomProjectRefsCheckBox.Checked || !customRefs.Any(x.StartsWith)).ToList();

                    foreach (var assemblyRef in filteredAssemblyReferences)
                    {
                        sb.AppendLine("\tassemblyref\t" + assemblyRef);
                    }

                    foreach (var projectRef in filteredProjectReferences)
                    {
                        var projectRefName = Path.GetFileNameWithoutExtension(projectRef);
                        sb.AppendLine($"{projectRef}");
                        sb.AppendLine($"\tprojref\t{projectRefName}");
                    }

                    var allRefs = new List<string>();
                    allRefs.AddRange(filteredAssemblyReferences);
                    allRefs.AddRange(filteredProjectReferences);
                    usageList.Add((projectName, allRefs.ToArray()));
                    priorityList.Add((projectName, allRefs.Count(x => x.Contains("Modules"))));
                    projectCollection.UnloadProject(project);
                }

                sw.Write(sb.ToString());
            }

            sbAnalyze.AppendLine("***REFS***");
            foreach (var tuple in priorityList.OrderBy(x => x.Item2))
            {
                sbAnalyze.AppendLine($"{tuple.Item1}\t{tuple.Item2}");
            }

            sbAnalyze.AppendLine("\r\n***USAGES***");
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

        private void Button3_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog(this);
            var selectedPath = folderBrowserDialog1.SelectedPath;
            pathToScanTextBox.Text = string.IsNullOrEmpty(selectedPath) ? pathToScanTextBox.Text : selectedPath;
        }
    }
}
