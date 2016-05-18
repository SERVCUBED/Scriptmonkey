using System;
using System.Windows.Forms;

namespace BHOUserScript
{
    public partial class AddScriptFrm : Form
    {
        public bool FromFile = true;
        public string Url;
        private readonly string _newUrl;

        public AddScriptFrm(bool isCss)
        {
            InitializeComponent();

            if (isCss)
            {
                Text = @"Add CSS";
                openFileDialog1.Filter =
                    @"CSS Files (*.css)|*.css|All files (*.*)|*.*";
                _newUrl = "https://servc.eu/p/scriptmonkey/new_files/new.css";
            }
            else
                _newUrl = "https://servc.eu/p/scriptmonkey/new_files/new.user.js";
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

        private void newBtn_Click(object sender, EventArgs e)
        {
            radioButton2.Checked = true;
            urlTxt.Text = _newUrl;
        }
    }
}
