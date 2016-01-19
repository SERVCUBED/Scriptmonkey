using System;
using System.Windows.Forms;

namespace BHOUserScript
{
    public partial class ReadSettingsFailureFrm : Form
    {
        public ReadSettingsFailureFrm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Yes;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkLabel1.Visible = false;
            errorTxt.Visible = true;
            Height += errorTxt.Height;
        }
    }
}
