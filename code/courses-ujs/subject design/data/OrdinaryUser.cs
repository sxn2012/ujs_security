using System;
using System.Text.RegularExpressions;
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
    public partial class OrdinaryUser : Form
    {
        private Login_Form lf = null;
        private string uname=null;
        private int currentCol = -1;//当前排序的列
        private bool sort;//升序或降序的标志
        public void Empty_all()//清空输入框
        {
            NewUsrname.Clear();
            NewUsrpwd.Clear();
            NewUsrLoc.Clear();
            NewMenuMnum.Clear();
            NewMenuStartDate.Clear();
            NewMenuQuan.Clear();
            NewMenuDur.Clear();
        }
        private void Updateinfo()//更新输入信息
        {
            UsrListTab.Clear();
            MenuListTab.Clear();
            MagListTab.Clear();
            string connString = "Data Source =.; Initial Catalog = Magazine_Ordering; Integrated Security = True";
            SqlConnection conn = new SqlConnection(connString);
            String sql = String.Format("select * from Userlist where username='{0}' ", uname);
            try 
            {
                conn.Open();
                SqlCommand comm = new SqlCommand(sql, conn);
                SqlDataReader reader = comm.ExecuteReader();
                UsrListTab.Columns.Add("项目", 150, HorizontalAlignment.Center);
                UsrListTab.Columns.Add("内容", 200, HorizontalAlignment.Center);
                if (reader.Read())
                {
                    ListViewItem item = new ListViewItem();
                    item.SubItems.Clear();
                    item.SubItems[0].Text = "用户名";
                    item.SubItems.Add(reader[0].ToString().Trim());
                    UsrListTab.Items.Add(item);
                    item = new ListViewItem();
                    item.SubItems.Clear();
                    item.SubItems[0].Text = "密码";
                    item.SubItems.Add("*********");
                        //(reader[1].ToString().Trim());
                    UsrListTab.Items.Add(item);
                    item = new ListViewItem();
                    item.SubItems.Clear();
                    item.SubItems[0].Text = "权限";
                    item.SubItems.Add(reader[2].ToString().Trim());
                    UsrListTab.Items.Add(item);
                    item = new ListViewItem();
                    item.SubItems.Clear();
                    item.SubItems[0].Text = "地址";
                    item.SubItems.Add(reader[3].ToString().Trim());
                    UsrListTab.Items.Add(item);
                }
                conn.Close();
                sql = String.Format("select Magazine.*,Mquantity,StartDate,duration from Magazine,Menulist where Magazine.Mnum=Menulist.Mnum and username='{0}' ", uname); ;
                conn.Open();
                comm = new SqlCommand(sql, conn);
                reader = comm.ExecuteReader();
                MenuListTab.Columns.Add("报刊名称", 160, HorizontalAlignment.Center);
                MenuListTab.Columns.Add("报刊编号", 100, HorizontalAlignment.Center);
                MenuListTab.Columns.Add("报刊版本", 100, HorizontalAlignment.Center);
                MenuListTab.Columns.Add("报刊单价", 100, HorizontalAlignment.Center);
                MenuListTab.Columns.Add("订阅数量", 100, HorizontalAlignment.Center);
                MenuListTab.Columns.Add("开始时间", 160, HorizontalAlignment.Center);
                MenuListTab.Columns.Add("订阅时长", 100, HorizontalAlignment.Center);
                MenuListTab.Columns.Add("订单总价", 100, HorizontalAlignment.Center);
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
                    item.SubItems.Add(reader[5].ToString().Trim());
                    item.SubItems.Add(reader[6].ToString().Trim());
                    double price = double.Parse(reader[3].ToString().Trim());
                    int num = int.Parse(reader[4].ToString().Trim());
                    int t = int.Parse(reader[6].ToString().Trim());
                    double totcost = price * num *t;
                    item.SubItems.Add(totcost.ToString().Trim());
                    sum = sum + totcost;
                    MenuListTab.Items.Add(item);
                }
                ListViewItem sall = new ListViewItem();
                sall.SubItems.Clear();
                sall.SubItems[0].Text = "合计";
                sall.SubItems.Add("---");
                sall.SubItems.Add("---");
                sall.SubItems.Add("---");
                sall.SubItems.Add("---");
                sall.SubItems.Add("---");
                sall.SubItems.Add("---");
                sall.SubItems.Add(sum.ToString().Trim());
                MenuListTab.Items.Add(sall);
                conn.Close();
                sql = "select * from Magazine";
                conn.Open();
                comm = new SqlCommand(sql, conn);
                reader = comm.ExecuteReader();
                MagListTab.Columns.Add("报刊名称", 160, HorizontalAlignment.Center);
                MagListTab.Columns.Add("报刊编号", 100, HorizontalAlignment.Center);
                MagListTab.Columns.Add("报刊版本", 100, HorizontalAlignment.Center);
                MagListTab.Columns.Add("报刊单价", 100, HorizontalAlignment.Center);
                while (reader.Read())
                {
                    ListViewItem item = new ListViewItem();
                    item.SubItems.Clear();
                    item.SubItems[0].Text = reader[0].ToString().Trim();
                    item.SubItems.Add(reader[1].ToString().Trim());
                    item.SubItems.Add(reader[2].ToString().Trim());
                    item.SubItems.Add(reader[3].ToString().Trim());
                    MagListTab.Items.Add(item);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "数据库异常！", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                conn.Close();
            }

        }
        public OrdinaryUser(Login_Form f1,string un)//构造函数
        {
            InitializeComponent();
            this.timelabel.Text = "";
            this.statuslable.Text = "就绪";
            this.lf = f1;
            this.uname = un;
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
            UsrListTab.GridLines = true;//表格是否显示网格线
            UsrListTab.FullRowSelect = false;//是否选中整行
            UsrListTab.View = View.Details;//设置显示方式
            UsrListTab.Scrollable = true;//是否自动显示滚动条
            UsrListTab.MultiSelect = false;//是否可以选择多行
            MenuListTab.GridLines = true;//表格是否显示网格线
            MenuListTab.FullRowSelect = true;//是否选中整行
            MenuListTab.View = View.Details;//设置显示方式
            MenuListTab.Scrollable = true;//是否自动显示滚动条
            MenuListTab.MultiSelect = false;//是否可以选择多行
            MagListTab.GridLines = true;//表格是否显示网格线
            MagListTab.FullRowSelect = false;//是否选中整行
            MagListTab.View = View.Details;//设置显示方式
            MagListTab.Scrollable = true;//是否自动显示滚动条
            MagListTab.MultiSelect = false;//是否可以选择多行
            Updateinfo();
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

        private void ClearinputToolStripMenuItem_Click(object sender, EventArgs e)//清空输入
        {
            Empty_all();
        }

        private void ClearoutputToolStripMenuItem_Click(object sender, EventArgs e)//清空输出
        {
            MessageOutTab.Clear();
        }

        private void ModifyPerInfoToolStripMenuItem_Click(object sender, EventArgs e)//修改个人基本信息
        {
            string u_name, u_pass, u_acc, u_loc;
            u_name = NewUsrname.Text.Trim();
            u_pass = NewUsrpwd.Text.Trim();
            u_acc = "3";
            u_loc = NewUsrLoc.Text.Trim();
            string connString = "Data Source =.; Initial Catalog = Magazine_Ordering; Integrated Security = True";
            SqlConnection conn = new SqlConnection(connString);
            String sql = String.Format("update Userlist set username='{1}', password='{2}', access='{3}', location='{4}'  where username='{0}'", uname, u_name, u_pass, u_acc, u_loc);
            try
            {
                if (u_name == null || u_pass == null || u_loc == null)
                    throw new Exception("输入信息不能为空！");
                if (u_name == "" || u_pass == "" || u_loc == "")
                    throw new Exception("输入信息不能为空！");
                conn.Open();
                SqlCommand comm = new SqlCommand(sql, conn);
                int num = (int)comm.ExecuteNonQuery();
                if (num > 0)
                {
                    MessageBox.Show("修改成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MessageBox.Show("修改个人信息后必须重新登录！", "注意", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    this.Close();
                }
                else
                    throw new Exception("数据库异常！");
                    //MessageBox.Show("修改失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                
            }    
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "修改失败！", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                conn.Close();
            }
            Updateinfo();
            Empty_all();
            uname = u_name;
            
        }

        private void DelPerInfoToolStripMenuItem_Click(object sender, EventArgs e)//删除当前用户
        {
            string connString = "Data Source =.; Initial Catalog = Magazine_Ordering; Integrated Security = True";
            SqlConnection conn = new SqlConnection(connString);
            String sql = String.Format("delete from Userlist where username='{0}'", uname);
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
            this.Close();
        }

        private void AddMenuToolStripMenuItem_Click(object sender, EventArgs e)//添加订单
        {
            string connString = "Data Source =.; Initial Catalog = Magazine_Ordering; Integrated Security = True";
            SqlConnection conn = new SqlConnection(connString);
            string  mnum, quan, bdate, durtime;
            mnum = NewMenuMnum.Text.Trim();
            quan = NewMenuQuan.Text.Trim();
            bdate = NewMenuStartDate.Text.Trim();
            durtime = NewMenuDur.Text.Trim();
            String sql = sql = String.Format("insert into Menulist(username,Mnum,Mquantity,StartDate,duration) values('{0}','{1}','{2}','{3}','{4}')", uname, mnum, quan, bdate, durtime);
            try
            {
                if (mnum == null || quan == null || bdate == null || durtime == null)
                    throw new Exception("输入信息不能为空！");
                if (mnum == "" || quan == "" || bdate == "" || durtime == "")
                    throw new Exception("输入信息不能为空！");
                conn.Open();


                SqlCommand comm = new SqlCommand(sql, conn);

                int num = (int)comm.ExecuteNonQuery();
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
                Updateinfo();
            }
        }

        private void DelMenuToolStripMenuItem_Click(object sender, EventArgs e)//删除订单
        {
            if (MenuListTab.SelectedItems.Count <= 0)
            {
                MessageBox.Show("请选择至少一条信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            foreach (ListViewItem lvi in MenuListTab.SelectedItems)
            {
                string str1 = uname;
                string str2 = lvi.SubItems[1].Text.Trim();
                string str3 = lvi.SubItems[5].Text.Trim();
                string connString = "Data Source =.; Initial Catalog = Magazine_Ordering; Integrated Security = True";
                SqlConnection conn = new SqlConnection(connString);
                String sql = String.Format("delete from Menulist where username='{0}' and Mnum='{1}' and StartDate='{2}'", str1, str2, str3);
                try
                {
                    if (str2 == "---")
                        throw new Exception("选择的信息无效！");
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
                Updateinfo();
            }
        }

        private void ModifyMenuToolStripMenuItem_Click(object sender, EventArgs e)//修改订单
        {
            if (MenuListTab.SelectedItems.Count != 1)
            {
                MessageBox.Show("请选择一条信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            foreach (ListViewItem lvi in MenuListTab.SelectedItems)
            {
                string str1 = uname;
                string str2 = lvi.SubItems[1].Text.Trim();
                string str3 = lvi.SubItems[5].Text.Trim();
                string  mnum, quan, bdate, durtime;
                mnum = NewMenuMnum.Text.Trim();
                quan = NewMenuQuan.Text.Trim();
                bdate = NewMenuStartDate.Text.Trim();
                durtime = NewMenuDur.Text.Trim();
                string connString = "Data Source =.; Initial Catalog = Magazine_Ordering; Integrated Security = True";
                SqlConnection conn = new SqlConnection(connString);
                String sql = String.Format("update Menulist set username='{3}', Mnum='{4}', Mquantity='{5}', StartDate='{6}',duration='{7}'  where username='{0}' and Mnum='{1}' and StartDate='{2}'", str1, str2, str3, uname, mnum, quan, bdate, durtime);
                try
                {
                    if (str2 == "---")
                        throw new Exception("选择的信息无效！");
                    if (mnum == null || quan == null || bdate == null || durtime == null)
                        throw new Exception("输入信息不能为空！");
                    if (mnum == "" || quan == "" || bdate == "" || durtime == "")
                        throw new Exception("输入信息不能为空！");
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
                Updateinfo();
                Empty_all();
            }
        }

        private void MnumFindToolStripMenuItem_Click(object sender, EventArgs e)//按报刊编号查询
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
            String sql;
            if (mnum.IndexOf('%') < 0 && mnum.IndexOf('_') < 0)
                sql = String.Format("select Menulist.*,MPrice from Menulist,Magazine where Menulist.Mnum=Magazine.Mnum and Menulist.Mnum='{0}' and username='{1}'", mnum,uname);
            else
                sql = String.Format("select Menulist.*,MPrice from Menulist,Magazine where Menulist.Mnum=Magazine.Mnum and Menulist.Mnum like '{0}' and username='{1}'", mnum,uname);
            try
            {
                conn.Open();
                SqlCommand comm = new SqlCommand(sql, conn);
                SqlDataReader reader = comm.ExecuteReader();

                
                MessageOutTab.Columns.Add("报刊编号", 80, HorizontalAlignment.Center);
                MessageOutTab.Columns.Add("订阅数量", 80, HorizontalAlignment.Center);
                MessageOutTab.Columns.Add("开始时间", 120, HorizontalAlignment.Center);
                MessageOutTab.Columns.Add("订阅时长", 80, HorizontalAlignment.Center);
                MessageOutTab.Columns.Add("订单总价", 80, HorizontalAlignment.Center);
                while (reader.Read())
                {
                    ListViewItem item = new ListViewItem();
                    item.SubItems.Clear();
                    item.SubItems[0].Text = reader[1].ToString().Trim();
                    item.SubItems.Add(reader[2].ToString().Trim());
                    item.SubItems.Add(reader[3].ToString().Trim());
                    item.SubItems.Add(reader[4].ToString().Trim());
                    int num = int.Parse(reader[2].ToString().Trim());
                    double price = double.Parse(reader[5].ToString().Trim());
                    int t = int.Parse(reader[4].ToString().Trim());
                    double totcost = num * price * t;
                    item.SubItems.Add(totcost.ToString().Trim());
                    MessageOutTab.Items.Add(item);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "查询失败！", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                conn.Close();
                Empty_all();
            }
        }

        private void StartDateFindToolStripMenuItem_Click(object sender, EventArgs e)//按开始日期查询
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
            String sql;
            if (datetime.IndexOf('%') < 0 && datetime.IndexOf('_') < 0)
                sql = String.Format("select Menulist.*,MPrice from Menulist,Magazine where Menulist.Mnum=Magazine.Mnum and StartDate='{0}' and username='{1}'", datetime,uname);
            else
                sql = String.Format("select Menulist.*,MPrice from Menulist,Magazine where Menulist.Mnum=Magazine.Mnum and StartDate like '{0}' and username='{1}'", datetime,uname);
            try
            {
                conn.Open();
                SqlCommand comm = new SqlCommand(sql, conn);
                SqlDataReader reader = comm.ExecuteReader();

                MessageOutTab.Columns.Add("报刊编号", 80, HorizontalAlignment.Center);
                MessageOutTab.Columns.Add("订阅数量", 80, HorizontalAlignment.Center);
                MessageOutTab.Columns.Add("开始时间", 120, HorizontalAlignment.Center);
                MessageOutTab.Columns.Add("订阅时长", 80, HorizontalAlignment.Center);
                MessageOutTab.Columns.Add("订单总价", 80, HorizontalAlignment.Center);
                while (reader.Read())
                {
                    ListViewItem item = new ListViewItem();
                    item.SubItems.Clear();
                    item.SubItems[0].Text = reader[1].ToString().Trim();
                    item.SubItems.Add(reader[2].ToString().Trim());
                    item.SubItems.Add(reader[3].ToString().Trim());
                    item.SubItems.Add(reader[4].ToString().Trim());
                    int num = int.Parse(reader[2].ToString().Trim());
                    double price = double.Parse(reader[5].ToString().Trim());
                    int t = int.Parse(reader[4].ToString().Trim());
                    double totcost = num * price * t;
                    item.SubItems.Add(totcost.ToString().Trim());
                    MessageOutTab.Items.Add(item);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "查询失败！", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                conn.Close();
                Empty_all();
            }
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
            
            MenuListTab_Sort(e);//排序


            int rowCount = this.MenuListTab.Items.Count ;//行数

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
                if (this.MenuListTab.Items[i].SubItems[1].Text!="---")
                    this.MenuListTab.Items[i].SubItems[e.Column].BackColor = Color.WhiteSmoke;//背景颜色为浅灰色
                currentCol = e.Column;//修改当前列
                
            }
        }

        private void MenuListTab_Sort(ColumnClickEventArgs e)//除去合计那一行的排序
        {
            int n = this.MenuListTab.Items.Count ;
            ListViewItem []a=new ListViewItem[n];
            ListViewItem t=new ListViewItem() ;
            bool temp_ = false;
            for (int i = 0; i < n;i++ )
            {
                if(this.MenuListTab.Items[i].SubItems[1].Text=="---")
                {
                    t = this.MenuListTab.Items[i];
                    this.MenuListTab.Items.Remove(t);
                    temp_ = true;
                    break;
                }
            }
            if (temp_ == false) return;
            for (int i = 0; i < n - 1; i++)
            {
                a[i] = this.MenuListTab.Items[i];
            }
            for (int i = 0; i < n - 1; i++)
            {
                bool flag = false;
                for (int j = 0; j < n - 2 - i; j++)
                {
                    if (MenuListTab_Compare(a[j].SubItems[e.Column].Text, a[j + 1].SubItems[e.Column].Text) < 0)
                    {
                        ListViewItem temp = a[j];
                        a[j] = a[j + 1];
                        a[j + 1] = temp;
                        flag = true;
                    }
                }
                if (flag == false)
                    break;
            }
            this.MenuListTab.Items.Clear();
            for (int i = n - 2; i >=0; i--)
            {
                this.MenuListTab.Items.Add(a[i]);
            }
            
            this.MenuListTab.Items.Add(t);
        }

        private int MenuListTab_Compare(string x,string y)//两个字符串比较的函数
        {
            if (Regex.IsMatch(x, @"^\d+(\.\d+)?$") && Regex.IsMatch(y, @"^\d+(\.\d+)?$"))
            {
                decimal a = Convert.ToDecimal(x);
                decimal b = Convert.ToDecimal(y);
                if(sort)
                {
                    if (a > b) return 1;
                    else if (a == b) return 0;
                    else if (a < b) return -1;
                }
                else
                {
                    if (b > a) return 1;
                    else if (b == a) return 0;
                    else if (b < a) return -1;
                }
            }
            
            if(sort)
            {
                return String.Compare(x, y);
            }
            else
            {
                return String.Compare(y, x);
            }
        }
    }
}
