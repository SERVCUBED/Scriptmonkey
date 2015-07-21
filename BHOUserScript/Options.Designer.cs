using System.ComponentModel;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Options));
            this.okBtn = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.addBtn = new System.Windows.Forms.Button();
            this.removeBtn = new System.Windows.Forms.Button();
            this.editBtn = new System.Windows.Forms.Button();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.enabledChk = new System.Windows.Forms.CheckBox();
            this.eachEnabledChk = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btnMoveUp = new System.Windows.Forms.Button();
            this.btnMoveDown = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // okBtn
            // 
            this.okBtn.Location = new System.Drawing.Point(285, 217);
            this.okBtn.Name = "okBtn";
            this.okBtn.Size = new System.Drawing.Size(75, 23);
            this.okBtn.TabIndex = 6;
            this.okBtn.Text = "Save";
            this.okBtn.UseVisualStyleBackColor = true;
            this.okBtn.Click += new System.EventHandler(this.okBtn_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(12, 12);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(267, 264);
            this.listBox1.TabIndex = 1;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            this.listBox1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBox1_MouseDoubleClick);
            // 
            // addBtn
            // 
            this.addBtn.Location = new System.Drawing.Point(285, 12);
            this.addBtn.Name = "addBtn";
            this.addBtn.Size = new System.Drawing.Size(75, 23);
            this.addBtn.TabIndex = 2;
            this.addBtn.Text = "Add";
            this.addBtn.UseVisualStyleBackColor = true;
            this.addBtn.Click += new System.EventHandler(this.addBtn_Click);
            // 
            // removeBtn
            // 
            this.removeBtn.Location = new System.Drawing.Point(285, 70);
            this.removeBtn.Name = "removeBtn";
            this.removeBtn.Size = new System.Drawing.Size(75, 23);
            this.removeBtn.TabIndex = 4;
            this.removeBtn.Text = "Remove";
            this.removeBtn.UseVisualStyleBackColor = true;
            this.removeBtn.Click += new System.EventHandler(this.removeBtn_Click);
            // 
            // editBtn
            // 
            this.editBtn.Location = new System.Drawing.Point(285, 41);
            this.editBtn.Name = "editBtn";
            this.editBtn.Size = new System.Drawing.Size(75, 23);
            this.editBtn.TabIndex = 3;
            this.editBtn.Text = "Edit";
            this.editBtn.UseVisualStyleBackColor = true;
            this.editBtn.Click += new System.EventHandler(this.editBtn_Click);
            // 
            // cancelBtn
            // 
            this.cancelBtn.Location = new System.Drawing.Point(285, 188);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(75, 23);
            this.cancelBtn.TabIndex = 5;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 285);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(339, 26);
            this.label1.TabIndex = 7;
            this.label1.Text = "Scriptmonkey userscript manager is free software. It offers no warranty \r\nof any " +
    "kind. For license and source code see GitHub repository.";
            // 
            // enabledChk
            // 
            this.enabledChk.AutoSize = true;
            this.enabledChk.Checked = true;
            this.enabledChk.CheckState = System.Windows.Forms.CheckState.Checked;
            this.enabledChk.Location = new System.Drawing.Point(285, 129);
            this.enabledChk.Name = "enabledChk";
            this.enabledChk.Size = new System.Drawing.Size(78, 17);
            this.enabledChk.TabIndex = 8;
            this.enabledChk.Text = "All Enabled";
            this.enabledChk.UseVisualStyleBackColor = true;
            this.enabledChk.CheckedChanged += new System.EventHandler(this.enabledChk_CheckedChanged);
            // 
            // eachEnabledChk
            // 
            this.eachEnabledChk.AutoSize = true;
            this.eachEnabledChk.Enabled = false;
            this.eachEnabledChk.Location = new System.Drawing.Point(285, 152);
            this.eachEnabledChk.Name = "eachEnabledChk";
            this.eachEnabledChk.Size = new System.Drawing.Size(70, 30);
            this.eachEnabledChk.TabIndex = 9;
            this.eachEnabledChk.Text = "Selected \r\nEnabled";
            this.eachEnabledChk.UseVisualStyleBackColor = true;
            this.eachEnabledChk.CheckedChanged += new System.EventHandler(this.eachEnabledChk_CheckedChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(286, 247);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 35);
            this.button1.TabIndex = 10;
            this.button1.Text = "Open Install Directory";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnMoveUp
            // 
            this.btnMoveUp.Location = new System.Drawing.Point(285, 100);
            this.btnMoveUp.Name = "btnMoveUp";
            this.btnMoveUp.Size = new System.Drawing.Size(36, 23);
            this.btnMoveUp.TabIndex = 11;
            this.btnMoveUp.Text = "/\\";
            this.btnMoveUp.UseVisualStyleBackColor = true;
            this.btnMoveUp.Click += new System.EventHandler(this.btnMoveUp_Click);
            // 
            // btnMoveDown
            // 
            this.btnMoveDown.Location = new System.Drawing.Point(325, 100);
            this.btnMoveDown.Name = "btnMoveDown";
            this.btnMoveDown.Size = new System.Drawing.Size(35, 23);
            this.btnMoveDown.TabIndex = 12;
            this.btnMoveDown.Text = "\\/";
            this.btnMoveDown.UseVisualStyleBackColor = true;
            this.btnMoveDown.Click += new System.EventHandler(this.btnMoveDown_Click);
            // 
            // Options
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(372, 318);
            this.Controls.Add(this.btnMoveDown);
            this.Controls.Add(this.btnMoveUp);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.eachEnabledChk);
            this.Controls.Add(this.enabledChk);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.editBtn);
            this.Controls.Add(this.removeBtn);
            this.Controls.Add(this.addBtn);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.okBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Options";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Scriptmonkey Options";
            this.Load += new System.EventHandler(this.Options_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button okBtn;
        private ListBox listBox1;
        private Button addBtn;
        private Button removeBtn;
        private Button editBtn;
        private Button cancelBtn;
        private Label label1;
        private CheckBox enabledChk;
        private CheckBox eachEnabledChk;
        private Button button1;
        private Button btnMoveUp;
        private Button btnMoveDown;
    }
}