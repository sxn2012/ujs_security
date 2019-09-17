namespace FileManagement
{
    partial class LoginForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.usrname = new System.Windows.Forms.TextBox();
            this.passwd = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.login_btn = new System.Windows.Forms.Button();
            this.FindPwd = new System.Windows.Forms.LinkLabel();
            this.RegNewUsr = new System.Windows.Forms.LinkLabel();
            this.label5 = new System.Windows.Forms.Label();
            this.pbVerifyCode = new System.Windows.Forms.PictureBox();
            this.VerifyCode = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbVerifyCode)).BeginInit();
            this.SuspendLayout();
            // 
            // usrname
            // 
            this.usrname.Font = new System.Drawing.Font("NSimSun", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.usrname.Location = new System.Drawing.Point(135, 52);
            this.usrname.Name = "usrname";
            this.usrname.Size = new System.Drawing.Size(172, 23);
            this.usrname.TabIndex = 0;
            // 
            // passwd
            // 
            this.passwd.Font = new System.Drawing.Font("NSimSun", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.passwd.Location = new System.Drawing.Point(135, 100);
            this.passwd.Name = "passwd";
            this.passwd.PasswordChar = '*';
            this.passwd.Size = new System.Drawing.Size(172, 23);
            this.passwd.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("NSimSun", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(64, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 14);
            this.label1.TabIndex = 3;
            this.label1.Text = "用户名：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("NSimSun", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(64, 103);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 14);
            this.label2.TabIndex = 4;
            this.label2.Text = "密码：";
            // 
            // login_btn
            // 
            this.login_btn.BackColor = System.Drawing.Color.Transparent;
            this.login_btn.Font = new System.Drawing.Font("NSimSun", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.login_btn.Location = new System.Drawing.Point(178, 237);
            this.login_btn.Name = "login_btn";
            this.login_btn.Size = new System.Drawing.Size(75, 23);
            this.login_btn.TabIndex = 6;
            this.login_btn.Text = "登陆";
            this.login_btn.UseVisualStyleBackColor = false;
            this.login_btn.Click += new System.EventHandler(this.login_btn_Click);
            // 
            // FindPwd
            // 
            this.FindPwd.AutoSize = true;
            this.FindPwd.BackColor = System.Drawing.Color.Transparent;
            this.FindPwd.Font = new System.Drawing.Font("KaiTi", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FindPwd.Location = new System.Drawing.Point(313, 103);
            this.FindPwd.Name = "FindPwd";
            this.FindPwd.Size = new System.Drawing.Size(67, 14);
            this.FindPwd.TabIndex = 8;
            this.FindPwd.TabStop = true;
            this.FindPwd.Text = "找回密码";
            this.FindPwd.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.FindPwd_LinkClicked);
            // 
            // RegNewUsr
            // 
            this.RegNewUsr.AutoSize = true;
            this.RegNewUsr.BackColor = System.Drawing.Color.Transparent;
            this.RegNewUsr.Font = new System.Drawing.Font("KaiTi", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.RegNewUsr.Location = new System.Drawing.Point(313, 55);
            this.RegNewUsr.Name = "RegNewUsr";
            this.RegNewUsr.Size = new System.Drawing.Size(82, 14);
            this.RegNewUsr.TabIndex = 7;
            this.RegNewUsr.TabStop = true;
            this.RegNewUsr.Text = "注册新用户";
            this.RegNewUsr.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.RegNewUsr_LinkClicked);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("SimHei", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(261, 128);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(140, 24);
            this.label5.TabIndex = 20;
            this.label5.Text = "Loading...";
            // 
            // pbVerifyCode
            // 
            this.pbVerifyCode.BackColor = System.Drawing.Color.Transparent;
            this.pbVerifyCode.Location = new System.Drawing.Point(135, 128);
            this.pbVerifyCode.Name = "pbVerifyCode";
            this.pbVerifyCode.Size = new System.Drawing.Size(172, 29);
            this.pbVerifyCode.TabIndex = 19;
            this.pbVerifyCode.TabStop = false;
            this.pbVerifyCode.Click += new System.EventHandler(this.pbVerifyCode_Click);
            // 
            // VerifyCode
            // 
            this.VerifyCode.Font = new System.Drawing.Font("NSimSun", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.VerifyCode.Location = new System.Drawing.Point(135, 163);
            this.VerifyCode.Name = "VerifyCode";
            this.VerifyCode.Size = new System.Drawing.Size(172, 23);
            this.VerifyCode.TabIndex = 18;
            this.VerifyCode.TextChanged += new System.EventHandler(this.VerifyCode_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("NSimSun", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(64, 166);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 14);
            this.label4.TabIndex = 17;
            this.label4.Text = "验证码：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft YaHei", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(103, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(232, 27);
            this.label3.TabIndex = 21;
            this.label3.Text = "局域网电子文档管理平台";
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(439, 300);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.pbVerifyCode);
            this.Controls.Add(this.VerifyCode);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.FindPwd);
            this.Controls.Add(this.RegNewUsr);
            this.Controls.Add(this.login_btn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.passwd);
            this.Controls.Add(this.usrname);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "用户登录";
            ((System.ComponentModel.ISupportInitialize)(this.pbVerifyCode)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox usrname;
        private System.Windows.Forms.TextBox passwd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button login_btn;
        private System.Windows.Forms.LinkLabel FindPwd;
        private System.Windows.Forms.LinkLabel RegNewUsr;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.PictureBox pbVerifyCode;
        private System.Windows.Forms.TextBox VerifyCode;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
    }
}

