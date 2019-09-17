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
    public partial class CreateUser : Form
    {
        private MainForm mf = null;
        public CreateUser(MainForm m)
        {
            InitializeComponent();
            BackgroundImage = Image.FromFile("main.jpg");
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

        private void admit_btn_Click(object sender, EventArgs e)
        {
            string connString = "Data Source =.; Initial Catalog = FileManage; Integrated Security = True";
            SqlConnection conn = new SqlConnection(connString);
            string sql = "insert into UserList(UserName,UserPass,UserDep) values(@UserName,@UserPass,@UserDep)";
            
            try
            {
                string uname = Security_Test.SafeSQL(usrname.Text.Trim());
                string pwd = Security_Test.SafeSQL(passwd.Text.Trim());
                if (!Security_Test.IsSecurePWD(pwd)) throw new Exception("密码不符合要求！密码应为6-30位的字母和数字");
                pwd = CryptoClass.SHA256String(pwd);
                string pwdadmit = Security_Test.SafeSQL(passwdadmit.Text.Trim());
                pwdadmit = CryptoClass.SHA256String(pwdadmit);
                string dept = DeptChoose.SelectedItem.ToString().Trim();
                if (uname == null || uname == "") throw new Exception("用户名不能为空！");
                if (pwd == null || pwd == "") throw new Exception("密码不能为空！");
                if (pwd != pwdadmit) throw new Exception("两次密码输入不一致！");
                if (dept == null || dept == "") throw new Exception("请选择一个部门！");
                DialogResult result = MessageBox.Show("确定要创建新用户吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                if (result != DialogResult.OK)
                    return;
                conn.Open();
                SqlCommand comm = new SqlCommand(sql, conn);
                comm.Parameters.AddWithValue("@UserName", uname);
                comm.Parameters.AddWithValue("@UserPass", pwd);
                comm.Parameters.AddWithValue("@UserDep", dept);
                int num = (int)comm.ExecuteNonQuery();
                if (num <= 0)
                {
                    conn.Close();
                    throw new Exception("数据库异常");
                }
                conn.Close();
                sql = "insert into UserSafety(UserName,UserStatus,Usercontact) values(@UserName,'1',NULL)";
                conn.Open();
                SqlCommand comm1 = new SqlCommand(sql, conn);
                comm1.Parameters.AddWithValue("@UserName", uname);
                num = (int)comm1.ExecuteNonQuery();
                if (num <= 0)
                {
                    conn.Close();
                    throw new Exception("数据库异常");
                }
                MessageBox.Show("创建用户成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                mf.UpdateData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "创建新用户失败！", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                usrname.Clear();
                passwd.Clear();
                passwdadmit.Clear();

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
