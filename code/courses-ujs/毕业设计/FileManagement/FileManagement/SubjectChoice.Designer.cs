namespace FileManagement
{
    partial class SubjectChoice
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
            this.SubjectChoose = new System.Windows.Forms.ComboBox();
            this.btn_admit = new System.Windows.Forms.Button();
            this.OpenFileDlg = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // SubjectChoose
            // 
            this.SubjectChoose.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SubjectChoose.Font = new System.Drawing.Font("NSimSun", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.SubjectChoose.FormattingEnabled = true;
            this.SubjectChoose.Location = new System.Drawing.Point(75, 26);
            this.SubjectChoose.Name = "SubjectChoose";
            this.SubjectChoose.Size = new System.Drawing.Size(121, 22);
            this.SubjectChoose.TabIndex = 6;
            // 
            // btn_admit
            // 
            this.btn_admit.Font = new System.Drawing.Font("NSimSun", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_admit.Location = new System.Drawing.Point(99, 78);
            this.btn_admit.Name = "btn_admit";
            this.btn_admit.Size = new System.Drawing.Size(75, 23);
            this.btn_admit.TabIndex = 7;
            this.btn_admit.Text = "确认";
            this.btn_admit.UseVisualStyleBackColor = true;
            this.btn_admit.Click += new System.EventHandler(this.btn_admit_Click);
            // 
            // OpenFileDlg
            // 
            this.OpenFileDlg.FileOk += new System.ComponentModel.CancelEventHandler(this.OpenFileDlg_FileOk);
            // 
            // SubjectChoice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 130);
            this.ControlBox = false;
            this.Controls.Add(this.btn_admit);
            this.Controls.Add(this.SubjectChoose);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SubjectChoice";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "选择上传文档的主题";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox SubjectChoose;
        private System.Windows.Forms.Button btn_admit;
        private System.Windows.Forms.OpenFileDialog OpenFileDlg;
    }
}