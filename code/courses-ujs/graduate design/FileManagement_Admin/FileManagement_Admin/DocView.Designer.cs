namespace FileManagement_Admin
{
    partial class DocView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DocView));
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.VersionView = new System.Windows.Forms.ListView();
            this.FileView = new System.Windows.Forms.ListView();
            this.DocImageList = new System.Windows.Forms.ImageList(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.查看ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewFileSummary_LargeIcon = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewFileSummary_SmallIcon = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewFileDetail = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("NSimSun", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(2, 208);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 14);
            this.label2.TabIndex = 9;
            this.label2.Text = "版本信息：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("NSimSun", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(0, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 14);
            this.label1.TabIndex = 8;
            this.label1.Text = "文件信息：";
            // 
            // VersionView
            // 
            this.VersionView.Location = new System.Drawing.Point(2, 226);
            this.VersionView.Name = "VersionView";
            this.VersionView.Size = new System.Drawing.Size(376, 159);
            this.VersionView.TabIndex = 7;
            this.VersionView.UseCompatibleStateImageBehavior = false;
            // 
            // FileView
            // 
            this.FileView.Location = new System.Drawing.Point(0, 44);
            this.FileView.Name = "FileView";
            this.FileView.Size = new System.Drawing.Size(378, 159);
            this.FileView.TabIndex = 6;
            this.FileView.UseCompatibleStateImageBehavior = false;
            this.FileView.SelectedIndexChanged += new System.EventHandler(this.FileView_SelectedIndexChanged);
            // 
            // DocImageList
            // 
            this.DocImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.DocImageList.ImageSize = new System.Drawing.Size(16, 16);
            this.DocImageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Transparent;
            this.menuStrip1.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.查看ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(396, 27);
            this.menuStrip1.TabIndex = 10;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 查看ToolStripMenuItem
            // 
            this.查看ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ViewFileSummary_LargeIcon,
            this.ViewFileSummary_SmallIcon,
            this.ViewFileDetail});
            this.查看ToolStripMenuItem.Name = "查看ToolStripMenuItem";
            this.查看ToolStripMenuItem.Size = new System.Drawing.Size(49, 23);
            this.查看ToolStripMenuItem.Text = "查看";
            // 
            // ViewFileSummary_LargeIcon
            // 
            this.ViewFileSummary_LargeIcon.Name = "ViewFileSummary_LargeIcon";
            this.ViewFileSummary_LargeIcon.Size = new System.Drawing.Size(195, 24);
            this.ViewFileSummary_LargeIcon.Text = "文件概览(大图标）";
            this.ViewFileSummary_LargeIcon.Click += new System.EventHandler(this.ViewFileSummary_LargeIcon_Click);
            // 
            // ViewFileSummary_SmallIcon
            // 
            this.ViewFileSummary_SmallIcon.Name = "ViewFileSummary_SmallIcon";
            this.ViewFileSummary_SmallIcon.Size = new System.Drawing.Size(195, 24);
            this.ViewFileSummary_SmallIcon.Text = "文件概览(小图标）";
            this.ViewFileSummary_SmallIcon.Click += new System.EventHandler(this.ViewFileSummary_SmallIcon_Click);
            // 
            // ViewFileDetail
            // 
            this.ViewFileDetail.Name = "ViewFileDetail";
            this.ViewFileDetail.Size = new System.Drawing.Size(195, 24);
            this.ViewFileDetail.Text = "文件详细信息";
            this.ViewFileDetail.Click += new System.EventHandler(this.ViewFileDetail_Click);
            // 
            // DocView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(396, 395);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.VersionView);
            this.Controls.Add(this.FileView);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DocView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "文档信息";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView VersionView;
        private System.Windows.Forms.ListView FileView;
        private System.Windows.Forms.ImageList DocImageList;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 查看ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ViewFileSummary_SmallIcon;
        private System.Windows.Forms.ToolStripMenuItem ViewFileDetail;
        private System.Windows.Forms.ToolStripMenuItem ViewFileSummary_LargeIcon;
    }
}