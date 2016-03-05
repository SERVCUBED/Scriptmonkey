using System;
using System.Drawing;
using System.Windows.Forms;

namespace BHOUserScript
{
    public partial class AskUpdateScriptFrm : Form
    {
        public AskUpdateScriptFrm()
        {
            InitializeComponent();
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void AskUpdateScriptFrm_Load(object sender, EventArgs e)
        {
            var workingArea = Screen.GetWorkingArea(Point.Empty);
            Location = new Point(workingArea.Width - Width, workingArea.Height - Height);
        }

        private void bgColourTimer_Tick(object sender, EventArgs e)
        {
            if (BackColor == Color.LightBlue)
            {
                BackColor = Color.SkyBlue;
            }
            else if (BackColor == Color.SkyBlue)
            {
                BackColor = Color.CornflowerBlue;
            }
            else if (BackColor == Color.CornflowerBlue)
            {
                BackColor = Color.LightBlue;
            }
        }
    }
}
