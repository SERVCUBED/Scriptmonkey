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
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.instanceNumTimer = new System.Windows.Forms.Timer(this.components);
            this.purgeTimer = new System.Windows.Forms.Timer(this.components);
            this.numInstancesLabel = new System.Windows.Forms.Label();
            this.broadcastBtn = new System.Windows.Forms.Button();
            this.txtBroadcast = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.moreContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.backupSettingsFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveUrlsAndCloseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.restoreSavedWindowsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moreContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(13, 13);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(60, 17);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.Text = "Started";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
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
            this.numInstancesLabel.Location = new System.Drawing.Point(13, 37);
            this.numInstancesLabel.Name = "numInstancesLabel";
            this.numInstancesLabel.Size = new System.Drawing.Size(132, 13);
            this.numInstancesLabel.TabIndex = 1;
            this.numInstancesLabel.Text = "Scriptmonkey Instances: 0";
            // 
            // broadcastBtn
            // 
            this.broadcastBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.broadcastBtn.Location = new System.Drawing.Point(208, 54);
            this.broadcastBtn.Name = "broadcastBtn";
            this.broadcastBtn.Size = new System.Drawing.Size(75, 23);
            this.broadcastBtn.TabIndex = 3;
            this.broadcastBtn.Text = "Broadcast";
            this.broadcastBtn.UseVisualStyleBackColor = true;
            this.broadcastBtn.Click += new System.EventHandler(this.broadcastBtn_Click);
            // 
            // txtBroadcast
            // 
            this.txtBroadcast.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBroadcast.Location = new System.Drawing.Point(13, 56);
            this.txtBroadcast.Name = "txtBroadcast";
            this.txtBroadcast.Size = new System.Drawing.Size(189, 20);
            this.txtBroadcast.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 79);
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
            this.txtLog.Location = new System.Drawing.Point(13, 96);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtLog.Size = new System.Drawing.Size(270, 113);
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
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "Scriptmonkey Link";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // moreContextMenuStrip
            // 
            this.moreContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.backupSettingsFileToolStripMenuItem,
            this.saveUrlsAndCloseToolStripMenuItem,
            this.restoreSavedWindowsToolStripMenuItem});
            this.moreContextMenuStrip.Name = "moreContextMenuStrip";
            this.moreContextMenuStrip.Size = new System.Drawing.Size(200, 70);
            // 
            // backupSettingsFileToolStripMenuItem
            // 
            this.backupSettingsFileToolStripMenuItem.Name = "backupSettingsFileToolStripMenuItem";
            this.backupSettingsFileToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.backupSettingsFileToolStripMenuItem.Text = "Backup Settings File";
            this.backupSettingsFileToolStripMenuItem.Click += new System.EventHandler(this.backupSettingsFileToolStripMenuItem_Click);
            // 
            // saveUrlsAndCloseToolStripMenuItem
            // 
            this.saveUrlsAndCloseToolStripMenuItem.Name = "saveUrlsAndCloseToolStripMenuItem";
            this.saveUrlsAndCloseToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.saveUrlsAndCloseToolStripMenuItem.Text = "Save URLs and Close";
            this.saveUrlsAndCloseToolStripMenuItem.Click += new System.EventHandler(this.saveUrlsAndCloseToolStripMenuItem_Click);
            // 
            // restoreSavedWindowsToolStripMenuItem
            // 
            this.restoreSavedWindowsToolStripMenuItem.Name = "restoreSavedWindowsToolStripMenuItem";
            this.restoreSavedWindowsToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.restoreSavedWindowsToolStripMenuItem.Text = "Restore Saved Windows";
            this.restoreSavedWindowsToolStripMenuItem.Click += new System.EventHandler(this.restoreSavedWindowsToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(295, 221);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtBroadcast);
            this.Controls.Add(this.broadcastBtn);
            this.Controls.Add(this.numInstancesLabel);
            this.Controls.Add(this.checkBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(730, 575);
            this.MinimumSize = new System.Drawing.Size(311, 132);
            this.Name = "Form1";
            this.Text = "Scriptmonkey Link";
            this.moreContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Timer instanceNumTimer;
        private System.Windows.Forms.Timer purgeTimer;
        private System.Windows.Forms.Label numInstancesLabel;
        private System.Windows.Forms.Button broadcastBtn;
        private System.Windows.Forms.TextBox txtBroadcast;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip moreContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem backupSettingsFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveUrlsAndCloseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem restoreSavedWindowsToolStripMenuItem;
    }
}

