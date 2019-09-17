namespace FileManagement_Admin
{
    partial class CreateSub
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateSub));
            this.label2 = new System.Windows.Forms.Label();
            this.subname = new System.Windows.Forms.TextBox();
            this.admit_btn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.sub_con = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("NSimSun", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(1, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 14);
            this.label2.TabIndex = 27;
            this.label2.Text = "主题名称：";
            // 
            // subname
            // 
            this.subname.Font = new System.Drawing.Font("NSimSun", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.subname.Location = new System.Drawing.Point(89, 25);
            this.subname.Name = "subname";
            this.subname.Size = new System.Drawing.Size(172, 23);
            this.subname.TabIndex = 26;
            // 
            // admit_btn
            // 
            this.admit_btn.Font = new System.Drawing.Font("NSimSun", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.admit_btn.Location = new System.Drawing.Point(108, 105);
            this.admit_btn.Name = "admit_btn";
            this.admit_btn.Size = new System.Drawing.Size(75, 23);
            this.admit_btn.TabIndex = 25;
            this.admit_btn.Text = "确定";
            this.admit_btn.UseVisualStyleBackColor = true;
            this.admit_btn.Click += new System.EventHandler(this.admit_btn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("NSimSun", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(1, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 14);
            this.label1.TabIndex = 24;
            this.label1.Text = "主题内容：";
            // 
            // sub_con
            // 
            this.sub_con.Font = new System.Drawing.Font("NSimSun", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.sub_con.Location = new System.Drawing.Point(89, 62);
            this.sub_con.Name = "sub_con";
            this.sub_con.Size = new System.Drawing.Size(172, 23);
            this.sub_con.TabIndex = 23;
            // 
            // CreateSub
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 146);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.subname);
            this.Controls.Add(this.admit_btn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.sub_con);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CreateSub";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "新建主题";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox subname;
        private System.Windows.Forms.Button admit_btn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox sub_con;
    }
}