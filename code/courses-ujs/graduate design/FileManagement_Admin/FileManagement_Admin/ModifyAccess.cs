using System;
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

namespace FileManagement_Admin
{
    public partial class ModifyAccess : Form
    {
        private string dname = null;
        private string sname = null;
        private string oldacc = null;
        private MainForm mf = null;
        public ModifyAccess(string dn,string sn,string oa,MainForm m)
        {
            InitializeComponent();
            BackgroundImage = Image.FromFile("main.jpg");
            privilege.Items.Clear();
            privilege.Items.Add("浏览");
            privilege.Items.Add("管理");
            dname = dn;
            sname = sn;
            oldacc = oa;
            mf = m;
            deptname.Items.Clear();
            subname.Items.Clear();
            string connString = "Data Source =.; Initial Catalog = FileManage; Integrated Security = True";
            SqlConnection conn = new SqlConnection(connString);
            String sql = "select DepName from Department";
            try
            {
                deptname.Items.Clear();
                conn.Open();
                SqlCommand comm = new SqlCommand(sql, conn);
                SqlDataReader reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    deptname.Items.Add(reader[0].ToString().Trim());
                }
                conn.Close();
                sql = "select SubName from SubjectList";
                subname.Items.Clear();
                conn.Open();
                SqlCommand comm1 = new SqlCommand(sql, conn);
                SqlDataReader reader1 = comm1.ExecuteReader();
                while (reader1.Read())
                {
                    subname.Items.Add(reader1[0].ToString().Trim());
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

        private void admit_btn_Click(object sender, EventArgs e)
        {
            string connString = "Data Source =.; Initial Catalog = FileManage; Integrated Security = True";
            SqlConnection conn = new SqlConnection(connString);
            string sql = "update AccessList set DepName=@newDepName, SubName=@newSubName, privilege=@newprivilege where DepName=@oldDepName and SubName=@oldSubName and privilege=@oldprivilege";
            
            string str_temp = "";
            string str_old = "";
            string dept = deptname.SelectedItem.ToString().Trim();
            string subj = subname.SelectedItem.ToString().Trim();
            string str_acc = privilege.SelectedItem.ToString().Trim();
            try
            {
                if (dept == null || dept == "") throw new Exception("请选择一个部门！");
                if (subj == null || subj == "") throw new Exception("请选择一个主题！");
                if (str_acc == null || str_acc == "") throw new Exception("请选择一种权限！");
                DialogResult result = MessageBox.Show("确定要修改权限吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                if (result != DialogResult.OK)
                    return;
                
                if (str_acc == "浏览") str_temp = "1";
                else if (str_acc == "管理") str_temp = "2";
                else throw new Exception("权限不合法！");
                if (oldacc == "浏览") str_old = "1";
                else if (oldacc == "管理") str_old = "2";
                else throw new Exception("权限不合法！");

                conn.Open();
                SqlCommand comm = new SqlCommand(sql, conn);
                comm.Parameters.AddWithValue("@newDepName", dept);
                comm.Parameters.AddWithValue("@newSubName", subj);
                comm.Parameters.AddWithValue("@newprivilege", str_temp);
                comm.Parameters.AddWithValue("@oldDepName", dname);
                comm.Parameters.AddWithValue("@oldSubName", sname);
                comm.Parameters.AddWithValue("@oldprivilege", str_old);
                int num = (int)comm.ExecuteNonQuery();
                if (num <= 0)
                {
                    
                    throw new Exception("数据库异常");
                }
                conn.Close();

                int flag = 0;//回滚标志
                //判断是否符合每个主题有且仅有一个部门可以有管理权限！
                sql = "select count(DepName) from AccessList where SubName=@SubName and privilege='2'";
                conn.Open();
                SqlCommand comm1 = new SqlCommand(sql, conn);
                comm1.Parameters.AddWithValue("@SubName", subj);
                num = (int)comm1.ExecuteScalar();
                if (num != 1)
                {
                    //Rollback
                    flag = 1;

                }
                conn.Close();
                sql = "select count(DepName) from AccessList where SubName=@SubName and privilege='2'";
                conn.Open();
                SqlCommand comm2 = new SqlCommand(sql, conn);
                comm2.Parameters.AddWithValue("@SubName", sname);
                num = (int)comm2.ExecuteScalar();
                if (num != 1)
                {
                    //Rollback
                    flag = 1;

                }
                conn.Close();
                if(flag!=0)
                {
                    sql = "update AccessList set DepName=@oldDepName, SubName=@oldSubName, privilege=@oldprivilege where DepName=@newDepName and SubName=@newSubName and privilege=@newprivilege";
                    conn.Open();
                    SqlCommand comm3 = new SqlCommand(sql, conn);
                    comm3.Parameters.AddWithValue("@newDepName", dept);
                    comm3.Parameters.AddWithValue("@newSubName", subj);
                    comm3.Parameters.AddWithValue("@newprivilege", str_temp);
                    comm3.Parameters.AddWithValue("@oldDepName", dname);
                    comm3.Parameters.AddWithValue("@oldSubName", sname);
                    comm3.Parameters.AddWithValue("@oldprivilege", str_old);
                    num = (int)comm3.ExecuteNonQuery();
                    if (num <= 0)
                    {
                        throw new Exception("数据库异常");
                    }
                    throw new Exception("每个主题有且仅有一个部门可以有管理权限！");
                }
                else
                    MessageBox.Show("修改权限成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "修改权限失败！", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                
                return;
            }
            finally
            {
                conn.Close();
                
            }
            this.Close();
        }
    }
}
