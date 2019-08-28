namespace ReferenceAnalyzer
{
 partial class Form1
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
            this.button1 = new System.Windows.Forms.Button();
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
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(897, 9);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 56);
            this.button1.TabIndex = 1;
            this.button1.Text = "Go!";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
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
            this.projectsTextBox.Location = new System.Drawing.Point(15, 67);
            this.projectsTextBox.Multiline = true;
            this.projectsTextBox.Name = "projectsTextBox";
            this.projectsTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.projectsTextBox.Size = new System.Drawing.Size(383, 451);
            this.projectsTextBox.TabIndex = 6;
            this.projectsTextBox.WordWrap = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(146, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Project List [separator = \\r\\n]:";
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
            this.label3.Size = new System.Drawing.Size(70, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Path to scan:";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(121, 24);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 11;
            this.button3.Text = "Browse...";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.Button3_Click);
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1296, 522);
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
            this.Controls.Add(this.button1);
            this.Controls.Add(this.outputTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reference Analyzer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox outputTextBox;
        private System.Windows.Forms.Button button1;
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
    }}

