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
            ((System.ComponentModel.ISupportInitialize)(this.reloadNum)).BeginInit();
            this.SuspendLayout();
            // 
            // saveBtn
            // 
            this.saveBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.saveBtn.Location = new System.Drawing.Point(219, 156);
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
            this.updateChk.Size = new System.Drawing.Size(112, 17);
            this.updateChk.TabIndex = 1;
            this.updateChk.Text = "Check for updates";
            this.updateChk.UseVisualStyleBackColor = true;
            // 
            // refreshChk
            // 
            this.refreshChk.AutoSize = true;
            this.refreshChk.Location = new System.Drawing.Point(13, 37);
            this.refreshChk.Name = "refreshChk";
            this.refreshChk.Size = new System.Drawing.Size(281, 17);
            this.refreshChk.TabIndex = 2;
            this.refreshChk.Text = "Run on page refresh (experimental, not recommended)";
            this.refreshChk.UseVisualStyleBackColor = true;
            // 
            // autoChk
            // 
            this.autoChk.AutoSize = true;
            this.autoChk.Location = new System.Drawing.Point(13, 61);
            this.autoChk.Name = "autoChk";
            this.autoChk.Size = new System.Drawing.Size(152, 17);
            this.autoChk.TabIndex = 3;
            this.autoChk.Text = "Detect scripts on webpage";
            this.autoChk.UseVisualStyleBackColor = true;
            // 
            // publicApiChk
            // 
            this.publicApiChk.AutoSize = true;
            this.publicApiChk.Location = new System.Drawing.Point(13, 85);
            this.publicApiChk.Name = "publicApiChk";
            this.publicApiChk.Size = new System.Drawing.Size(178, 17);
            this.publicApiChk.TabIndex = 4;
            this.publicApiChk.Text = "Use public API (developers only)";
            this.publicApiChk.UseVisualStyleBackColor = true;
            // 
            // cacheChk
            // 
            this.cacheChk.AutoSize = true;
            this.cacheChk.Location = new System.Drawing.Point(13, 109);
            this.cacheChk.Name = "cacheChk";
            this.cacheChk.Size = new System.Drawing.Size(89, 17);
            this.cacheChk.TabIndex = 5;
            this.cacheChk.Text = "Cache scripts";
            this.cacheChk.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 133);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Reload settings after";
            // 
            // reloadNum
            // 
            this.reloadNum.Location = new System.Drawing.Point(122, 131);
            this.reloadNum.Name = "reloadNum";
            this.reloadNum.Size = new System.Drawing.Size(57, 20);
            this.reloadNum.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(185, 133);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "pages";
            // 
            // AdvancedOptionsFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(306, 186);
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
            this.TopMost = true;
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
    }
}