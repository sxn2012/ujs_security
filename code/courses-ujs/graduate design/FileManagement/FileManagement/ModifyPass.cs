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
    public partial class ModifyPass : Form
    {
        private MainForm mf = null;
        public ModifyPass(MainForm m)
        {
            InitializeComponent();
            BackgroundImage = Image.FromFile("main.jpg");
            mf = m;
            string connString = "Data Source =101.132.111.23; Initial Catalog = FileManage; User ID = userlogin; Pwd = User123456";
            SqlConnection conn = new SqlConnection(connString);
            string sql = "select Usercontact from UserSafety where UserName=@UserName and Usercontact is not null";
            try
            {
                conn.Open();
                SqlCommand comm = new SqlCommand(sql, conn);
                comm.Parameters.AddWithValue("@Username", mf.u_name);
                string email = (string)comm.ExecuteScalar();
                if (email == null || email.Trim() == "")
                {
                    throw new Exception("该用户无法修改密码，请咨询系统管理员！");
                }
                else
                {
                    email = CryptoClass.AesDecrypt(email, CryptoClass.key);
                    this.Visible = false;
                    VerifyID vi = new VerifyID(email.Trim(), this);
                    vi.Show();
                    
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "无法修改密码!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
            }
        }

        private void admit_btn_Click(object sender, EventArgs e)
        {
            mf.countnum = 0;//清零
            string lold = Security_Test.SafeSQL(origin.Text.Trim());
            lold = CryptoClass.SHA256String(lold);
            string connString = "Data Source =101.132.111.23; Initial Catalog = FileManage; User ID = userlogin; Pwd = User123456";
            SqlConnection conn = new SqlConnection(connString);
            string sql = "update UserList set UserPass=@UserPass where UserName=@UserName and UserPass=@oldPass";
            
            try
            {
                string pwd = Security_Test.SafeSQL(passwd.Text.Trim());
                if (!Security_Test.IsSecurePWD(pwd)) throw new Exception("密码不符合要求！密码应为6-30位的字母和数字");
                pwd = CryptoClass.SHA256String(pwd);
                string pwdadmit = Security_Test.SafeSQL(passwdadmit.Text.Trim());
                pwdadmit = CryptoClass.SHA256String(pwdadmit);
                if (pwd == null || pwd == "") throw new Exception("密码不能为空！");
                if (pwd != pwdadmit) throw new Exception("两次密码输入不一致！");
                if (lold == pwd) throw new Exception("新密码不能与原密码相同！");
                DialogResult result = MessageBox.Show("确定要修改密码吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                if (result != DialogResult.OK)
                    return;
                conn.Open();
                SqlCommand comm = new SqlCommand(sql, conn);
                comm.Parameters.AddWithValue("@UserPass", pwd);
                comm.Parameters.AddWithValue("@UserName", mf.u_name);
                comm.Parameters.AddWithValue("@oldPass", lold);


                int num = (int)comm.ExecuteNonQuery();
                if (num <= 0)
                {
                    conn.Close();
                    throw new Exception("原密码错误！");
                }
                mf._sc.Sending(mf.u_name + "用户修改密码成功");
                MessageBox.Show("修改密码成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MessageBox.Show("修改密码后需要重新登陆！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                mf.Close();
            }
            catch (Exception ex)
            {
                mf._sc.Sending(mf.u_name + "用户修改密码失败");
                MessageBox.Show(ex.Message, "修改密码失败！", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                origin.Clear();
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
