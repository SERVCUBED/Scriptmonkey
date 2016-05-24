using System;
using System.Windows.Forms;

namespace Scriptmonkey_Link
{
    public partial class Form1 : Form
    {
        private readonly Server _s = new Server();
        public Form1()
        {
            InitializeComponent();
            _s.OnReceived += OnServerReceived;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            _s.IsActive = checkBox1.Checked;
        }

        private void instanceNumTimer_Tick(object sender, EventArgs e)
        {
            numInstancesLabel.Text = @"Scriptmonkey Instances: " + _s.NumInstances;
        }

        private void purgeTimer_Tick(object sender, EventArgs e)
        {
            _s.Purge();
        }

        private void broadcastBtn_Click(object sender, EventArgs e)
        {
            _s.Broadcast(txtBroadcast.Text);
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
            moreContextMenuStrip.Show(this, new System.Drawing.Point(
                button1.Left, button1.Top + button1.Height));
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Visible = !Visible;
        }

        private void backupSettingsFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Enabled = false;
            _s.DoBackup();
            Enabled = true;
        }

        private void saveUrlsAndCloseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _s.StartSaveWindowState();
        }

        private void restoreSavedWindowsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _s.RestoreSavedWindows();
        }
    }
}
