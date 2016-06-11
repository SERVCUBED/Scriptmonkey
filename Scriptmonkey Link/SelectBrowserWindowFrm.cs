using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SHDocVw;

namespace Scriptmonkey_Link
{
    public partial class SelectBrowserWindowFrm : Form
    {
        private List<InternetExplorer> _instances; 

        public InternetExplorer SelectedInstance => _instances[listBox1.SelectedIndex];


        public SelectBrowserWindowFrm()
        {
            InitializeComponent();
            _refreshInstances();
        }

        private void _refreshInstances()
        {
            _instances = new List<InternetExplorer>();
            listBox1.Items.Clear();

            IETools.TryForEachInternetExplorer(iExplorer =>
            {
                _instances.Add(iExplorer);
                listBox1.Items.Add(iExplorer.LocationName);
            });
        }

        private void okBtn_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        public static InternetExplorer SelectBrowserWindow()
        {
            var f = new SelectBrowserWindowFrm();
            if (f.ShowDialog() == DialogResult.OK)
                return f.SelectedInstance;
            else
                return null;
        }

        private void refreshBtn_Click(object sender, EventArgs e)
        {
            Refresh();
        }
    }
}
