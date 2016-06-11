using Microsoft.VisualBasic;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Consulgo.QrCode4cs;

namespace Scriptmonkey_Link
{
    public partial class Form1 : Form
    {
        private readonly string _location = Assembly.GetEntryAssembly().Location;
        private readonly string _startupPath = Environment.GetFolderPath(Environment.SpecialFolder.Startup) + Path.DirectorySeparatorChar +
                           "Scriptmonkey Link.exe";
        private readonly Server _s = new Server();
        public Form1()
        {
            InitializeComponent();
            _s.OnReceived += OnServerReceived;
            _s.OnNotify += OnNotifyReceived;

            // If startup file exists or is run from startup
            if (_location.Contains(Environment.GetFolderPath(Environment.SpecialFolder.Startup)) || File.Exists(_startupPath))
            {
                runLinkAtStartupToolStripMenuItem.Visible = false;
                Visible = false;
            }
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

        private void OnNotifyReceived(string title, string text)
        {
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
    }
}
