﻿using Microsoft.VisualBasic;
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

        private void instanceNumTimer_Tick(object sender, EventArgs e)
        {
            numInstancesLabel.Text = @"Scriptmonkey Instances: " + _s.NumInstances;
        }

        private void purgeTimer_Tick(object sender, EventArgs e)
        {
            _s.Purge();
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

        private void refreshAllWindowsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _s.RefreshAllInstances();
        }

        private void broadcastCommandToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var s = Interaction.InputBox("Enter command to broadcast:");
            if (String.IsNullOrWhiteSpace((s)))
                return;

            txtLog.Text = @"Broadcast: " + s + Environment.NewLine + txtLog.Text;
            _s.Broadcast(s);
        }

        private void purgeNowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _s.Purge();
        }

        private void stopServerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _s.IsActive = !_s.IsActive;
            stopServerToolStripMenuItem.Text = (_s.IsActive ? "Stop" : "Start") + @" Server";
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.UserClosing)
                return;
            Hide();
            e.Cancel = true;
            
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void showHideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            notifyIcon1_MouseDoubleClick(null, null);
        }
    }
}
