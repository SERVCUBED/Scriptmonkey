using System.ComponentModel;
using System.Windows.Forms;

namespace BHOUserScript
{
    partial class AddScriptFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddScriptFrm));
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.fileTxt = new System.Windows.Forms.TextBox();
            this.browseBtn = new System.Windows.Forms.Button();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.urlTxt = new System.Windows.Forms.TextBox();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.okBtn = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.newBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(13, 13);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(67, 17);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "From File";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // fileTxt
            // 
            this.fileTxt.Location = new System.Drawing.Point(113, 12);
            this.fileTxt.Name = "fileTxt";
            this.fileTxt.ReadOnly = true;
            this.fileTxt.Size = new System.Drawing.Size(170, 20);
            this.fileTxt.TabIndex = 1;
            // 
            // browseBtn
            // 
            this.browseBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.browseBtn.Location = new System.Drawing.Point(288, 9);
            this.browseBtn.Name = "browseBtn";
            this.browseBtn.Size = new System.Drawing.Size(75, 23);
            this.browseBtn.TabIndex = 2;
            this.browseBtn.Text = "Browse";
            this.browseBtn.UseVisualStyleBackColor = false;
            this.browseBtn.Click += new System.EventHandler(this.browseBtn_Click);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(13, 39);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(99, 17);
            this.radioButton2.TabIndex = 3;
            this.radioButton2.Text = "From Web URL";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // urlTxt
            // 
            this.urlTxt.Enabled = false;
            this.urlTxt.Location = new System.Drawing.Point(113, 38);
            this.urlTxt.Name = "urlTxt";
            this.urlTxt.Size = new System.Drawing.Size(250, 20);
            this.urlTxt.TabIndex = 4;
            // 
            // cancelBtn
            // 
            this.cancelBtn.Location = new System.Drawing.Point(207, 64);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(75, 23);
            this.cancelBtn.TabIndex = 6;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // okBtn
            // 
            this.okBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.okBtn.Location = new System.Drawing.Point(288, 64);
            this.okBtn.Name = "okBtn";
            this.okBtn.Size = new System.Drawing.Size(75, 23);
            this.okBtn.TabIndex = 7;
            this.okBtn.Text = "Ok";
            this.okBtn.UseVisualStyleBackColor = false;
            this.okBtn.Click += new System.EventHandler(this.okBtn_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "Userscript Files (*.user.js)|*.user.js|JavaScript Files (*.js)|*.js|All files (*." +
    "*)|*.*";
            this.openFileDialog1.Title = "Select a userscript to install";
            // 
            // newBtn
            // 
            this.newBtn.Location = new System.Drawing.Point(126, 64);
            this.newBtn.Name = "newBtn";
            this.newBtn.Size = new System.Drawing.Size(75, 23);
            this.newBtn.TabIndex = 5;
            this.newBtn.Text = "New File";
            this.newBtn.UseVisualStyleBackColor = true;
            this.newBtn.Click += new System.EventHandler(this.newBtn_Click);
            // 
            // AddScriptFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(376, 96);
            this.Controls.Add(this.newBtn);
            this.Controls.Add(this.okBtn);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.urlTxt);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.browseBtn);
            this.Controls.Add(this.fileTxt);
            this.Controls.Add(this.radioButton1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddScriptFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add Script";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private RadioButton radioButton1;
        private TextBox fileTxt;
        private Button browseBtn;
        private RadioButton radioButton2;
        private TextBox urlTxt;
        private Button cancelBtn;
        private Button okBtn;
        public OpenFileDialog openFileDialog1;
        private Button newBtn;
    }
}