namespace MagazineOrdering
{
    partial class Regis
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Regis));
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pwd = new System.Windows.Forms.TextBox();
            this.usr = new System.Windows.Forms.TextBox();
            this.btn_admit = new System.Windows.Forms.Button();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.loc = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 105);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "密码：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "用户名：";
            // 
            // pwd
            // 
            this.pwd.Location = new System.Drawing.Point(81, 102);
            this.pwd.Name = "pwd";
            this.pwd.PasswordChar = '*';
            this.pwd.Size = new System.Drawing.Size(224, 21);
            this.pwd.TabIndex = 6;
            // 
            // usr
            // 
            this.usr.Location = new System.Drawing.Point(81, 48);
            this.usr.Name = "usr";
            this.usr.Size = new System.Drawing.Size(224, 21);
            this.usr.TabIndex = 5;
            // 
            // btn_admit
            // 
            this.btn_admit.Location = new System.Drawing.Point(81, 213);
            this.btn_admit.Name = "btn_admit";
            this.btn_admit.Size = new System.Drawing.Size(75, 23);
            this.btn_admit.TabIndex = 9;
            this.btn_admit.Text = "提交";
            this.btn_admit.UseVisualStyleBackColor = true;
            this.btn_admit.Click += new System.EventHandler(this.btn_admit_Click);
            // 
            // btn_cancel
            // 
            this.btn_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_cancel.Location = new System.Drawing.Point(205, 213);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_cancel.TabIndex = 10;
            this.btn_cancel.Text = "取消";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 157);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 12;
            this.label1.Text = "地址：";
            // 
            // loc
            // 
            this.loc.Location = new System.Drawing.Point(81, 154);
            this.loc.Name = "loc";
            this.loc.Size = new System.Drawing.Size(224, 21);
            this.loc.TabIndex = 11;
            // 
            // Regis
            // 
            this.AcceptButton = this.btn_admit;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_cancel;
            this.ClientSize = new System.Drawing.Size(367, 272);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.loc);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_admit);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pwd);
            this.Controls.Add(this.usr);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Regis";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "注册用户";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox pwd;
        private System.Windows.Forms.TextBox usr;
        private System.Windows.Forms.Button btn_admit;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox loc;
    }
}