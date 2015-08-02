using System.ComponentModel;
using System.Windows.Forms;

namespace BHOUserScript
{
    partial class GetUserscriptsFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GetUserscriptsFrm));
            this.userscriptsMirrorLbl = new System.Windows.Forms.LinkLabel();
            this.greasyforkLbl = new System.Windows.Forms.LinkLabel();
            this.openUserJsLbl = new System.Windows.Forms.LinkLabel();
            this.proTurkersLbl = new System.Windows.Forms.LinkLabel();
            this.openJsScriptsLbl = new System.Windows.Forms.LinkLabel();
            this.userStylesLbl = new System.Windows.Forms.LinkLabel();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // userscriptsMirrorLbl
            // 
            this.userscriptsMirrorLbl.AutoSize = true;
            this.userscriptsMirrorLbl.Location = new System.Drawing.Point(13, 13);
            this.userscriptsMirrorLbl.Name = "userscriptsMirrorLbl";
            this.userscriptsMirrorLbl.Size = new System.Drawing.Size(103, 13);
            this.userscriptsMirrorLbl.TabIndex = 0;
            this.userscriptsMirrorLbl.TabStop = true;
            this.userscriptsMirrorLbl.Text = "userscripts-mirror.org";
            this.userscriptsMirrorLbl.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.userscriptsMirrorLbl_LinkClicked);
            // 
            // greasyforkLbl
            // 
            this.greasyforkLbl.AutoSize = true;
            this.greasyforkLbl.Location = new System.Drawing.Point(13, 30);
            this.greasyforkLbl.Name = "greasyforkLbl";
            this.greasyforkLbl.Size = new System.Drawing.Size(74, 13);
            this.greasyforkLbl.TabIndex = 1;
            this.greasyforkLbl.TabStop = true;
            this.greasyforkLbl.Text = "greasyfork.org";
            this.greasyforkLbl.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.greasyforkLbl_LinkClicked);
            // 
            // openUserJsLbl
            // 
            this.openUserJsLbl.AutoSize = true;
            this.openUserJsLbl.Location = new System.Drawing.Point(13, 47);
            this.openUserJsLbl.Name = "openUserJsLbl";
            this.openUserJsLbl.Size = new System.Drawing.Size(76, 13);
            this.openUserJsLbl.TabIndex = 2;
            this.openUserJsLbl.TabStop = true;
            this.openUserJsLbl.Text = "openuserjs.org";
            this.openUserJsLbl.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.openUserJsLbl_LinkClicked);
            // 
            // proTurkersLbl
            // 
            this.proTurkersLbl.AutoSize = true;
            this.proTurkersLbl.Location = new System.Drawing.Point(14, 64);
            this.proTurkersLbl.Name = "proTurkersLbl";
            this.proTurkersLbl.Size = new System.Drawing.Size(77, 13);
            this.proTurkersLbl.TabIndex = 3;
            this.proTurkersLbl.TabStop = true;
            this.proTurkersLbl.Text = "proturkers.com";
            this.proTurkersLbl.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.proTurkersLbl_LinkClicked);
            // 
            // openJsScriptsLbl
            // 
            this.openJsScriptsLbl.AutoSize = true;
            this.openJsScriptsLbl.Location = new System.Drawing.Point(14, 81);
            this.openJsScriptsLbl.Name = "openJsScriptsLbl";
            this.openJsScriptsLbl.Size = new System.Drawing.Size(101, 13);
            this.openJsScriptsLbl.TabIndex = 4;
            this.openJsScriptsLbl.TabStop = true;
            this.openJsScriptsLbl.Text = "openjs.com/scripts/";
            this.openJsScriptsLbl.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.openJsScriptsLbl_LinkClicked);
            // 
            // userStylesLbl
            // 
            this.userStylesLbl.AutoSize = true;
            this.userStylesLbl.Location = new System.Drawing.Point(14, 98);
            this.userStylesLbl.Name = "userStylesLbl";
            this.userStylesLbl.Size = new System.Drawing.Size(71, 13);
            this.userStylesLbl.TabIndex = 5;
            this.userStylesLbl.TabStop = true;
            this.userStylesLbl.Text = "userstyles.org";
            this.userStylesLbl.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.userStylesLbl_LinkClicked);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(200, 93);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "Cancel";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // GetUserscriptsFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(287, 129);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.userStylesLbl);
            this.Controls.Add(this.openJsScriptsLbl);
            this.Controls.Add(this.proTurkersLbl);
            this.Controls.Add(this.openUserJsLbl);
            this.Controls.Add(this.greasyforkLbl);
            this.Controls.Add(this.userscriptsMirrorLbl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GetUserscriptsFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Get Userscripts";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private LinkLabel userscriptsMirrorLbl;
        private LinkLabel greasyforkLbl;
        private LinkLabel openUserJsLbl;
        private LinkLabel proTurkersLbl;
        private LinkLabel openJsScriptsLbl;
        private LinkLabel userStylesLbl;
        private Button button1;
    }
}