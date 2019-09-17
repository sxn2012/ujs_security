namespace FileManagement_Admin
{
    partial class SystemLog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SystemLog));
            this.LogBox = new System.Windows.Forms.TextBox();
            this.LogContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.UpdateView = new System.Windows.Forms.ToolStripMenuItem();
            this.ClearView = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteView = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewHistoryLog = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenLogFileDlg = new System.Windows.Forms.OpenFileDialog();
            this.LogContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // LogBox
            // 
            this.LogBox.ContextMenuStrip = this.LogContextMenu;
            this.LogBox.Font = new System.Drawing.Font("NSimSun", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LogBox.Location = new System.Drawing.Point(6, 12);
            this.LogBox.Multiline = true;
            this.LogBox.Name = "LogBox";
            this.LogBox.ReadOnly = true;
            this.LogBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.LogBox.Size = new System.Drawing.Size(348, 188);
            this.LogBox.TabIndex = 55;
            // 
            // LogContextMenu
            // 
            this.LogContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.UpdateView,
            this.ClearView,
            this.DeleteView,
            this.ViewHistoryLog});
            this.LogContextMenu.Name = "LogContextMenu";
            this.LogContextMenu.Size = new System.Drawing.Size(149, 92);
            // 
            // UpdateView
            // 
            this.UpdateView.Name = "UpdateView";
            this.UpdateView.Size = new System.Drawing.Size(148, 22);
            this.UpdateView.Text = "刷新日志显示";
            this.UpdateView.Click += new System.EventHandler(this.UpdateView_Click);
            // 
            // ClearView
            // 
            this.ClearView.Name = "ClearView";
            this.ClearView.Size = new System.Drawing.Size(148, 22);
            this.ClearView.Text = "清空日志显示";
            this.ClearView.Click += new System.EventHandler(this.ClearView_Click);
            // 
            // DeleteView
            // 
            this.DeleteView.Name = "DeleteView";
            this.DeleteView.Size = new System.Drawing.Size(148, 22);
            this.DeleteView.Text = "删除系统日志";
            this.DeleteView.Click += new System.EventHandler(this.DeleteView_Click);
            // 
            // ViewHistoryLog
            // 
            this.ViewHistoryLog.Name = "ViewHistoryLog";
            this.ViewHistoryLog.Size = new System.Drawing.Size(148, 22);
            this.ViewHistoryLog.Text = "显示历史日志";
            this.ViewHistoryLog.Click += new System.EventHandler(this.ViewHistoryLog_Click);
            // 
            // OpenLogFileDlg
            // 
            this.OpenLogFileDlg.FileOk += new System.ComponentModel.CancelEventHandler(this.OpenLogFileDlg_FileOk);
            // 
            // SystemLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(360, 212);
            this.Controls.Add(this.LogBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SystemLog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "系统日志";
            this.LogContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox LogBox;
        private System.Windows.Forms.ContextMenuStrip LogContextMenu;
        private System.Windows.Forms.ToolStripMenuItem UpdateView;
        private System.Windows.Forms.ToolStripMenuItem ClearView;
        private System.Windows.Forms.ToolStripMenuItem DeleteView;
        private System.Windows.Forms.ToolStripMenuItem ViewHistoryLog;
        private System.Windows.Forms.OpenFileDialog OpenLogFileDlg;
    }
}