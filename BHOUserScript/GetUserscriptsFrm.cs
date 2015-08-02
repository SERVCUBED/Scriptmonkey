using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace BHOUserScript
{
    public partial class GetUserscriptsFrm : Form
    {
        public GetUserscriptsFrm()
        {
            InitializeComponent();
        }

        private void Start(string url)
        {
            new Process
            {
                StartInfo =
                {
                    FileName = "iexplore.exe",
                    Arguments = url
                }
            }.Start();
            DialogResult = DialogResult.OK;
        }

        private void userscriptsMirrorLbl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Start("http://userscripts-mirror.org/");
        }

        private void greasyforkLbl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Start("https://greasyfork.org/en");
        }

        private void openUserJsLbl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Start("https://openuserjs.org/");
        }

        private void proTurkersLbl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Start("http://proturkers.com/");
        }

        private void openJsScriptsLbl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Start("http://openjs.com/scripts/");
        }

        private void userStylesLbl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Start("https://userstyles.org/");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
