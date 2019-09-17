namespace FileManagement_Admin
{
    partial class BackupandRecover
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BackupandRecover));
            this.backup_btn = new System.Windows.Forms.Button();
            this.recover_btn = new System.Windows.Forms.Button();
            this.OpenFileDlg = new System.Windows.Forms.OpenFileDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // backup_btn
            // 
            this.backup_btn.Font = new System.Drawing.Font("NSimSun", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.backup_btn.Location = new System.Drawing.Point(96, 31);
            this.backup_btn.Name = "backup_btn";
            this.backup_btn.Size = new System.Drawing.Size(75, 23);
            this.backup_btn.TabIndex = 14;
            this.backup_btn.Text = "备份";
            this.backup_btn.UseVisualStyleBackColor = true;
            this.backup_btn.Click += new System.EventHandler(this.backup_btn_Click);
            // 
            // recover_btn
            // 
            this.recover_btn.Font = new System.Drawing.Font("NSimSun", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.recover_btn.Location = new System.Drawing.Point(96, 79);
            this.recover_btn.Name = "recover_btn";
            this.recover_btn.Size = new System.Drawing.Size(75, 23);
            this.recover_btn.TabIndex = 15;
            this.recover_btn.Text = "恢复";
            this.recover_btn.UseVisualStyleBackColor = true;
            this.recover_btn.Click += new System.EventHandler(this.recover_btn_Click);
            // 
            // OpenFileDlg
            // 
            this.OpenFileDlg.Multiselect = true;
            this.OpenFileDlg.FileOk += new System.ComponentModel.CancelEventHandler(this.OpenFileDlg_FileOk);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("NSimSun", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(12, 115);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 14);
            this.label1.TabIndex = 16;
            this.label1.Text = "Loading";
            // 
            // BackupandRecover
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 148);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.recover_btn);
            this.Controls.Add(this.backup_btn);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BackupandRecover";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "数据备份与恢复";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button backup_btn;
        private System.Windows.Forms.Button recover_btn;
        private System.Windows.Forms.OpenFileDialog OpenFileDlg;
        private System.Windows.Forms.Label label1;
    }
}