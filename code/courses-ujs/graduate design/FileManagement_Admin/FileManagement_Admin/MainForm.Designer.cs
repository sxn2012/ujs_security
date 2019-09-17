namespace FileManagement_Admin
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
            this.UsrView = new System.Windows.Forms.ListView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.DepView = new System.Windows.Forms.ListView();
            this.label3 = new System.Windows.Forms.Label();
            this.SubView = new System.Windows.Forms.ListView();
            this.label4 = new System.Windows.Forms.Label();
            this.AccessView = new System.Windows.Forms.ListView();
            this.UserMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.DeleteUser = new System.Windows.Forms.ToolStripMenuItem();
            this.修改用户信息ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ModifyUserName = new System.Windows.Forms.ToolStripMenuItem();
            this.ResetPwd = new System.Windows.Forms.ToolStripMenuItem();
            this.ModifyUserDept = new System.Windows.Forms.ToolStripMenuItem();
            this.用户账户安全ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LockAcc = new System.Windows.Forms.ToolStripMenuItem();
            this.UnlockAcc = new System.Windows.Forms.ToolStripMenuItem();
            this.AddUserMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.NewUser = new System.Windows.Forms.ToolStripMenuItem();
            this.DeptMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.DeleteDept = new System.Windows.Forms.ToolStripMenuItem();
            this.修改部门信息ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ModifyDeptName = new System.Windows.Forms.ToolStripMenuItem();
            this.ModifyDeptLoc = new System.Windows.Forms.ToolStripMenuItem();
            this.ListDeptPermission = new System.Windows.Forms.ToolStripMenuItem();
            this.AddDeptMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.AddDept = new System.Windows.Forms.ToolStripMenuItem();
            this.SubMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.DeleteSub = new System.Windows.Forms.ToolStripMenuItem();
            this.修改主题ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ModifySubName = new System.Windows.Forms.ToolStripMenuItem();
            this.ModifySubContent = new System.Windows.Forms.ToolStripMenuItem();
            this.ListSubPermission = new System.Windows.Forms.ToolStripMenuItem();
            this.AddSubMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.AddSub = new System.Windows.Forms.ToolStripMenuItem();
            this.PermissionMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ModifyPermission = new System.Windows.Forms.ToolStripMenuItem();
            this.WithdrawPermission = new System.Windows.Forms.ToolStripMenuItem();
            this.AccessTransfer = new System.Windows.Forms.ToolStripMenuItem();
            this.AddPermissionMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.GrantPermission = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.查看ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewSyslog = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewDoc = new System.Windows.Forms.ToolStripMenuItem();
            this.安全ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ModifyAdminPwd = new System.Windows.Forms.ToolStripMenuItem();
            this.LogOut = new System.Windows.Forms.ToolStripMenuItem();
            this.帮助ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Aboutme = new System.Windows.Forms.ToolStripMenuItem();
            this.timelabel = new System.Windows.Forms.Label();
            this.Backup_Recover = new System.Windows.Forms.ToolStripMenuItem();
            this.UserMenu.SuspendLayout();
            this.AddUserMenu.SuspendLayout();
            this.DeptMenu.SuspendLayout();
            this.AddDeptMenu.SuspendLayout();
            this.SubMenu.SuspendLayout();
            this.AddSubMenu.SuspendLayout();
            this.PermissionMenu.SuspendLayout();
            this.AddPermissionMenu.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // UsrView
            // 
            this.UsrView.Location = new System.Drawing.Point(14, 46);
            this.UsrView.Name = "UsrView";
            this.UsrView.Size = new System.Drawing.Size(347, 250);
            this.UsrView.TabIndex = 0;
            this.UsrView.UseCompatibleStateImageBehavior = false;
            this.UsrView.MouseUp += new System.Windows.Forms.MouseEventHandler(this.UsrView_MouseUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("NSimSun", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(13, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 14);
            this.label1.TabIndex = 5;
            this.label1.Text = "用户管理：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("NSimSun", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(377, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 14);
            this.label2.TabIndex = 7;
            this.label2.Text = "部门管理：";
            // 
            // DepView
            // 
            this.DepView.Location = new System.Drawing.Point(378, 46);
            this.DepView.Name = "DepView";
            this.DepView.Size = new System.Drawing.Size(347, 250);
            this.DepView.TabIndex = 6;
            this.DepView.UseCompatibleStateImageBehavior = false;
            this.DepView.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DepView_MouseUp);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("NSimSun", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(14, 307);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 14);
            this.label3.TabIndex = 9;
            this.label3.Text = "主题管理：";
            // 
            // SubView
            // 
            this.SubView.Location = new System.Drawing.Point(15, 326);
            this.SubView.Name = "SubView";
            this.SubView.Size = new System.Drawing.Size(347, 250);
            this.SubView.TabIndex = 8;
            this.SubView.UseCompatibleStateImageBehavior = false;
            this.SubView.MouseUp += new System.Windows.Forms.MouseEventHandler(this.SubView_MouseUp);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("NSimSun", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(378, 307);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 14);
            this.label4.TabIndex = 11;
            this.label4.Text = "权限管理：";
            // 
            // AccessView
            // 
            this.AccessView.Location = new System.Drawing.Point(379, 326);
            this.AccessView.Name = "AccessView";
            this.AccessView.Size = new System.Drawing.Size(347, 250);
            this.AccessView.TabIndex = 10;
            this.AccessView.UseCompatibleStateImageBehavior = false;
            this.AccessView.MouseUp += new System.Windows.Forms.MouseEventHandler(this.AccessView_MouseUp);
            // 
            // UserMenu
            // 
            this.UserMenu.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.UserMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DeleteUser,
            this.修改用户信息ToolStripMenuItem,
            this.用户账户安全ToolStripMenuItem});
            this.UserMenu.Name = "UserMenu";
            this.UserMenu.Size = new System.Drawing.Size(163, 76);
            // 
            // DeleteUser
            // 
            this.DeleteUser.Name = "DeleteUser";
            this.DeleteUser.Size = new System.Drawing.Size(162, 24);
            this.DeleteUser.Text = "删除用户";
            this.DeleteUser.Click += new System.EventHandler(this.DeleteUser_Click);
            // 
            // 修改用户信息ToolStripMenuItem
            // 
            this.修改用户信息ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ModifyUserName,
            this.ResetPwd,
            this.ModifyUserDept});
            this.修改用户信息ToolStripMenuItem.Name = "修改用户信息ToolStripMenuItem";
            this.修改用户信息ToolStripMenuItem.Size = new System.Drawing.Size(162, 24);
            this.修改用户信息ToolStripMenuItem.Text = "修改用户信息";
            // 
            // ModifyUserName
            // 
            this.ModifyUserName.Name = "ModifyUserName";
            this.ModifyUserName.Size = new System.Drawing.Size(190, 24);
            this.ModifyUserName.Text = "修改用户名";
            this.ModifyUserName.Click += new System.EventHandler(this.ModifyUserName_Click);
            // 
            // ResetPwd
            // 
            this.ResetPwd.Name = "ResetPwd";
            this.ResetPwd.Size = new System.Drawing.Size(190, 24);
            this.ResetPwd.Text = "重置密码";
            this.ResetPwd.Click += new System.EventHandler(this.ResetPwd_Click);
            // 
            // ModifyUserDept
            // 
            this.ModifyUserDept.Name = "ModifyUserDept";
            this.ModifyUserDept.Size = new System.Drawing.Size(190, 24);
            this.ModifyUserDept.Text = "修改用户所属部门";
            this.ModifyUserDept.Click += new System.EventHandler(this.ModifyUserDept_Click);
            // 
            // 用户账户安全ToolStripMenuItem
            // 
            this.用户账户安全ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LockAcc,
            this.UnlockAcc});
            this.用户账户安全ToolStripMenuItem.Name = "用户账户安全ToolStripMenuItem";
            this.用户账户安全ToolStripMenuItem.Size = new System.Drawing.Size(162, 24);
            this.用户账户安全ToolStripMenuItem.Text = "用户账户安全";
            // 
            // LockAcc
            // 
            this.LockAcc.Name = "LockAcc";
            this.LockAcc.Size = new System.Drawing.Size(134, 24);
            this.LockAcc.Text = "锁定账户";
            this.LockAcc.Click += new System.EventHandler(this.LockAcc_Click);
            // 
            // UnlockAcc
            // 
            this.UnlockAcc.Name = "UnlockAcc";
            this.UnlockAcc.Size = new System.Drawing.Size(134, 24);
            this.UnlockAcc.Text = "解锁账户";
            this.UnlockAcc.Click += new System.EventHandler(this.UnlockAcc_Click);
            // 
            // AddUserMenu
            // 
            this.AddUserMenu.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.AddUserMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewUser});
            this.AddUserMenu.Name = "AddUserMenu";
            this.AddUserMenu.Size = new System.Drawing.Size(135, 28);
            // 
            // NewUser
            // 
            this.NewUser.Name = "NewUser";
            this.NewUser.Size = new System.Drawing.Size(134, 24);
            this.NewUser.Text = "新建用户";
            this.NewUser.Click += new System.EventHandler(this.NewUser_Click);
            // 
            // DeptMenu
            // 
            this.DeptMenu.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.DeptMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DeleteDept,
            this.修改部门信息ToolStripMenuItem,
            this.ListDeptPermission});
            this.DeptMenu.Name = "DeptMenu";
            this.DeptMenu.Size = new System.Drawing.Size(163, 76);
            // 
            // DeleteDept
            // 
            this.DeleteDept.Name = "DeleteDept";
            this.DeleteDept.Size = new System.Drawing.Size(162, 24);
            this.DeleteDept.Text = "删除部门";
            this.DeleteDept.Click += new System.EventHandler(this.DeleteDept_Click);
            // 
            // 修改部门信息ToolStripMenuItem
            // 
            this.修改部门信息ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ModifyDeptName,
            this.ModifyDeptLoc});
            this.修改部门信息ToolStripMenuItem.Name = "修改部门信息ToolStripMenuItem";
            this.修改部门信息ToolStripMenuItem.Size = new System.Drawing.Size(162, 24);
            this.修改部门信息ToolStripMenuItem.Text = "修改部门信息";
            // 
            // ModifyDeptName
            // 
            this.ModifyDeptName.Name = "ModifyDeptName";
            this.ModifyDeptName.Size = new System.Drawing.Size(162, 24);
            this.ModifyDeptName.Text = "修改部门名称";
            this.ModifyDeptName.Click += new System.EventHandler(this.ModifyDeptName_Click);
            // 
            // ModifyDeptLoc
            // 
            this.ModifyDeptLoc.Name = "ModifyDeptLoc";
            this.ModifyDeptLoc.Size = new System.Drawing.Size(162, 24);
            this.ModifyDeptLoc.Text = "修改部门地址";
            this.ModifyDeptLoc.Click += new System.EventHandler(this.ModifyDeptLoc_Click);
            // 
            // ListDeptPermission
            // 
            this.ListDeptPermission.Name = "ListDeptPermission";
            this.ListDeptPermission.Size = new System.Drawing.Size(162, 24);
            this.ListDeptPermission.Text = "查看权限";
            this.ListDeptPermission.Click += new System.EventHandler(this.ListDeptPermission_Click);
            // 
            // AddDeptMenu
            // 
            this.AddDeptMenu.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.AddDeptMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddDept});
            this.AddDeptMenu.Name = "AddDeptMenu";
            this.AddDeptMenu.Size = new System.Drawing.Size(135, 28);
            // 
            // AddDept
            // 
            this.AddDept.Name = "AddDept";
            this.AddDept.Size = new System.Drawing.Size(134, 24);
            this.AddDept.Text = "添加部门";
            this.AddDept.Click += new System.EventHandler(this.AddDept_Click);
            // 
            // SubMenu
            // 
            this.SubMenu.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.SubMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DeleteSub,
            this.修改主题ToolStripMenuItem,
            this.ListSubPermission});
            this.SubMenu.Name = "SubMenu";
            this.SubMenu.Size = new System.Drawing.Size(135, 76);
            // 
            // DeleteSub
            // 
            this.DeleteSub.Name = "DeleteSub";
            this.DeleteSub.Size = new System.Drawing.Size(134, 24);
            this.DeleteSub.Text = "删除主题";
            this.DeleteSub.Click += new System.EventHandler(this.DeleteSub_Click);
            // 
            // 修改主题ToolStripMenuItem
            // 
            this.修改主题ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ModifySubName,
            this.ModifySubContent});
            this.修改主题ToolStripMenuItem.Name = "修改主题ToolStripMenuItem";
            this.修改主题ToolStripMenuItem.Size = new System.Drawing.Size(134, 24);
            this.修改主题ToolStripMenuItem.Text = "修改主题";
            // 
            // ModifySubName
            // 
            this.ModifySubName.Name = "ModifySubName";
            this.ModifySubName.Size = new System.Drawing.Size(162, 24);
            this.ModifySubName.Text = "修改主题名称";
            this.ModifySubName.Click += new System.EventHandler(this.ModifySubName_Click);
            // 
            // ModifySubContent
            // 
            this.ModifySubContent.Name = "ModifySubContent";
            this.ModifySubContent.Size = new System.Drawing.Size(162, 24);
            this.ModifySubContent.Text = "修改主题内容";
            this.ModifySubContent.Click += new System.EventHandler(this.ModifySubContent_Click);
            // 
            // ListSubPermission
            // 
            this.ListSubPermission.Name = "ListSubPermission";
            this.ListSubPermission.Size = new System.Drawing.Size(134, 24);
            this.ListSubPermission.Text = "查看权限";
            this.ListSubPermission.Click += new System.EventHandler(this.ListSubPermission_Click);
            // 
            // AddSubMenu
            // 
            this.AddSubMenu.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.AddSubMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddSub});
            this.AddSubMenu.Name = "AddSubMenu";
            this.AddSubMenu.Size = new System.Drawing.Size(135, 28);
            // 
            // AddSub
            // 
            this.AddSub.Name = "AddSub";
            this.AddSub.Size = new System.Drawing.Size(134, 24);
            this.AddSub.Text = "添加主题";
            this.AddSub.Click += new System.EventHandler(this.AddSub_Click);
            // 
            // PermissionMenu
            // 
            this.PermissionMenu.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.PermissionMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ModifyPermission,
            this.WithdrawPermission,
            this.AccessTransfer});
            this.PermissionMenu.Name = "PermissionMenu";
            this.PermissionMenu.Size = new System.Drawing.Size(149, 76);
            // 
            // ModifyPermission
            // 
            this.ModifyPermission.Name = "ModifyPermission";
            this.ModifyPermission.Size = new System.Drawing.Size(148, 24);
            this.ModifyPermission.Text = "修改权限";
            this.ModifyPermission.Click += new System.EventHandler(this.ModifyPermission_Click);
            // 
            // WithdrawPermission
            // 
            this.WithdrawPermission.Name = "WithdrawPermission";
            this.WithdrawPermission.Size = new System.Drawing.Size(148, 24);
            this.WithdrawPermission.Text = "撤销权限";
            this.WithdrawPermission.Click += new System.EventHandler(this.WithdrawPermission_Click);
            // 
            // AccessTransfer
            // 
            this.AccessTransfer.Name = "AccessTransfer";
            this.AccessTransfer.Size = new System.Drawing.Size(148, 24);
            this.AccessTransfer.Text = "管理权转移";
            this.AccessTransfer.Click += new System.EventHandler(this.AccessTransfer_Click);
            // 
            // AddPermissionMenu
            // 
            this.AddPermissionMenu.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.AddPermissionMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.GrantPermission});
            this.AddPermissionMenu.Name = "AddPermissionMenu";
            this.AddPermissionMenu.Size = new System.Drawing.Size(149, 28);
            // 
            // GrantPermission
            // 
            this.GrantPermission.Name = "GrantPermission";
            this.GrantPermission.Size = new System.Drawing.Size(148, 24);
            this.GrantPermission.Text = "授予新权限";
            this.GrantPermission.Click += new System.EventHandler(this.GrantPermission_Click);
            // 
            // menuStrip
            // 
            this.menuStrip.BackColor = System.Drawing.Color.Transparent;
            this.menuStrip.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.查看ToolStripMenuItem,
            this.安全ToolStripMenuItem,
            this.帮助ToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(732, 27);
            this.menuStrip.TabIndex = 13;
            this.menuStrip.Text = "menuStrip";
            // 
            // 查看ToolStripMenuItem
            // 
            this.查看ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ViewSyslog,
            this.ViewDoc});
            this.查看ToolStripMenuItem.Name = "查看ToolStripMenuItem";
            this.查看ToolStripMenuItem.Size = new System.Drawing.Size(49, 23);
            this.查看ToolStripMenuItem.Text = "查看";
            // 
            // ViewSyslog
            // 
            this.ViewSyslog.Name = "ViewSyslog";
            this.ViewSyslog.Size = new System.Drawing.Size(134, 24);
            this.ViewSyslog.Text = "系统日志";
            this.ViewSyslog.Click += new System.EventHandler(this.ViewSyslog_Click);
            // 
            // ViewDoc
            // 
            this.ViewDoc.Name = "ViewDoc";
            this.ViewDoc.Size = new System.Drawing.Size(134, 24);
            this.ViewDoc.Text = "文档信息";
            this.ViewDoc.Click += new System.EventHandler(this.ViewDoc_Click);
            // 
            // 安全ToolStripMenuItem
            // 
            this.安全ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ModifyAdminPwd,
            this.LogOut,
            this.Backup_Recover});
            this.安全ToolStripMenuItem.Name = "安全ToolStripMenuItem";
            this.安全ToolStripMenuItem.Size = new System.Drawing.Size(49, 23);
            this.安全ToolStripMenuItem.Text = "安全";
            // 
            // ModifyAdminPwd
            // 
            this.ModifyAdminPwd.Name = "ModifyAdminPwd";
            this.ModifyAdminPwd.Size = new System.Drawing.Size(192, 24);
            this.ModifyAdminPwd.Text = "修改管理员密码";
            this.ModifyAdminPwd.Click += new System.EventHandler(this.ModifyAdminPwd_Click);
            // 
            // LogOut
            // 
            this.LogOut.Name = "LogOut";
            this.LogOut.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.LogOut.Size = new System.Drawing.Size(192, 24);
            this.LogOut.Text = "退出登录";
            this.LogOut.Click += new System.EventHandler(this.LogOut_Click);
            // 
            // 帮助ToolStripMenuItem
            // 
            this.帮助ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Aboutme});
            this.帮助ToolStripMenuItem.Name = "帮助ToolStripMenuItem";
            this.帮助ToolStripMenuItem.Size = new System.Drawing.Size(49, 23);
            this.帮助ToolStripMenuItem.Text = "帮助";
            // 
            // Aboutme
            // 
            this.Aboutme.Name = "Aboutme";
            this.Aboutme.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F1)));
            this.Aboutme.Size = new System.Drawing.Size(168, 24);
            this.Aboutme.Text = "关于";
            this.Aboutme.Click += new System.EventHandler(this.Aboutme_Click);
            // 
            // timelabel
            // 
            this.timelabel.AutoSize = true;
            this.timelabel.BackColor = System.Drawing.Color.Transparent;
            this.timelabel.Font = new System.Drawing.Font("NSimSun", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.timelabel.Location = new System.Drawing.Point(483, 27);
            this.timelabel.Name = "timelabel";
            this.timelabel.Size = new System.Drawing.Size(111, 14);
            this.timelabel.TabIndex = 30;
            this.timelabel.Text = "Loading......";
            // 
            // Backup_Recover
            // 
            this.Backup_Recover.Name = "Backup_Recover";
            this.Backup_Recover.Size = new System.Drawing.Size(192, 24);
            this.Backup_Recover.Text = "数据备份与恢复";
            this.Backup_Recover.Click += new System.EventHandler(this.Backup_Recover_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(732, 578);
            this.Controls.Add(this.timelabel);
            this.Controls.Add(this.menuStrip);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.AccessView);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.SubView);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.DepView);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.UsrView);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "局域网电子文档管理平台";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.UserMenu.ResumeLayout(false);
            this.AddUserMenu.ResumeLayout(false);
            this.DeptMenu.ResumeLayout(false);
            this.AddDeptMenu.ResumeLayout(false);
            this.SubMenu.ResumeLayout(false);
            this.AddSubMenu.ResumeLayout(false);
            this.PermissionMenu.ResumeLayout(false);
            this.AddPermissionMenu.ResumeLayout(false);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView UsrView;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListView DepView;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListView SubView;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListView AccessView;
        private System.Windows.Forms.ContextMenuStrip UserMenu;
        private System.Windows.Forms.ToolStripMenuItem DeleteUser;
        private System.Windows.Forms.ToolStripMenuItem 修改用户信息ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ModifyUserName;
        private System.Windows.Forms.ToolStripMenuItem ResetPwd;
        private System.Windows.Forms.ToolStripMenuItem ModifyUserDept;
        private System.Windows.Forms.ContextMenuStrip AddUserMenu;
        private System.Windows.Forms.ToolStripMenuItem NewUser;
        private System.Windows.Forms.ContextMenuStrip DeptMenu;
        private System.Windows.Forms.ToolStripMenuItem DeleteDept;
        private System.Windows.Forms.ToolStripMenuItem 修改部门信息ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ModifyDeptName;
        private System.Windows.Forms.ToolStripMenuItem ModifyDeptLoc;
        private System.Windows.Forms.ContextMenuStrip AddDeptMenu;
        private System.Windows.Forms.ToolStripMenuItem AddDept;
        private System.Windows.Forms.ContextMenuStrip SubMenu;
        private System.Windows.Forms.ToolStripMenuItem DeleteSub;
        private System.Windows.Forms.ToolStripMenuItem 修改主题ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ModifySubName;
        private System.Windows.Forms.ToolStripMenuItem ModifySubContent;
        private System.Windows.Forms.ContextMenuStrip AddSubMenu;
        private System.Windows.Forms.ToolStripMenuItem AddSub;
        private System.Windows.Forms.ToolStripMenuItem ListDeptPermission;
        private System.Windows.Forms.ToolStripMenuItem ListSubPermission;
        private System.Windows.Forms.ContextMenuStrip PermissionMenu;
        private System.Windows.Forms.ToolStripMenuItem ModifyPermission;
        private System.Windows.Forms.ToolStripMenuItem WithdrawPermission;
        private System.Windows.Forms.ContextMenuStrip AddPermissionMenu;
        private System.Windows.Forms.ToolStripMenuItem GrantPermission;
        private System.Windows.Forms.ToolStripMenuItem AccessTransfer;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem 查看ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ViewSyslog;
        private System.Windows.Forms.ToolStripMenuItem ViewDoc;
        private System.Windows.Forms.ToolStripMenuItem 安全ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ModifyAdminPwd;
        private System.Windows.Forms.ToolStripMenuItem LogOut;
        private System.Windows.Forms.ToolStripMenuItem 帮助ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Aboutme;
        private System.Windows.Forms.ToolStripMenuItem 用户账户安全ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem LockAcc;
        private System.Windows.Forms.ToolStripMenuItem UnlockAcc;
        private System.Windows.Forms.Label timelabel;
        private System.Windows.Forms.ToolStripMenuItem Backup_Recover;
    }
}