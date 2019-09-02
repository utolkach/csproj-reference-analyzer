namespace ReferenceAnalyzer
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.outputTextBox = new System.Windows.Forms.TextBox();
            this.StartAnalyzis = new System.Windows.Forms.Button();
            this.keepCustomProjectRefsCheckBox = new System.Windows.Forms.CheckBox();
            this.keepSystemCheckBox = new System.Windows.Forms.CheckBox();
            this.projectsTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pathToScanTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.customReferenceTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.fullInfoOutput = new System.Windows.Forms.CheckBox();
            this.analyzeTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.fullScanCheckBox = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.namespaceToCountTextBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.globalStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // outputTextBox
            // 
            this.outputTextBox.Location = new System.Drawing.Point(413, 67);
            this.outputTextBox.Multiline = true;
            this.outputTextBox.Name = "outputTextBox";
            this.outputTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.outputTextBox.Size = new System.Drawing.Size(559, 451);
            this.outputTextBox.TabIndex = 0;
            this.outputTextBox.WordWrap = false;
            // 
            // StartAnalyzis
            // 
            this.StartAnalyzis.Enabled = false;
            this.StartAnalyzis.Location = new System.Drawing.Point(897, 9);
            this.StartAnalyzis.Name = "StartAnalyzis";
            this.StartAnalyzis.Size = new System.Drawing.Size(75, 56);
            this.StartAnalyzis.TabIndex = 1;
            this.StartAnalyzis.Text = "3. Go!";
            this.StartAnalyzis.UseVisualStyleBackColor = true;
            this.StartAnalyzis.Click += new System.EventHandler(this.StartAnalysis);
            // 
            // keepCustomProjectRefsCheckBox
            // 
            this.keepCustomProjectRefsCheckBox.AutoSize = true;
            this.keepCustomProjectRefsCheckBox.Location = new System.Drawing.Point(413, 24);
            this.keepCustomProjectRefsCheckBox.Name = "keepCustomProjectRefsCheckBox";
            this.keepCustomProjectRefsCheckBox.Size = new System.Drawing.Size(66, 17);
            this.keepCustomProjectRefsCheckBox.TabIndex = 4;
            this.keepCustomProjectRefsCheckBox.Text = "Remove";
            this.keepCustomProjectRefsCheckBox.UseVisualStyleBackColor = true;
            // 
            // keepSystemCheckBox
            // 
            this.keepSystemCheckBox.AutoSize = true;
            this.keepSystemCheckBox.Location = new System.Drawing.Point(413, 5);
            this.keepSystemCheckBox.Name = "keepSystemCheckBox";
            this.keepSystemCheckBox.Size = new System.Drawing.Size(88, 17);
            this.keepSystemCheckBox.TabIndex = 5;
            this.keepSystemCheckBox.Text = "Keep System";
            this.keepSystemCheckBox.UseVisualStyleBackColor = true;
            // 
            // projectsTextBox
            // 
            this.projectsTextBox.Location = new System.Drawing.Point(15, 87);
            this.projectsTextBox.Multiline = true;
            this.projectsTextBox.Name = "projectsTextBox";
            this.projectsTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.projectsTextBox.Size = new System.Drawing.Size(383, 431);
            this.projectsTextBox.TabIndex = 6;
            this.projectsTextBox.WordWrap = false;
            this.projectsTextBox.TextChanged += new System.EventHandler(this.ProjectsTextBox_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(177, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Or set Project List [separator = \\r\\n]:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(410, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "References:";
            // 
            // pathToScanTextBox
            // 
            this.pathToScanTextBox.Location = new System.Drawing.Point(15, 25);
            this.pathToScanTextBox.Name = "pathToScanTextBox";
            this.pathToScanTextBox.ReadOnly = true;
            this.pathToScanTextBox.Size = new System.Drawing.Size(100, 20);
            this.pathToScanTextBox.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(114, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "1. Select path to scan:";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(121, 24);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 11;
            this.button3.Text = "Browse...";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.SetDirectoryPathDialogOpen);
            // 
            // customReferenceTextBox
            // 
            this.customReferenceTextBox.Location = new System.Drawing.Point(485, 22);
            this.customReferenceTextBox.Name = "customReferenceTextBox";
            this.customReferenceTextBox.Size = new System.Drawing.Size(100, 20);
            this.customReferenceTextBox.TabIndex = 12;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(591, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "references";
            // 
            // fullInfoOutput
            // 
            this.fullInfoOutput.AutoSize = true;
            this.fullInfoOutput.Location = new System.Drawing.Point(507, 5);
            this.fullInfoOutput.Name = "fullInfoOutput";
            this.fullInfoOutput.Size = new System.Drawing.Size(95, 17);
            this.fullInfoOutput.TabIndex = 14;
            this.fullInfoOutput.Text = "Full info output";
            this.fullInfoOutput.UseVisualStyleBackColor = true;
            // 
            // analyzeTextBox
            // 
            this.analyzeTextBox.Location = new System.Drawing.Point(978, 67);
            this.analyzeTextBox.Multiline = true;
            this.analyzeTextBox.Name = "analyzeTextBox";
            this.analyzeTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.analyzeTextBox.Size = new System.Drawing.Size(306, 451);
            this.analyzeTextBox.TabIndex = 15;
            this.analyzeTextBox.WordWrap = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(978, 49);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(117, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "Usages and references";
            // 
            // fullScanCheckBox
            // 
            this.fullScanCheckBox.AutoSize = true;
            this.fullScanCheckBox.Location = new System.Drawing.Point(34, 51);
            this.fullScanCheckBox.Name = "fullScanCheckBox";
            this.fullScanCheckBox.Size = new System.Drawing.Size(104, 17);
            this.fullScanCheckBox.TabIndex = 17;
            this.fullScanCheckBox.Text = "Scan all projects";
            this.fullScanCheckBox.UseVisualStyleBackColor = true;
            this.fullScanCheckBox.CheckedChanged += new System.EventHandler(this.FullScanCheckBox_CheckedChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 52);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(16, 13);
            this.label6.TabIndex = 18;
            this.label6.Text = "2:";
            // 
            // namespaceToCountTextBox
            // 
            this.namespaceToCountTextBox.Location = new System.Drawing.Point(1184, 21);
            this.namespaceToCountTextBox.Name = "namespaceToCountTextBox";
            this.namespaceToCountTextBox.Size = new System.Drawing.Size(100, 20);
            this.namespaceToCountTextBox.TabIndex = 19;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(981, 24);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(201, 13);
            this.label7.TabIndex = 20;
            this.label7.Text = "Part of namespace for reference counter:";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar1,
            this.globalStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 523);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1296, 22);
            this.statusStrip1.TabIndex = 21;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 16);
            // 
            // globalStatus
            // 
            this.globalStatus.Name = "globalStatus";
            this.globalStatus.Size = new System.Drawing.Size(0, 17);
            // 
            // MainWindow
            // 
            this.ClientSize = new System.Drawing.Size(1296, 545);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.namespaceToCountTextBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.fullScanCheckBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.analyzeTextBox);
            this.Controls.Add(this.fullInfoOutput);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.customReferenceTextBox);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pathToScanTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.projectsTextBox);
            this.Controls.Add(this.keepSystemCheckBox);
            this.Controls.Add(this.keepCustomProjectRefsCheckBox);
            this.Controls.Add(this.StartAnalyzis);
            this.Controls.Add(this.outputTextBox);
            this.MaximizeBox = false;
            this.Name = "MainWindow";
            this.ShowIcon = false;
            this.Text = "Reference Analyzer";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox outputTextBox;
        private System.Windows.Forms.Button StartAnalyzis;
        private System.Windows.Forms.CheckBox keepCustomProjectRefsCheckBox;
        private System.Windows.Forms.CheckBox keepSystemCheckBox;
        private System.Windows.Forms.TextBox projectsTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox pathToScanTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.TextBox customReferenceTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox fullInfoOutput;
        private System.Windows.Forms.TextBox analyzeTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox fullScanCheckBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox namespaceToCountTextBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.ToolStripStatusLabel globalStatusTextBox;
        private System.Windows.Forms.ToolStripStatusLabel globalStatus;
    }
}

