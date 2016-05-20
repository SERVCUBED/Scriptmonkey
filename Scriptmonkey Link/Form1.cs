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
    public partial class Form1 : Form
    {
        Server s = new Server();
        public Form1()
        {
            InitializeComponent();
            s.OnReceived += OnServerReceived;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            s.IsActive = checkBox1.Checked;
        }

        private void instanceNumTimer_Tick(object sender, EventArgs e)
        {
            numInstancesLabel.Text = @"Scriptmonkey Instances: " + s.NumInstances;
        }

        private void purgeTimer_Tick(object sender, EventArgs e)
        {
            s.Purge();
        }

        private void broadcastBtn_Click(object sender, EventArgs e)
        {
            s.Broadcast(txtBroadcast.Text);
            txtLog.Text = "Broadcast: " + txtBroadcast.Text + Environment.NewLine + txtLog.Text;
            txtBroadcast.Text = String.Empty;
        }

        private void OnServerReceived(string key, string data)
        {
            if (txtLog.InvokeRequired)
            {
                Invoke((MethodInvoker)(() => OnServerReceived(key, data)));
                return;
            }
            txtLog.Text = DateTime.Now.TimeOfDay.ToString() + " " + key + "=>" + data + Environment.NewLine +  txtLog.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Enabled = false;
            s.DoBackup();
            Enabled = true;
        }
    }
}
