using System;
using System.Windows.Forms;

namespace Scriptmonkey_Link
{
    public partial class QROptions : Form
    {
        public QROptions()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void foreBtn_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = (sender as Button).BackColor;
            if (colorDialog1.ShowDialog() != DialogResult.OK)
                return;
            (sender as Button).BackColor = colorDialog1.Color;
        }

        private void borderChk_CheckedChanged(object sender, EventArgs e)
        {
            widthNum.Enabled = borderChk.Checked;
        }
    }
}
