namespace ReportExtractor
{
    partial class MainForm
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
            this.MakeHtmlButton = new System.Windows.Forms.Button();
            this.SettingButton = new System.Windows.Forms.Button();
            this.OpenFolderButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // MakeHtmlButton
            // 
            this.MakeHtmlButton.Location = new System.Drawing.Point(30, 36);
            this.MakeHtmlButton.Name = "MakeHtmlButton";
            this.MakeHtmlButton.Size = new System.Drawing.Size(121, 52);
            this.MakeHtmlButton.TabIndex = 0;
            this.MakeHtmlButton.Text = "HTML作成";
            this.MakeHtmlButton.UseVisualStyleBackColor = true;
            this.MakeHtmlButton.Click += new System.EventHandler(this.MakeHtmlButton_Click);
            // 
            // SettingButton
            // 
            this.SettingButton.Location = new System.Drawing.Point(30, 94);
            this.SettingButton.Name = "SettingButton";
            this.SettingButton.Size = new System.Drawing.Size(121, 52);
            this.SettingButton.TabIndex = 1;
            this.SettingButton.Text = "フォルダ設定";
            this.SettingButton.UseVisualStyleBackColor = true;
            this.SettingButton.Click += new System.EventHandler(this.SettingButton_Click);
            // 
            // OpenFolderButton
            // 
            this.OpenFolderButton.Location = new System.Drawing.Point(30, 152);
            this.OpenFolderButton.Name = "OpenFolderButton";
            this.OpenFolderButton.Size = new System.Drawing.Size(121, 52);
            this.OpenFolderButton.TabIndex = 2;
            this.OpenFolderButton.Text = "フォルダを開く";
            this.OpenFolderButton.UseVisualStyleBackColor = true;
            this.OpenFolderButton.Click += new System.EventHandler(this.OpenFolderButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(294, 250);
            this.Controls.Add(this.OpenFolderButton);
            this.Controls.Add(this.SettingButton);
            this.Controls.Add(this.MakeHtmlButton);
            this.Name = "MainForm";
            this.Text = "報告書出力";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button MakeHtmlButton;
        private System.Windows.Forms.Button SettingButton;
        private System.Windows.Forms.Button OpenFolderButton;
    }
}