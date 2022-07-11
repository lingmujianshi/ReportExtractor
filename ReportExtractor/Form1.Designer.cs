namespace ReportExtractor
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.BlueButton = new System.Windows.Forms.ToolStripButton();
            this.BlackButton = new System.Windows.Forms.ToolStripButton();
            this.Title1Button = new System.Windows.Forms.ToolStripButton();
            this.Title2Button = new System.Windows.Forms.ToolStripButton();
            this.ResetButton = new System.Windows.Forms.ToolStripButton();
            this.label1 = new System.Windows.Forms.Label();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.richTextBox1.Location = new System.Drawing.Point(12, 60);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(715, 530);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BlueButton,
            this.BlackButton,
            this.Title1Button,
            this.Title2Button,
            this.ResetButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(739, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // BlueButton
            // 
            this.BlueButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.BlueButton.Image = ((System.Drawing.Image)(resources.GetObject("BlueButton.Image")));
            this.BlueButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BlueButton.Name = "BlueButton";
            this.BlueButton.Size = new System.Drawing.Size(70, 22);
            this.BlueButton.Text = "BlueButton";
            this.BlueButton.Click += new System.EventHandler(this.BlueButton_Click);
            // 
            // BlackButton
            // 
            this.BlackButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.BlackButton.Image = ((System.Drawing.Image)(resources.GetObject("BlackButton.Image")));
            this.BlackButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BlackButton.Name = "BlackButton";
            this.BlackButton.Size = new System.Drawing.Size(75, 22);
            this.BlackButton.Text = "BlackButton";
            this.BlackButton.Click += new System.EventHandler(this.BlackButton_Click);
            // 
            // Title1Button
            // 
            this.Title1Button.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.Title1Button.Image = ((System.Drawing.Image)(resources.GetObject("Title1Button.Image")));
            this.Title1Button.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Title1Button.Name = "Title1Button";
            this.Title1Button.Size = new System.Drawing.Size(75, 22);
            this.Title1Button.Text = "Title1Button";
            this.Title1Button.Click += new System.EventHandler(this.Title1Button_Click);
            // 
            // Title2Button
            // 
            this.Title2Button.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.Title2Button.Image = ((System.Drawing.Image)(resources.GetObject("Title2Button.Image")));
            this.Title2Button.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Title2Button.Name = "Title2Button";
            this.Title2Button.Size = new System.Drawing.Size(75, 22);
            this.Title2Button.Text = "Title2Button";
            this.Title2Button.Click += new System.EventHandler(this.Title2Button_Click);
            // 
            // ResetButton
            // 
            this.ResetButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ResetButton.Image = ((System.Drawing.Image)(resources.GetObject("ResetButton.Image")));
            this.ResetButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ResetButton.Name = "ResetButton";
            this.ResetButton.Size = new System.Drawing.Size(75, 22);
            this.ResetButton.Text = "ResetButton";
            this.ResetButton.Click += new System.EventHandler(this.ResetButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "label1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(739, 602);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.richTextBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton BlackButton;
        private System.Windows.Forms.ToolStripButton BlueButton;
        private System.Windows.Forms.ToolStripButton Title1Button;
        private System.Windows.Forms.ToolStripButton Title2Button;
        private System.Windows.Forms.ToolStripButton ResetButton;
        private System.Windows.Forms.Label label1;
    }
}

