namespace MagazineOrdering
{
    partial class OrdinaryUser
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OrdinaryUser));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.个人信息管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ModifyPerInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DelPerInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.订单信息管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AddMenuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DelMenuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ModifyMenuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.查询订单ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MnumFindToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.StartDateFindToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.选项ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ClearinputToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ClearoutputToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LogoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.UsrListTab = new System.Windows.Forms.ListView();
            this.label19 = new System.Windows.Forms.Label();
            this.NewMenuDur = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.NewMenuStartDate = new System.Windows.Forms.TextBox();
            this.NewMenuQuan = new System.Windows.Forms.TextBox();
            this.NewMenuMnum = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.MenuListTab = new System.Windows.Forms.ListView();
            this.label8 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.NewUsrLoc = new System.Windows.Forms.TextBox();
            this.NewUsrpwd = new System.Windows.Forms.TextBox();
            this.NewUsrname = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.MessageOutTab = new System.Windows.Forms.ListView();
            this.label2 = new System.Windows.Forms.Label();
            this.MagListTab = new System.Windows.Forms.ListView();
            this.timelabel = new System.Windows.Forms.Label();
            this.statuslable = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.个人信息管理ToolStripMenuItem,
            this.订单信息管理ToolStripMenuItem,
            this.选项ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1117, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 个人信息管理ToolStripMenuItem
            // 
            this.个人信息管理ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ModifyPerInfoToolStripMenuItem,
            this.DelPerInfoToolStripMenuItem});
            this.个人信息管理ToolStripMenuItem.Name = "个人信息管理ToolStripMenuItem";
            this.个人信息管理ToolStripMenuItem.Size = new System.Drawing.Size(92, 21);
            this.个人信息管理ToolStripMenuItem.Text = "个人信息管理";
            // 
            // ModifyPerInfoToolStripMenuItem
            // 
            this.ModifyPerInfoToolStripMenuItem.Name = "ModifyPerInfoToolStripMenuItem";
            this.ModifyPerInfoToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.ModifyPerInfoToolStripMenuItem.Text = "修改个人基本信息";
            this.ModifyPerInfoToolStripMenuItem.Click += new System.EventHandler(this.ModifyPerInfoToolStripMenuItem_Click);
            // 
            // DelPerInfoToolStripMenuItem
            // 
            this.DelPerInfoToolStripMenuItem.Name = "DelPerInfoToolStripMenuItem";
            this.DelPerInfoToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.DelPerInfoToolStripMenuItem.Text = "删除账户";
            this.DelPerInfoToolStripMenuItem.Click += new System.EventHandler(this.DelPerInfoToolStripMenuItem_Click);
            // 
            // 订单信息管理ToolStripMenuItem
            // 
            this.订单信息管理ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddMenuToolStripMenuItem,
            this.DelMenuToolStripMenuItem,
            this.ModifyMenuToolStripMenuItem,
            this.查询订单ToolStripMenuItem});
            this.订单信息管理ToolStripMenuItem.Name = "订单信息管理ToolStripMenuItem";
            this.订单信息管理ToolStripMenuItem.Size = new System.Drawing.Size(92, 21);
            this.订单信息管理ToolStripMenuItem.Text = "订单信息管理";
            // 
            // AddMenuToolStripMenuItem
            // 
            this.AddMenuToolStripMenuItem.Name = "AddMenuToolStripMenuItem";
            this.AddMenuToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.AddMenuToolStripMenuItem.Text = "添加订单";
            this.AddMenuToolStripMenuItem.Click += new System.EventHandler(this.AddMenuToolStripMenuItem_Click);
            // 
            // DelMenuToolStripMenuItem
            // 
            this.DelMenuToolStripMenuItem.Name = "DelMenuToolStripMenuItem";
            this.DelMenuToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.DelMenuToolStripMenuItem.Text = "删除订单";
            this.DelMenuToolStripMenuItem.Click += new System.EventHandler(this.DelMenuToolStripMenuItem_Click);
            // 
            // ModifyMenuToolStripMenuItem
            // 
            this.ModifyMenuToolStripMenuItem.Name = "ModifyMenuToolStripMenuItem";
            this.ModifyMenuToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.ModifyMenuToolStripMenuItem.Text = "修改订单";
            this.ModifyMenuToolStripMenuItem.Click += new System.EventHandler(this.ModifyMenuToolStripMenuItem_Click);
            // 
            // 查询订单ToolStripMenuItem
            // 
            this.查询订单ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MnumFindToolStripMenuItem,
            this.StartDateFindToolStripMenuItem});
            this.查询订单ToolStripMenuItem.Name = "查询订单ToolStripMenuItem";
            this.查询订单ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.查询订单ToolStripMenuItem.Text = "查询订单";
            // 
            // MnumFindToolStripMenuItem
            // 
            this.MnumFindToolStripMenuItem.Name = "MnumFindToolStripMenuItem";
            this.MnumFindToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.MnumFindToolStripMenuItem.Text = "按报刊编号查询";
            this.MnumFindToolStripMenuItem.Click += new System.EventHandler(this.MnumFindToolStripMenuItem_Click);
            // 
            // StartDateFindToolStripMenuItem
            // 
            this.StartDateFindToolStripMenuItem.Name = "StartDateFindToolStripMenuItem";
            this.StartDateFindToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.StartDateFindToolStripMenuItem.Text = "按开始时间查询";
            this.StartDateFindToolStripMenuItem.Click += new System.EventHandler(this.StartDateFindToolStripMenuItem_Click);
            // 
            // 选项ToolStripMenuItem
            // 
            this.选项ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ClearinputToolStripMenuItem,
            this.ClearoutputToolStripMenuItem,
            this.LogoutToolStripMenuItem});
            this.选项ToolStripMenuItem.Name = "选项ToolStripMenuItem";
            this.选项ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.选项ToolStripMenuItem.Text = "选项";
            // 
            // ClearinputToolStripMenuItem
            // 
            this.ClearinputToolStripMenuItem.Name = "ClearinputToolStripMenuItem";
            this.ClearinputToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.ClearinputToolStripMenuItem.Text = "清空输入信息";
            this.ClearinputToolStripMenuItem.Click += new System.EventHandler(this.ClearinputToolStripMenuItem_Click);
            // 
            // ClearoutputToolStripMenuItem
            // 
            this.ClearoutputToolStripMenuItem.Name = "ClearoutputToolStripMenuItem";
            this.ClearoutputToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.ClearoutputToolStripMenuItem.Text = "清空输出信息";
            this.ClearoutputToolStripMenuItem.Click += new System.EventHandler(this.ClearoutputToolStripMenuItem_Click);
            // 
            // LogoutToolStripMenuItem
            // 
            this.LogoutToolStripMenuItem.Name = "LogoutToolStripMenuItem";
            this.LogoutToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.LogoutToolStripMenuItem.Text = "退出登录";
            this.LogoutToolStripMenuItem.Click += new System.EventHandler(this.LogoutToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("LiSu", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(192, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 24);
            this.label1.TabIndex = 6;
            this.label1.Text = "个人信息：";
            // 
            // UsrListTab
            // 
            this.UsrListTab.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.UsrListTab.Location = new System.Drawing.Point(21, 65);
            this.UsrListTab.Name = "UsrListTab";
            this.UsrListTab.Size = new System.Drawing.Size(498, 186);
            this.UsrListTab.TabIndex = 7;
            this.UsrListTab.UseCompatibleStateImageBehavior = false;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(877, 304);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(65, 12);
            this.label19.TabIndex = 54;
            this.label19.Text = "订阅时长：";
            // 
            // NewMenuDur
            // 
            this.NewMenuDur.Location = new System.Drawing.Point(948, 301);
            this.NewMenuDur.Name = "NewMenuDur";
            this.NewMenuDur.Size = new System.Drawing.Size(142, 21);
            this.NewMenuDur.TabIndex = 53;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(607, 304);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(65, 12);
            this.label14.TabIndex = 52;
            this.label14.Text = "开始时间：";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(877, 263);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(65, 12);
            this.label15.TabIndex = 51;
            this.label15.Text = "订阅数量：";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(606, 266);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(65, 12);
            this.label16.TabIndex = 50;
            this.label16.Text = "报刊编号：";
            // 
            // NewMenuStartDate
            // 
            this.NewMenuStartDate.Location = new System.Drawing.Point(678, 304);
            this.NewMenuStartDate.Name = "NewMenuStartDate";
            this.NewMenuStartDate.Size = new System.Drawing.Size(151, 21);
            this.NewMenuStartDate.TabIndex = 48;
            // 
            // NewMenuQuan
            // 
            this.NewMenuQuan.Location = new System.Drawing.Point(948, 263);
            this.NewMenuQuan.Name = "NewMenuQuan";
            this.NewMenuQuan.Size = new System.Drawing.Size(142, 21);
            this.NewMenuQuan.TabIndex = 47;
            // 
            // NewMenuMnum
            // 
            this.NewMenuMnum.Location = new System.Drawing.Point(678, 263);
            this.NewMenuMnum.Name = "NewMenuMnum";
            this.NewMenuMnum.Size = new System.Drawing.Size(151, 21);
            this.NewMenuMnum.TabIndex = 46;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("LiSu", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label18.Location = new System.Drawing.Point(777, 38);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(130, 24);
            this.label18.TabIndex = 44;
            this.label18.Text = "订单信息：";
            // 
            // MenuListTab
            // 
            this.MenuListTab.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.MenuListTab.Location = new System.Drawing.Point(607, 65);
            this.MenuListTab.Name = "MenuListTab";
            this.MenuListTab.Size = new System.Drawing.Size(483, 186);
            this.MenuListTab.TabIndex = 43;
            this.MenuListTab.UseCompatibleStateImageBehavior = false;
            this.MenuListTab.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.MenuListTab_ColumnClick);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(367, 263);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 12);
            this.label8.TabIndex = 62;
            this.label8.Text = "地址：";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(194, 263);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 12);
            this.label10.TabIndex = 60;
            this.label10.Text = "密码：";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(7, 263);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 12);
            this.label11.TabIndex = 59;
            this.label11.Text = "用户名：";
            // 
            // NewUsrLoc
            // 
            this.NewUsrLoc.Location = new System.Drawing.Point(414, 257);
            this.NewUsrLoc.Name = "NewUsrLoc";
            this.NewUsrLoc.Size = new System.Drawing.Size(105, 21);
            this.NewUsrLoc.TabIndex = 58;
            // 
            // NewUsrpwd
            // 
            this.NewUsrpwd.Location = new System.Drawing.Point(241, 257);
            this.NewUsrpwd.Name = "NewUsrpwd";
            this.NewUsrpwd.PasswordChar = '*';
            this.NewUsrpwd.Size = new System.Drawing.Size(108, 21);
            this.NewUsrpwd.TabIndex = 56;
            // 
            // NewUsrname
            // 
            this.NewUsrname.Location = new System.Drawing.Point(66, 257);
            this.NewUsrname.Name = "NewUsrname";
            this.NewUsrname.Size = new System.Drawing.Size(109, 21);
            this.NewUsrname.TabIndex = 55;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("LiSu", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(777, 332);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(130, 24);
            this.label6.TabIndex = 64;
            this.label6.Text = "输出信息：";
            // 
            // MessageOutTab
            // 
            this.MessageOutTab.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.MessageOutTab.Location = new System.Drawing.Point(593, 359);
            this.MessageOutTab.Name = "MessageOutTab";
            this.MessageOutTab.Size = new System.Drawing.Size(512, 138);
            this.MessageOutTab.TabIndex = 63;
            this.MessageOutTab.UseCompatibleStateImageBehavior = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("LiSu", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(162, 284);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(130, 24);
            this.label2.TabIndex = 66;
            this.label2.Text = "报刊信息：";
            // 
            // MagListTab
            // 
            this.MagListTab.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.MagListTab.Location = new System.Drawing.Point(21, 311);
            this.MagListTab.Name = "MagListTab";
            this.MagListTab.Size = new System.Drawing.Size(506, 186);
            this.MagListTab.TabIndex = 65;
            this.MagListTab.UseCompatibleStateImageBehavior = false;
            this.MagListTab.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.MagListTab_ColumnClick);
            // 
            // timelabel
            // 
            this.timelabel.AutoSize = true;
            this.timelabel.Location = new System.Drawing.Point(946, 509);
            this.timelabel.Name = "timelabel";
            this.timelabel.Size = new System.Drawing.Size(35, 12);
            this.timelabel.TabIndex = 67;
            this.timelabel.Text = "label";
            // 
            // statuslable
            // 
            this.statuslable.AutoSize = true;
            this.statuslable.Location = new System.Drawing.Point(25, 509);
            this.statuslable.Name = "statuslable";
            this.statuslable.Size = new System.Drawing.Size(35, 12);
            this.statuslable.TabIndex = 68;
            this.statuslable.Text = "label";
            // 
            // OrdinaryUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1117, 530);
            this.Controls.Add(this.statuslable);
            this.Controls.Add(this.timelabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.MagListTab);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.MessageOutTab);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.NewUsrLoc);
            this.Controls.Add(this.NewUsrpwd);
            this.Controls.Add(this.NewUsrname);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.NewMenuDur);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.NewMenuStartDate);
            this.Controls.Add(this.NewMenuQuan);
            this.Controls.Add(this.NewMenuMnum);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.MenuListTab);
            this.Controls.Add(this.UsrListTab);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OrdinaryUser";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "报刊管理";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Admin_Closing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 个人信息管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ModifyPerInfoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DelPerInfoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 订单信息管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AddMenuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DelMenuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ModifyMenuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 查询订单ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 选项ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ClearinputToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ClearoutputToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem LogoutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MnumFindToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem StartDateFindToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView UsrListTab;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox NewMenuDur;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox NewMenuStartDate;
        private System.Windows.Forms.TextBox NewMenuQuan;
        private System.Windows.Forms.TextBox NewMenuMnum;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.ListView MenuListTab;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox NewUsrLoc;
        private System.Windows.Forms.TextBox NewUsrpwd;
        private System.Windows.Forms.TextBox NewUsrname;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ListView MessageOutTab;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListView MagListTab;
        private System.Windows.Forms.Label timelabel;
        private System.Windows.Forms.Label statuslable;
    }
}