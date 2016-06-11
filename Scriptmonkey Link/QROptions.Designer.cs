namespace Scriptmonkey_Link
{
    partial class QROptions
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
            this.borderChk = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.widthNum = new System.Windows.Forms.NumericUpDown();
            this.sizeNum = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.foreBtn = new System.Windows.Forms.Button();
            this.backBtn = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.borderBtn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.widthNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sizeNum)).BeginInit();
            this.SuspendLayout();
            // 
            // borderChk
            // 
            this.borderChk.AutoSize = true;
            this.borderChk.Location = new System.Drawing.Point(19, 41);
            this.borderChk.Name = "borderChk";
            this.borderChk.Size = new System.Drawing.Size(87, 17);
            this.borderChk.TabIndex = 0;
            this.borderChk.Text = "Show Border";
            this.borderChk.UseVisualStyleBackColor = true;
            this.borderChk.CheckedChanged += new System.EventHandler(this.borderChk_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(112, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Width:";
            // 
            // widthNum
            // 
            this.widthNum.Location = new System.Drawing.Point(156, 40);
            this.widthNum.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.widthNum.Name = "widthNum";
            this.widthNum.Size = new System.Drawing.Size(76, 20);
            this.widthNum.TabIndex = 2;
            this.widthNum.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // sizeNum
            // 
            this.sizeNum.Location = new System.Drawing.Point(52, 12);
            this.sizeNum.Maximum = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            this.sizeNum.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.sizeNum.Name = "sizeNum";
            this.sizeNum.Size = new System.Drawing.Size(76, 20);
            this.sizeNum.TabIndex = 4;
            this.sizeNum.Value = new decimal(new int[] {
            64,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Size:";
            // 
            // foreBtn
            // 
            this.foreBtn.Location = new System.Drawing.Point(19, 66);
            this.foreBtn.Name = "foreBtn";
            this.foreBtn.Size = new System.Drawing.Size(109, 23);
            this.foreBtn.TabIndex = 5;
            this.foreBtn.Text = "Foreground Colour";
            this.foreBtn.UseVisualStyleBackColor = true;
            this.foreBtn.Click += new System.EventHandler(this.foreBtn_Click);
            // 
            // backBtn
            // 
            this.backBtn.Location = new System.Drawing.Point(134, 66);
            this.backBtn.Name = "backBtn";
            this.backBtn.Size = new System.Drawing.Size(108, 23);
            this.backBtn.TabIndex = 6;
            this.backBtn.Text = "Background Colour";
            this.backBtn.UseVisualStyleBackColor = true;
            this.backBtn.Click += new System.EventHandler(this.foreBtn_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(267, 95);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 7;
            this.button3.Text = "Close";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // borderBtn
            // 
            this.borderBtn.Location = new System.Drawing.Point(248, 66);
            this.borderBtn.Name = "borderBtn";
            this.borderBtn.Size = new System.Drawing.Size(94, 23);
            this.borderBtn.TabIndex = 8;
            this.borderBtn.Text = "Border Colour";
            this.borderBtn.UseVisualStyleBackColor = true;
            this.borderBtn.Click += new System.EventHandler(this.foreBtn_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 95);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(248, 26);
            this.label3.TabIndex = 9;
            this.label3.Text = "Warning: Verify the QR Code and still be read after \r\nchanging these settings.";
            // 
            // QROptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(355, 129);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.borderBtn);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.backBtn);
            this.Controls.Add(this.foreBtn);
            this.Controls.Add(this.sizeNum);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.widthNum);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.borderChk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "QROptions";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Options";
            ((System.ComponentModel.ISupportInitialize)(this.widthNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sizeNum)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.NumericUpDown widthNum;
        public System.Windows.Forms.NumericUpDown sizeNum;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.Button foreBtn;
        public System.Windows.Forms.Button backBtn;
        private System.Windows.Forms.Button button3;
        public System.Windows.Forms.CheckBox borderChk;
        private System.Windows.Forms.ColorDialog colorDialog1;
        public System.Windows.Forms.Button borderBtn;
        private System.Windows.Forms.Label label3;
    }
}