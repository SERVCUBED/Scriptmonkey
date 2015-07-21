using System.ComponentModel;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScriptEditFrm));
            this.label1 = new System.Windows.Forms.Label();
            this.nameTxt = new System.Windows.Forms.TextBox();
            this.okBtn = new System.Windows.Forms.Button();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.fileTxt = new System.Windows.Forms.TextBox();
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
            this.button1 = new System.Windows.Forms.Button();
            this.editBtn = new System.Windows.Forms.Button();
            this.enabledChk = new System.Windows.Forms.CheckBox();
            this.refBtn = new System.Windows.Forms.Button();
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
            this.okBtn.Location = new System.Drawing.Point(424, 262);
            this.okBtn.Name = "okBtn";
            this.okBtn.Size = new System.Drawing.Size(75, 23);
            this.okBtn.TabIndex = 14;
            this.okBtn.Text = "Save";
            this.okBtn.UseVisualStyleBackColor = true;
            this.okBtn.Click += new System.EventHandler(this.okBtn_Click);
            // 
            // cancelBtn
            // 
            this.cancelBtn.Location = new System.Drawing.Point(343, 262);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(75, 23);
            this.cancelBtn.TabIndex = 13;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "File:";
            // 
            // fileTxt
            // 
            this.fileTxt.Location = new System.Drawing.Point(89, 11);
            this.fileTxt.Name = "fileTxt";
            this.fileTxt.ReadOnly = true;
            this.fileTxt.Size = new System.Drawing.Size(295, 20);
            this.fileTxt.TabIndex = 1;
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
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(89, 117);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(410, 82);
            this.listBox1.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 117);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Match:";
            // 
            // addMatchBtn
            // 
            this.addMatchBtn.Location = new System.Drawing.Point(16, 137);
            this.addMatchBtn.Name = "addMatchBtn";
            this.addMatchBtn.Size = new System.Drawing.Size(26, 23);
            this.addMatchBtn.TabIndex = 7;
            this.addMatchBtn.Text = "+";
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
            this.descriptionTxt.Location = new System.Drawing.Point(89, 209);
            this.descriptionTxt.Name = "descriptionTxt";
            this.descriptionTxt.Size = new System.Drawing.Size(410, 20);
            this.descriptionTxt.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 212);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Description:";
            // 
            // updateTxt
            // 
            this.updateTxt.Location = new System.Drawing.Point(89, 236);
            this.updateTxt.Name = "updateTxt";
            this.updateTxt.Size = new System.Drawing.Size(410, 20);
            this.updateTxt.TabIndex = 10;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 239);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(70, 13);
            this.label7.TabIndex = 17;
            this.label7.Text = "Update URL:";
            // 
            // button1
            // 
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.Location = new System.Drawing.Point(390, 9);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(28, 23);
            this.button1.TabIndex = 2;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // editBtn
            // 
            this.editBtn.Location = new System.Drawing.Point(262, 262);
            this.editBtn.Name = "editBtn";
            this.editBtn.Size = new System.Drawing.Size(75, 23);
            this.editBtn.TabIndex = 12;
            this.editBtn.Text = "Edit File";
            this.editBtn.UseVisualStyleBackColor = true;
            this.editBtn.Click += new System.EventHandler(this.editBtn_Click);
            // 
            // enabledChk
            // 
            this.enabledChk.AutoSize = true;
            this.enabledChk.Checked = true;
            this.enabledChk.CheckState = System.Windows.Forms.CheckState.Checked;
            this.enabledChk.Location = new System.Drawing.Point(89, 266);
            this.enabledChk.Name = "enabledChk";
            this.enabledChk.Size = new System.Drawing.Size(64, 17);
            this.enabledChk.TabIndex = 11;
            this.enabledChk.Text = "Enabled";
            this.enabledChk.UseVisualStyleBackColor = true;
            // 
            // refBtn
            // 
            this.refBtn.Location = new System.Drawing.Point(424, 9);
            this.refBtn.Name = "refBtn";
            this.refBtn.Size = new System.Drawing.Size(75, 23);
            this.refBtn.TabIndex = 3;
            this.refBtn.Text = "Refresh";
            this.refBtn.UseVisualStyleBackColor = true;
            this.refBtn.Click += new System.EventHandler(this.refBtn_Click);
            // 
            // ScriptEditFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(511, 293);
            this.Controls.Add(this.refBtn);
            this.Controls.Add(this.enabledChk);
            this.Controls.Add(this.editBtn);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.updateTxt);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.descriptionTxt);
            this.Controls.Add(this.remMatchBtn);
            this.Controls.Add(this.addMatchBtn);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.versionTxt);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.authorTxt);
            this.Controls.Add(this.fileTxt);
            this.Controls.Add(this.label2);
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
            this.Text = "Edit userscript";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label1;
        private TextBox nameTxt;
        private Button okBtn;
        private Button cancelBtn;
        private Label label2;
        private TextBox fileTxt;
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
        private Button button1;
        private Button editBtn;
        private CheckBox enabledChk;
        private Button refBtn;
    }
}