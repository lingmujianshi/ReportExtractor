using ReportExtractor.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReportExtractor
{
    public partial class MainForm : Form
    {
        MainFormViewModel _vm;

        public MainForm()
        {
            InitializeComponent();
            _vm = new MainFormViewModel();
            
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void MakeHtmlButton_Click(object sender, EventArgs e)
        {
            try
            {
                _vm.MakeHtml();
                CreateMailButton.Visible = true;
                DisplayMail_sButton.Visible = true;
                DisplayMailButton.Visible = true;

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void SettingButton_Click(object sender, EventArgs e)
        {
            using (var f = new SettingForm())
            {
               f.ShowDialog();
            }
        }

        private void OpenFolderButton_Click(object sender, EventArgs e)
        {
            //フォルダ"C:\My Documents\My Pictures"を開く
            //System.Diagnostics.Process.Start(Info.GitFolder);
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.StartInfo.FileName = @"Report.html";
            proc.Start();
        }

        private void CreateMailButton_Click(object sender, EventArgs e)
        {
            _vm.CreateMail();
        }

        private void DisplayMail_sButton_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.StartInfo.FileName = @"Report_s.html";
            proc.Start();
        }

        private void DisplayMailButton_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.StartInfo.FileName = @"Report.html";
            proc.Start();
        }
    }
}
