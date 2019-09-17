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
    public partial class ModifyUDep : Form
    {
        private string username = null;
        private MainForm mf = null;
        public ModifyUDep(string uname,MainForm m)
        {
            InitializeComponent();
            BackgroundImage = Image.FromFile("main.jpg");
            username = uname;
            mf = m;
            string connString = "Data Source =.; Initial Catalog = FileManage; Integrated Security = True";
            SqlConnection conn = new SqlConnection(connString);
            String sql = "select DepName from Department";
            try
            {
                DeptChoose.Items.Clear();
                conn.Open();
                SqlCommand comm = new SqlCommand(sql, conn);
                SqlDataReader reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    DeptChoose.Items.Add(reader[0].ToString().Trim());
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

        private void btn_admit_Click(object sender, EventArgs e)
        {
            string choosingdept = DeptChoose.SelectedItem.ToString().Trim();
            string connString = "Data Source =.; Initial Catalog = FileManage; Integrated Security = True";
            SqlConnection conn = new SqlConnection(connString);
            string sql = "update UserList set UserDep=@UserDep where UserName=@UserName";
            string str = username;
            string userdep = choosingdept;
            try
            {
                if (userdep == null || userdep == "") throw new Exception("请选择一个部门！");
                DialogResult result = MessageBox.Show("确定要修改用户所属部门吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                if (result != DialogResult.OK)
                    return;
                conn.Open();
                SqlCommand comm = new SqlCommand(sql, conn);
                comm.Parameters.AddWithValue("@UserDep", userdep);
                comm.Parameters.AddWithValue("@UserName", str);
                int num = (int)comm.ExecuteNonQuery();
                if (num <= 0)
                {
                    conn.Close();
                    throw new Exception("数据库异常");
                }
                MessageBox.Show("修改用户所属部门成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "修改用户所属部门失败！", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                return;
            }
            finally
            {
                conn.Close();
                mf.UpdateData();
            }
            this.Close();
        }
    }
}
