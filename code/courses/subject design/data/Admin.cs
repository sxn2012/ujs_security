using System;
using System.Threading;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace MagazineOrdering
{
    public partial class Admin : Form
    {
        private Login_Form lf = null;
        private int currentCol = -1;//当前排序的列
        private bool sort;//升序或降序的标志
        private void Empty_all()//清空输入框
        {
            //左边的输入框
            NewMagName.Clear();
            NewMagNum.Clear();
            NewMagVer.Clear();
            NewMagPrice.Clear();
            //中间的输入框
            NewUsrAcc.Clear();
            NewUsrLoc.Clear();
            NewUsrname.Clear();
            NewUsrpwd.Clear();
            //右边的输入框
            NewMenuDur.Clear();
            NewMenuMnum.Clear();
            NewMenuQuan.Clear();
            NewMenuStartDate.Clear();
            NewMenuUname.Clear();
            
        }
        public void UpdateInfo()//更新显示信息
        {
           
            //清空显示
            MagListTab.Clear();
            UsrListTab.Clear();
            MenuListTab.Clear();
            string connString = "Data Source =.; Initial Catalog = Magazine_Ordering; Integrated Security = True";
            SqlConnection conn = new SqlConnection(connString);
            String sql = "select * from Magazine";
            try
            {
                //左边的要显示的数据读入
                conn.Open();
                SqlCommand comm = new SqlCommand(sql, conn);
                SqlDataReader reader = comm.ExecuteReader();
                MagListTab.Columns.Add("报刊名称", 160, HorizontalAlignment.Center);
                MagListTab.Columns.Add("报刊编号", 100, HorizontalAlignment.Center);
                MagListTab.Columns.Add("报刊版本", 100, HorizontalAlignment.Center);
                MagListTab.Columns.Add("报刊单价", 100, HorizontalAlignment.Center);
                while (reader.Read())
                {
                    ListViewItem item = new ListViewItem();
                    item.SubItems.Clear();
                    item.SubItems[0].Text=reader[0].ToString().Trim();
                    item.SubItems.Add(reader[1].ToString().Trim());
                    item.SubItems.Add(reader[2].ToString().Trim());
                    
                    //string price_ = String.Format("{0:F}", reader[3].ToString().Trim());
                    //item.SubItems.Add(price_);
                    item.SubItems.Add(reader[3].ToString().Trim());
                    MagListTab.Items.Add(item);
                }
                conn.Close();
                //中间的要显示的数据读入
                sql = "select * from Userlist";
                conn.Open();
                comm = new SqlCommand(sql, conn);
                reader = comm.ExecuteReader();
                UsrListTab.Columns.Add("用户名", 100, HorizontalAlignment.Center);
                UsrListTab.Columns.Add("密码", 100, HorizontalAlignment.Center);
                UsrListTab.Columns.Add("权限", 100, HorizontalAlignment.Center);
                UsrListTab.Columns.Add("地址", 160, HorizontalAlignment.Center);
                while (reader.Read())
                {
                    ListViewItem item = new ListViewItem();
                    item.SubItems.Clear();
                    item.SubItems[0].Text = reader[0].ToString().Trim();
                    item.SubItems.Add(reader[1].ToString().Trim());
                    item.SubItems.Add(reader[2].ToString().Trim());
                    item.SubItems.Add(reader[3].ToString().Trim());
                    UsrListTab.Items.Add(item);
                }
                conn.Close();
                //右边要显示的数据读入
                sql = "select Menulist.*,MPrice from Menulist,Magazine where Menulist.Mnum=Magazine.Mnum";
                conn.Open();
                comm = new SqlCommand(sql, conn);
                reader = comm.ExecuteReader();
                MenuListTab.Columns.Add("订单编号", 100, HorizontalAlignment.Center);
                MenuListTab.Columns.Add("用户名", 100, HorizontalAlignment.Center);
                MenuListTab.Columns.Add("报刊编号", 100, HorizontalAlignment.Center);
                MenuListTab.Columns.Add("订阅数量", 100, HorizontalAlignment.Center);
                MenuListTab.Columns.Add("开始时间", 160, HorizontalAlignment.Center);
                MenuListTab.Columns.Add("订阅时长", 100, HorizontalAlignment.Center);
                MenuListTab.Columns.Add("订单总价", 140, HorizontalAlignment.Center);
                int i = 0;
                while (reader.Read())
                {
                    ListViewItem item = new ListViewItem();
                    item.SubItems.Clear();
                    i++;
                    item.SubItems[0].Text = i.ToString().Trim();
                    item.SubItems.Add(reader[0].ToString().Trim());
                    item.SubItems.Add(reader[1].ToString().Trim());
                    item.SubItems.Add(reader[2].ToString().Trim());
                    item.SubItems.Add(reader[3].ToString().Trim());
                    item.SubItems.Add(reader[4].ToString().Trim());
                    int num = int.Parse(reader[2].ToString().Trim());
                    double price = double.Parse(reader[5].ToString().Trim());
                    int t = int.Parse(reader[4].ToString().Trim());
                    double totcost = num * price *t;
                    string coststr = String.Format("{0:F}", totcost);
                    item.SubItems.Add(coststr.ToString().Trim());
                    MenuListTab.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "数据库异常！", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                conn.Close();
            }
        }
        public Admin(Login_Form f)//构造函数
        {
            
            InitializeComponent();
            this.timelabel.Text = "";
            this.statuslable.Text = "就绪";
            this.lf = f;
            //创建计时器线程
            new Thread(
                () =>
            {
                while (true)
                {
                    try 
                    {
                        timelabel.BeginInvoke(new MethodInvoker(() => timelabel.Text = DateTime.Now.ToString())); 
                    }
                    catch(Exception ex) 
                    {
                        //MessageBox.Show(ex.Message, "计时器异常！", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    Thread.Sleep(1000);
                }
            }
            ) 
            { 
                IsBackground = true
            }.Start();
            //左边的显示窗口
            MagListTab.GridLines = true;//表格是否显示网格线
            MagListTab.FullRowSelect = true;//是否选中整行
            MagListTab.View = View.Details;//设置显示方式
            MagListTab.Scrollable = true;//是否自动显示滚动条
            MagListTab.MultiSelect = false;//是否可以选择多行
            //中间的显示窗口
            UsrListTab.GridLines = true;//表格是否显示网格线
            UsrListTab.FullRowSelect = true;//是否选中整行
            UsrListTab.View = View.Details;//设置显示方式
            UsrListTab.Scrollable = true;//是否自动显示滚动条
            UsrListTab.MultiSelect = false;//是否可以选择多行
            //右边的显示窗口
            MenuListTab.GridLines = true;//表格是否显示网格线
            MenuListTab.FullRowSelect = true;//是否选中整行
            MenuListTab.View = View.Details;//设置显示方式
            MenuListTab.Scrollable = true;//是否自动显示滚动条
            MenuListTab.MultiSelect = false;//是否可以选择多行
            UpdateInfo();
        }
        protected override void WndProc(ref Message msg)//用于改变关闭窗口的消息处理
        {
            
            const int WM_SYSCOMMAND = 0x0112;
            
            const int SC_CLOSE = 0xF060;

            if (msg.Msg == WM_SYSCOMMAND && ((int)msg.WParam == SC_CLOSE))
            {
                // 点击winform右上关闭按钮 
                DialogResult result = MessageBox.Show("确定要退出登陆吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                if (result == DialogResult.OK)
                {
                    
                    this.Close();

                }
                else
                    return;//阻止了窗体关闭
            }
            base.WndProc(ref msg);
        }
        private void Admin_Closing(object sender, FormClosingEventArgs e)//关闭窗口消息
        {
            this.lf.Visible = true;
            this.lf.Clear_all();
            this.lf.UpdateVerifyCode();
        }

        private void DelMagToolStripMenuItem_Click(object sender, EventArgs e)//删除报刊
        {
            if (MagListTab.SelectedItems.Count <= 0)
            {
                MessageBox.Show("请选择至少一条信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //throw new Exception("请选择至少一条信息！");
                return;
            }
            foreach (ListViewItem lvi in MagListTab.SelectedItems)
            {
                string str = lvi.SubItems[1].Text.Trim();
                string connString = "Data Source =.; Initial Catalog = Magazine_Ordering; Integrated Security = True";
                SqlConnection conn = new SqlConnection(connString);
                String sql = String.Format("delete from Magazine where Mnum='{0}'", str);
                try
                {
                    conn.Open();
                    SqlCommand comm = new SqlCommand(sql, conn);
                    int num = (int)comm.ExecuteNonQuery();
                    if (num > 0)
                    {

                        MessageBox.Show("删除成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    else
                        throw new Exception("数据库异常!");
                        //MessageBox.Show("删除失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "删除失败！", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                finally
                {
                    conn.Close();
                }
                UpdateInfo();
            }
        }

        private void AddMagToolStripMenuItem_Click(object sender, EventArgs e)//增加报刊
        {
            string connString = "Data Source =.; Initial Catalog = Magazine_Ordering; Integrated Security = True";
            SqlConnection conn = new SqlConnection(connString);
            string mname, mnum, mver, mprice;
            mname = NewMagName.Text.Trim();
            mnum = NewMagNum.Text.Trim();
            mver = NewMagVer.Text.Trim();
            mprice = NewMagPrice.Text.Trim();
            String sql = String.Format("select count(*) from Magazine where Mnum='{0}' ", mnum);
            try
            {
                if (mname == null || mnum == null || mver == null || mprice == null)
                    throw new Exception("输入数据不能为空！");
                if (mname == "" || mnum == "" || mver == "" || mprice == "")
                    throw new Exception("输入数据不能为空！");
                conn.Open();
                SqlCommand comm = new SqlCommand(sql, conn);
                int num = (int)comm.ExecuteScalar();
                if (num > 0)
                {
                    //MessageBox.Show("添加失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Empty_all();
                    throw new Exception("报刊信息已存在!");
                    return;

                }

                else
                {
                    conn.Close();
                    sql = String.Format("insert into magazine(Mname,Mnum,MVersion,MPrice) values('{0}','{1}','{2}','{3}')", mname,mnum,mver,mprice);
                    conn.Open();
                    comm = new SqlCommand(sql, conn);
                    num = 0;
                    num = comm.ExecuteNonQuery();
                    if (num > 0)

                        MessageBox.Show("添加成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);


                    else
                        throw new Exception("数据库异常！");
                        //MessageBox.Show("添加失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "添加失败！", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                conn.Close();
                Empty_all();
                UpdateInfo();
            }
        }

        private void ModifyMagToolStripMenuItem_Click(object sender, EventArgs e)//修改报刊
        {
            if (MagListTab.SelectedItems.Count != 1)
            {
                MessageBox.Show("请选择一条信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //throw new Exception("请选择一条信息！");
                return;
            }
            foreach (ListViewItem lvi in MagListTab.SelectedItems)
            {
                string str = lvi.SubItems[1].Text.Trim();
                string mname, mnum, mver, mprice;
                mname = NewMagName.Text.Trim();
                mnum = NewMagNum.Text.Trim();
                mver = NewMagVer.Text.Trim();
                mprice = NewMagPrice.Text.Trim();
                string connString = "Data Source =.; Initial Catalog = Magazine_Ordering; Integrated Security = True";
                SqlConnection conn = new SqlConnection(connString);
                String sql = String.Format("update Magazine set Mname='{1}', Mnum='{2}', MVersion='{3}', Mprice='{4}'  where Mnum='{0}'", str,mname,mnum,mver,mprice);
                try
                {
                    if (mname == null || mnum == null || mver == null || mprice == null)
                        throw new Exception("输入数据不能为空！");
                    if (mname == "" || mnum == "" || mver == "" || mprice == "")
                        throw new Exception("输入数据不能为空！");
                    conn.Open();
                    SqlCommand comm = new SqlCommand(sql, conn);
                    int num = (int)comm.ExecuteNonQuery();
                    if (num > 0)
                    {

                        MessageBox.Show("更新成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    else
                        throw new Exception("数据库异常！");
                        //MessageBox.Show("更新失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "更新失败！", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                finally
                {
                    conn.Close();
                }
                UpdateInfo();
                Empty_all();
            }
        }

        private void MagNameFindToolStripMenuItem_Click(object sender, EventArgs e)//按名称查找报刊
        {
            MessageOutTab.GridLines = true;//表格是否显示网格线
            MessageOutTab.FullRowSelect = false;//是否选中整行
            MessageOutTab.View = View.Details;//设置显示方式
            MessageOutTab.Scrollable = true;//是否自动显示滚动条
            MessageOutTab.MultiSelect = false;//是否可以选择多行
            MessageOutTab.Clear();
            string connString = "Data Source =.; Initial Catalog = Magazine_Ordering; Integrated Security = True";
            SqlConnection conn = new SqlConnection(connString);
            string mname = NewMagName.Text.Trim();
            if (mname == null || mname == "") mname = "%";
            String sql;
            if(mname.IndexOf('%')<0&&mname.IndexOf('_')<0)
                sql=String.Format("select * from magazine where Mname='{0}'", mname);
            else
                sql = String.Format("select * from magazine where Mname like '{0}'", mname);
            try
            {
                conn.Open();
                SqlCommand comm = new SqlCommand(sql, conn);
                SqlDataReader reader = comm.ExecuteReader();

                MessageOutTab.Columns.Add("报刊名称", 420, HorizontalAlignment.Center);
                MessageOutTab.Columns.Add("报刊编号", 320, HorizontalAlignment.Center);
                MessageOutTab.Columns.Add("报刊版本", 320, HorizontalAlignment.Center);
                MessageOutTab.Columns.Add("报刊单价", 320, HorizontalAlignment.Center);

                while (reader.Read())
                {
                    ListViewItem item = new ListViewItem();
                    item.SubItems.Clear();
                    item.SubItems[0].Text = reader[0].ToString().Trim();
                    item.SubItems.Add(reader[1].ToString().Trim());
                    item.SubItems.Add(reader[2].ToString().Trim());
                    item.SubItems.Add(reader[3].ToString().Trim());
                    MessageOutTab.Items.Add(item);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "查找失败！", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                conn.Close();
                Empty_all();
            }
        }

        private void MagNumFindToolStripMenuItem_Click(object sender, EventArgs e)//按编号查找报刊
        {
            MessageOutTab.GridLines = true;//表格是否显示网格线
            MessageOutTab.FullRowSelect = false;//是否选中整行
            MessageOutTab.View = View.Details;//设置显示方式
            MessageOutTab.Scrollable = true;//是否自动显示滚动条
            MessageOutTab.MultiSelect = false;//是否可以选择多行
            MessageOutTab.Clear();
            string connString = "Data Source =.; Initial Catalog = Magazine_Ordering; Integrated Security = True";
            SqlConnection conn = new SqlConnection(connString);
            string mnum = NewMagNum.Text.Trim();
            if (mnum == null || mnum == "") mnum = "%";
            String sql ;
            if(mnum.IndexOf('%')<0&&mnum.IndexOf('_')<0)
                sql= String.Format("select * from magazine where Mnum='{0}'", mnum);
            else
                sql = String.Format("select * from magazine where Mnum like '{0}'", mnum);
            try
            {
                conn.Open();
                SqlCommand comm = new SqlCommand(sql, conn);
                SqlDataReader reader = comm.ExecuteReader();

                MessageOutTab.Columns.Add("报刊名称", 420, HorizontalAlignment.Center);
                MessageOutTab.Columns.Add("报刊编号", 320, HorizontalAlignment.Center);
                MessageOutTab.Columns.Add("报刊版本", 320, HorizontalAlignment.Center);
                MessageOutTab.Columns.Add("报刊单价", 320, HorizontalAlignment.Center);

                while (reader.Read())
                {
                    ListViewItem item = new ListViewItem();
                    item.SubItems.Clear();
                    item.SubItems[0].Text = reader[0].ToString().Trim();
                    item.SubItems.Add(reader[1].ToString().Trim());
                    item.SubItems.Add(reader[2].ToString().Trim());
                    item.SubItems.Add(reader[3].ToString().Trim());
                    MessageOutTab.Items.Add(item);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "查找失败！", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                conn.Close();
                Empty_all();
            }
        }

        private void ClearInputToolStripMenuItem_Click(object sender, EventArgs e)//清空输入
        {
            Empty_all();
        }

        private void ClearOutputToolStripMenuItem_Click(object sender, EventArgs e)//清空输出
        {
            MessageOutTab.Clear();
        }

        private void LogoutToolStripMenuItem_Click(object sender, EventArgs e)//退出登录
        {
            DialogResult result = MessageBox.Show("确定要退出登陆吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
            if (result == DialogResult.OK)
            {

                this.Close();

            }
            else
                return;
        }

        private void AddUserToolStripMenuItem1_Click(object sender, EventArgs e)//添加用户
        {
            string connString = "Data Source =.; Initial Catalog = Magazine_Ordering; Integrated Security = True";
            SqlConnection conn = new SqlConnection(connString);
            string uname, upass, uacc, uloc;
            uname = NewUsrname.Text.Trim();
            upass = NewUsrpwd.Text.Trim();
            uacc = NewUsrAcc.Text.Trim();
            uloc = NewUsrLoc.Text.Trim();
            String sql = String.Format("select count(*) from Userlist where username='{0}' ", uname);
            try
            {
                if (uacc == "0")
                    throw new Exception("无法添加管理员账户!");
                if (uacc != "3")
                    throw new Exception("普通用户的权限必须为3！");
                if(uname==null||upass==null||uacc==null||uloc==null)
                    throw new Exception("输入数据不能为空！");
                if(uname==""||upass==""||uacc==""||uloc=="")
                    throw new Exception("输入数据不能为空！");
                conn.Open();
                SqlCommand comm = new SqlCommand(sql, conn);
                int num = (int)comm.ExecuteScalar();
                if (num > 0)
                {
                    //MessageBox.Show("添加失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Empty_all();
                    throw new Exception("用户已存在！");
                    return;

                }

                else
                {
                    conn.Close();
                    sql = String.Format("insert into userlist(username,password,access,location) values('{0}','{1}','{2}','{3}')", uname,upass,uacc,uloc);
                    conn.Open();
                    comm = new SqlCommand(sql, conn);
                    num = 0;
                    num = comm.ExecuteNonQuery();
                    if (num > 0)

                        MessageBox.Show("添加成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);


                    else
                        throw new Exception("数据库异常！");
                        //MessageBox.Show("添加失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "添加失败！", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                conn.Close();
                Empty_all();
                UpdateInfo();
            }
        }

        private void DelUserToolStripMenuItem1_Click(object sender, EventArgs e)//删除用户
        {
            if (UsrListTab.SelectedItems.Count <= 0)
            {
                //throw new Exception("请选择至少一条信息！");
                MessageBox.Show("请选择至少一条信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            foreach (ListViewItem lvi in UsrListTab.SelectedItems)
            {
                string str = lvi.SubItems[0].Text.Trim();
                string ac = lvi.SubItems[2].Text.Trim();
                string connString = "Data Source =.; Initial Catalog = Magazine_Ordering; Integrated Security = True";
                SqlConnection conn = new SqlConnection(connString);
                String sql = String.Format("delete from Userlist where username='{0}'", str);
                try
                {
                    if (ac == "0")
                        throw new Exception("无法删除管理员账户！");
                    conn.Open();
                    SqlCommand comm = new SqlCommand(sql, conn);
                    int num = (int)comm.ExecuteNonQuery();
                    if (num > 0)
                    {
                        MessageBox.Show("删除成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    else
                        throw new Exception("数据库异常！");
                        //MessageBox.Show("删除失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "删除失败！", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                finally
                {
                    conn.Close();
                }
                UpdateInfo();
            }
        }

        private void ModifyUserToolStripMenuItem1_Click(object sender, EventArgs e)//修改用户
        {
            if (UsrListTab.SelectedItems.Count != 1)
            {
                //throw new Exception("请选择一条信息！");
                MessageBox.Show("请选择一条信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            foreach (ListViewItem lvi in UsrListTab.SelectedItems)
            {
                string str = lvi.SubItems[0].Text.Trim();
                string ac = lvi.SubItems[2].Text.Trim();
                string uname, upass, uacc, uloc;
                uname = NewUsrname.Text.Trim();
                upass = NewUsrpwd.Text.Trim();
                uacc = NewUsrAcc.Text.Trim();
                uloc = NewUsrLoc.Text.Trim();
                string connString = "Data Source =.; Initial Catalog = Magazine_Ordering; Integrated Security = True";
                SqlConnection conn = new SqlConnection(connString);
                String sql = String.Format("update Userlist set username='{1}', password='{2}', access='{3}', location='{4}'  where username='{0}'", str, uname,upass,uacc,uloc);
                try
                {
                    if (ac == "0")
                        throw new Exception("无法修改管理员账户！");
                    if (uacc == "0")
                        throw new Exception("无法将普通用户修改为管理员账户！");
                    if (uacc != "3")
                        throw new Exception("普通用户的权限必须为3！");
                    if (uname == null || upass == null || uacc == null || uloc == null)
                        throw new Exception("输入数据不能为空！");
                    if (uname == "" || upass == "" || uacc == "" || uloc == "")
                        throw new Exception("输入数据不能为空！");
                    conn.Open();
                    SqlCommand comm = new SqlCommand(sql, conn);
                    int num = (int)comm.ExecuteNonQuery();
                    if (num > 0)
                    {
                        MessageBox.Show("更新成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    else
                        throw new Exception("数据库异常！");
                        //MessageBox.Show("更新失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "更新失败！", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                finally
                {
                    conn.Close();
                }
                UpdateInfo();
                Empty_all();
            }
        }

        private void UsrnameFindToolStripMenuItem_Click(object sender, EventArgs e)//按用户名查找用户
        {
            MessageOutTab.GridLines = true;//表格是否显示网格线
            MessageOutTab.FullRowSelect = false;//是否选中整行
            MessageOutTab.View = View.Details;//设置显示方式
            MessageOutTab.Scrollable = true;//是否自动显示滚动条
            MessageOutTab.MultiSelect = false;//是否可以选择多行
            MessageOutTab.Clear();
            string connString = "Data Source =.; Initial Catalog = Magazine_Ordering; Integrated Security = True";
            SqlConnection conn = new SqlConnection(connString);
            string uname = NewUsrname.Text.Trim();
            if (uname == null || uname == "") uname = "%";
            String sql ;
            if(uname.IndexOf('%')<0&&uname.IndexOf('_')<0)
                sql= String.Format("select * from Userlist where username='{0}'", uname);
            else
                sql = String.Format("select * from Userlist where username like '{0}'", uname);
            try
            {
                conn.Open();
                SqlCommand comm = new SqlCommand(sql, conn);
                SqlDataReader reader = comm.ExecuteReader();

                MessageOutTab.Columns.Add("用户名", 320, HorizontalAlignment.Center);
                MessageOutTab.Columns.Add("密码", 320, HorizontalAlignment.Center);
                MessageOutTab.Columns.Add("权限", 320, HorizontalAlignment.Center);
                MessageOutTab.Columns.Add("地址", 420, HorizontalAlignment.Center);

                while (reader.Read())
                {
                    ListViewItem item = new ListViewItem();
                    item.SubItems.Clear();
                    item.SubItems[0].Text = reader[0].ToString().Trim();
                    item.SubItems.Add(reader[1].ToString().Trim());
                    item.SubItems.Add(reader[2].ToString().Trim());
                    item.SubItems.Add(reader[3].ToString().Trim());
                    MessageOutTab.Items.Add(item);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "查找失败！", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                conn.Close();
                Empty_all();
            }
        }

        private void UsrlocFindToolStripMenuItem_Click(object sender, EventArgs e)//按地址查找用户
        {
            MessageOutTab.GridLines = true;//表格是否显示网格线
            MessageOutTab.FullRowSelect = false;//是否选中整行
            MessageOutTab.View = View.Details;//设置显示方式
            MessageOutTab.Scrollable = true;//是否自动显示滚动条
            MessageOutTab.MultiSelect = false;//是否可以选择多行
            MessageOutTab.Clear();
            string connString = "Data Source =.; Initial Catalog = Magazine_Ordering; Integrated Security = True";
            SqlConnection conn = new SqlConnection(connString);
            string uloc = NewUsrLoc.Text.Trim();
            if (uloc == null || uloc == "") uloc = "%";
            String sql ;
            if(uloc.IndexOf('%')<0&&uloc.IndexOf('_')<0)
                sql= String.Format("select * from Userlist where location='{0}'", uloc);
            else
                sql = String.Format("select * from Userlist where location like '{0}'", uloc);
            try
            {
                conn.Open();
                SqlCommand comm = new SqlCommand(sql, conn);
                SqlDataReader reader = comm.ExecuteReader();

                MessageOutTab.Columns.Add("用户名", 320, HorizontalAlignment.Center);
                MessageOutTab.Columns.Add("密码", 320, HorizontalAlignment.Center);
                MessageOutTab.Columns.Add("权限", 320, HorizontalAlignment.Center);
                MessageOutTab.Columns.Add("地址", 420, HorizontalAlignment.Center);

                while (reader.Read())
                {
                    ListViewItem item = new ListViewItem();
                    item.SubItems.Clear();
                    item.SubItems[0].Text = reader[0].ToString().Trim();
                    item.SubItems.Add(reader[1].ToString().Trim());
                    item.SubItems.Add(reader[2].ToString().Trim());
                    item.SubItems.Add(reader[3].ToString().Trim());
                    MessageOutTab.Items.Add(item);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "查找失败！", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                conn.Close();
                Empty_all();
            }
        }

        private void AddMenuToolStripMenuItem2_Click(object sender, EventArgs e)//添加订单
        {
            string connString = "Data Source =.; Initial Catalog = Magazine_Ordering; Integrated Security = True";
            SqlConnection conn = new SqlConnection(connString);
            string uname, mnum, quan, bdate, durtime;
            uname = NewMenuUname.Text.Trim();
            mnum = NewMenuMnum.Text.Trim();
            quan = NewMenuQuan.Text.Trim();
            bdate = NewMenuStartDate.Text.Trim();
            durtime = NewMenuDur.Text.Trim();
            String sql = sql = String.Format("insert into Menulist(username,Mnum,Mquantity,StartDate,duration) values('{0}','{1}','{2}','{3}','{4}')", uname, mnum, quan, bdate,durtime);
            try
            {
                if(uname==null||mnum==null||quan==null||bdate==null||durtime==null)
                    throw new Exception("输入数据不能为空！");
                if(uname==""||mnum==""||quan==""||bdate==""||durtime=="")
                    throw new Exception("输入数据不能为空！");
                conn.Open();

               
                SqlCommand comm = new SqlCommand(sql, conn);
               
                int num =(int) comm.ExecuteNonQuery();
                if (num > 0)

                    MessageBox.Show("添加成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);


                else
                    throw new Exception("数据库异常！");
                    //MessageBox.Show("添加失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
               

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "添加失败！", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                conn.Close();
                Empty_all();
                UpdateInfo();
            }
        }

        private void DelMenuToolStripMenuItem2_Click(object sender, EventArgs e)//删除订单
        {
            if (MenuListTab.SelectedItems.Count <= 0)
            {
                //throw new Exception("请选择至少一条信息！");
                MessageBox.Show("请选择至少一条信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            foreach (ListViewItem lvi in MenuListTab.SelectedItems)
            {
                string str1 = lvi.SubItems[1].Text.Trim();
                string str2 = lvi.SubItems[2].Text.Trim();
                string str3 = lvi.SubItems[4].Text.Trim();
                string connString = "Data Source =.; Initial Catalog = Magazine_Ordering; Integrated Security = True";
                SqlConnection conn = new SqlConnection(connString);
                String sql = String.Format("delete from Menulist where username='{0}' and Mnum='{1}' and StartDate='{2}'", str1,str2,str3);
                try
                {
                    conn.Open();
                    SqlCommand comm = new SqlCommand(sql, conn);
                    int num = (int)comm.ExecuteNonQuery();
                    if (num > 0)
                    {
                        MessageBox.Show("删除成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    else
                        throw new Exception("数据库异常！");
                        //MessageBox.Show("删除失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "删除失败！", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                finally
                {
                    conn.Close();
                }
                UpdateInfo();
            }
        }

        private void ModifyMenuToolStripMenuItem2_Click(object sender, EventArgs e)//修改订单
        {
            if (MenuListTab.SelectedItems.Count != 1)
            {
                //throw new Exception("请选择一条信息！");
                MessageBox.Show("请选择一条信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            foreach (ListViewItem lvi in MenuListTab.SelectedItems)
            {
                string str1 = lvi.SubItems[1].Text.Trim();
                string str2 = lvi.SubItems[2].Text.Trim();
                string str3 = lvi.SubItems[4].Text.Trim();
                string uname, mnum, quan, bdate, durtime;
                uname = NewMenuUname.Text.Trim();
                mnum = NewMenuMnum.Text.Trim();
                quan = NewMenuQuan.Text.Trim();
                bdate = NewMenuStartDate.Text.Trim();
                durtime = NewMenuDur.Text.Trim();
                string connString = "Data Source =.; Initial Catalog = Magazine_Ordering; Integrated Security = True";
                SqlConnection conn = new SqlConnection(connString);
                String sql = String.Format("update Menulist set username='{3}', Mnum='{4}', Mquantity='{5}', StartDate='{6}',duration='{7}'  where username='{0}' and Mnum='{1}' and StartDate='{2}'", str1,str2,str3,uname,mnum,quan,bdate,durtime);
                try
                {
                    conn.Open();
                    SqlCommand comm = new SqlCommand(sql, conn);
                    int num = (int)comm.ExecuteNonQuery();
                    if (num > 0)
                    {
                        MessageBox.Show("更新成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    else
                        throw new Exception("数据库异常！");
                        //MessageBox.Show("更新失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "更新失败！", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                finally
                {
                    conn.Close();
                }
                UpdateInfo();
                Empty_all();
            }
        }

        private void MenuUnameFindToolStripMenuItem1_Click(object sender, EventArgs e)//按用户名查找订单
        {
            MessageOutTab.GridLines = true;//表格是否显示网格线
            MessageOutTab.FullRowSelect = false;//是否选中整行
            MessageOutTab.View = View.Details;//设置显示方式
            MessageOutTab.Scrollable = true;//是否自动显示滚动条
            MessageOutTab.MultiSelect = false;//是否可以选择多行
            MessageOutTab.Clear();
            string connString = "Data Source =.; Initial Catalog = Magazine_Ordering; Integrated Security = True";
            SqlConnection conn = new SqlConnection(connString);
            string uname = NewMenuUname.Text.Trim();
            if (uname == null || uname == "") uname = "%";
            String sql ;
            if(uname.IndexOf('%')<0&&uname.IndexOf('_')<0)
                sql = String.Format("select Menulist.*,MPrice from Menulist,Magazine where Menulist.Mnum=Magazine.Mnum and username='{0}'", uname);
            else
                sql = String.Format("select Menulist.*,MPrice from Menulist,Magazine where Menulist.Mnum=Magazine.Mnum and username like '{0}'", uname);
            try
            {
                conn.Open();
                SqlCommand comm = new SqlCommand(sql, conn);
                SqlDataReader reader = comm.ExecuteReader();

                MessageOutTab.Columns.Add("用户名", 260, HorizontalAlignment.Center);
                MessageOutTab.Columns.Add("报刊编号", 260, HorizontalAlignment.Center);
                MessageOutTab.Columns.Add("订阅数量", 260, HorizontalAlignment.Center);
                MessageOutTab.Columns.Add("开始时间", 420, HorizontalAlignment.Center);
                MessageOutTab.Columns.Add("订阅时长", 260, HorizontalAlignment.Center);
                MessageOutTab.Columns.Add("订单总价", 260, HorizontalAlignment.Center);
                double sum = 0.0;
                while (reader.Read())
                {
                    ListViewItem item = new ListViewItem();
                    item.SubItems.Clear();
                    item.SubItems[0].Text = reader[0].ToString().Trim();
                    item.SubItems.Add(reader[1].ToString().Trim());
                    item.SubItems.Add(reader[2].ToString().Trim());
                    item.SubItems.Add(reader[3].ToString().Trim());
                    item.SubItems.Add(reader[4].ToString().Trim());
                    int num = int.Parse(reader[2].ToString().Trim());
                    double price = double.Parse(reader[5].ToString().Trim());
                    int t = int.Parse(reader[4].ToString().Trim());
                    double totcost = num * price *t;
                    item.SubItems.Add(totcost.ToString().Trim());
                    sum = sum + totcost;
                    MessageOutTab.Items.Add(item);
                }
                ListViewItem sall = new ListViewItem();
                sall.SubItems.Clear();
                sall.SubItems[0].Text = "合计";
                sall.SubItems.Add("---");
                sall.SubItems.Add("---");
                sall.SubItems.Add("---");
                sall.SubItems.Add("---");
                sall.SubItems.Add(sum.ToString().Trim());
                MessageOutTab.Items.Add(sall);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "查找失败！", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                conn.Close();
                Empty_all();
            }
        }

        private void MenuMnumFindToolStripMenuItem_Click(object sender, EventArgs e)//按报刊编号查找订单
        {
            MessageOutTab.GridLines = true;//表格是否显示网格线
            MessageOutTab.FullRowSelect = false;//是否选中整行
            MessageOutTab.View = View.Details;//设置显示方式
            MessageOutTab.Scrollable = true;//是否自动显示滚动条
            MessageOutTab.MultiSelect = false;//是否可以选择多行
            MessageOutTab.Clear();
            string connString = "Data Source =.; Initial Catalog = Magazine_Ordering; Integrated Security = True";
            SqlConnection conn = new SqlConnection(connString);
            string mnum = NewMenuMnum.Text.Trim();
            if (mnum == null || mnum == "") mnum = "%";
            String sql ;
            if(mnum.IndexOf('%')<0&&mnum.IndexOf('_')<0)
                sql = String.Format("select Menulist.*,MPrice from Menulist,Magazine where Menulist.Mnum=Magazine.Mnum and Menulist.Mnum='{0}'", mnum);
            else
                sql = String.Format("select Menulist.*,MPrice from Menulist,Magazine where Menulist.Mnum=Magazine.Mnum and Menulist.Mnum like '{0}'", mnum);
            try
            {
                conn.Open();
                SqlCommand comm = new SqlCommand(sql, conn);
                SqlDataReader reader = comm.ExecuteReader();

                MessageOutTab.Columns.Add("用户名", 260, HorizontalAlignment.Center);
                MessageOutTab.Columns.Add("报刊编号", 260, HorizontalAlignment.Center);
                MessageOutTab.Columns.Add("订阅数量", 260, HorizontalAlignment.Center);
                MessageOutTab.Columns.Add("开始时间", 420, HorizontalAlignment.Center);
                MessageOutTab.Columns.Add("订阅时长", 260, HorizontalAlignment.Center);
                MessageOutTab.Columns.Add("订单总价", 260, HorizontalAlignment.Center);
                double sum = 0.0;
                while (reader.Read())
                {
                    ListViewItem item = new ListViewItem();
                    item.SubItems.Clear();
                    item.SubItems[0].Text = reader[0].ToString().Trim();
                    item.SubItems.Add(reader[1].ToString().Trim());
                    item.SubItems.Add(reader[2].ToString().Trim());
                    item.SubItems.Add(reader[3].ToString().Trim());
                    item.SubItems.Add(reader[4].ToString().Trim());
                    int num = int.Parse(reader[2].ToString().Trim());
                    double price = double.Parse(reader[5].ToString().Trim());
                    int t = int.Parse(reader[4].ToString().Trim());
                    double totcost = num * price * t;
                    item.SubItems.Add(totcost.ToString().Trim());
                    sum = sum + totcost;
                    MessageOutTab.Items.Add(item);

                }
                ListViewItem sall = new ListViewItem();
                sall.SubItems.Clear();
                sall.SubItems[0].Text = "合计";
                sall.SubItems.Add("---");
                sall.SubItems.Add("---");
                sall.SubItems.Add("---");
                sall.SubItems.Add("---");
                sall.SubItems.Add(sum.ToString().Trim());
                MessageOutTab.Items.Add(sall);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "查找失败！", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                conn.Close();
                Empty_all();
            }
        }

        private void MenuDateFindToolStripMenuItem_Click(object sender, EventArgs e)//按开始日期查找订单
        {
            MessageOutTab.GridLines = true;//表格是否显示网格线
            MessageOutTab.FullRowSelect = false;//是否选中整行
            MessageOutTab.View = View.Details;//设置显示方式
            MessageOutTab.Scrollable = true;//是否自动显示滚动条
            MessageOutTab.MultiSelect = false;//是否可以选择多行
            MessageOutTab.Clear();
            string connString = "Data Source =.; Initial Catalog = Magazine_Ordering; Integrated Security = True";
            SqlConnection conn = new SqlConnection(connString);
            string datetime = NewMenuStartDate.Text.Trim();
            if (datetime == null || datetime == "") datetime = "%";
            String sql ;
            if(datetime.IndexOf('%')<0&&datetime.IndexOf('_')<0)
                sql = String.Format("select Menulist.*,MPrice from Menulist,Magazine where Menulist.Mnum=Magazine.Mnum and StartDate='{0}'", datetime);
            else
                sql = String.Format("select Menulist.*,MPrice from Menulist,Magazine where Menulist.Mnum=Magazine.Mnum and StartDate like '{0}'", datetime);
            try
            {
                conn.Open();
                SqlCommand comm = new SqlCommand(sql, conn);
                SqlDataReader reader = comm.ExecuteReader();

                MessageOutTab.Columns.Add("用户名", 260, HorizontalAlignment.Center);
                MessageOutTab.Columns.Add("报刊编号", 260, HorizontalAlignment.Center);
                MessageOutTab.Columns.Add("订阅数量", 260, HorizontalAlignment.Center);
                MessageOutTab.Columns.Add("开始时间", 420, HorizontalAlignment.Center);
                MessageOutTab.Columns.Add("订阅时长", 260, HorizontalAlignment.Center);
                MessageOutTab.Columns.Add("订单总价", 260, HorizontalAlignment.Center);
                double sum = 0.0;
                while (reader.Read())
                {
                    ListViewItem item = new ListViewItem();
                    item.SubItems.Clear();
                    item.SubItems[0].Text = reader[0].ToString().Trim();
                    item.SubItems.Add(reader[1].ToString().Trim());
                    item.SubItems.Add(reader[2].ToString().Trim());
                    item.SubItems.Add(reader[3].ToString().Trim());
                    item.SubItems.Add(reader[4].ToString().Trim());
                    int num = int.Parse(reader[2].ToString().Trim());
                    double price = double.Parse(reader[5].ToString().Trim());
                    int t = int.Parse(reader[4].ToString().Trim());
                    double totcost = num * price * t;
                    item.SubItems.Add(totcost.ToString().Trim());
                    sum = sum + totcost;
                    MessageOutTab.Items.Add(item);

                }
                ListViewItem sall = new ListViewItem();
                sall.SubItems.Clear();
                sall.SubItems[0].Text = "合计";
                sall.SubItems.Add("---");
                sall.SubItems.Add("---");
                sall.SubItems.Add("---");
                sall.SubItems.Add("---");
                sall.SubItems.Add(sum.ToString().Trim());
                MessageOutTab.Items.Add(sall);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "查找失败！", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                conn.Close();
                Empty_all();
            }
        }

        private void ModifyPassToolStripMenuItem_Click(object sender, EventArgs e)//修改管理员账户密码
        {
            ModifyPassWord mdpwd = new ModifyPassWord(this);
            mdpwd.Show();

        }

        private void MagListTab_ColumnClick(object sender, ColumnClickEventArgs e)//报刊信息排序函数
        {
            string Asc = ((char)0x25bc).ToString().PadLeft(4, ' ');//向上箭头
            string Des = ((char)0x25b2).ToString().PadLeft(4, ' ');//向下箭头
            if (sort == false)//降序
            {
                sort = true;
                string oldStr = this.MagListTab.Columns[e.Column].Text.TrimEnd((char)0x25bc, (char)0x25b2, ' ');
                this.MagListTab.Columns[e.Column].Text = oldStr + Des;//添加向下箭头
            }
            else if (sort == true)//升序
            {
                sort = false;
                string oldStr = this.MagListTab.Columns[e.Column].Text.TrimEnd((char)0x25bc, (char)0x25b2, ' ');
                this.MagListTab.Columns[e.Column].Text = oldStr + Asc;//添加向上箭头
            }
            MagListTab.ListViewItemSorter = new ListViewItemComparer(e.Column, sort);
            this.MagListTab.Sort();//排序
            int rowCount = this.MagListTab.Items.Count;//行数
            if (currentCol != -1)
            {
                for (int i = 0; i < rowCount; i++)//遍历行
                {
                    this.MagListTab.Items[i].UseItemStyleForSubItems = false;
                    this.MagListTab.Items[i].SubItems[currentCol].BackColor = Color.White;//背景颜色为白色

                    if (e.Column != currentCol)//排序列与当前列不一致
                        this.MagListTab.Columns[currentCol].Text = this.MagListTab.Columns[currentCol].Text.TrimEnd((char)0x25bc, (char)0x25b2, ' ');//列首部移除箭头
                }
            }

            for (int i = 0; i < rowCount; i++)//遍历行
            {
                this.MagListTab.Items[i].UseItemStyleForSubItems = false;
                this.MagListTab.Items[i].SubItems[e.Column].BackColor = Color.WhiteSmoke;//背景颜色为浅灰色
                currentCol = e.Column;//修改当前列
            }
        }

        private void UsrListTab_ColumnClick(object sender, ColumnClickEventArgs e)//用户信息排序函数
        {
            string Asc = ((char)0x25bc).ToString().PadLeft(4, ' ');//向上箭头
            string Des = ((char)0x25b2).ToString().PadLeft(4, ' ');//向下箭头
            if (sort == false)//降序
            {
                sort = true;
                string oldStr = this.UsrListTab.Columns[e.Column].Text.TrimEnd((char)0x25bc, (char)0x25b2, ' ');
                this.UsrListTab.Columns[e.Column].Text = oldStr + Des;//添加向下箭头
            }
            else if (sort == true)//升序
            {
                sort = false;
                string oldStr = this.UsrListTab.Columns[e.Column].Text.TrimEnd((char)0x25bc, (char)0x25b2, ' ');
                this.UsrListTab.Columns[e.Column].Text = oldStr + Asc;//添加向上箭头
            }
            UsrListTab.ListViewItemSorter = new ListViewItemComparer(e.Column, sort);
            this.UsrListTab.Sort();//排序
            int rowCount = this.UsrListTab.Items.Count;//行数
            if (currentCol != -1)
            {
                for (int i = 0; i < rowCount; i++)//遍历行
                {
                    this.UsrListTab.Items[i].UseItemStyleForSubItems = false;
                    this.UsrListTab.Items[i].SubItems[currentCol].BackColor = Color.White;//背景颜色为白色

                    if (e.Column != currentCol)//排序列与当前列不一致
                        this.UsrListTab.Columns[currentCol].Text = this.UsrListTab.Columns[currentCol].Text.TrimEnd((char)0x25bc, (char)0x25b2, ' ');//列首部移除箭头
                }
            }

            for (int i = 0; i < rowCount; i++)//遍历行
            {
                this.UsrListTab.Items[i].UseItemStyleForSubItems = false;
                this.UsrListTab.Items[i].SubItems[e.Column].BackColor = Color.WhiteSmoke;//背景颜色为浅灰色
                currentCol = e.Column;//修改当前列
            }
        }

        private void MenuListTab_ColumnClick(object sender, ColumnClickEventArgs e)//订单信息排序函数
        {
            string Asc = ((char)0x25bc).ToString().PadLeft(4, ' ');//向上箭头
            string Des = ((char)0x25b2).ToString().PadLeft(4, ' ');//向下箭头
           
            if (sort == false)//降序
            {
                sort = true;//下次为升序
                string oldStr = this.MenuListTab.Columns[e.Column].Text.TrimEnd((char)0x25bc, (char)0x25b2, ' ');
                this.MenuListTab.Columns[e.Column].Text = oldStr + Des;//添加向下箭头
            }

            else if (sort == true)//升序
            {
                sort = false;//下次为降序
                string oldStr = this.MenuListTab.Columns[e.Column].Text.TrimEnd((char)0x25bc, (char)0x25b2, ' ');
                this.MenuListTab.Columns[e.Column].Text = oldStr + Asc;//添加向上箭头
            }
            MenuListTab.ListViewItemSorter = new ListViewItemComparer(e.Column, sort);
            this.MenuListTab.Sort();//排序


            int rowCount = this.MenuListTab.Items.Count;//行数

            if (currentCol != -1)
            {
                for (int i = 0; i < rowCount; i++)//遍历行
                {
                    this.MenuListTab.Items[i].UseItemStyleForSubItems = false;
                    this.MenuListTab.Items[i].SubItems[currentCol].BackColor = Color.White;//背景颜色为白色

                    if (e.Column != currentCol)//排序列与当前列不一致
                        this.MenuListTab.Columns[currentCol].Text = this.MenuListTab.Columns[currentCol].Text.TrimEnd((char)0x25bc, (char)0x25b2, ' ');//列首部移除箭头
                }
            }



            for (int i = 0; i < rowCount; i++)//遍历行
            {
                this.MenuListTab.Items[i].UseItemStyleForSubItems = false;
                this.MenuListTab.Items[i].SubItems[e.Column].BackColor = Color.WhiteSmoke;//背景颜色为浅灰色
                currentCol = e.Column;//修改当前列
            }





        }
    }
}
