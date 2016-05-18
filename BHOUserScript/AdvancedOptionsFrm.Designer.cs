namespace BHOUserScript
{
    partial class AdvancedOptionsFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdvancedOptionsFrm));
            this.saveBtn = new System.Windows.Forms.Button();
            this.updateChk = new System.Windows.Forms.CheckBox();
            this.refreshChk = new System.Windows.Forms.CheckBox();
            this.autoChk = new System.Windows.Forms.CheckBox();
            this.publicApiChk = new System.Windows.Forms.CheckBox();
            this.cacheChk = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.reloadNum = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.refreshPageChk = new System.Windows.Forms.CheckBox();
            this.injectApiChk = new System.Windows.Forms.CheckBox();
            this.useLinkChk = new System.Windows.Forms.CheckBox();
            this.lockSettingsBtn = new System.Windows.Forms.Button();
            this.menuCssBtn = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.updateDisabledChk = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.reloadNum)).BeginInit();
            this.SuspendLayout();
            // 
            // saveBtn
            // 
            this.saveBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.saveBtn.Location = new System.Drawing.Point(240, 253);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(75, 23);
            this.saveBtn.TabIndex = 0;
            this.saveBtn.Text = "Save";
            this.saveBtn.UseVisualStyleBackColor = false;
            this.saveBtn.Click += new System.EventHandler(this.saveBtn_Click);
            // 
            // updateChk
            // 
            this.updateChk.AutoSize = true;
            this.updateChk.Location = new System.Drawing.Point(13, 13);
            this.updateChk.Name = "updateChk";
            this.updateChk.Size = new System.Drawing.Size(113, 17);
            this.updateChk.TabIndex = 1;
            this.updateChk.Text = "Check for updates";
            this.toolTip1.SetToolTip(this.updateChk, "Enable to check for Scriptmonkey updates every 3 days.");
            this.updateChk.UseVisualStyleBackColor = true;
            // 
            // refreshChk
            // 
            this.refreshChk.AutoSize = true;
            this.refreshChk.Location = new System.Drawing.Point(13, 37);
            this.refreshChk.Name = "refreshChk";
            this.refreshChk.Size = new System.Drawing.Size(282, 17);
            this.refreshChk.TabIndex = 2;
            this.refreshChk.Text = "Run on page refresh (experimental, not recommended)";
            this.toolTip1.SetToolTip(this.refreshChk, "This feature is very experimental.");
            this.refreshChk.UseVisualStyleBackColor = true;
            // 
            // autoChk
            // 
            this.autoChk.AutoSize = true;
            this.autoChk.Location = new System.Drawing.Point(13, 61);
            this.autoChk.Name = "autoChk";
            this.autoChk.Size = new System.Drawing.Size(153, 17);
            this.autoChk.TabIndex = 3;
            this.autoChk.Text = "Detect scripts on webpage";
            this.toolTip1.SetToolTip(this.autoChk, "Prompt to automatically install the userscript if the webpage is one.");
            this.autoChk.UseVisualStyleBackColor = true;
            // 
            // publicApiChk
            // 
            this.publicApiChk.AutoSize = true;
            this.publicApiChk.Location = new System.Drawing.Point(32, 109);
            this.publicApiChk.Name = "publicApiChk";
            this.publicApiChk.Size = new System.Drawing.Size(179, 17);
            this.publicApiChk.TabIndex = 5;
            this.publicApiChk.Text = "Use public API (developers only)";
            this.toolTip1.SetToolTip(this.publicApiChk, "Use the API key \'public\' to access any script via the API. \r\n\r\nThis setting is on" +
        "ly for developers as it can allow websites to change Scriptmonkey settings.");
            this.publicApiChk.UseVisualStyleBackColor = true;
            // 
            // cacheChk
            // 
            this.cacheChk.AutoSize = true;
            this.cacheChk.Location = new System.Drawing.Point(13, 132);
            this.cacheChk.Name = "cacheChk";
            this.cacheChk.Size = new System.Drawing.Size(90, 17);
            this.cacheChk.TabIndex = 6;
            this.cacheChk.Text = "Cache scripts";
            this.toolTip1.SetToolTip(this.cacheChk, "Make loading times faster and reduce disk usage by caching script contents instea" +
        "d of reload from disk every page load.");
            this.cacheChk.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 229);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Reload settings after";
            // 
            // reloadNum
            // 
            this.reloadNum.Location = new System.Drawing.Point(122, 227);
            this.reloadNum.Name = "reloadNum";
            this.reloadNum.Size = new System.Drawing.Size(57, 20);
            this.reloadNum.TabIndex = 9;
            this.toolTip1.SetToolTip(this.reloadNum, "Reload the settings file after this many pages have been visited.\r\n\r\nThis number " +
        "may be increased up to 100 if Scriptmonkey Link is running to avoid extra disk u" +
        "sage.");
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(185, 229);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "pages";
            // 
            // refreshPageChk
            // 
            this.refreshPageChk.AutoSize = true;
            this.refreshPageChk.Location = new System.Drawing.Point(13, 156);
            this.refreshPageChk.Name = "refreshPageChk";
            this.refreshPageChk.Size = new System.Drawing.Size(131, 17);
            this.refreshPageChk.TabIndex = 7;
            this.refreshPageChk.Text = "Refresh page on save";
            this.toolTip1.SetToolTip(this.refreshPageChk, "Automatically refresh the current webpage when the options window is closed.");
            this.refreshPageChk.UseVisualStyleBackColor = true;
            // 
            // injectApiChk
            // 
            this.injectApiChk.AutoSize = true;
            this.injectApiChk.Location = new System.Drawing.Point(13, 86);
            this.injectApiChk.Name = "injectApiChk";
            this.injectApiChk.Size = new System.Drawing.Size(139, 17);
            this.injectApiChk.TabIndex = 4;
            this.injectApiChk.Text = "Inject API into webpage";
            this.toolTip1.SetToolTip(this.injectApiChk, "Allows access to GM_ functions.");
            this.injectApiChk.UseVisualStyleBackColor = true;
            this.injectApiChk.CheckedChanged += new System.EventHandler(this.injectApiChk_CheckedChanged);
            // 
            // useLinkChk
            // 
            this.useLinkChk.AutoSize = true;
            this.useLinkChk.Location = new System.Drawing.Point(13, 179);
            this.useLinkChk.Name = "useLinkChk";
            this.useLinkChk.Size = new System.Drawing.Size(211, 17);
            this.useLinkChk.TabIndex = 8;
            this.useLinkChk.Text = "Use Scriptmonkey Link (recommended)";
            this.toolTip1.SetToolTip(this.useLinkChk, "Use Scriptmonkey Link to synchronise settings between browser windows if Link is " +
        "currently running.");
            this.useLinkChk.UseVisualStyleBackColor = true;
            // 
            // lockSettingsBtn
            // 
            this.lockSettingsBtn.Location = new System.Drawing.Point(121, 253);
            this.lockSettingsBtn.Name = "lockSettingsBtn";
            this.lockSettingsBtn.Size = new System.Drawing.Size(113, 23);
            this.lockSettingsBtn.TabIndex = 11;
            this.lockSettingsBtn.Text = "Lock Settings File";
            this.toolTip1.SetToolTip(this.lockSettingsBtn, resources.GetString("lockSettingsBtn.ToolTip"));
            this.lockSettingsBtn.UseVisualStyleBackColor = true;
            this.lockSettingsBtn.Click += new System.EventHandler(this.lockSettingsBtn_Click);
            // 
            // menuCssBtn
            // 
            this.menuCssBtn.Location = new System.Drawing.Point(15, 253);
            this.menuCssBtn.Name = "menuCssBtn";
            this.menuCssBtn.Size = new System.Drawing.Size(101, 23);
            this.menuCssBtn.TabIndex = 10;
            this.menuCssBtn.Text = "Edit Menu CSS";
            this.toolTip1.SetToolTip(this.menuCssBtn, "Edit how the menu command buttons appear on the webpage. These buttons only appea" +
        "r if a script has registered menu commands with Scriptmonkey.\r\n\r\nEnter \'default\'" +
        " to set to the default value.");
            this.menuCssBtn.UseVisualStyleBackColor = true;
            this.menuCssBtn.Click += new System.EventHandler(this.menuCssBtn_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ToolTipTitle = "Help";
            // 
            // updateDisabledChk
            // 
            this.updateDisabledChk.AutoSize = true;
            this.updateDisabledChk.Location = new System.Drawing.Point(13, 202);
            this.updateDisabledChk.Name = "updateDisabledChk";
            this.updateDisabledChk.Size = new System.Drawing.Size(136, 17);
            this.updateDisabledChk.TabIndex = 12;
            this.updateDisabledChk.Text = "Update disabled scripts";
            this.updateDisabledChk.UseVisualStyleBackColor = true;
            // 
            // AdvancedOptionsFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(327, 286);
            this.Controls.Add(this.updateDisabledChk);
            this.Controls.Add(this.menuCssBtn);
            this.Controls.Add(this.lockSettingsBtn);
            this.Controls.Add(this.useLinkChk);
            this.Controls.Add(this.injectApiChk);
            this.Controls.Add(this.refreshPageChk);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.reloadNum);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cacheChk);
            this.Controls.Add(this.publicApiChk);
            this.Controls.Add(this.autoChk);
            this.Controls.Add(this.refreshChk);
            this.Controls.Add(this.updateChk);
            this.Controls.Add(this.saveBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AdvancedOptionsFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Advanced Options";
            this.Load += new System.EventHandler(this.AdvancedOptionsFrm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.reloadNum)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button saveBtn;
        private System.Windows.Forms.CheckBox updateChk;
        private System.Windows.Forms.CheckBox refreshChk;
        private System.Windows.Forms.CheckBox autoChk;
        private System.Windows.Forms.CheckBox publicApiChk;
        private System.Windows.Forms.CheckBox cacheChk;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown reloadNum;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox refreshPageChk;
        private System.Windows.Forms.CheckBox injectApiChk;
        private System.Windows.Forms.CheckBox useLinkChk;
        private System.Windows.Forms.Button lockSettingsBtn;
        private System.Windows.Forms.Button menuCssBtn;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.CheckBox updateDisabledChk;
    }
}