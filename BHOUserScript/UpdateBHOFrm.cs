using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BHOUserScript
{
    public partial class UpdateBHOFrm : Form
    {
        public enum UpdateBHOFrmResponse
        {
            Now,
            NextTime,
            In3Days
        }

        public UpdateBHOFrmResponse Response = UpdateBHOFrmResponse.NextTime;


        public UpdateBHOFrm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Response = UpdateBHOFrmResponse.Now;
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Response = UpdateBHOFrmResponse.In3Days;
            Close();
        }
    }
}
