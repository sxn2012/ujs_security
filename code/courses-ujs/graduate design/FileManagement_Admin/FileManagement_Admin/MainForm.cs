using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Threading;

namespace FileManagement_Admin
{
    public partial class MainForm : Form
    {
        private LoginForm lf = null;
        private Socket_Receive _ss = null;
        public DateTime lastclicktime;
        public MainForm(LoginForm l,Socket_Receive sserver)
        {
            InitializeComponent();
            BackgroundImage = Image.FromFile("main.jpg");
            lf = l;
            _ss = sserver;
            lastclicktime = DateTime.Now;

            UsrView.GridLines = true;//表格是否显示网格线
            UsrView.FullRowSelect = true;//是否选中整行
            UsrView.View = View.Details;//设置显示方式
            UsrView.Scrollable = true;//是否自动显示滚动条
            UsrView.MultiSelect = false;//是否可以选择多行

            DepView.GridLines = true;//表格是否显示网格线
            DepView.FullRowSelect = true;//是否选中整行
            DepView.View = View.Details;//设置显示方式
            DepView.Scrollable = true;//是否自动显示滚动条
            DepView.MultiSelect = false;//是否可以选择多行

            SubView.GridLines = true;//表格是否显示网格线
            SubView.FullRowSelect = true;//是否选中整行
            SubView.View = View.Details;//设置显示方式
            SubView.Scrollable = true;//是否自动显示滚动条
            SubView.MultiSelect = false;//是否可以选择多行

            AccessView.GridLines = true;//表格是否显示网格线
            AccessView.FullRowSelect = true;//是否选中整行
            AccessView.View = View.Details;//设置显示方式
            AccessView.Scrollable = true;//是否自动显示滚动条
            AccessView.MultiSelect = false;//是否可以选择多行

            Thread t = new Thread(Counting);
            t.IsBackground = true;
            t.Start();
            UpdateData();

        }


        private void Counting()
        {
            while (true)
            {
                try
                {
                    
                    timelabel.BeginInvoke(new MethodInvoker(() => timelabel.Text = DateTime.Now.ToString()));

                }
                catch (Exception)
                {

                }
                Thread.Sleep(1000);
            }
        }

