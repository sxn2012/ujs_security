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
    public partial class ModifyUName : Form
    {
        private string old_user = null;
        private MainForm mf = null;
        public ModifyUName(string ousr,MainForm m)
        {
            InitializeComponent();
            BackgroundImage = Image.FromFile("main.jpg");
            old_user = ousr;
            mf = m;
        }

        private void admit_btn_Click(object sender, EventArgs e)
        {
            string connString = "Data Source =.; Initial Catalog = FileManage; Integrated Security = True";
            SqlConnection conn = new SqlConnection(connString);
            string sql = "delete from UserSafety where UserName=@Username and (UserStatus='0' or UserStatus='1')";
            string str = old_user;
            string nuname = Security_Test.SafeSQL(newusrname.Text.Trim());
            try
            {
                if (nuname == null || nuname == "") throw new Exception("用户名不能为空！");
                DialogResult result = MessageBox.Show("确定要修改用户名吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                if (result != DialogResult.OK)
                    return;
                conn.Open();
                SqlCommand comm = new SqlCommand(sql, conn);
                comm.Parameters.AddWithValue("@UserName", str);
                int num = (int)comm.ExecuteNonQuery();
                if (num <= 0)
                {
                    conn.Close();
                    throw new Exception("用户账户繁忙！");
                }
                conn.Close();
                sql = "update UserList set Username=@NewUsername where UserName=@UserName";
                conn.Open();
                SqlCommand comm2 = new SqlCommand(sql, conn);
                comm2.Parameters.AddWithValue("@NewUsername", nuname);
                comm2.Parameters.AddWithValue("@UserName", str);
                num = (int)comm2.ExecuteNonQuery();
                if (num <= 0)
                {
                    conn.Close();
                    throw new Exception("数据库异常");
                }
                conn.Close();
                sql = "insert into UserSafety(UserName,UserStatus,Usercontact) values(@NewUserName,'1',NULL)";
                conn.Open();
                SqlCommand comm1 = new SqlCommand(sql, conn);
                comm1.Parameters.AddWithValue("@NewUserName", nuname);
                num = (int)comm1.ExecuteNonQuery();
                if (num <= 0)
                {
                    conn.Close();
                    throw new Exception("数据库异常");
                }

                MessageBox.Show("修改用户名成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "修改用户名失败！", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                newusrname.Clear();
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
