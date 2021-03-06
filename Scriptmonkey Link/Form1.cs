﻿using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace Scriptmonkey_Link
{
    public partial class Form1 : Form
    {
        private readonly string _location = Assembly.GetEntryAssembly().Location;
        private readonly string _startupPath = Environment.GetFolderPath(Environment.SpecialFolder.Startup) + Path.DirectorySeparatorChar +
                           "Scriptmonkey Link.exe";
        private readonly Server _s = new Server();
        private List<string> _blockedNotificationHosts = new List<string>();
        private readonly Dictionary<string, int> _notificationHosts = new Dictionary<string, int>();
        
        public Form1()
        {
            InitializeComponent();
            _s.OnReceived += OnServerReceived;
            _s.OnNotify += OnNotifyReceived;

            if (!File.Exists(_startupPath)) return;

            // Hide the statup option
            runLinkAtStartupToolStripMenuItem.Visible = false;

            ThreadPool.QueueUserWorkItem(callback =>
            {
                // If not run at startup
                if (_location.Contains(Environment.GetFolderPath(Environment.SpecialFolder.Startup))) return;
                try
                {
                    // If current version is newer than the startup version, replace the startup version with this one
                    if (new Version(FileVersionInfo.GetVersionInfo(_startupPath).FileVersion) >=
                        Server.CurrentVersion()) return;

                    File.Delete(_startupPath);
                    File.Copy(_location, _startupPath);
                    txtLog.Text = @"Startup version updated" + Environment.NewLine + txtLog.Text;
                }
                catch (Exception) { }
            });
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

        private void OnNotifyReceived(string title, string text, string host)
        {
            if (_notificationHosts.ContainsKey(host))
                _notificationHosts[host]++;
            else
                _notificationHosts.Add(host, 1);

            if (_blockedNotificationHosts.Contains(host))
                return;
            
            if (_notificationHosts[host] == 20)
            {
                notifyIcon1.ShowBalloonTip(3000, $"Too many notifications from {host}", $"The host {host} has sent too " + 
                    "many notifications and has been blocked. Go to More -> Unblock Notification Hosts to remove.", 
                    ToolTipIcon.Info);
                OnServerReceived("notify", $"Too many notifications from {host}");
                _blockedNotificationHosts.Add(host);
            }
            else
            notifyIcon1.ShowBalloonTip(3000, title, text, ToolTipIcon.Info);

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

        private void runLinkAtStartupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!File.Exists(_startupPath))
                    File.Copy(_location, _startupPath);
                runLinkAtStartupToolStripMenuItem.Visible = false;
            }
            catch (Exception)
            {
                MessageBox.Show(@"Unable to add to startup. Is access denied?");
            }
        }

        private void manageCookiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var f = new CookieManager();
            f.ShowDialog();
        }

        private void acceptRemoteConnectionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _s.AllowRemote();
            acceptRemoteConnectionsToolStripMenuItem.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var r = new Regex(@"(http|ftp|https):\/\/[\w\-_]+(\.[\w\-_]+)+([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?");
            var n = r.Match(Clipboard.GetText());
            if (n.Length == 0)
                return;
            _s.OpenUrl(n.ToString());
        }

        private void shareURLViaQRCodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowQR.Show(SelectBrowserWindowFrm.SelectBrowserWindow()?.LocationURL);
        }

        private void showQRCodeFromTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowQR.Show(Interaction.InputBox("Enter text:", "Scriptmonkey Link - Show QR Code™"));
        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void refreshSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtLog.Text = @"Broadcast: Refersh Settings" + Environment.NewLine + txtLog.Text;
            _s.Broadcast("refresh");
        }

        private void refreshScriptCacheToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtLog.Text = @"Broadcast: Refersh Script Cache" + Environment.NewLine + txtLog.Text;
            _s.Broadcast("refreshCache");
        }

        private void verifyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtLog.Text = @"Broadcast: Verify" + Environment.NewLine + txtLog.Text;
            _s.Broadcast("verify");
        }

        private void otherToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var s = Interaction.InputBox("Enter command to broadcast:");
            if (String.IsNullOrWhiteSpace((s)))
                return;

            txtLog.Text = @"Broadcast: " + s + Environment.NewLine + txtLog.Text;
            _s.Broadcast(s);
        }

        private void unblockNotificationHostsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _blockedNotificationHosts = ManageBlockedHosts.Manage(_blockedNotificationHosts, this);
        }
    }
}