        protected override void WndProc(ref Message msg)//关闭事件响应函数
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
                    return;


            }
            base.WndProc(ref msg);
        }

        

        public void UpdateData()
        {
            UsrView.Clear();
            DepView.Clear();
            SubView.Clear();
            

            string connString = "Data Source =.; Initial Catalog = FileManage; Integrated Security = True";
            SqlConnection conn = new SqlConnection(connString);
            String sql = "select UserName,UserDep from UserList";
            try
            {
                conn.Open();
                SqlCommand comm = new SqlCommand(sql, conn);
                SqlDataReader reader = comm.ExecuteReader();
                UsrView.Columns.Add("用户名", 100, HorizontalAlignment.Center);
                UsrView.Columns.Add("用户所属部门", 100, HorizontalAlignment.Center);
                
                while (reader.Read())
                {
                    ListViewItem item = new ListViewItem();
                    item.SubItems.Clear();
                    item.SubItems[0].Text = reader[0].ToString().Trim();
                    item.SubItems.Add(reader[1].ToString().Trim());
                    if (item.SubItems[0].Text == "admin") continue;
                    UsrView.Items.Add(item);
                }
                conn.Close();

                sql = "select DepName,DepLoc from Department";
                conn.Open();
                SqlCommand comm1 = new SqlCommand(sql, conn);
                SqlDataReader reader1 = comm1.ExecuteReader();
                DepView.Columns.Add("部门名称", 100, HorizontalAlignment.Center);
                DepView.Columns.Add("部门地址", 100, HorizontalAlignment.Center);

                while (reader1.Read())
                {
                    ListViewItem item = new ListViewItem();
                    item.SubItems.Clear();
                    item.SubItems[0].Text = reader1[0].ToString().Trim();
                    item.SubItems.Add(CryptoClass.AesDecrypt(reader1[1].ToString().Trim(),CryptoClass.key).Trim());
                    DepView.Items.Add(item);
                }
                conn.Close();

                sql = "select SubName,SubContent from SubjectList";
                conn.Open();
                SqlCommand comm2 = new SqlCommand(sql, conn);
                SqlDataReader reader2 = comm2.ExecuteReader();
                SubView.Columns.Add("主题名称", 100, HorizontalAlignment.Center);
                SubView.Columns.Add("主题内容", 100, HorizontalAlignment.Center);

                while (reader2.Read())
                {
                    ListViewItem item = new ListViewItem();
                    item.SubItems.Clear();
                    item.SubItems[0].Text = reader2[0].ToString().Trim();
                    item.SubItems.Add(CryptoClass.AesDecrypt(reader2[1].ToString().Trim(),CryptoClass.key).Trim());
                    SubView.Items.Add(item);
                }
                conn.Close();


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

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
           
            this.lf.Visible = true;
        }

        private void UsrView_MouseUp(object sender, MouseEventArgs e)
        {
            if (UsrView.SelectedItems.Count != 1)
            {
                if (e.Button == MouseButtons.Right)
                {
                    AddUserMenu.Show(UsrView, e.Location);
                }

            }
            else
            {
                if (e.Button == MouseButtons.Right)
                {
                    UserMenu.Show(UsrView, e.Location);
                }
            }
        }

        private void DepView_MouseUp(object sender, MouseEventArgs e)
        {
            if (DepView.SelectedItems.Count != 1)
            {
                if (e.Button == MouseButtons.Right)
                {
                    AddDeptMenu.Show(DepView, e.Location);
                }

            }
            else
            {
                if (e.Button == MouseButtons.Right)
                {
                    DeptMenu.Show(DepView, e.Location);
                }
            }
        }

        private void SubView_MouseUp(object sender, MouseEventArgs e)
        {
            if (SubView.SelectedItems.Count != 1)
            {
                if (e.Button == MouseButtons.Right)
                {
                    AddSubMenu.Show(SubView, e.Location);
                }

            }
            else
            {
                if (e.Button == MouseButtons.Right)
                {
                    SubMenu.Show(SubView, e.Location);
                }
            }
        }

        private void AccessView_MouseUp(object sender, MouseEventArgs e)
        {
            if (AccessView.SelectedItems.Count != 1)
            {
                if (e.Button == MouseButtons.Right)
                {
                    AddPermissionMenu.Show(AccessView, e.Location);
                }

            }
            else
            {
                if (e.Button == MouseButtons.Right)
                {
                    PermissionMenu.Show(AccessView, e.Location);
                }
            }
        }

        private void DeleteUser_Click(object sender, EventArgs e)
        {
            if (DateTime.Compare(lastclicktime.AddSeconds(1), DateTime.Now) > 0) //限制点击频率
            {
                MessageBox.Show("操作过于频繁！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
                lastclicktime = DateTime.Now;
            if (UsrView.SelectedItems.Count != 1)
            {
                MessageBox.Show("请选择一条信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DialogResult result = MessageBox.Show("确定要删除该用户吗？\n注意该操作无法恢复！", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
            if (result != DialogResult.OK)
                return;
            string connString = "Data Source =.; Initial Catalog = FileManage; Integrated Security = True";
            SqlConnection conn = new SqlConnection(connString);
            string sql = "delete from UserSafety where UserName=@UserName and (UserStatus='0' or UserStatus='1')";
            string str = "";
            try
            {
                conn.Open();
                SqlCommand comm = new SqlCommand(sql, conn);
                foreach (ListViewItem lvi in UsrView.SelectedItems)
                {
                    str = lvi.SubItems[0].Text.Trim();
                    comm.Parameters.AddWithValue("@UserName", str);
                }
                
                int num = (int)comm.ExecuteNonQuery();
                if (num <= 0)
                    throw new Exception("用户繁忙！");
                conn.Close();
                sql = "delete from UserList where UserName=@UserName";
                conn.Open();
                SqlCommand comm1 = new SqlCommand(sql, conn);
                comm1.Parameters.AddWithValue("@UserName", str);
                num = (int)comm1.ExecuteNonQuery();
                if (num <= 0)
                    throw new Exception("数据库异常");
                MessageBox.Show("删除用户成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "删除用户失败！", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                conn.Close();
                UpdateData();
            }
        }

        private void ModifyUserName_Click(object sender, EventArgs e)
        {
            if (DateTime.Compare(lastclicktime.AddSeconds(1), DateTime.Now) > 0) //限制点击频率
            {
                MessageBox.Show("操作过于频繁！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
                lastclicktime = DateTime.Now;
            if (UsrView.SelectedItems.Count != 1)
            {
                MessageBox.Show("请选择一条信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            string str = "";
            foreach (ListViewItem lvi in UsrView.SelectedItems)
            {
                str = lvi.SubItems[0].Text.Trim();
            }
            string connString = "Data Source =.; Initial Catalog = FileManage; Integrated Security = True";
            SqlConnection conn = new SqlConnection(connString);
            string sql = "select count(*) from UserSafety where UserName=@UserName and (UserStatus='0' or UserStatus='1')";
            try
            {
                conn.Open();
                SqlCommand comm = new SqlCommand(sql, conn);
                comm.Parameters.AddWithValue("@UserName", str);
                int num = (int)comm.ExecuteScalar();
                if (num <= 0)
                    throw new Exception("用户账户繁忙！");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "修改用户名失败！", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            finally
            {
                conn.Close();
            }
            ModifyUName mun = new ModifyUName(str, this);
            mun.Show();
                
        }

        private void ResetPwd_Click(object sender, EventArgs e)
        {
            if (DateTime.Compare(lastclicktime.AddSeconds(1), DateTime.Now) > 0) //限制点击频率
            {
                MessageBox.Show("操作过于频繁！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
                lastclicktime = DateTime.Now;
            if (UsrView.SelectedItems.Count != 1)
            {
                MessageBox.Show("请选择一条信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string str = "";
            foreach (ListViewItem lvi in UsrView.SelectedItems)
            {
                str = lvi.SubItems[0].Text.Trim();
            }
            string connString = "Data Source =.; Initial Catalog = FileManage; Integrated Security = True";
            SqlConnection conn = new SqlConnection(connString);
            string sql = "select count(*) from UserSafety where UserName=@UserName and (UserStatus='0' or UserStatus='1')";
            try
            {
                conn.Open();
                SqlCommand comm = new SqlCommand(sql, conn);
                comm.Parameters.AddWithValue("@UserName", str);
                int num = (int)comm.ExecuteScalar();
                if (num <= 0)
                    throw new Exception("用户账户繁忙！");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "重置用户密码失败！", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            finally
            {
                conn.Close();
            }
            ModifyUPass rps = new ModifyUPass(str);
            rps.Show();
        }

        private void ModifyUserDept_Click(object sender, EventArgs e)
        {
            if (DateTime.Compare(lastclicktime.AddSeconds(1), DateTime.Now) > 0) //限制点击频率
            {
                MessageBox.Show("操作过于频繁！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
                lastclicktime = DateTime.Now;
            if (UsrView.SelectedItems.Count != 1)
            {
                MessageBox.Show("请选择一条信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string str = "";
            foreach (ListViewItem lvi in UsrView.SelectedItems)
            {
                str = lvi.SubItems[0].Text.Trim();
            }
            string connString = "Data Source =.; Initial Catalog = FileManage; Integrated Security = True";
            SqlConnection conn = new SqlConnection(connString);
            string sql = "select count(*) from UserSafety where UserName=@UserName and (UserStatus='0' or UserStatus='1')";
            try
            {
                conn.Open();
                SqlCommand comm = new SqlCommand(sql, conn);
                comm.Parameters.AddWithValue("@UserName", str);
                int num = (int)comm.ExecuteScalar();
                if (num <= 0)
                    throw new Exception("用户账户繁忙！");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "修改用户所属部门失败！", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            finally
            {
                conn.Close();
            }
            ModifyUDep mudep = new ModifyUDep(str,this);
            mudep.Show();
        }

        private void NewUser_Click(object sender, EventArgs e)
        {
            if (DateTime.Compare(lastclicktime.AddSeconds(1), DateTime.Now) > 0) //限制点击频率
            {
                MessageBox.Show("操作过于频繁！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
                lastclicktime = DateTime.Now;
            CreateUser cu = new CreateUser(this);
            cu.Show();
        }

        private void DeleteDept_Click(object sender, EventArgs e)
        {
            if (DateTime.Compare(lastclicktime.AddSeconds(1), DateTime.Now) > 0) //限制点击频率
            {
                MessageBox.Show("操作过于频繁！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
                lastclicktime = DateTime.Now;
            if (DepView.SelectedItems.Count != 1)
            {
                MessageBox.Show("请选择一条信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DialogResult result = MessageBox.Show("确定要删除该部门吗？\n注意该操作无法恢复！", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
            if (result != DialogResult.OK)
                return;
            string connString = "Data Source =.; Initial Catalog = FileManage; Integrated Security = True";
            SqlConnection conn = new SqlConnection(connString);
            string sql = "delete from Department where DepName=@DepName";
            string str = "";
            try
            {
                conn.Open();
                SqlCommand comm = new SqlCommand(sql, conn);
                foreach (ListViewItem lvi in DepView.SelectedItems)
                {
                    str = lvi.SubItems[0].Text.Trim();
                    comm.Parameters.AddWithValue("@DepName", str);
                }

                int num = (int)comm.ExecuteNonQuery();
                if (num <= 0)
                    throw new Exception("数据库异常");
                MessageBox.Show("删除部门成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "删除部门失败！", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                conn.Close();
                UpdateData();
            }
        }

        private void ModifyDeptName_Click(object sender, EventArgs e)
        {
            if (DateTime.Compare(lastclicktime.AddSeconds(1), DateTime.Now) > 0) //限制点击频率
            {
                MessageBox.Show("操作过于频繁！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
                lastclicktime = DateTime.Now;
            if (DepView.SelectedItems.Count != 1)
            {
                MessageBox.Show("请选择一条信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string str = "";
            foreach (ListViewItem lvi in DepView.SelectedItems)
            {
                str = lvi.SubItems[0].Text.Trim();
            }
            ModifyDName mdn = new ModifyDName(str, this);
            mdn.Show();
        }

        private void ModifyDeptLoc_Click(object sender, EventArgs e)
        {
            if (DateTime.Compare(lastclicktime.AddSeconds(1), DateTime.Now) > 0) //限制点击频率
            {
                MessageBox.Show("操作过于频繁！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
                lastclicktime = DateTime.Now;
            if (DepView.SelectedItems.Count != 1)
            {
                MessageBox.Show("请选择一条信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string str = "";
            foreach (ListViewItem lvi in DepView.SelectedItems)
            {
                str = lvi.SubItems[0].Text.Trim();
            }
            ModifyDLoc mdl = new ModifyDLoc(str, this);
            mdl.Show();
        }

        private void ListDeptPermission_Click(object sender, EventArgs e)
        {
            if (DateTime.Compare(lastclicktime.AddSeconds(1), DateTime.Now) > 0) //限制点击频率
            {
                MessageBox.Show("操作过于频繁！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
                lastclicktime = DateTime.Now;
            if (DepView.SelectedItems.Count != 1)
            {
                MessageBox.Show("请选择一条信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string str = "";
            foreach (ListViewItem lvi in DepView.SelectedItems)
            {
                str = lvi.SubItems[0].Text.Trim();
            }
            AccessView.Clear();
            string connString = "Data Source =.; Initial Catalog = FileManage; Integrated Security = True";
            SqlConnection conn = new SqlConnection(connString);
            String sql = "select DepName,SubName,privilege from AccessList where DepName=@DepName";
            try
            {
                conn.Open();
                SqlCommand comm = new SqlCommand(sql, conn);
                comm.Parameters.AddWithValue("@DepName", str);
                SqlDataReader reader = comm.ExecuteReader();
                AccessView.Columns.Add("部门名", 100, HorizontalAlignment.Center);
                AccessView.Columns.Add("主题名", 100, HorizontalAlignment.Center);
                AccessView.Columns.Add("拥有的权限", 100, HorizontalAlignment.Center);
                while (reader.Read())
                {
                    ListViewItem item = new ListViewItem();
                    item.SubItems.Clear();
                    item.SubItems[0].Text = reader[0].ToString().Trim();
                    item.SubItems.Add(reader[1].ToString().Trim());
                    int temp = int.Parse(reader[2].ToString().Trim());
                    if (temp == 1)
                        item.SubItems.Add("浏览");
                    else if (temp == 2)
                        item.SubItems.Add("管理");
                    else
                        throw new Exception("权限数据不合法！");
                    
                    AccessView.Items.Add(item);
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

        private void AddDept_Click(object sender, EventArgs e)
        {
            if (DateTime.Compare(lastclicktime.AddSeconds(1), DateTime.Now) > 0) //限制点击频率
            {
                MessageBox.Show("操作过于频繁！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
                lastclicktime = DateTime.Now;
            CreateDept cd = new CreateDept(this);
            cd.Show();
        }

        private void DeleteSub_Click(object sender, EventArgs e)
        {
            if (DateTime.Compare(lastclicktime.AddSeconds(1), DateTime.Now) > 0) //限制点击频率
            {
                MessageBox.Show("操作过于频繁！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
                lastclicktime = DateTime.Now;
            if (SubView.SelectedItems.Count != 1)
            {
                MessageBox.Show("请选择一条信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DialogResult result = MessageBox.Show("确定要删除该主题吗？\n注意该操作无法恢复！", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
            if (result != DialogResult.OK)
                return;
            string connString = "Data Source =.; Initial Catalog = FileManage; Integrated Security = True";
            SqlConnection conn = new SqlConnection(connString);
            string sql = "delete from SubjectList where SubName=@SubName";
            string str = "";
            try
            {
                conn.Open();
                SqlCommand comm = new SqlCommand(sql, conn);
                foreach (ListViewItem lvi in SubView.SelectedItems)
                {
                    str = lvi.SubItems[0].Text.Trim();
                    comm.Parameters.AddWithValue("@SubName", str);
                }

                int num = (int)comm.ExecuteNonQuery();
                if (num <= 0)
                    throw new Exception("数据库异常");
                if (Directory.Exists("C:\\ftp\\" + str))
                {
                    Directory.Delete("C:\\ftp\\" + str, true);
                }
                MessageBox.Show("删除主题成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "删除主题失败！", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                conn.Close();
                UpdateData();
            }
        }

        private void ModifySubName_Click(object sender, EventArgs e)
        {
            if (DateTime.Compare(lastclicktime.AddSeconds(1), DateTime.Now) > 0) //限制点击频率
            {
                MessageBox.Show("操作过于频繁！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
                lastclicktime = DateTime.Now;
            if (SubView.SelectedItems.Count != 1)
            {
                MessageBox.Show("请选择一条信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string str = "";
            foreach (ListViewItem lvi in SubView.SelectedItems)
            {
                str = lvi.SubItems[0].Text.Trim();
            }
            ModifySName msn = new ModifySName(str, this);
            msn.Show();
        }

        private void ModifySubContent_Click(object sender, EventArgs e)
        {
            if (DateTime.Compare(lastclicktime.AddSeconds(1), DateTime.Now) > 0) //限制点击频率
            {
                MessageBox.Show("操作过于频繁！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
                lastclicktime = DateTime.Now;
            if (SubView.SelectedItems.Count != 1)
            {
                MessageBox.Show("请选择一条信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string str = "";
            foreach (ListViewItem lvi in SubView.SelectedItems)
            {
                str = lvi.SubItems[0].Text.Trim();
            }
            ModifySContent msc = new ModifySContent(str, this);
            msc.Show();
        }

        private void ListSubPermission_Click(object sender, EventArgs e)
        {
            if (DateTime.Compare(lastclicktime.AddSeconds(1), DateTime.Now) > 0) //限制点击频率
            {
                MessageBox.Show("操作过于频繁！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
                lastclicktime = DateTime.Now;
            if (SubView.SelectedItems.Count != 1)
            {
                MessageBox.Show("请选择一条信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string str = "";
            foreach (ListViewItem lvi in SubView.SelectedItems)
            {
                str = lvi.SubItems[0].Text.Trim();
            }
            AccessView.Clear();
            string connString = "Data Source =.; Initial Catalog = FileManage; Integrated Security = True";
            SqlConnection conn = new SqlConnection(connString);
            String sql = "select DepName,SubName,privilege from AccessList where SubName=@SubName";
            try
            {
                conn.Open();
                SqlCommand comm = new SqlCommand(sql, conn);
                comm.Parameters.AddWithValue("@SubName", str);
                SqlDataReader reader = comm.ExecuteReader();
                AccessView.Columns.Add("部门名", 100, HorizontalAlignment.Center);
                AccessView.Columns.Add("主题名", 100, HorizontalAlignment.Center);
                AccessView.Columns.Add("拥有的权限", 100, HorizontalAlignment.Center);
                while (reader.Read())
                {
                    ListViewItem item = new ListViewItem();
                    item.SubItems.Clear();
                    item.SubItems[0].Text = reader[0].ToString().Trim();
                    item.SubItems.Add(reader[1].ToString().Trim());
                    int temp = int.Parse(reader[2].ToString().Trim());
                    if (temp == 1)
                        item.SubItems.Add("浏览");
                    else if (temp == 2)
                        item.SubItems.Add("管理");
                    else
                        throw new Exception("权限数据不合法！");
                    AccessView.Items.Add(item);
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

        private void AddSub_Click(object sender, EventArgs e)
        {
            if (DateTime.Compare(lastclicktime.AddSeconds(1), DateTime.Now) > 0) //限制点击频率
            {
                MessageBox.Show("操作过于频繁！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
                lastclicktime = DateTime.Now;
            CreateSub cs = new CreateSub(this);
            cs.Show();
        }

        private void ModifyPermission_Click(object sender, EventArgs e)
        {
            if (DateTime.Compare(lastclicktime.AddSeconds(1), DateTime.Now) > 0) //限制点击频率
            {
                MessageBox.Show("操作过于频繁！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
                lastclicktime = DateTime.Now;
            if (AccessView.SelectedItems.Count != 1)
            {
                MessageBox.Show("请选择一条信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string str1 = "";
            string str2 = "";
            string str3 = "";
            foreach (ListViewItem lvi in AccessView.SelectedItems)
            {
                str1 = lvi.SubItems[0].Text.Trim();
                str2 = lvi.SubItems[1].Text.Trim();
                str3 = lvi.SubItems[2].Text.Trim();
            }
            ModifyAccess mas = new ModifyAccess(str1, str2, str3, this);
            mas.Show();
            AccessView.Clear();
        }

        private void WithdrawPermission_Click(object sender, EventArgs e)
        {
            if (DateTime.Compare(lastclicktime.AddSeconds(1), DateTime.Now) > 0) //限制点击频率
            {
                MessageBox.Show("操作过于频繁！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
                lastclicktime = DateTime.Now;
            if (AccessView.SelectedItems.Count != 1)
            {
                MessageBox.Show("请选择一条信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DialogResult result = MessageBox.Show("确定要撤销该权限吗？\n注意该操作无法恢复！", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
            if (result != DialogResult.OK)
                return;
            string connString = "Data Source =.; Initial Catalog = FileManage; Integrated Security = True";
            SqlConnection conn = new SqlConnection(connString);
            string sql = "delete from AccessList where DepName=@DepName and SubName=@SubName";
            string str1 = "";
            string str2 = "";
            string str3 = "";
            try
            {
                foreach (ListViewItem lvi in AccessView.SelectedItems)
                {
                    str1 = lvi.SubItems[0].Text.Trim();
                    str2 = lvi.SubItems[1].Text.Trim();
                    str3 = lvi.SubItems[2].Text.Trim();
                }
                if(str3=="管理")
                {
                    //撤销管理权限后，该主题将无具有管理权限的部门
                    throw new Exception("每个主题有且仅有一个部门可以有管理权限！");
                    
                }
                
                conn.Open();
                SqlCommand comm = new SqlCommand(sql, conn);
                
                comm.Parameters.AddWithValue("@DepName", str1);
                comm.Parameters.AddWithValue("@SubName", str2);
                int num = (int)comm.ExecuteNonQuery();
                if (num <= 0)
                    throw new Exception("数据库异常");
                MessageBox.Show("撤销权限成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "撤销权限失败！", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                conn.Close();
                AccessView.Clear();
            }
        }

        private void GrantPermission_Click(object sender, EventArgs e)
        {
            if (DateTime.Compare(lastclicktime.AddSeconds(1), DateTime.Now) > 0) //限制点击频率
            {
                MessageBox.Show("操作过于频繁！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
                lastclicktime = DateTime.Now;
            CreateAccess cas = new CreateAccess(this);
            cas.Show();
            AccessView.Clear();
        }

        private void AccessTransfer_Click(object sender, EventArgs e)
        {
            if (DateTime.Compare(lastclicktime.AddSeconds(1), DateTime.Now) > 0) //限制点击频率
            {
                MessageBox.Show("操作过于频繁！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
                lastclicktime = DateTime.Now;
            if (AccessView.SelectedItems.Count != 1)
            {
                MessageBox.Show("请选择一条信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string str1 = "";
            string str2 = "";
            string str3 = "";
            foreach (ListViewItem lvi in AccessView.SelectedItems)
            {
                str1 = lvi.SubItems[0].Text.Trim();
                str2 = lvi.SubItems[1].Text.Trim();
                str3 = lvi.SubItems[2].Text.Trim();
                if(str3!="管理")
                {
                    MessageBox.Show("该功能仅供转移管理权限", "转移管理权失败！", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }
            TransferAccess ta = new TransferAccess(str1, str2);
            ta.Show();
            AccessView.Clear();
        }

        private void ViewSyslog_Click(object sender, EventArgs e)
        {
            if (DateTime.Compare(lastclicktime.AddSeconds(1), DateTime.Now) > 0) //限制点击频率
            {
                MessageBox.Show("操作过于频繁！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
                lastclicktime = DateTime.Now;
            SystemLog sl = new SystemLog();
            sl.Show();
        }

        private void ViewDoc_Click(object sender, EventArgs e)
        {
            if (DateTime.Compare(lastclicktime.AddSeconds(1), DateTime.Now) > 0) //限制点击频率
            {
                MessageBox.Show("操作过于频繁！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
                lastclicktime = DateTime.Now;
            DocView dv = new DocView();
            dv.Show();
        }

        private void ModifyAdminPwd_Click(object sender, EventArgs e)
        {
            if (DateTime.Compare(lastclicktime.AddSeconds(1), DateTime.Now) > 0) //限制点击频率
            {
                MessageBox.Show("操作过于频繁！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
                lastclicktime = DateTime.Now;
            ModifyAdminPass map = new ModifyAdminPass(this);
            map.Show();
        }

        private void LogOut_Click(object sender, EventArgs e)
        {
            if (DateTime.Compare(lastclicktime.AddSeconds(1), DateTime.Now) > 0) //限制点击频率
            {
                MessageBox.Show("操作过于频繁！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
                lastclicktime = DateTime.Now;
            DialogResult result = MessageBox.Show("确定要退出登陆吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
            if (result == DialogResult.OK)
            {

                this.Close();
            }
            
        }

        private void Aboutme_Click(object sender, EventArgs e)
        {
            if (DateTime.Compare(lastclicktime.AddSeconds(1), DateTime.Now) > 0) //限制点击频率
            {
                MessageBox.Show("操作过于频繁！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
                lastclicktime = DateTime.Now;
            AboutMeForm am = new AboutMeForm();
            am.Show();
        }

        private void LockAcc_Click(object sender, EventArgs e)
        {
            if (DateTime.Compare(lastclicktime.AddSeconds(1), DateTime.Now) > 0) //限制点击频率
            {
                MessageBox.Show("操作过于频繁！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
                lastclicktime = DateTime.Now;
            if (UsrView.SelectedItems.Count != 1)
            {
                MessageBox.Show("请选择一条信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DialogResult result = MessageBox.Show("确定要锁定该用户账户吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
            if (result != DialogResult.OK)
                return;
            string connString = "Data Source =.; Initial Catalog = FileManage; Integrated Security = True";
            SqlConnection conn = new SqlConnection(connString);
            string sql = "Update UserSafety Set UserStatus='-1' where UserName=@UserName and (UserStatus='0' or UserStatus='1')";
            string str = "";
            try
            {
                conn.Open();
                SqlCommand comm = new SqlCommand(sql, conn);
                foreach (ListViewItem lvi in UsrView.SelectedItems)
                {
                    str = lvi.SubItems[0].Text.Trim();
                    comm.Parameters.AddWithValue("@UserName", str);
                }

                int num = (int)comm.ExecuteNonQuery();
                if (num <= 0)
                    throw new Exception("用户账户繁忙！");
                MessageBox.Show("锁定用户账户成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "锁定用户账户失败！", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                conn.Close();
                UpdateData();
            }
        }

        private void UnlockAcc_Click(object sender, EventArgs e)
        {
            if (DateTime.Compare(lastclicktime.AddSeconds(1), DateTime.Now) > 0) //限制点击频率
            {
                MessageBox.Show("操作过于频繁！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
                lastclicktime = DateTime.Now;
            if (UsrView.SelectedItems.Count != 1)
            {
                MessageBox.Show("请选择一条信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DialogResult result = MessageBox.Show("确定要解锁该用户账户吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
            if (result != DialogResult.OK)
                return;
            string connString = "Data Source =.; Initial Catalog = FileManage; Integrated Security = True";
            SqlConnection conn = new SqlConnection(connString);
            string sql = "Update UserSafety Set UserStatus='1' where UserName=@UserName and UserStatus='-1'";
            string str = "";
            try
            {
                conn.Open();
                SqlCommand comm = new SqlCommand(sql, conn);
                foreach (ListViewItem lvi in UsrView.SelectedItems)
                {
                    str = lvi.SubItems[0].Text.Trim();
                    comm.Parameters.AddWithValue("@UserName", str);
                }

                int num = (int)comm.ExecuteNonQuery();
                if (num <= 0)
                    throw new Exception("用户账户未被锁定！");
                MessageBox.Show("解锁用户账户成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "解锁用户账户失败！", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                conn.Close();
                UpdateData();
            }
        }

        private void Backup_Recover_Click(object sender, EventArgs e)
        {
            BackupandRecover br = new BackupandRecover();
            br.Show();
        }
        

    }
}
