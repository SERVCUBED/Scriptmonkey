﻿using System.ComponentModel;
using System.Windows.Forms;

namespace BHOUserScript
{
    partial class ScriptEditFrm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScriptEditFrm));
            this.label1 = new System.Windows.Forms.Label();
            this.nameTxt = new System.Windows.Forms.TextBox();
            this.okBtn = new System.Windows.Forms.Button();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.authorTxt = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.versionTxt = new System.Windows.Forms.TextBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label5 = new System.Windows.Forms.Label();
            this.addMatchBtn = new System.Windows.Forms.Button();
            this.remMatchBtn = new System.Windows.Forms.Button();
            this.descriptionTxt = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.updateTxt = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.browseBtn = new System.Windows.Forms.Button();
            this.editBtn = new System.Windows.Forms.Button();
            this.enabledChk = new System.Windows.Forms.CheckBox();
            this.refBtn = new System.Windows.Forms.Button();
            this.noURLWarningLbl = new System.Windows.Forms.Label();
            this.excludesBox = new System.Windows.Forms.ListBox();
            this.label8 = new System.Windows.Forms.Label();
            this.addExcludeBtn = new System.Windows.Forms.Button();
            this.remExcludeBtn = new System.Windows.Forms.Button();
            this.noExcludeWarningLbl = new System.Windows.Forms.Label();
            this.clearValsBtn = new System.Windows.Forms.Button();
            this.editIncludeBtn = new System.Windows.Forms.Button();
            this.editExcludeBtn = new System.Windows.Forms.Button();
            this.menuCmdChk = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name:";
            // 
            // nameTxt
            // 
            this.nameTxt.Location = new System.Drawing.Point(89, 37);
            this.nameTxt.Name = "nameTxt";
            this.nameTxt.Size = new System.Drawing.Size(410, 20);
            this.nameTxt.TabIndex = 3;
            // 
            // okBtn
            // 
            this.okBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.okBtn.Location = new System.Drawing.Point(424, 370);
            this.okBtn.Name = "okBtn";
            this.okBtn.Size = new System.Drawing.Size(75, 23);
            this.okBtn.TabIndex = 19;
            this.okBtn.Text = "Save";
            this.okBtn.UseVisualStyleBackColor = false;
            this.okBtn.Click += new System.EventHandler(this.okBtn_Click);
            // 
            // cancelBtn
            // 
            this.cancelBtn.Location = new System.Drawing.Point(343, 370);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(75, 23);
            this.cancelBtn.TabIndex = 18;
            this.cancelBtn.Text = "Cancel";
            this.toolTip1.SetToolTip(this.cancelBtn, "Discard all changes.");
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // authorTxt
            // 
            this.authorTxt.Location = new System.Drawing.Point(89, 63);
            this.authorTxt.Name = "authorTxt";
            this.authorTxt.Size = new System.Drawing.Size(410, 20);
            this.authorTxt.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Author:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 93);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Version:";
            // 
            // versionTxt
            // 
            this.versionTxt.Location = new System.Drawing.Point(89, 90);
            this.versionTxt.Name = "versionTxt";
            this.versionTxt.Size = new System.Drawing.Size(410, 20);
            this.versionTxt.TabIndex = 5;
            // 
            // listBox1
            // 
            this.listBox1.Enabled = false;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(89, 117);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(410, 82);
            this.listBox1.TabIndex = 6;
            this.listBox1.DoubleClick += new System.EventHandler(this.editIncludeBtn_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 117);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "URLs:";
            // 
            // addMatchBtn
            // 
            this.addMatchBtn.Location = new System.Drawing.Point(16, 137);
            this.addMatchBtn.Name = "addMatchBtn";
            this.addMatchBtn.Size = new System.Drawing.Size(26, 23);
            this.addMatchBtn.TabIndex = 7;
            this.addMatchBtn.Text = "+";
            this.toolTip1.SetToolTip(this.addMatchBtn, "The script will be run on these URLs.\r\n\r\nFor regex, start and end the input with " +
        "a forward-slash. Otherwise * and ? \r\ncan be used as wildcard characters.");
            this.addMatchBtn.UseVisualStyleBackColor = true;
            this.addMatchBtn.Click += new System.EventHandler(this.addMatchBtn_Click);
            // 
            // remMatchBtn
            // 
            this.remMatchBtn.Location = new System.Drawing.Point(48, 137);
            this.remMatchBtn.Name = "remMatchBtn";
            this.remMatchBtn.Size = new System.Drawing.Size(24, 23);
            this.remMatchBtn.TabIndex = 8;
            this.remMatchBtn.Text = "-";
            this.remMatchBtn.UseVisualStyleBackColor = true;
            this.remMatchBtn.Click += new System.EventHandler(this.remMatchBtn_Click);
            // 
            // descriptionTxt
            // 
            this.descriptionTxt.Location = new System.Drawing.Point(89, 293);
            this.descriptionTxt.Name = "descriptionTxt";
            this.descriptionTxt.Size = new System.Drawing.Size(410, 20);
            this.descriptionTxt.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 296);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Description:";
            // 
            // updateTxt
            // 
            this.updateTxt.Location = new System.Drawing.Point(89, 320);
            this.updateTxt.Name = "updateTxt";
            this.updateTxt.Size = new System.Drawing.Size(410, 20);
            this.updateTxt.TabIndex = 13;
            this.toolTip1.SetToolTip(this.updateTxt, "The URL to check for updates from every 7 days. Leave blank to disable update che" +
        "cking for this script.");
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 323);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(70, 13);
            this.label7.TabIndex = 17;
            this.label7.Text = "Update URL:";
            // 
            // browseBtn
            // 
            this.browseBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.browseBtn.Image = ((System.Drawing.Image)(resources.GetObject("browseBtn.Image")));
            this.browseBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.browseBtn.Location = new System.Drawing.Point(89, 8);
            this.browseBtn.Name = "browseBtn";
            this.browseBtn.Size = new System.Drawing.Size(254, 23);
            this.browseBtn.TabIndex = 2;
            this.browseBtn.Text = "Browse for file";
            this.toolTip1.SetToolTip(this.browseBtn, "Load a script from a file or web URL.");
            this.browseBtn.UseVisualStyleBackColor = false;
            this.browseBtn.Click += new System.EventHandler(this.button1_Click);
            // 
            // editBtn
            // 
            this.editBtn.Location = new System.Drawing.Point(262, 370);
            this.editBtn.Name = "editBtn";
            this.editBtn.Size = new System.Drawing.Size(75, 23);
            this.editBtn.TabIndex = 17;
            this.editBtn.Text = "Edit Content";
            this.toolTip1.SetToolTip(this.editBtn, "Edit the script file in the specified text editor. The editor used can be \r\nchang" +
        "ed from the options button on the main window.");
            this.editBtn.UseVisualStyleBackColor = true;
            this.editBtn.Click += new System.EventHandler(this.editBtn_Click);
            // 
            // enabledChk
            // 
            this.enabledChk.AutoSize = true;
            this.enabledChk.Checked = true;
            this.enabledChk.CheckState = System.Windows.Forms.CheckState.Checked;
            this.enabledChk.Location = new System.Drawing.Point(72, 374);
            this.enabledChk.Name = "enabledChk";
            this.enabledChk.Size = new System.Drawing.Size(65, 17);
            this.enabledChk.TabIndex = 15;
            this.enabledChk.Text = "Enabled";
            this.toolTip1.SetToolTip(this.enabledChk, "Run the script.");
            this.enabledChk.UseVisualStyleBackColor = true;
            // 
            // refBtn
            // 
            this.refBtn.Location = new System.Drawing.Point(349, 8);
            this.refBtn.Name = "refBtn";
            this.refBtn.Size = new System.Drawing.Size(150, 23);
            this.refBtn.TabIndex = 3;
            this.refBtn.Text = "Refresh metadata from file";
            this.toolTip1.SetToolTip(this.refBtn, "Reload the metadata from the script.");
            this.refBtn.UseVisualStyleBackColor = true;
            this.refBtn.Click += new System.EventHandler(this.refBtn_Click);
            // 
            // noURLWarningLbl
            // 
            this.noURLWarningLbl.AutoSize = true;
            this.noURLWarningLbl.BackColor = System.Drawing.SystemColors.Window;
            this.noURLWarningLbl.Location = new System.Drawing.Point(91, 119);
            this.noURLWarningLbl.Name = "noURLWarningLbl";
            this.noURLWarningLbl.Size = new System.Drawing.Size(212, 13);
            this.noURLWarningLbl.TabIndex = 18;
            this.noURLWarningLbl.Text = "No items! This script will run on all websites!";
            // 
            // excludesBox
            // 
            this.excludesBox.Enabled = false;
            this.excludesBox.FormattingEnabled = true;
            this.excludesBox.Location = new System.Drawing.Point(89, 205);
            this.excludesBox.Name = "excludesBox";
            this.excludesBox.Size = new System.Drawing.Size(410, 82);
            this.excludesBox.TabIndex = 9;
            this.excludesBox.DoubleClick += new System.EventHandler(this.editExcludeBtn_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(13, 205);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(45, 26);
            this.label8.TabIndex = 11;
            this.label8.Text = "Exclude\r\nURLs:";
            // 
            // addExcludeBtn
            // 
            this.addExcludeBtn.Location = new System.Drawing.Point(16, 234);
            this.addExcludeBtn.Name = "addExcludeBtn";
            this.addExcludeBtn.Size = new System.Drawing.Size(26, 23);
            this.addExcludeBtn.TabIndex = 10;
            this.addExcludeBtn.Text = "+";
            this.toolTip1.SetToolTip(this.addExcludeBtn, "The script will not be run on these URLs.\r\n\r\nFor regex, start and end the input w" +
        "ith a forward-slash. Otherwise * and ? \r\ncan be used as wildcard characters.");
            this.addExcludeBtn.UseVisualStyleBackColor = true;
            this.addExcludeBtn.Click += new System.EventHandler(this.addExcludeBtn_Click);
            // 
            // remExcludeBtn
            // 
            this.remExcludeBtn.Location = new System.Drawing.Point(48, 234);
            this.remExcludeBtn.Name = "remExcludeBtn";
            this.remExcludeBtn.Size = new System.Drawing.Size(24, 23);
            this.remExcludeBtn.TabIndex = 11;
            this.remExcludeBtn.Text = "-";
            this.remExcludeBtn.UseVisualStyleBackColor = true;
            this.remExcludeBtn.Click += new System.EventHandler(this.remExcludeBtn_Click);
            // 
            // noExcludeWarningLbl
            // 
            this.noExcludeWarningLbl.AutoSize = true;
            this.noExcludeWarningLbl.BackColor = System.Drawing.SystemColors.Window;
            this.noExcludeWarningLbl.Location = new System.Drawing.Point(91, 207);
            this.noExcludeWarningLbl.Name = "noExcludeWarningLbl";
            this.noExcludeWarningLbl.Size = new System.Drawing.Size(51, 13);
            this.noExcludeWarningLbl.TabIndex = 18;
            this.noExcludeWarningLbl.Text = "No items!";
            // 
            // clearValsBtn
            // 
            this.clearValsBtn.Location = new System.Drawing.Point(142, 370);
            this.clearValsBtn.Name = "clearValsBtn";
            this.clearValsBtn.Size = new System.Drawing.Size(114, 23);
            this.clearValsBtn.TabIndex = 16;
            this.clearValsBtn.Text = "Clear Saved Values";
            this.toolTip1.SetToolTip(this.clearValsBtn, "Clear the saved values and menu commands.");
            this.clearValsBtn.UseVisualStyleBackColor = true;
            this.clearValsBtn.Visible = false;
            this.clearValsBtn.Click += new System.EventHandler(this.clearValsBtn_Click);
            // 
            // editIncludeBtn
            // 
            this.editIncludeBtn.Location = new System.Drawing.Point(16, 166);
            this.editIncludeBtn.Name = "editIncludeBtn";
            this.editIncludeBtn.Size = new System.Drawing.Size(56, 23);
            this.editIncludeBtn.TabIndex = 20;
            this.editIncludeBtn.Text = "Edit";
            this.editIncludeBtn.UseVisualStyleBackColor = true;
            this.editIncludeBtn.Click += new System.EventHandler(this.editIncludeBtn_Click);
            // 
            // editExcludeBtn
            // 
            this.editExcludeBtn.Location = new System.Drawing.Point(16, 263);
            this.editExcludeBtn.Name = "editExcludeBtn";
            this.editExcludeBtn.Size = new System.Drawing.Size(56, 23);
            this.editExcludeBtn.TabIndex = 20;
            this.editExcludeBtn.Text = "Edit";
            this.editExcludeBtn.UseVisualStyleBackColor = true;
            this.editExcludeBtn.Click += new System.EventHandler(this.editExcludeBtn_Click);
            // 
            // menuCmdChk
            // 
            this.menuCmdChk.AutoSize = true;
            this.menuCmdChk.Checked = true;
            this.menuCmdChk.CheckState = System.Windows.Forms.CheckState.Checked;
            this.menuCmdChk.Location = new System.Drawing.Point(72, 347);
            this.menuCmdChk.Name = "menuCmdChk";
            this.menuCmdChk.Size = new System.Drawing.Size(161, 17);
            this.menuCmdChk.TabIndex = 14;
            this.menuCmdChk.Text = "Show menu commands if set";
            this.toolTip1.SetToolTip(this.menuCmdChk, "If this script sets menu commands, show those menu commands in the command list i" +
        "n the browser window.");
            this.menuCmdChk.UseVisualStyleBackColor = true;
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ToolTipTitle = "Help";
            // 
            // ScriptEditFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(511, 405);
            this.Controls.Add(this.menuCmdChk);
            this.Controls.Add(this.editExcludeBtn);
            this.Controls.Add(this.editIncludeBtn);
            this.Controls.Add(this.clearValsBtn);
            this.Controls.Add(this.noExcludeWarningLbl);
            this.Controls.Add(this.noURLWarningLbl);
            this.Controls.Add(this.refBtn);
            this.Controls.Add(this.enabledChk);
            this.Controls.Add(this.editBtn);
            this.Controls.Add(this.browseBtn);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.updateTxt);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.descriptionTxt);
            this.Controls.Add(this.remExcludeBtn);
            this.Controls.Add(this.remMatchBtn);
            this.Controls.Add(this.addExcludeBtn);
            this.Controls.Add(this.addMatchBtn);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.excludesBox);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.versionTxt);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.authorTxt);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.okBtn);
            this.Controls.Add(this.nameTxt);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ScriptEditFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "New Userscript";
            this.Load += new System.EventHandler(this.ScriptEditFrm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label1;
        private TextBox nameTxt;
        private Button okBtn;
        private Button cancelBtn;
        private TextBox authorTxt;
        private Label label3;
        private Label label4;
        private TextBox versionTxt;
        private ListBox listBox1;
        private Label label5;
        private Button addMatchBtn;
        private Button remMatchBtn;
        private TextBox descriptionTxt;
        private Label label6;
        private TextBox updateTxt;
        private Label label7;
        private Button browseBtn;
        private Button editBtn;
        private CheckBox enabledChk;
        private Button refBtn;
        private Label noURLWarningLbl;
        private ListBox excludesBox;
        private Label label8;
        private Button addExcludeBtn;
        private Button remExcludeBtn;
        private Label noExcludeWarningLbl;
        private Button clearValsBtn;
        private Button editIncludeBtn;
        private Button editExcludeBtn;
        private CheckBox menuCmdChk;
        private ToolTip toolTip1;
    }
}