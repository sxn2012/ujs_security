namespace MagazineOrdering
{
    partial class Login_Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login_Form));
            this.usr = new System.Windows.Forms.TextBox();
            this.pwd = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_login = new System.Windows.Forms.Button();
            this.btn_exit = new System.Windows.Forms.Button();
            this.btn_Register = new System.Windows.Forms.Button();
            this.btn_Findpass = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.VerifyCode = new System.Windows.Forms.TextBox();
            this.pbVerifyCode = new System.Windows.Forms.PictureBox();
            this.VerifyStatus = new System.Windows.Forms.PictureBox();
            this.StatusList = new System.Windows.Forms.ImageList(this.components);
            this.UsrImg = new System.Windows.Forms.PictureBox();
            this.PassImg = new System.Windows.Forms.PictureBox();
            this.LeftSideList = new System.Windows.Forms.ImageList(this.components);
            this.VerifyImg = new System.Windows.Forms.PictureBox();
            this.TitileImg = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbVerifyCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.VerifyStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UsrImg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PassImg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.VerifyImg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TitileImg)).BeginInit();
            this.SuspendLayout();
            // 
            // usr
            // 
            this.usr.Location = new System.Drawing.Point(126, 112);
            this.usr.Name = "usr";
            this.usr.Size = new System.Drawing.Size(204, 21);
            this.usr.TabIndex = 0;
            // 
            // pwd
            // 
            this.pwd.Location = new System.Drawing.Point(126, 166);
            this.pwd.Name = "pwd";
            this.pwd.PasswordChar = '*';
            this.pwd.Size = new System.Drawing.Size(204, 21);
            this.pwd.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("STZhongsong", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(113, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(250, 23);
            this.label1.TabIndex = 2;
            this.label1.Text = "报刊管理系统——用户登录";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(67, 115);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "用户名：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(67, 169);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "密码：";
            // 
            // btn_login
            // 
            this.btn_login.Location = new System.Drawing.Point(126, 280);
            this.btn_login.Name = "btn_login";
            this.btn_login.Size = new System.Drawing.Size(75, 23);
            this.btn_login.TabIndex = 5;
            this.btn_login.Text = "登陆";
            this.btn_login.UseVisualStyleBackColor = true;
            this.btn_login.Click += new System.EventHandler(this.btn_login_Click);
            // 
            // btn_exit
            // 
            this.btn_exit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_exit.Location = new System.Drawing.Point(237, 280);
            this.btn_exit.Name = "btn_exit";
            this.btn_exit.Size = new System.Drawing.Size(75, 23);
            this.btn_exit.TabIndex = 6;
            this.btn_exit.Text = "退出";
            this.btn_exit.UseVisualStyleBackColor = true;
            this.btn_exit.Click += new System.EventHandler(this.btn_exit_Click);
            // 
            // btn_Register
            // 
            this.btn_Register.Location = new System.Drawing.Point(336, 110);
            this.btn_Register.Name = "btn_Register";
            this.btn_Register.Size = new System.Drawing.Size(67, 23);
            this.btn_Register.TabIndex = 7;
            this.btn_Register.Text = "注册";
            this.btn_Register.UseVisualStyleBackColor = true;
            this.btn_Register.Click += new System.EventHandler(this.btn_Register_Click);
            // 
            // btn_Findpass
            // 
            this.btn_Findpass.Location = new System.Drawing.Point(336, 164);
            this.btn_Findpass.Name = "btn_Findpass";
            this.btn_Findpass.Size = new System.Drawing.Size(67, 23);
            this.btn_Findpass.TabIndex = 8;
            this.btn_Findpass.Text = "找回密码";
            this.btn_Findpass.UseVisualStyleBackColor = true;
            this.btn_Findpass.Click += new System.EventHandler(this.btn_Findpass_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(67, 215);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 9;
            this.label4.Text = "验证码：";
            // 
            // VerifyCode
            // 
            this.VerifyCode.Location = new System.Drawing.Point(126, 212);
            this.VerifyCode.Name = "VerifyCode";
            this.VerifyCode.Size = new System.Drawing.Size(123, 21);
            this.VerifyCode.TabIndex = 10;
            this.VerifyCode.TextChanged += new System.EventHandler(this.VerifyCode_TextChanged);
            // 
            // pbVerifyCode
            // 
            this.pbVerifyCode.Location = new System.Drawing.Point(266, 203);
            this.pbVerifyCode.Name = "pbVerifyCode";
            this.pbVerifyCode.Size = new System.Drawing.Size(97, 39);
            this.pbVerifyCode.TabIndex = 11;
            this.pbVerifyCode.TabStop = false;
            this.pbVerifyCode.Click += new System.EventHandler(this.pbVerifyCode_Click);
            // 
            // VerifyStatus
            // 
            this.VerifyStatus.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.VerifyStatus.Location = new System.Drawing.Point(360, 212);
            this.VerifyStatus.Name = "VerifyStatus";
            this.VerifyStatus.Size = new System.Drawing.Size(23, 23);
            this.VerifyStatus.TabIndex = 12;
            this.VerifyStatus.TabStop = false;
            // 
            // StatusList
            // 
            this.StatusList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("StatusList.ImageStream")));
            this.StatusList.TransparentColor = System.Drawing.Color.Transparent;
            this.StatusList.Images.SetKeyName(0, "sign-check-icon.png");
            this.StatusList.Images.SetKeyName(1, "sign-error-icon.png");
            // 
            // UsrImg
            // 
            this.UsrImg.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.UsrImg.Location = new System.Drawing.Point(38, 110);
            this.UsrImg.Name = "UsrImg";
            this.UsrImg.Size = new System.Drawing.Size(23, 23);
            this.UsrImg.TabIndex = 13;
            this.UsrImg.TabStop = false;
            // 
            // PassImg
            // 
            this.PassImg.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.PassImg.Location = new System.Drawing.Point(38, 158);
            this.PassImg.Name = "PassImg";
            this.PassImg.Size = new System.Drawing.Size(23, 23);
            this.PassImg.TabIndex = 14;
            this.PassImg.TabStop = false;
            // 
            // LeftSideList
            // 
            this.LeftSideList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("LeftSideList.ImageStream")));
            this.LeftSideList.TransparentColor = System.Drawing.Color.Transparent;
            this.LeftSideList.Images.SetKeyName(0, "name.ico");
            this.LeftSideList.Images.SetKeyName(1, "pass.ico");
            this.LeftSideList.Images.SetKeyName(2, "verify.png");
            this.LeftSideList.Images.SetKeyName(3, "title.png");
            // 
            // VerifyImg
            // 
            this.VerifyImg.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.VerifyImg.Location = new System.Drawing.Point(29, 205);
            this.VerifyImg.Name = "VerifyImg";
            this.VerifyImg.Size = new System.Drawing.Size(32, 30);
            this.VerifyImg.TabIndex = 15;
            this.VerifyImg.TabStop = false;
            // 
            // TitileImg
            // 
            this.TitileImg.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.TitileImg.Location = new System.Drawing.Point(77, 49);
            this.TitileImg.Name = "TitileImg";
            this.TitileImg.Size = new System.Drawing.Size(31, 23);
            this.TitileImg.TabIndex = 16;
            this.TitileImg.TabStop = false;
            // 
            // Login_Form
            // 
            this.AcceptButton = this.btn_login;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_exit;
            this.ClientSize = new System.Drawing.Size(484, 359);
            this.Controls.Add(this.TitileImg);
            this.Controls.Add(this.VerifyImg);
            this.Controls.Add(this.PassImg);
            this.Controls.Add(this.UsrImg);
            this.Controls.Add(this.VerifyStatus);
            this.Controls.Add(this.pbVerifyCode);
            this.Controls.Add(this.VerifyCode);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btn_Findpass);
            this.Controls.Add(this.btn_Register);
            this.Controls.Add(this.btn_exit);
            this.Controls.Add(this.btn_login);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pwd);
            this.Controls.Add(this.usr);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Login_Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "用户登录";
            ((System.ComponentModel.ISupportInitialize)(this.pbVerifyCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.VerifyStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UsrImg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PassImg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.VerifyImg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TitileImg)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox usr;
        private System.Windows.Forms.TextBox pwd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_login;
        private System.Windows.Forms.Button btn_exit;
        private System.Windows.Forms.Button btn_Register;
        private System.Windows.Forms.Button btn_Findpass;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox VerifyCode;
        private System.Windows.Forms.PictureBox pbVerifyCode;
        private System.Windows.Forms.PictureBox VerifyStatus;
        private System.Windows.Forms.ImageList StatusList;
        private System.Windows.Forms.PictureBox UsrImg;
        private System.Windows.Forms.PictureBox PassImg;
        private System.Windows.Forms.ImageList LeftSideList;
        private System.Windows.Forms.PictureBox VerifyImg;
        private System.Windows.Forms.PictureBox TitileImg;
    }
}

