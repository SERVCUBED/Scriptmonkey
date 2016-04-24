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
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.instanceNumTimer = new System.Windows.Forms.Timer(this.components);
            this.purgeTimer = new System.Windows.Forms.Timer(this.components);
            this.numInstancesLabel = new System.Windows.Forms.Label();
            this.broadcastBtn = new System.Windows.Forms.Button();
            this.txtBroadcast = new System.Windows.Forms.TextBox();
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
            this.broadcastBtn.Location = new System.Drawing.Point(204, 54);
            this.broadcastBtn.Name = "broadcastBtn";
            this.broadcastBtn.Size = new System.Drawing.Size(75, 23);
            this.broadcastBtn.TabIndex = 2;
            this.broadcastBtn.Text = "Broadcast";
            this.broadcastBtn.UseVisualStyleBackColor = true;
            this.broadcastBtn.Click += new System.EventHandler(this.broadcastBtn_Click);
            // 
            // txtBroadcast
            // 
            this.txtBroadcast.Location = new System.Drawing.Point(13, 56);
            this.txtBroadcast.Name = "txtBroadcast";
            this.txtBroadcast.Size = new System.Drawing.Size(185, 20);
            this.txtBroadcast.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(291, 89);
            this.Controls.Add(this.txtBroadcast);
            this.Controls.Add(this.broadcastBtn);
            this.Controls.Add(this.numInstancesLabel);
            this.Controls.Add(this.checkBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.Text = "Scriptmonkey Link";
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
    }
}

