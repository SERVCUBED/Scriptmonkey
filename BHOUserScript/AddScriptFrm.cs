using System;
using System.Windows.Forms;

namespace BHOUserScript
{
    public partial class AddScriptFrm : Form
    {
        public bool FromFile = true;
        public string Url;

        public AddScriptFrm()
        {
            InitializeComponent();
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void okBtn_Click(object sender, EventArgs e)
        {
            FromFile = radioButton1.Checked;
            if (radioButton1.Checked)
                Url = fileTxt.Text;
            else
                Url = urlTxt.Text;
            DialogResult = DialogResult.OK;
        }

        private void browseBtn_Click(object sender, EventArgs e)
        {
            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                fileTxt.Text = openFileDialog1.FileName;
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            browseBtn.Enabled = radioButton1.Checked;
            fileTxt.Enabled = radioButton1.Checked;
            urlTxt.Enabled = !radioButton1.Checked;
        }
    }
}
