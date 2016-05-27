namespace Scriptmonkey_Link
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.instanceNumTimer = new System.Windows.Forms.Timer(this.components);
            this.purgeTimer = new System.Windows.Forms.Timer(this.components);
            this.numInstancesLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.notifyIconContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showHideToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moreContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.saveUrlsAndCloseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.restoreSavedWindowsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshAllWindowsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.backupSettingsFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.advancedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.broadcastCommandToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.purgeNowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runLinkAtStartupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manageCookiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.notifyIconContextMenuStrip.SuspendLayout();
            this.moreContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // instanceNumTimer
            // 
            this.instanceNumTimer.Enabled = true;
            this.instanceNumTimer.Tick += new System.EventHandler(this.instanceNumTimer_Tick);
            // 
            // purgeTimer
            // 
            this.purgeTimer.Enabled = true;
            this.purgeTimer.Interval = 60000;
            this.purgeTimer.Tick += new System.EventHandler(this.purgeTimer_Tick);
            // 
            // numInstancesLabel
            // 
            this.numInstancesLabel.AutoSize = true;
            this.numInstancesLabel.Location = new System.Drawing.Point(13, 14);
            this.numInstancesLabel.Name = "numInstancesLabel";
            this.numInstancesLabel.Size = new System.Drawing.Size(132, 13);
            this.numInstancesLabel.TabIndex = 1;
            this.numInstancesLabel.Text = "Scriptmonkey Instances: 0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Log:";
            // 
            // txtLog
            // 
            this.txtLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLog.Location = new System.Drawing.Point(13, 52);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtLog.Size = new System.Drawing.Size(270, 157);
            this.txtLog.TabIndex = 5;
            this.txtLog.WordWrap = false;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(229, 9);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(54, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "More...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.notifyIconContextMenuStrip;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "Scriptmonkey Link";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // notifyIconContextMenuStrip
            // 
            this.notifyIconContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showHideToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.notifyIconContextMenuStrip.Name = "notifyIconContextMenuStrip";
            this.notifyIconContextMenuStrip.Size = new System.Drawing.Size(134, 48);
            // 
            // showHideToolStripMenuItem
            // 
            this.showHideToolStripMenuItem.Name = "showHideToolStripMenuItem";
            this.showHideToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.showHideToolStripMenuItem.Text = "Show/Hide";
            this.showHideToolStripMenuItem.Click += new System.EventHandler(this.showHideToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // moreContextMenuStrip
            // 
            this.moreContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveUrlsAndCloseToolStripMenuItem,
            this.restoreSavedWindowsToolStripMenuItem,
            this.refreshAllWindowsToolStripMenuItem,
            this.manageCookiesToolStripMenuItem,
            this.toolStripSeparator1,
            this.backupSettingsFileToolStripMenuItem,
            this.toolStripSeparator2,
            this.advancedToolStripMenuItem,
            this.runLinkAtStartupToolStripMenuItem});
            this.moreContextMenuStrip.Name = "moreContextMenuStrip";
            this.moreContextMenuStrip.Size = new System.Drawing.Size(200, 192);
            // 
            // saveUrlsAndCloseToolStripMenuItem
            // 
            this.saveUrlsAndCloseToolStripMenuItem.Name = "saveUrlsAndCloseToolStripMenuItem";
            this.saveUrlsAndCloseToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.saveUrlsAndCloseToolStripMenuItem.Text = "&Save URLs and Close";
            this.saveUrlsAndCloseToolStripMenuItem.Click += new System.EventHandler(this.saveUrlsAndCloseToolStripMenuItem_Click);
            // 
            // restoreSavedWindowsToolStripMenuItem
            // 
            this.restoreSavedWindowsToolStripMenuItem.Name = "restoreSavedWindowsToolStripMenuItem";
            this.restoreSavedWindowsToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.restoreSavedWindowsToolStripMenuItem.Text = "&Restore Saved Windows";
            this.restoreSavedWindowsToolStripMenuItem.Click += new System.EventHandler(this.restoreSavedWindowsToolStripMenuItem_Click);
            // 
            // refreshAllWindowsToolStripMenuItem
            // 
            this.refreshAllWindowsToolStripMenuItem.Name = "refreshAllWindowsToolStripMenuItem";
            this.refreshAllWindowsToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.refreshAllWindowsToolStripMenuItem.Text = "R&efresh All Windows";
            this.refreshAllWindowsToolStripMenuItem.Click += new System.EventHandler(this.refreshAllWindowsToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(196, 6);
            // 
            // backupSettingsFileToolStripMenuItem
            // 
            this.backupSettingsFileToolStripMenuItem.Name = "backupSettingsFileToolStripMenuItem";
            this.backupSettingsFileToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.backupSettingsFileToolStripMenuItem.Text = "&Backup Settings File";
            this.backupSettingsFileToolStripMenuItem.Click += new System.EventHandler(this.backupSettingsFileToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(196, 6);
            // 
            // advancedToolStripMenuItem
            // 
            this.advancedToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.broadcastCommandToolStripMenuItem,
            this.purgeNowToolStripMenuItem,
            this.stopServerToolStripMenuItem});
            this.advancedToolStripMenuItem.Name = "advancedToolStripMenuItem";
            this.advancedToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.advancedToolStripMenuItem.Text = "Advanced";
            // 
            // broadcastCommandToolStripMenuItem
            // 
            this.broadcastCommandToolStripMenuItem.Name = "broadcastCommandToolStripMenuItem";
            this.broadcastCommandToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.broadcastCommandToolStripMenuItem.Text = "Broadcast Command";
            this.broadcastCommandToolStripMenuItem.Click += new System.EventHandler(this.broadcastCommandToolStripMenuItem_Click);
            // 
            // purgeNowToolStripMenuItem
            // 
            this.purgeNowToolStripMenuItem.Name = "purgeNowToolStripMenuItem";
            this.purgeNowToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.purgeNowToolStripMenuItem.Text = "Purge Now";
            this.purgeNowToolStripMenuItem.Click += new System.EventHandler(this.purgeNowToolStripMenuItem_Click);
            // 
            // stopServerToolStripMenuItem
            // 
            this.stopServerToolStripMenuItem.Name = "stopServerToolStripMenuItem";
            this.stopServerToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.stopServerToolStripMenuItem.Text = "Stop Server";
            this.stopServerToolStripMenuItem.Click += new System.EventHandler(this.stopServerToolStripMenuItem_Click);
            // 
            // runLinkAtStartupToolStripMenuItem
            // 
            this.runLinkAtStartupToolStripMenuItem.Name = "runLinkAtStartupToolStripMenuItem";
            this.runLinkAtStartupToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.runLinkAtStartupToolStripMenuItem.Text = "Run Link At Startup";
            this.runLinkAtStartupToolStripMenuItem.Click += new System.EventHandler(this.runLinkAtStartupToolStripMenuItem_Click);
            // 
            // manageCookiesToolStripMenuItem
            // 
            this.manageCookiesToolStripMenuItem.Name = "manageCookiesToolStripMenuItem";
            this.manageCookiesToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.manageCookiesToolStripMenuItem.Text = "Manage Cookies";
            this.manageCookiesToolStripMenuItem.Click += new System.EventHandler(this.manageCookiesToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(295, 221);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numInstancesLabel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(730, 575);
            this.MinimumSize = new System.Drawing.Size(311, 260);
            this.Name = "Form1";
            this.Text = "Scriptmonkey Link";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.notifyIconContextMenuStrip.ResumeLayout(false);
            this.moreContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer instanceNumTimer;
        private System.Windows.Forms.Timer purgeTimer;
        private System.Windows.Forms.Label numInstancesLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip moreContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem backupSettingsFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveUrlsAndCloseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem restoreSavedWindowsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem refreshAllWindowsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem advancedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem broadcastCommandToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem purgeNowToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem stopServerToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip notifyIconContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem showHideToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem runLinkAtStartupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem manageCookiesToolStripMenuItem;
    }
}

