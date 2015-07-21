using System;
using System.Windows.Forms;

namespace BHOUserScript
{
    public partial class AddScriptUrlFrm : Form
    {
        public AddScriptUrlFrm()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Yes;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
        }
    }
}
