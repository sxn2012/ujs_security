namespace FileManagement_Admin
{
    partial class ModifyDName
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModifyDName));
            this.admit_btn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.depname = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // admit_btn
            // 
            this.admit_btn.Font = new System.Drawing.Font("NSimSun", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.admit_btn.Location = new System.Drawing.Point(111, 88);
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
            this.label1.Location = new System.Drawing.Point(4, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 14);
            this.label1.TabIndex = 16;
            this.label1.Text = "新部门名：";
            // 
            // depname
            // 
            this.depname.Font = new System.Drawing.Font("NSimSun", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.depname.Location = new System.Drawing.Point(92, 45);
            this.depname.Name = "depname";
            this.depname.Size = new System.Drawing.Size(172, 23);
            this.depname.TabIndex = 15;
            // 
            // ModifyDName
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 157);
            this.Controls.Add(this.admit_btn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.depname);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ModifyDName";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "修改部门名称";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button admit_btn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox depname;
    }
}