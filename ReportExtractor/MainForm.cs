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
            System.Diagnostics.Process.Start(Info.GitFolder);
        }
    }
}
