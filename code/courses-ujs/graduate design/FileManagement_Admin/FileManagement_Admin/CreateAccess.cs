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
    public partial class CreateAccess : Form
    {
        private MainForm mf = null;
        public CreateAccess(MainForm m)
        {
            InitializeComponent();
            BackgroundImage = Image.FromFile("main.jpg");
            mf = m;
            privilege.Items.Clear();
            privilege.Items.Add("浏览");
            privilege.Items.Add("管理");
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
            string sql = null;
            string dept = deptname.SelectedItem.ToString().Trim();
            string subj = subname.SelectedItem.ToString().Trim();
            string acc = privilege.SelectedItem.ToString().Trim();
            try
            {
                if (dept == null || dept == "") throw new Exception("请选择一个部门！");
                if (subj == null || subj == "") throw new Exception("请选择一个主题！");
                if (acc == null || acc == "") throw new Exception("请选择一种权限！");
                string temp = "";
                if (acc == "浏览") temp = "1";
                else if (acc == "管理")
                {
                    temp = "2";
                    sql = "select count(DepName) from AccessList where SubName=@SubName and privilege='2'";
                    conn.Open();
                    SqlCommand comm = new SqlCommand(sql, conn);
                    comm.Parameters.AddWithValue("@SubName", subj);
                    int num = (int)comm.ExecuteScalar();
                    if (num != 0)
                        throw new Exception("每个主题有且仅有一个部门可以有管理权限！");
                    conn.Close();
                }
                else throw new Exception("权限不合法！");
                DialogResult result = MessageBox.Show("确定要授予新权限吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                if (result != DialogResult.OK)
                    return;
                sql = "insert into AccessList(DepName,SubName,privilege) values(@DepName,@SubName,@privilege)";
                conn.Open();
                SqlCommand comm1 = new SqlCommand(sql, conn);
                comm1.Parameters.AddWithValue("@DepName", dept);
                comm1.Parameters.AddWithValue("@SubName", subj);
                comm1.Parameters.AddWithValue("@privilege", temp);
                int num1 = (int)comm1.ExecuteNonQuery();
                if (num1 <= 0)
                {
                    conn.Close();
                    throw new Exception("数据库异常");
                }
                MessageBox.Show("授予新权限成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "授予新权限失败！", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
