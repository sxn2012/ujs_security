namespace FileManagement_Admin
{
    partial class CreateAccess
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateAccess));
            this.privilege = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.admit_btn = new System.Windows.Forms.Button();
            this.subname = new System.Windows.Forms.ComboBox();
            this.deptname = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // privilege
            // 
            this.privilege.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.privilege.Font = new System.Drawing.Font("NSimSun", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.privilege.FormattingEnabled = true;
            this.privilege.Location = new System.Drawing.Point(85, 107);
            this.privilege.Name = "privilege";
            this.privilege.Size = new System.Drawing.Size(172, 22);
            this.privilege.TabIndex = 32;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("NSimSun", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(14, 110);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 14);
            this.label3.TabIndex = 31;
            this.label3.Text = "权限：";
            // 
            // admit_btn
            // 
            this.admit_btn.Font = new System.Drawing.Font("NSimSun", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.admit_btn.Location = new System.Drawing.Point(110, 150);
            this.admit_btn.Name = "admit_btn";
            this.admit_btn.Size = new System.Drawing.Size(75, 23);
            this.admit_btn.TabIndex = 30;
            this.admit_btn.Text = "确定";
            this.admit_btn.UseVisualStyleBackColor = true;
            this.admit_btn.Click += new System.EventHandler(this.admit_btn_Click);
            // 
            // subname
            // 
            this.subname.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.subname.Font = new System.Drawing.Font("NSimSun", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.subname.FormattingEnabled = true;
            this.subname.Location = new System.Drawing.Point(85, 66);
            this.subname.Name = "subname";
            this.subname.Size = new System.Drawing.Size(172, 22);
            this.subname.TabIndex = 33;
            // 
            // deptname
            // 
            this.deptname.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.deptname.Font = new System.Drawing.Font("NSimSun", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.deptname.FormattingEnabled = true;
            this.deptname.Location = new System.Drawing.Point(85, 25);
            this.deptname.Name = "deptname";
            this.deptname.Size = new System.Drawing.Size(172, 22);
            this.deptname.TabIndex = 34;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("NSimSun", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(-3, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 14);
            this.label1.TabIndex = 35;
            this.label1.Text = "主题名称：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("NSimSun", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(-3, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 14);
            this.label2.TabIndex = 36;
            this.label2.Text = "部门名称：";
            // 
            // CreateAccess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 206);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.deptname);
            this.Controls.Add(this.subname);
            this.Controls.Add(this.privilege);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.admit_btn);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CreateAccess";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "授予新权限";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox privilege;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button admit_btn;
        private System.Windows.Forms.ComboBox subname;
        private System.Windows.Forms.ComboBox deptname;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}