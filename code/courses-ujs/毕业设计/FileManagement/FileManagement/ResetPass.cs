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

namespace FileManagement
{
    public partial class ResetPass : Form
    {
        private string uname = null;
        public ResetPass()
        {
            InitializeComponent();
            BackgroundImage = Image.FromFile("main.jpg");
            label1.Visible = false;
            label2.Visible = false;
            passwd.Visible = false;
            passwdadmit.Visible = false;
            btn_commit.Visible = false;
            label3.Visible = true;
            usrname.Visible = true;
            admit_btn.Visible = true;
        }

        private void admit_btn_Click(object sender, EventArgs e)
        {
            try
            {
                string connString = "Data Source =101.132.111.23; Initial Catalog = FileManage; User ID = userlogin; Pwd = User123456";
                SqlConnection conn = new SqlConnection(connString);
                string sql = "select Usercontact from UserSafety where UserName=@UserName and Usercontact is not null and UserStatus='0'";
                conn.Open();
                SqlCommand comm = new SqlCommand(sql, conn);
                uname = Security_Test.SafeSQL(usrname.Text.Trim());
                comm.Parameters.AddWithValue("@Username", uname);
                string email = (string)comm.ExecuteScalar();
                if (email == null || email.Trim() == "")
                {
                    throw new Exception("该用户无法找回密码，请咨询系统管理员！");
                }
                else
                {
                    email = CryptoClass.AesDecrypt(email, CryptoClass.key);
                    VerifyID vi = new VerifyID(email, this);
                    vi.Show();
                    this.Visible = false;
                    label1.Visible = true;
                    label2.Visible = true;
                    passwd.Visible = true;
                    passwdadmit.Visible = true;
                    btn_commit.Visible = true;
                    label3.Visible = false;
                    usrname.Visible = false;
                    admit_btn.Visible = false;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "无法找回密码!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
            }
        }

        private void btn_commit_Click(object sender, EventArgs e)
        {
            string connString = "Data Source =101.132.111.23; Initial Catalog = FileManage; User ID = userlogin; Pwd = User123456";
            SqlConnection conn = new SqlConnection(connString);
            string sql = "update UserList set UserPass=@UserPass where UserName=@UserName";
            
            try
            {
                string pwd = Security_Test.SafeSQL(passwd.Text.Trim());
                if (!Security_Test.IsSecurePWD(pwd)) throw new Exception("密码不符合要求！密码应为6-30位的字母和数字");
                pwd = CryptoClass.SHA256String(pwd);
                string pwdadmit = Security_Test.SafeSQL(passwdadmit.Text.Trim());
                pwdadmit = CryptoClass.SHA256String(pwdadmit);
                if (pwd == null || pwd == "") throw new Exception("密码不能为空！");
                if (pwd != pwdadmit) throw new Exception("两次密码输入不一致！");
                DialogResult result = MessageBox.Show("确定要修改密码吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                if (result != DialogResult.OK)
                    return;
                conn.Open();
                SqlCommand comm = new SqlCommand(sql, conn);
                comm.Parameters.AddWithValue("@UserPass", pwd);
                comm.Parameters.AddWithValue("@UserName", uname);
                


                int num = (int)comm.ExecuteNonQuery();
                if (num <= 0)
                {
                    conn.Close();
                    throw new Exception("数据库异常");
                }
                
                MessageBox.Show("找回密码成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.Message, "找回密码失败！", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
