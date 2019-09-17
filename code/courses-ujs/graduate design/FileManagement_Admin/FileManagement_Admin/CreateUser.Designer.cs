namespace FileManagement_Admin
{
    partial class CreateUser
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateUser));
            this.admit_btn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.usrname = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.passwdadmit = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.passwd = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.DeptChoose = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // admit_btn
            // 
            this.admit_btn.Font = new System.Drawing.Font("NSimSun", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.admit_btn.Location = new System.Drawing.Point(117, 187);
            this.admit_btn.Name = "admit_btn";
            this.admit_btn.Size = new System.Drawing.Size(75, 23);
            this.admit_btn.TabIndex = 17;
            this.admit_btn.Text = "确定";
            this.admit_btn.UseVisualStyleBackColor = true;
            this.admit_btn.Click += new System.EventHandler(this.admit_btn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("NSimSun", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(10, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 14);
            this.label1.TabIndex = 16;
            this.label1.Text = "用户名：";
            // 
            // usrname
            // 
            this.usrname.Font = new System.Drawing.Font("NSimSun", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.usrname.Location = new System.Drawing.Point(81, 23);
            this.usrname.Name = "usrname";
            this.usrname.Size = new System.Drawing.Size(172, 23);
            this.usrname.TabIndex = 15;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("NSimSun", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(-5, 102);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 14);
            this.label2.TabIndex = 21;
            this.label2.Text = "确认密码：";
            // 
            // passwdadmit
            // 
            this.passwdadmit.Font = new System.Drawing.Font("NSimSun", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.passwdadmit.Location = new System.Drawing.Point(81, 101);
            this.passwdadmit.Name = "passwdadmit";
            this.passwdadmit.PasswordChar = '*';
            this.passwdadmit.Size = new System.Drawing.Size(172, 23);
            this.passwdadmit.TabIndex = 20;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("NSimSun", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(10, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 14);
            this.label3.TabIndex = 19;
            this.label3.Text = "新密码：";
            // 
            // passwd
            // 
            this.passwd.Font = new System.Drawing.Font("NSimSun", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.passwd.Location = new System.Drawing.Point(81, 60);
            this.passwd.Name = "passwd";
            this.passwd.PasswordChar = '*';
            this.passwd.Size = new System.Drawing.Size(172, 23);
            this.passwd.TabIndex = 18;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("NSimSun", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(22, 148);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 14);
            this.label4.TabIndex = 23;
            this.label4.Text = "部门：";
            // 
            // DeptChoose
            // 
            this.DeptChoose.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DeptChoose.Font = new System.Drawing.Font("NSimSun", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.DeptChoose.FormattingEnabled = true;
            this.DeptChoose.Location = new System.Drawing.Point(81, 145);
            this.DeptChoose.Name = "DeptChoose";
            this.DeptChoose.Size = new System.Drawing.Size(172, 22);
            this.DeptChoose.TabIndex = 22;
            // 
            // CreateUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 222);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.DeptChoose);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.passwdadmit);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.passwd);
            this.Controls.Add(this.admit_btn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.usrname);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CreateUser";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "新建用户";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button admit_btn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox usrname;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox passwdadmit;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox passwd;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox DeptChoose;
    }
}