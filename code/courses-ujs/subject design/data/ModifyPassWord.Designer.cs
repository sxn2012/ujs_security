namespace MagazineOrdering
{
    partial class ModifyPassWord
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
            this.label2 = new System.Windows.Forms.Label();
            this.usr = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pass = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.newpass = new System.Windows.Forms.TextBox();
            this.btn_exit = new System.Windows.Forms.Button();
            this.btn_admit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(71, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "用户名：";
            // 
            // usr
            // 
            this.usr.Location = new System.Drawing.Point(130, 49);
            this.usr.Name = "usr";
            this.usr.Size = new System.Drawing.Size(204, 21);
            this.usr.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(71, 102);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 12;
            this.label1.Text = "原密码：";
            // 
            // pass
            // 
            this.pass.Location = new System.Drawing.Point(130, 99);
            this.pass.Name = "pass";
            this.pass.PasswordChar = '*';
            this.pass.Size = new System.Drawing.Size(204, 21);
            this.pass.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(71, 152);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 14;
            this.label3.Text = "新密码：";
            // 
            // newpass
            // 
            this.newpass.Location = new System.Drawing.Point(130, 149);
            this.newpass.Name = "newpass";
            this.newpass.PasswordChar = '*';
            this.newpass.Size = new System.Drawing.Size(204, 21);
            this.newpass.TabIndex = 13;
            // 
            // btn_exit
            // 
            this.btn_exit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_exit.Location = new System.Drawing.Point(221, 199);
            this.btn_exit.Name = "btn_exit";
            this.btn_exit.Size = new System.Drawing.Size(75, 23);
            this.btn_exit.TabIndex = 16;
            this.btn_exit.Text = "取消";
            this.btn_exit.UseVisualStyleBackColor = true;
            this.btn_exit.Click += new System.EventHandler(this.btn_exit_Click);
            // 
            // btn_admit
            // 
            this.btn_admit.Location = new System.Drawing.Point(110, 199);
            this.btn_admit.Name = "btn_admit";
            this.btn_admit.Size = new System.Drawing.Size(75, 23);
            this.btn_admit.TabIndex = 15;
            this.btn_admit.Text = "确认";
            this.btn_admit.UseVisualStyleBackColor = true;
            this.btn_admit.Click += new System.EventHandler(this.btn_admit_Click);
            // 
            // ModifyPassWord
            // 
            this.AcceptButton = this.btn_admit;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_exit;
            this.ClientSize = new System.Drawing.Size(433, 250);
            this.Controls.Add(this.btn_exit);
            this.Controls.Add(this.btn_admit);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.newpass);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pass);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.usr);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ModifyPassWord";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "修改管理员账户密码";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ModifyPassWord_Closing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox usr;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox pass;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox newpass;
        private System.Windows.Forms.Button btn_exit;
        private System.Windows.Forms.Button btn_admit;
    }
}