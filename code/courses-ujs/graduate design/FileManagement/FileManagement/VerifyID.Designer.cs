namespace FileManagement
{
    partial class VerifyID
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VerifyID));
            this.btn_email = new System.Windows.Forms.LinkLabel();
            this.EmailVerifyCode = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.pbVerifyCode = new System.Windows.Forms.PictureBox();
            this.VerifyCode = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btn_admit = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbVerifyCode)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_email
            // 
            this.btn_email.AutoSize = true;
            this.btn_email.BackColor = System.Drawing.Color.Transparent;
            this.btn_email.Font = new System.Drawing.Font("NSimSun", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_email.Location = new System.Drawing.Point(119, 153);
            this.btn_email.Name = "btn_email";
            this.btn_email.Size = new System.Drawing.Size(93, 16);
            this.btn_email.TabIndex = 62;
            this.btn_email.TabStop = true;
            this.btn_email.Text = "发送验证码";
            this.btn_email.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btn_email_LinkClicked);
            // 
            // EmailVerifyCode
            // 
            this.EmailVerifyCode.Font = new System.Drawing.Font("NSimSun", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.EmailVerifyCode.Location = new System.Drawing.Point(12, 190);
            this.EmailVerifyCode.Name = "EmailVerifyCode";
            this.EmailVerifyCode.Size = new System.Drawing.Size(250, 23);
            this.EmailVerifyCode.TabIndex = 61;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("NSimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(10, 175);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(155, 12);
            this.label8.TabIndex = 60;
            this.label8.Text = "邮箱验证码/短信验证码：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("SimHei", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(209, 56);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(140, 24);
            this.label6.TabIndex = 59;
            this.label6.Text = "Loading...";
            // 
            // pbVerifyCode
            // 
            this.pbVerifyCode.BackColor = System.Drawing.Color.Transparent;
            this.pbVerifyCode.Location = new System.Drawing.Point(81, 56);
            this.pbVerifyCode.Name = "pbVerifyCode";
            this.pbVerifyCode.Size = new System.Drawing.Size(135, 51);
            this.pbVerifyCode.TabIndex = 58;
            this.pbVerifyCode.TabStop = false;
            this.pbVerifyCode.Click += new System.EventHandler(this.pbVerifyCode_Click);
            // 
            // VerifyCode
            // 
            this.VerifyCode.Font = new System.Drawing.Font("NSimSun", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.VerifyCode.Location = new System.Drawing.Point(81, 120);
            this.VerifyCode.Name = "VerifyCode";
            this.VerifyCode.Size = new System.Drawing.Size(181, 23);
            this.VerifyCode.TabIndex = 57;
            this.VerifyCode.TextChanged += new System.EventHandler(this.VerifyCode_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("NSimSun", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(9, 121);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 14);
            this.label7.TabIndex = 56;
            this.label7.Text = "验证码：";
            // 
            // btn_admit
            // 
            this.btn_admit.Font = new System.Drawing.Font("NSimSun", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_admit.Location = new System.Drawing.Point(120, 217);
            this.btn_admit.Name = "btn_admit";
            this.btn_admit.Size = new System.Drawing.Size(75, 23);
            this.btn_admit.TabIndex = 55;
            this.btn_admit.Text = "确定";
            this.btn_admit.UseVisualStyleBackColor = true;
            this.btn_admit.Click += new System.EventHandler(this.btn_admit_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("NSimSun", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 14);
            this.label1.TabIndex = 53;
            this.label1.Text = "邮箱/手机号:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("STZhongsong", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(37, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 19);
            this.label2.TabIndex = 63;
            this.label2.Text = "label2";
            // 
            // VerifyID
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btn_email);
            this.Controls.Add(this.EmailVerifyCode);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.pbVerifyCode);
            this.Controls.Add(this.VerifyCode);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btn_admit);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VerifyID";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "身份验证";
            ((System.ComponentModel.ISupportInitialize)(this.pbVerifyCode)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel btn_email;
        private System.Windows.Forms.TextBox EmailVerifyCode;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.PictureBox pbVerifyCode;
        private System.Windows.Forms.TextBox VerifyCode;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btn_admit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}