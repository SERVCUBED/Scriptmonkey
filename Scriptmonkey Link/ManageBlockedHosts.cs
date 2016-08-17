using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scriptmonkey_Link
{
    public partial class ManageBlockedHosts : Form
    {
        public ManageBlockedHosts()
        {
            InitializeComponent();
        }

        public static List<string> Manage(List<string> oldList, IWin32Window parent = null)
        {
            var f = new ManageBlockedHosts();
            foreach (var item in oldList)
                f.listBox1.Items.Add(item);
            f.ShowDialog(parent);
            var l = new List<string>(f.listBox1.Items.Count);
            l.AddRange(from object item in f.listBox1.Items select item.ToString());
            return l;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex < 0)
                return;

            listBox1.Items.RemoveAt(listBox1.SelectedIndex);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
