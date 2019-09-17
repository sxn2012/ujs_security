namespace FileManagement
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.FileView = new System.Windows.Forms.ListView();
            this.VersionView = new System.Windows.Forms.ListView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SaveFileDlg = new System.Windows.Forms.SaveFileDialog();
            this.UpdateFileDlg = new System.Windows.Forms.OpenFileDialog();
            this.SaveVersionDlg = new System.Windows.Forms.SaveFileDialog();
            this.VersionMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ViewVersion = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteVersion = new System.Windows.Forms.ToolStripMenuItem();
            this.FileMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ViewFile = new System.Windows.Forms.ToolStripMenuItem();
            this.ModifyFile = new System.Windows.Forms.ToolStripMenuItem();
            this.StartModifyFile = new System.Windows.Forms.ToolStripMenuItem();
            this.CompleteModifyFile = new System.Windows.Forms.ToolStripMenuItem();
            this.GiveUpModifyFile = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteFile = new System.Windows.Forms.ToolStripMenuItem();
            this.UploadMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.UploadFile = new System.Windows.Forms.ToolStripMenuItem();
            this.progress_updown = new System.Windows.Forms.ProgressBar();
            this.label_updown = new System.Windows.Forms.Label();
            this.label_percent = new System.Windows.Forms.Label();
            this.DocImageList = new System.Windows.Forms.ImageList(this.components);
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.查看ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewFileSummary_LargeIcon = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewFileSummary_SmallIcon = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewFileDetail = new System.Windows.Forms.ToolStripMenuItem();
            this.安全ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AccRollback = new System.Windows.Forms.ToolStripMenuItem();
            this.ModifyPwd = new System.Windows.Forms.ToolStripMenuItem();
            this.LogOut = new System.Windows.Forms.ToolStripMenuItem();
            this.帮助ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Aboutme = new System.Windows.Forms.ToolStripMenuItem();
            this.timelabel = new System.Windows.Forms.Label();
            this.ModifyFileDlg = new System.Windows.Forms.SaveFileDialog();
            this.VersionMenu.SuspendLayout();
            this.FileMenu.SuspendLayout();
            this.UploadMenu.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // FileView
            // 
            this.FileView.Location = new System.Drawing.Point(10, 49);
            this.FileView.Name = "FileView";
            this.FileView.Size = new System.Drawing.Size(378, 310);
            this.FileView.TabIndex = 0;
            this.FileView.UseCompatibleStateImageBehavior = false;
            this.FileView.SelectedIndexChanged += new System.EventHandler(this.FileView_SelectedIndexChanged);
            this.FileView.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FileView_MouseUp);
            // 
            // VersionView
            // 
            this.VersionView.Location = new System.Drawing.Point(402, 49);
            this.VersionView.Name = "VersionView";
            this.VersionView.Size = new System.Drawing.Size(377, 310);
            this.VersionView.TabIndex = 1;
            this.VersionView.UseCompatibleStateImageBehavior = false;
            this.VersionView.MouseUp += new System.Windows.Forms.MouseEventHandler(this.VersionView_MouseUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("NSimSun", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(10, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 14);
            this.label1.TabIndex = 4;
            this.label1.Text = "文档信息：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("NSimSun", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(402, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 14);
            this.label2.TabIndex = 5;
            this.label2.Text = "版本信息：";
            // 
            // SaveFileDlg
            // 
            this.SaveFileDlg.FileOk += new System.ComponentModel.CancelEventHandler(this.SaveFileDlg_FileOk);
            // 
            // UpdateFileDlg
            // 
            this.UpdateFileDlg.FileOk += new System.ComponentModel.CancelEventHandler(this.UpdateFileDlg_FileOk);
            // 
            // SaveVersionDlg
            // 
            this.SaveVersionDlg.FileOk += new System.ComponentModel.CancelEventHandler(this.SaveVersionDlg_FileOk);
            // 
            // VersionMenu
            // 
            this.VersionMenu.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.VersionMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ViewVersion,
            this.DeleteVersion});
            this.VersionMenu.Name = "VersionMenu";
            this.VersionMenu.Size = new System.Drawing.Size(145, 56);
            // 
            // ViewVersion
            // 
            this.ViewVersion.Name = "ViewVersion";
            this.ViewVersion.Size = new System.Drawing.Size(144, 26);
            this.ViewVersion.Text = "浏览版本";
            this.ViewVersion.Click += new System.EventHandler(this.ViewVersion_Click);
            // 
            // DeleteVersion
            // 
            this.DeleteVersion.Name = "DeleteVersion";
            this.DeleteVersion.Size = new System.Drawing.Size(144, 26);
            this.DeleteVersion.Text = "删除版本";
            this.DeleteVersion.Click += new System.EventHandler(this.DeleteVersion_Click);
            // 
            // FileMenu
            // 
            this.FileMenu.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FileMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ViewFile,
            this.ModifyFile,
            this.DeleteFile});
            this.FileMenu.Name = "FileMenu";
            this.FileMenu.Size = new System.Drawing.Size(145, 82);
            // 
            // ViewFile
            // 
            this.ViewFile.Name = "ViewFile";
            this.ViewFile.Size = new System.Drawing.Size(144, 26);
            this.ViewFile.Text = "浏览文档";
            this.ViewFile.Click += new System.EventHandler(this.ViewFile_Click);
            // 
            // ModifyFile
            // 
            this.ModifyFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StartModifyFile,
            this.CompleteModifyFile,
            this.GiveUpModifyFile});
            this.ModifyFile.Name = "ModifyFile";
            this.ModifyFile.Size = new System.Drawing.Size(144, 26);
            this.ModifyFile.Text = "修改文档";
            // 
            // StartModifyFile
            // 
            this.StartModifyFile.Name = "StartModifyFile";
            this.StartModifyFile.Size = new System.Drawing.Size(152, 26);
            this.StartModifyFile.Text = "开始修改";
            this.StartModifyFile.Click += new System.EventHandler(this.StartModifyFile_Click);
            // 
            // CompleteModifyFile
            // 
            this.CompleteModifyFile.Name = "CompleteModifyFile";
            this.CompleteModifyFile.Size = new System.Drawing.Size(152, 26);
            this.CompleteModifyFile.Text = "修改完成";
            this.CompleteModifyFile.Click += new System.EventHandler(this.CompleteModifyFile_Click);
            // 
            // GiveUpModifyFile
            // 
            this.GiveUpModifyFile.Name = "GiveUpModifyFile";
            this.GiveUpModifyFile.Size = new System.Drawing.Size(152, 26);
            this.GiveUpModifyFile.Text = "放弃修改";
            this.GiveUpModifyFile.Click += new System.EventHandler(this.GiveUpModifyFile_Click);
            // 
            // DeleteFile
            // 
            this.DeleteFile.Name = "DeleteFile";
            this.DeleteFile.Size = new System.Drawing.Size(144, 26);
            this.DeleteFile.Text = "删除文档";
            this.DeleteFile.Click += new System.EventHandler(this.DeleteFile_Click);
            // 
            // UploadMenu
            // 
            this.UploadMenu.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.UploadMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.UploadFile});
            this.UploadMenu.Name = "UploadMenu";
            this.UploadMenu.Size = new System.Drawing.Size(145, 30);
            // 
            // UploadFile
            // 
            this.UploadFile.Name = "UploadFile";
            this.UploadFile.Size = new System.Drawing.Size(144, 26);
            this.UploadFile.Text = "上传文档";
            this.UploadFile.Click += new System.EventHandler(this.UploadFile_Click);
            // 
            // progress_updown
            // 
            this.progress_updown.Location = new System.Drawing.Point(12, 384);
            this.progress_updown.Name = "progress_updown";
            this.progress_updown.Size = new System.Drawing.Size(700, 23);
            this.progress_updown.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progress_updown.TabIndex = 6;
            // 
            // label_updown
            // 
            this.label_updown.AutoSize = true;
            this.label_updown.BackColor = System.Drawing.Color.Transparent;
            this.label_updown.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_updown.Location = new System.Drawing.Point(19, 362);
            this.label_updown.Name = "label_updown";
            this.label_updown.Size = new System.Drawing.Size(62, 16);
            this.label_updown.TabIndex = 7;
            this.label_updown.Text = "label3";
            this.label_updown.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_percent
            // 
            this.label_percent.AutoSize = true;
            this.label_percent.BackColor = System.Drawing.Color.Transparent;
            this.label_percent.Font = new System.Drawing.Font("NSimSun", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_percent.Location = new System.Drawing.Point(718, 384);
            this.label_percent.Name = "label_percent";
            this.label_percent.Size = new System.Drawing.Size(61, 15);
            this.label_percent.TabIndex = 8;
            this.label_percent.Text = "label3";
            // 
            // DocImageList
            // 
            this.DocImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.DocImageList.ImageSize = new System.Drawing.Size(16, 16);
            this.DocImageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // menuStrip
            // 
            this.menuStrip.BackColor = System.Drawing.Color.Transparent;
            this.menuStrip.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.查看ToolStripMenuItem,
            this.安全ToolStripMenuItem,
            this.帮助ToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(789, 30);
            this.menuStrip.TabIndex = 14;
            this.menuStrip.Text = "menuStrip";
            // 
            // 查看ToolStripMenuItem
            // 
            this.查看ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ViewFileSummary_LargeIcon,
            this.ViewFileSummary_SmallIcon,
            this.ViewFileDetail});
            this.查看ToolStripMenuItem.Name = "查看ToolStripMenuItem";
            this.查看ToolStripMenuItem.Size = new System.Drawing.Size(54, 26);
            this.查看ToolStripMenuItem.Text = "查看";
            // 
            // ViewFileSummary_LargeIcon
            // 
            this.ViewFileSummary_LargeIcon.Name = "ViewFileSummary_LargeIcon";
            this.ViewFileSummary_LargeIcon.Size = new System.Drawing.Size(214, 26);
            this.ViewFileSummary_LargeIcon.Text = "文档概览(大图标）";
            this.ViewFileSummary_LargeIcon.Click += new System.EventHandler(this.ViewFileSummary_LargeIcon_Click);
            // 
            // ViewFileSummary_SmallIcon
            // 
            this.ViewFileSummary_SmallIcon.Name = "ViewFileSummary_SmallIcon";
            this.ViewFileSummary_SmallIcon.Size = new System.Drawing.Size(214, 26);
            this.ViewFileSummary_SmallIcon.Text = "文档概览(小图标）";
            this.ViewFileSummary_SmallIcon.Click += new System.EventHandler(this.ViewFileSummary_SmallIcon_Click);
            // 
            // ViewFileDetail
            // 
            this.ViewFileDetail.Name = "ViewFileDetail";
            this.ViewFileDetail.Size = new System.Drawing.Size(214, 26);
            this.ViewFileDetail.Text = "文档详细信息";
            this.ViewFileDetail.Click += new System.EventHandler(this.ViewFileDetail_Click);
            // 
            // 安全ToolStripMenuItem
            // 
            this.安全ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AccRollback,
            this.ModifyPwd,
            this.LogOut});
            this.安全ToolStripMenuItem.Name = "安全ToolStripMenuItem";
            this.安全ToolStripMenuItem.Size = new System.Drawing.Size(54, 26);
            this.安全ToolStripMenuItem.Text = "安全";
            // 
            // AccRollback
            // 
            this.AccRollback.Name = "AccRollback";
            this.AccRollback.Size = new System.Drawing.Size(209, 26);
            this.AccRollback.Text = "账号还原";
            this.AccRollback.Click += new System.EventHandler(this.AccRollback_Click);
            // 
            // ModifyPwd
            // 
            this.ModifyPwd.Name = "ModifyPwd";
            this.ModifyPwd.Size = new System.Drawing.Size(209, 26);
            this.ModifyPwd.Text = "修改密码";
            this.ModifyPwd.Click += new System.EventHandler(this.ModifyPwd_Click);
            // 
            // LogOut
            // 
            this.LogOut.Name = "LogOut";
            this.LogOut.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.LogOut.Size = new System.Drawing.Size(209, 26);
            this.LogOut.Text = "退出登录";
            this.LogOut.Click += new System.EventHandler(this.LogOut_Click);
            // 
            // 帮助ToolStripMenuItem
            // 
            this.帮助ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Aboutme});
            this.帮助ToolStripMenuItem.Name = "帮助ToolStripMenuItem";
            this.帮助ToolStripMenuItem.Size = new System.Drawing.Size(54, 26);
            this.帮助ToolStripMenuItem.Text = "帮助";
            // 
            // Aboutme
            // 
            this.Aboutme.Name = "Aboutme";
            this.Aboutme.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F1)));
            this.Aboutme.Size = new System.Drawing.Size(183, 26);
            this.Aboutme.Text = "关于";
            this.Aboutme.Click += new System.EventHandler(this.Aboutme_Click);
            // 
            // timelabel
            // 
            this.timelabel.AutoSize = true;
            this.timelabel.BackColor = System.Drawing.Color.Transparent;
            this.timelabel.Font = new System.Drawing.Font("NSimSun", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.timelabel.Location = new System.Drawing.Point(575, 32);
            this.timelabel.Name = "timelabel";
            this.timelabel.Size = new System.Drawing.Size(111, 14);
            this.timelabel.TabIndex = 29;
            this.timelabel.Text = "Loading......";
            // 
            // ModifyFileDlg
            // 
            this.ModifyFileDlg.FileOk += new System.ComponentModel.CancelEventHandler(this.ModifyFileDlg_FileOk);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(789, 419);
            this.Controls.Add(this.timelabel);
            this.Controls.Add(this.menuStrip);
            this.Controls.Add(this.label_percent);
            this.Controls.Add(this.label_updown);
            this.Controls.Add(this.progress_updown);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.VersionView);
            this.Controls.Add(this.FileView);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "局域网电子文档管理平台";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.VersionMenu.ResumeLayout(false);
            this.FileMenu.ResumeLayout(false);
            this.UploadMenu.ResumeLayout(false);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView FileView;
        private System.Windows.Forms.ListView VersionView;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.SaveFileDialog SaveFileDlg;
        private System.Windows.Forms.OpenFileDialog UpdateFileDlg;
        private System.Windows.Forms.SaveFileDialog SaveVersionDlg;
        private System.Windows.Forms.ContextMenuStrip VersionMenu;
        private System.Windows.Forms.ToolStripMenuItem ViewVersion;
        private System.Windows.Forms.ToolStripMenuItem DeleteVersion;
        private System.Windows.Forms.ContextMenuStrip FileMenu;
        private System.Windows.Forms.ToolStripMenuItem ViewFile;
        private System.Windows.Forms.ToolStripMenuItem ModifyFile;
        private System.Windows.Forms.ToolStripMenuItem DeleteFile;
        private System.Windows.Forms.ToolStripMenuItem StartModifyFile;
        private System.Windows.Forms.ToolStripMenuItem CompleteModifyFile;
        private System.Windows.Forms.ToolStripMenuItem GiveUpModifyFile;
        private System.Windows.Forms.ContextMenuStrip UploadMenu;
        private System.Windows.Forms.ToolStripMenuItem UploadFile;
        private System.Windows.Forms.ProgressBar progress_updown;
        private System.Windows.Forms.Label label_updown;
        private System.Windows.Forms.Label label_percent;
        private System.Windows.Forms.ImageList DocImageList;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem 查看ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ViewFileSummary_SmallIcon;
        private System.Windows.Forms.ToolStripMenuItem ViewFileDetail;
        private System.Windows.Forms.ToolStripMenuItem 安全ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ModifyPwd;
        private System.Windows.Forms.ToolStripMenuItem LogOut;
        private System.Windows.Forms.ToolStripMenuItem 帮助ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Aboutme;
        private System.Windows.Forms.ToolStripMenuItem ViewFileSummary_LargeIcon;
        private System.Windows.Forms.Label timelabel;
        private System.Windows.Forms.ToolStripMenuItem AccRollback;
        private System.Windows.Forms.SaveFileDialog ModifyFileDlg;
    }
}