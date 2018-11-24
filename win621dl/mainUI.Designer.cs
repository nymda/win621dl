namespace win621dl
{
    partial class mainUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainUI));
            this.tagBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.helpButton = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.resultsLbl = new System.Windows.Forms.Label();
            this.dlLbl = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.directoryBtn = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // tagBox
            // 
            this.tagBox.Location = new System.Drawing.Point(12, 25);
            this.tagBox.Multiline = true;
            this.tagBox.Name = "tagBox";
            this.tagBox.Size = new System.Drawing.Size(222, 92);
            this.tagBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Tags:";
            // 
            // helpButton
            // 
            this.helpButton.Location = new System.Drawing.Point(181, 123);
            this.helpButton.Name = "helpButton";
            this.helpButton.Size = new System.Drawing.Size(53, 23);
            this.helpButton.TabIndex = 3;
            this.helpButton.Text = "?";
            this.helpButton.UseVisualStyleBackColor = true;
            this.helpButton.Click += new System.EventHandler(this.helpButton_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(12, 123);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(163, 23);
            this.button3.TabIndex = 4;
            this.button3.Text = "start";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // resultsLbl
            // 
            this.resultsLbl.AutoSize = true;
            this.resultsLbl.Location = new System.Drawing.Point(9, 172);
            this.resultsLbl.Name = "resultsLbl";
            this.resultsLbl.Size = new System.Drawing.Size(48, 13);
            this.resultsLbl.TabIndex = 5;
            this.resultsLbl.Text = "Results: ";
            // 
            // dlLbl
            // 
            this.dlLbl.AutoSize = true;
            this.dlLbl.Location = new System.Drawing.Point(9, 185);
            this.dlLbl.Name = "dlLbl";
            this.dlLbl.Size = new System.Drawing.Size(70, 13);
            this.dlLbl.TabIndex = 6;
            this.dlLbl.Text = "Downloaded:";
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(12, 230);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(518, 23);
            this.progressBar.TabIndex = 8;
            // 
            // directoryBtn
            // 
            this.directoryBtn.Location = new System.Drawing.Point(12, 201);
            this.directoryBtn.Name = "directoryBtn";
            this.directoryBtn.Size = new System.Drawing.Size(219, 23);
            this.directoryBtn.TabIndex = 10;
            this.directoryBtn.Text = "Directory";
            this.directoryBtn.UseVisualStyleBackColor = true;
            this.directoryBtn.Click += new System.EventHandler(this.directoryBtn_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(240, 12);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(290, 212);
            this.listBox1.TabIndex = 11;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(12, 152);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(49, 17);
            this.radioButton1.TabIndex = 12;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "e621";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(67, 152);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(69, 17);
            this.radioButton2.TabIndex = 13;
            this.radioButton2.Text = "Inkbunny";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // mainUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(544, 260);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.directoryBtn);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.dlLbl);
            this.Controls.Add(this.resultsLbl);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.helpButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tagBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "mainUI";
            this.Text = "win621dl";
            this.Load += new System.EventHandler(this.mainUI_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tagBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button helpButton;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label resultsLbl;
        private System.Windows.Forms.Label dlLbl;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Button directoryBtn;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
    }
}

