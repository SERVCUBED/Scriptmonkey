﻿using System.ComponentModel;
using System.Windows.Forms;

namespace BHOUserScript
{
    partial class Options
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Options));
            this.okBtn = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.addBtn = new System.Windows.Forms.Button();
            this.removeBtn = new System.Windows.Forms.Button();
            this.editBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.enabledChk = new System.Windows.Forms.CheckBox();
            this.eachEnabledChk = new System.Windows.Forms.CheckBox();
            this.btnMoveUp = new System.Windows.Forms.Button();
            this.btnMoveDown = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.optionsBtn = new System.Windows.Forms.Button();
            this.scriptEditorOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.addContextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.newScriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newCSSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addContextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // okBtn
            // 
            this.okBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.okBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.okBtn.Location = new System.Drawing.Point(348, 258);
            this.okBtn.Name = "okBtn";
            this.okBtn.Size = new System.Drawing.Size(75, 23);
            this.okBtn.TabIndex = 12;
            this.okBtn.Text = "Save";
            this.okBtn.UseVisualStyleBackColor = false;
            this.okBtn.Click += new System.EventHandler(this.okBtn_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(12, 12);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(330, 225);
            this.listBox1.TabIndex = 1;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            this.listBox1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBox1_MouseDoubleClick);
            // 
            // addBtn
            // 
            this.addBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.addBtn.Location = new System.Drawing.Point(348, 12);
            this.addBtn.Name = "addBtn";
            this.addBtn.Size = new System.Drawing.Size(75, 23);
            this.addBtn.TabIndex = 2;
            this.addBtn.Text = "Add";
            this.addBtn.UseVisualStyleBackColor = false;
            this.addBtn.Click += new System.EventHandler(this.addBtn_Click);
            // 
            // removeBtn
            // 
            this.removeBtn.BackColor = System.Drawing.Color.IndianRed;
            this.removeBtn.Location = new System.Drawing.Point(348, 70);
            this.removeBtn.Name = "removeBtn";
            this.removeBtn.Size = new System.Drawing.Size(75, 23);
            this.removeBtn.TabIndex = 4;
            this.removeBtn.Text = "Remove";
            this.removeBtn.UseVisualStyleBackColor = false;
            this.removeBtn.Click += new System.EventHandler(this.removeBtn_Click);
            // 
            // editBtn
            // 
            this.editBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.editBtn.Location = new System.Drawing.Point(348, 41);
            this.editBtn.Name = "editBtn";
            this.editBtn.Size = new System.Drawing.Size(75, 23);
            this.editBtn.TabIndex = 3;
            this.editBtn.Text = "Edit";
            this.editBtn.UseVisualStyleBackColor = false;
            this.editBtn.Click += new System.EventHandler(this.editBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 242);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(296, 39);
            this.label1.TabIndex = 7;
            this.label1.Text = "Scriptmonkey userscript manager is free software. It offers no \r\nwarranty of any " +
    "kind. For license and source code see \r\nGitHub repository.";
            // 
            // enabledChk
            // 
            this.enabledChk.AutoSize = true;
            this.enabledChk.Checked = true;
            this.enabledChk.CheckState = System.Windows.Forms.CheckState.Checked;
            this.enabledChk.Location = new System.Drawing.Point(348, 129);
            this.enabledChk.Name = "enabledChk";
            this.enabledChk.Size = new System.Drawing.Size(79, 17);
            this.enabledChk.TabIndex = 7;
            this.enabledChk.Text = "All Enabled";
            this.enabledChk.UseVisualStyleBackColor = true;
            this.enabledChk.CheckedChanged += new System.EventHandler(this.enabledChk_CheckedChanged);
            // 
            // eachEnabledChk
            // 
            this.eachEnabledChk.AutoSize = true;
            this.eachEnabledChk.Enabled = false;
            this.eachEnabledChk.Location = new System.Drawing.Point(348, 152);
            this.eachEnabledChk.Name = "eachEnabledChk";
            this.eachEnabledChk.Size = new System.Drawing.Size(71, 30);
            this.eachEnabledChk.TabIndex = 8;
            this.eachEnabledChk.Text = "Selected \r\nEnabled";
            this.eachEnabledChk.UseVisualStyleBackColor = true;
            this.eachEnabledChk.CheckedChanged += new System.EventHandler(this.eachEnabledChk_CheckedChanged);
            // 
            // btnMoveUp
            // 
            this.btnMoveUp.Location = new System.Drawing.Point(348, 100);
            this.btnMoveUp.Name = "btnMoveUp";
            this.btnMoveUp.Size = new System.Drawing.Size(36, 23);
            this.btnMoveUp.TabIndex = 5;
            this.btnMoveUp.Text = "/\\";
            this.btnMoveUp.UseVisualStyleBackColor = true;
            this.btnMoveUp.Click += new System.EventHandler(this.btnMoveUp_Click);
            // 
            // btnMoveDown
            // 
            this.btnMoveDown.Location = new System.Drawing.Point(388, 100);
            this.btnMoveDown.Name = "btnMoveDown";
            this.btnMoveDown.Size = new System.Drawing.Size(35, 23);
            this.btnMoveDown.TabIndex = 6;
            this.btnMoveDown.Text = "\\/";
            this.btnMoveDown.UseVisualStyleBackColor = true;
            this.btnMoveDown.Click += new System.EventHandler(this.btnMoveDown_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(348, 188);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 35);
            this.button2.TabIndex = 10;
            this.button2.Text = "Get Userscripts";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // optionsBtn
            // 
            this.optionsBtn.Location = new System.Drawing.Point(348, 229);
            this.optionsBtn.Name = "optionsBtn";
            this.optionsBtn.Size = new System.Drawing.Size(75, 23);
            this.optionsBtn.TabIndex = 11;
            this.optionsBtn.Text = "Advanced";
            this.optionsBtn.UseVisualStyleBackColor = true;
            this.optionsBtn.Click += new System.EventHandler(this.optionsBtn_Click);
            // 
            // scriptEditorOpenFileDialog
            // 
            this.scriptEditorOpenFileDialog.Filter = "Executable files (*.exe)|*.exe|All files (*.*)|*.*";
            // 
            // addContextMenuStrip1
            // 
            this.addContextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newScriptToolStripMenuItem,
            this.newCSSToolStripMenuItem});
            this.addContextMenuStrip1.Name = "addContextMenuStrip1";
            this.addContextMenuStrip1.Size = new System.Drawing.Size(132, 48);
            // 
            // newScriptToolStripMenuItem
            // 
            this.newScriptToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.newScriptToolStripMenuItem.Name = "newScriptToolStripMenuItem";
            this.newScriptToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.newScriptToolStripMenuItem.Text = "New &Script";
            this.newScriptToolStripMenuItem.Click += new System.EventHandler(this.newScriptToolStripMenuItem_Click);
            // 
            // newCSSToolStripMenuItem
            // 
            this.newCSSToolStripMenuItem.Name = "newCSSToolStripMenuItem";
            this.newCSSToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.newCSSToolStripMenuItem.Text = "New &CSS";
            this.newCSSToolStripMenuItem.Click += new System.EventHandler(this.newCSSToolStripMenuItem_Click);
            // 
            // Options
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.okBtn;
            this.ClientSize = new System.Drawing.Size(435, 291);
            this.Controls.Add(this.optionsBtn);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnMoveDown);
            this.Controls.Add(this.btnMoveUp);
            this.Controls.Add(this.eachEnabledChk);
            this.Controls.Add(this.enabledChk);
            this.Controls.Add(this.editBtn);
            this.Controls.Add(this.removeBtn);
            this.Controls.Add(this.addBtn);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.okBtn);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Options";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Scriptmonkey Options";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Options_FormClosed);
            this.Load += new System.EventHandler(this.Options_Load);
            this.addContextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button okBtn;
        private ListBox listBox1;
        private Button addBtn;
        private Button removeBtn;
        private Button editBtn;
        private Label label1;
        private CheckBox enabledChk;
        private CheckBox eachEnabledChk;
        private Button btnMoveUp;
        private Button btnMoveDown;
        private Button button2;
        private Button optionsBtn;
        private OpenFileDialog scriptEditorOpenFileDialog;
        private ContextMenuStrip addContextMenuStrip1;
        private ToolStripMenuItem newScriptToolStripMenuItem;
        private ToolStripMenuItem newCSSToolStripMenuItem;
    }
}