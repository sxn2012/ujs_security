namespace MagazineOrdering
{
    partial class Admin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Admin));
            this.MagListTab = new System.Windows.Forms.ListView();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.报刊信息ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AddMagToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DelMagToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ModifyMagToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FindMagToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MagNameFindToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MagNumFindToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.用户信息管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AddUserToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.DelUserToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.ModifyUserToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.FindUserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.UsrnameFindToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.UsrlocFindToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.订单信息管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AddMenuToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.DelMenuToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.ModifyMenuToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.FindMenuToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuUnameFindToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuMnumFindToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuDateFindToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.清空ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ClearInputToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ClearOutputToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LogoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ModifyPassToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.NewMagName = new System.Windows.Forms.TextBox();
            this.NewMagNum = new System.Windows.Forms.TextBox();
            this.NewMagVer = new System.Windows.Forms.TextBox();
            this.NewMagPrice = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.MessageOutTab = new System.Windows.Forms.ListView();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.NewUsrLoc = new System.Windows.Forms.TextBox();
            this.NewUsrAcc = new System.Windows.Forms.TextBox();
            this.NewUsrpwd = new System.Windows.Forms.TextBox();
            this.NewUsrname = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.UsrListTab = new System.Windows.Forms.ListView();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.NewMenuStartDate = new System.Windows.Forms.TextBox();
            this.NewMenuQuan = new System.Windows.Forms.TextBox();
            this.NewMenuMnum = new System.Windows.Forms.TextBox();
            this.NewMenuUname = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.MenuListTab = new System.Windows.Forms.ListView();
            this.label19 = new System.Windows.Forms.Label();
            this.NewMenuDur = new System.Windows.Forms.TextBox();
            this.timelabel = new System.Windows.Forms.Label();
            this.statuslable = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // MagListTab
            // 
            this.MagListTab.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.MagListTab.Location = new System.Drawing.Point(12, 57);
            this.MagListTab.Name = "MagListTab";
            this.MagListTab.Size = new System.Drawing.Size(398, 186);
            this.MagListTab.TabIndex = 4;
            this.MagListTab.UseCompatibleStateImageBehavior = false;
            this.MagListTab.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.MagListTab_ColumnClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("LiSu", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(123, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 24);
            this.label1.TabIndex = 5;
            this.label1.Text = "报刊信息：";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.报刊信息ToolStripMenuItem,
            this.用户信息管理ToolStripMenuItem,
            this.订单信息管理ToolStripMenuItem,
            this.清空ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.menuStrip1.Size = new System.Drawing.Size(1283, 25);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 报刊信息ToolStripMenuItem
            // 
            this.报刊信息ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddMagToolStripMenuItem,
            this.DelMagToolStripMenuItem,
            this.ModifyMagToolStripMenuItem,
            this.FindMagToolStripMenuItem});
            this.报刊信息ToolStripMenuItem.Name = "报刊信息ToolStripMenuItem";
            this.报刊信息ToolStripMenuItem.Size = new System.Drawing.Size(92, 21);
            this.报刊信息ToolStripMenuItem.Text = "报刊信息管理";
            // 
            // AddMagToolStripMenuItem
            // 
            this.AddMagToolStripMenuItem.Name = "AddMagToolStripMenuItem";
            this.AddMagToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.AddMagToolStripMenuItem.Text = "添加信息";
            this.AddMagToolStripMenuItem.Click += new System.EventHandler(this.AddMagToolStripMenuItem_Click);
            // 
            // DelMagToolStripMenuItem
            // 
            this.DelMagToolStripMenuItem.Name = "DelMagToolStripMenuItem";
            this.DelMagToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.DelMagToolStripMenuItem.Text = "删除信息";
            this.DelMagToolStripMenuItem.Click += new System.EventHandler(this.DelMagToolStripMenuItem_Click);
            // 
            // ModifyMagToolStripMenuItem
            // 
            this.ModifyMagToolStripMenuItem.Name = "ModifyMagToolStripMenuItem";
            this.ModifyMagToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.ModifyMagToolStripMenuItem.Text = "修改信息";
            this.ModifyMagToolStripMenuItem.Click += new System.EventHandler(this.ModifyMagToolStripMenuItem_Click);
            // 
            // FindMagToolStripMenuItem
            // 
            this.FindMagToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MagNameFindToolStripMenuItem,
            this.MagNumFindToolStripMenuItem});
            this.FindMagToolStripMenuItem.Name = "FindMagToolStripMenuItem";
            this.FindMagToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.FindMagToolStripMenuItem.Text = "查询信息";
            // 
            // MagNameFindToolStripMenuItem
            // 
            this.MagNameFindToolStripMenuItem.Name = "MagNameFindToolStripMenuItem";
            this.MagNameFindToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.MagNameFindToolStripMenuItem.Text = "按报刊名称查询";
            this.MagNameFindToolStripMenuItem.Click += new System.EventHandler(this.MagNameFindToolStripMenuItem_Click);
            // 
            // MagNumFindToolStripMenuItem
            // 
            this.MagNumFindToolStripMenuItem.Name = "MagNumFindToolStripMenuItem";
            this.MagNumFindToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.MagNumFindToolStripMenuItem.Text = "按报刊编号查询";
            this.MagNumFindToolStripMenuItem.Click += new System.EventHandler(this.MagNumFindToolStripMenuItem_Click);
            // 
            // 用户信息管理ToolStripMenuItem
            // 
            this.用户信息管理ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddUserToolStripMenuItem1,
            this.DelUserToolStripMenuItem1,
            this.ModifyUserToolStripMenuItem1,
            this.FindUserToolStripMenuItem});
            this.用户信息管理ToolStripMenuItem.Name = "用户信息管理ToolStripMenuItem";
            this.用户信息管理ToolStripMenuItem.Size = new System.Drawing.Size(92, 21);
            this.用户信息管理ToolStripMenuItem.Text = "用户信息管理";
            // 
            // AddUserToolStripMenuItem1
            // 
            this.AddUserToolStripMenuItem1.Name = "AddUserToolStripMenuItem1";
            this.AddUserToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.AddUserToolStripMenuItem1.Text = "添加信息";
            this.AddUserToolStripMenuItem1.Click += new System.EventHandler(this.AddUserToolStripMenuItem1_Click);
            // 
            // DelUserToolStripMenuItem1
            // 
            this.DelUserToolStripMenuItem1.Name = "DelUserToolStripMenuItem1";
            this.DelUserToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.DelUserToolStripMenuItem1.Text = "删除信息";
            this.DelUserToolStripMenuItem1.Click += new System.EventHandler(this.DelUserToolStripMenuItem1_Click);
            // 
            // ModifyUserToolStripMenuItem1
            // 
            this.ModifyUserToolStripMenuItem1.Name = "ModifyUserToolStripMenuItem1";
            this.ModifyUserToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.ModifyUserToolStripMenuItem1.Text = "修改信息";
            this.ModifyUserToolStripMenuItem1.Click += new System.EventHandler(this.ModifyUserToolStripMenuItem1_Click);
            // 
            // FindUserToolStripMenuItem
            // 
            this.FindUserToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.UsrnameFindToolStripMenuItem,
            this.UsrlocFindToolStripMenuItem});
            this.FindUserToolStripMenuItem.Name = "FindUserToolStripMenuItem";
            this.FindUserToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.FindUserToolStripMenuItem.Text = "查询信息";
            // 
            // UsrnameFindToolStripMenuItem
            // 
            this.UsrnameFindToolStripMenuItem.Name = "UsrnameFindToolStripMenuItem";
            this.UsrnameFindToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.UsrnameFindToolStripMenuItem.Text = "按用户名查询";
            this.UsrnameFindToolStripMenuItem.Click += new System.EventHandler(this.UsrnameFindToolStripMenuItem_Click);
            // 
            // UsrlocFindToolStripMenuItem
            // 
            this.UsrlocFindToolStripMenuItem.Name = "UsrlocFindToolStripMenuItem";
            this.UsrlocFindToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.UsrlocFindToolStripMenuItem.Text = "按地址查询";
            this.UsrlocFindToolStripMenuItem.Click += new System.EventHandler(this.UsrlocFindToolStripMenuItem_Click);
            // 
            // 订单信息管理ToolStripMenuItem
            // 
            this.订单信息管理ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddMenuToolStripMenuItem2,
            this.DelMenuToolStripMenuItem2,
            this.ModifyMenuToolStripMenuItem2,
            this.FindMenuToolStripMenuItem1});
            this.订单信息管理ToolStripMenuItem.Name = "订单信息管理ToolStripMenuItem";
            this.订单信息管理ToolStripMenuItem.Size = new System.Drawing.Size(92, 21);
            this.订单信息管理ToolStripMenuItem.Text = "订单信息管理";
            // 
            // AddMenuToolStripMenuItem2
            // 
            this.AddMenuToolStripMenuItem2.Name = "AddMenuToolStripMenuItem2";
            this.AddMenuToolStripMenuItem2.Size = new System.Drawing.Size(152, 22);
            this.AddMenuToolStripMenuItem2.Text = "添加信息";
            this.AddMenuToolStripMenuItem2.Click += new System.EventHandler(this.AddMenuToolStripMenuItem2_Click);
            // 
            // DelMenuToolStripMenuItem2
            // 
            this.DelMenuToolStripMenuItem2.Name = "DelMenuToolStripMenuItem2";
            this.DelMenuToolStripMenuItem2.Size = new System.Drawing.Size(152, 22);
            this.DelMenuToolStripMenuItem2.Text = "删除信息";
            this.DelMenuToolStripMenuItem2.Click += new System.EventHandler(this.DelMenuToolStripMenuItem2_Click);
            // 
            // ModifyMenuToolStripMenuItem2
            // 
            this.ModifyMenuToolStripMenuItem2.Name = "ModifyMenuToolStripMenuItem2";
            this.ModifyMenuToolStripMenuItem2.Size = new System.Drawing.Size(152, 22);
            this.ModifyMenuToolStripMenuItem2.Text = "修改信息";
            this.ModifyMenuToolStripMenuItem2.Click += new System.EventHandler(this.ModifyMenuToolStripMenuItem2_Click);
            // 
            // FindMenuToolStripMenuItem1
            // 
            this.FindMenuToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuUnameFindToolStripMenuItem1,
            this.MenuMnumFindToolStripMenuItem,
            this.MenuDateFindToolStripMenuItem});
            this.FindMenuToolStripMenuItem1.Name = "FindMenuToolStripMenuItem1";
            this.FindMenuToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.FindMenuToolStripMenuItem1.Text = "查询信息";
            // 
            // MenuUnameFindToolStripMenuItem1
            // 
            this.MenuUnameFindToolStripMenuItem1.Name = "MenuUnameFindToolStripMenuItem1";
            this.MenuUnameFindToolStripMenuItem1.Size = new System.Drawing.Size(160, 22);
            this.MenuUnameFindToolStripMenuItem1.Text = "按用户名查询";
            this.MenuUnameFindToolStripMenuItem1.Click += new System.EventHandler(this.MenuUnameFindToolStripMenuItem1_Click);
            // 
            // MenuMnumFindToolStripMenuItem
            // 
            this.MenuMnumFindToolStripMenuItem.Name = "MenuMnumFindToolStripMenuItem";
            this.MenuMnumFindToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.MenuMnumFindToolStripMenuItem.Text = "按报刊编号查询";
            this.MenuMnumFindToolStripMenuItem.Click += new System.EventHandler(this.MenuMnumFindToolStripMenuItem_Click);
            // 
            // MenuDateFindToolStripMenuItem
            // 
            this.MenuDateFindToolStripMenuItem.Name = "MenuDateFindToolStripMenuItem";
            this.MenuDateFindToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.MenuDateFindToolStripMenuItem.Text = "按开始时间查询";
            this.MenuDateFindToolStripMenuItem.Click += new System.EventHandler(this.MenuDateFindToolStripMenuItem_Click);
            // 
            // 清空ToolStripMenuItem
            // 
            this.清空ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ClearInputToolStripMenuItem,
            this.ClearOutputToolStripMenuItem,
            this.LogoutToolStripMenuItem,
            this.ModifyPassToolStripMenuItem});
            this.清空ToolStripMenuItem.Name = "清空ToolStripMenuItem";
            this.清空ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.清空ToolStripMenuItem.Text = "选项";
            // 
            // ClearInputToolStripMenuItem
            // 
            this.ClearInputToolStripMenuItem.Name = "ClearInputToolStripMenuItem";
            this.ClearInputToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.ClearInputToolStripMenuItem.Text = "清空输入信息";
            this.ClearInputToolStripMenuItem.Click += new System.EventHandler(this.ClearInputToolStripMenuItem_Click);
            // 
            // ClearOutputToolStripMenuItem
            // 
            this.ClearOutputToolStripMenuItem.Name = "ClearOutputToolStripMenuItem";
            this.ClearOutputToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.ClearOutputToolStripMenuItem.Text = "清空显示信息";
            this.ClearOutputToolStripMenuItem.Click += new System.EventHandler(this.ClearOutputToolStripMenuItem_Click);
            // 
            // LogoutToolStripMenuItem
            // 
            this.LogoutToolStripMenuItem.Name = "LogoutToolStripMenuItem";
            this.LogoutToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.LogoutToolStripMenuItem.Text = "退出登录";
            this.LogoutToolStripMenuItem.Click += new System.EventHandler(this.LogoutToolStripMenuItem_Click);
            // 
            // ModifyPassToolStripMenuItem
            // 
            this.ModifyPassToolStripMenuItem.Name = "ModifyPassToolStripMenuItem";
            this.ModifyPassToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.ModifyPassToolStripMenuItem.Text = "修改管理员账户密码";
            this.ModifyPassToolStripMenuItem.Click += new System.EventHandler(this.ModifyPassToolStripMenuItem_Click);
            // 
            // NewMagName
            // 
            this.NewMagName.Location = new System.Drawing.Point(85, 255);
            this.NewMagName.Name = "NewMagName";
            this.NewMagName.Size = new System.Drawing.Size(130, 21);
            this.NewMagName.TabIndex = 7;
            // 
            // NewMagNum
            // 
            this.NewMagNum.Location = new System.Drawing.Point(85, 293);
            this.NewMagNum.Name = "NewMagNum";
            this.NewMagNum.Size = new System.Drawing.Size(130, 21);
            this.NewMagNum.TabIndex = 8;
            // 
            // NewMagVer
            // 
            this.NewMagVer.Location = new System.Drawing.Point(305, 258);
            this.NewMagVer.Name = "NewMagVer";
            this.NewMagVer.Size = new System.Drawing.Size(105, 21);
            this.NewMagVer.TabIndex = 9;
            // 
            // NewMagPrice
            // 
            this.NewMagPrice.Location = new System.Drawing.Point(305, 296);
            this.NewMagPrice.Name = "NewMagPrice";
            this.NewMagPrice.Size = new System.Drawing.Size(105, 21);
            this.NewMagPrice.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 258);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 11;
            this.label2.Text = "报刊名称：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 296);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 12;
            this.label3.Text = "报刊编号：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(234, 258);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 13;
            this.label4.Text = "报刊版本：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(234, 296);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 14;
            this.label5.Text = "报刊单价：";
            // 
            // MessageOutTab
            // 
            this.MessageOutTab.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.MessageOutTab.Location = new System.Drawing.Point(15, 367);
            this.MessageOutTab.Name = "MessageOutTab";
            this.MessageOutTab.Size = new System.Drawing.Size(1245, 204);
            this.MessageOutTab.TabIndex = 15;
            this.MessageOutTab.UseCompatibleStateImageBehavior = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("LiSu", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(572, 340);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(130, 24);
            this.label6.TabIndex = 16;
            this.label6.Text = "输出信息：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(661, 296);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 12);
            this.label8.TabIndex = 26;
            this.label8.Text = "地址：";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(661, 258);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 12);
            this.label9.TabIndex = 25;
            this.label9.Text = "权限：";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(440, 296);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 12);
            this.label10.TabIndex = 24;
            this.label10.Text = "密码：";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(440, 258);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 12);
            this.label11.TabIndex = 23;
            this.label11.Text = "用户名：";
            // 
            // NewUsrLoc
            // 
            this.NewUsrLoc.Location = new System.Drawing.Point(732, 296);
            this.NewUsrLoc.Name = "NewUsrLoc";
            this.NewUsrLoc.Size = new System.Drawing.Size(105, 21);
            this.NewUsrLoc.TabIndex = 22;
            // 
            // NewUsrAcc
            // 
            this.NewUsrAcc.Location = new System.Drawing.Point(732, 258);
            this.NewUsrAcc.Name = "NewUsrAcc";
            this.NewUsrAcc.Size = new System.Drawing.Size(105, 21);
            this.NewUsrAcc.TabIndex = 21;
            // 
            // NewUsrpwd
            // 
            this.NewUsrpwd.Location = new System.Drawing.Point(512, 293);
            this.NewUsrpwd.Name = "NewUsrpwd";
            this.NewUsrpwd.PasswordChar = '*';
            this.NewUsrpwd.Size = new System.Drawing.Size(130, 21);
            this.NewUsrpwd.TabIndex = 20;
            // 
            // NewUsrname
            // 
            this.NewUsrname.Location = new System.Drawing.Point(512, 255);
            this.NewUsrname.Name = "NewUsrname";
            this.NewUsrname.Size = new System.Drawing.Size(130, 21);
            this.NewUsrname.TabIndex = 19;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("LiSu", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label12.Location = new System.Drawing.Point(550, 25);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(130, 24);
            this.label12.TabIndex = 18;
            this.label12.Text = "用户信息：";
            // 
            // UsrListTab
            // 
            this.UsrListTab.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.UsrListTab.Location = new System.Drawing.Point(439, 57);
            this.UsrListTab.Name = "UsrListTab";
            this.UsrListTab.Size = new System.Drawing.Size(398, 186);
            this.UsrListTab.TabIndex = 17;
            this.UsrListTab.UseCompatibleStateImageBehavior = false;
            this.UsrListTab.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.UsrListTab_ColumnClick);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(1065, 276);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(65, 12);
            this.label14.TabIndex = 38;
            this.label14.Text = "开始时间：";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(1065, 249);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(65, 12);
            this.label15.TabIndex = 37;
            this.label15.Text = "订阅数量：";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(874, 296);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(65, 12);
            this.label16.TabIndex = 36;
            this.label16.Text = "报刊编号：";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(874, 258);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(53, 12);
            this.label17.TabIndex = 35;
            this.label17.Text = "用户名：";
            // 
            // NewMenuStartDate
            // 
            this.NewMenuStartDate.Location = new System.Drawing.Point(1136, 276);
            this.NewMenuStartDate.Name = "NewMenuStartDate";
            this.NewMenuStartDate.Size = new System.Drawing.Size(112, 21);
            this.NewMenuStartDate.TabIndex = 34;
            // 
            // NewMenuQuan
            // 
            this.NewMenuQuan.Location = new System.Drawing.Point(1136, 249);
            this.NewMenuQuan.Name = "NewMenuQuan";
            this.NewMenuQuan.Size = new System.Drawing.Size(112, 21);
            this.NewMenuQuan.TabIndex = 33;
            // 
            // NewMenuMnum
            // 
            this.NewMenuMnum.Location = new System.Drawing.Point(946, 293);
            this.NewMenuMnum.Name = "NewMenuMnum";
            this.NewMenuMnum.Size = new System.Drawing.Size(113, 21);
            this.NewMenuMnum.TabIndex = 32;
            // 
            // NewMenuUname
            // 
            this.NewMenuUname.Location = new System.Drawing.Point(946, 255);
            this.NewMenuUname.Name = "NewMenuUname";
            this.NewMenuUname.Size = new System.Drawing.Size(113, 21);
            this.NewMenuUname.TabIndex = 31;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("LiSu", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label18.Location = new System.Drawing.Point(984, 25);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(130, 24);
            this.label18.TabIndex = 30;
            this.label18.Text = "订单信息：";
            // 
            // MenuListTab
            // 
            this.MenuListTab.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.MenuListTab.Location = new System.Drawing.Point(873, 57);
            this.MenuListTab.Name = "MenuListTab";
            this.MenuListTab.Size = new System.Drawing.Size(398, 186);
            this.MenuListTab.TabIndex = 29;
            this.MenuListTab.UseCompatibleStateImageBehavior = false;
            this.MenuListTab.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.MenuListTab_ColumnClick);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(1065, 305);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(65, 12);
            this.label19.TabIndex = 42;
            this.label19.Text = "订阅时长：";
            // 
            // NewMenuDur
            // 
            this.NewMenuDur.Location = new System.Drawing.Point(1136, 302);
            this.NewMenuDur.Name = "NewMenuDur";
            this.NewMenuDur.Size = new System.Drawing.Size(112, 21);
            this.NewMenuDur.TabIndex = 41;
            // 
            // timelabel
            // 
            this.timelabel.AutoSize = true;
            this.timelabel.Location = new System.Drawing.Point(1095, 578);
            this.timelabel.Name = "timelabel";
            this.timelabel.Size = new System.Drawing.Size(35, 12);
            this.timelabel.TabIndex = 43;
            this.timelabel.Text = "label";
            // 
            // statuslable
            // 
            this.statuslable.AutoSize = true;
            this.statuslable.Location = new System.Drawing.Point(24, 578);
            this.statuslable.Name = "statuslable";
            this.statuslable.Size = new System.Drawing.Size(35, 12);
            this.statuslable.TabIndex = 44;
            this.statuslable.Text = "label";
            // 
            // Admin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1283, 599);
            this.Controls.Add(this.statuslable);
            this.Controls.Add(this.timelabel);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.NewMenuDur);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.NewMenuStartDate);
            this.Controls.Add(this.NewMenuQuan);
            this.Controls.Add(this.NewMenuMnum);
            this.Controls.Add(this.NewMenuUname);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.MenuListTab);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.NewUsrLoc);
            this.Controls.Add(this.NewUsrAcc);
            this.Controls.Add(this.NewUsrpwd);
            this.Controls.Add(this.NewUsrname);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.UsrListTab);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.MessageOutTab);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.NewMagPrice);
            this.Controls.Add(this.NewMagVer);
            this.Controls.Add(this.NewMagNum);
            this.Controls.Add(this.NewMagName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.MagListTab);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Admin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "报刊管理（管理员）";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Admin_Closing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView MagListTab;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 报刊信息ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AddMagToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DelMagToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ModifyMagToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FindMagToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 用户信息管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AddUserToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem DelUserToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem ModifyUserToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem FindUserToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 订单信息管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AddMenuToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem DelMenuToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem ModifyMenuToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem FindMenuToolStripMenuItem1;
        private System.Windows.Forms.TextBox NewMagName;
        private System.Windows.Forms.TextBox NewMagNum;
        private System.Windows.Forms.TextBox NewMagVer;
        private System.Windows.Forms.TextBox NewMagPrice;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ToolStripMenuItem MagNameFindToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MagNumFindToolStripMenuItem;
        private System.Windows.Forms.ListView MessageOutTab;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox NewUsrLoc;
        private System.Windows.Forms.TextBox NewUsrAcc;
        private System.Windows.Forms.TextBox NewUsrpwd;
        private System.Windows.Forms.TextBox NewUsrname;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ListView UsrListTab;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox NewMenuStartDate;
        private System.Windows.Forms.TextBox NewMenuQuan;
        private System.Windows.Forms.TextBox NewMenuMnum;
        private System.Windows.Forms.TextBox NewMenuUname;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.ListView MenuListTab;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox NewMenuDur;
        private System.Windows.Forms.ToolStripMenuItem UsrnameFindToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem UsrlocFindToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MenuUnameFindToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem MenuMnumFindToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MenuDateFindToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 清空ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ClearInputToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ClearOutputToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem LogoutToolStripMenuItem;
        private System.Windows.Forms.Label timelabel;
        private System.Windows.Forms.Label statuslable;
        private System.Windows.Forms.ToolStripMenuItem ModifyPassToolStripMenuItem;
    }
}