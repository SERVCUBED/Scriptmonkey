using System;
using System.Windows.Forms;

namespace BHOUserScript
{
    public partial class AlreadyExistsFrm : Form
    {
        public AlreadyExistsFrm()
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
    }
}
