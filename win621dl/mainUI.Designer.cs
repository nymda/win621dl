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
            this.picBox = new System.Windows.Forms.PictureBox();
            this.directoryBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).BeginInit();
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
            this.resultsLbl.Location = new System.Drawing.Point(12, 149);
            this.resultsLbl.Name = "resultsLbl";
            this.resultsLbl.Size = new System.Drawing.Size(48, 13);
            this.resultsLbl.TabIndex = 5;
            this.resultsLbl.Text = "Results: ";
            // 
            // dlLbl
            // 
            this.dlLbl.AutoSize = true;
            this.dlLbl.Location = new System.Drawing.Point(12, 162);
            this.dlLbl.Name = "dlLbl";
            this.dlLbl.Size = new System.Drawing.Size(70, 13);
            this.dlLbl.TabIndex = 6;
            this.dlLbl.Text = "Downloaded:";
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(12, 207);
            this.progressBar.Maximum = 50;
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(518, 23);
            this.progressBar.TabIndex = 8;
            // 
            // picBox
            // 
            this.picBox.Location = new System.Drawing.Point(240, 12);
            this.picBox.Name = "picBox";
            this.picBox.Size = new System.Drawing.Size(290, 189);
            this.picBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBox.TabIndex = 9;
            this.picBox.TabStop = false;
            this.picBox.Click += new System.EventHandler(this.picBox_Click);
            // 
            // directoryBtn
            // 
            this.directoryBtn.Location = new System.Drawing.Point(12, 178);
            this.directoryBtn.Name = "directoryBtn";
            this.directoryBtn.Size = new System.Drawing.Size(219, 23);
            this.directoryBtn.TabIndex = 10;
            this.directoryBtn.Text = "Directory";
            this.directoryBtn.UseVisualStyleBackColor = true;
            this.directoryBtn.Click += new System.EventHandler(this.directoryBtn_Click);
            // 
            // mainUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(540, 238);
            this.Controls.Add(this.directoryBtn);
            this.Controls.Add(this.picBox);
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
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).EndInit();
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
        private System.Windows.Forms.PictureBox picBox;
        private System.Windows.Forms.Button directoryBtn;
    }
}

