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
    public partial class LoginForm : Form
    {
        private Socket_Receive sserver = null;
        //验证码的长度
        private const int iVerifyCodeLength = 6;
        //验证码
        private String strVerifyCode = "";
        public LoginForm()
        {
            InitializeComponent();
            BackgroundImage = Image.FromFile("main.jpg");
            sserver = new Socket_Receive("0.0.0.0", 8888);
            label5.Text = "";
            UpdateVerifyCode();
        }

        public void UpdateVerifyCode()//更新验证码
        {
            strVerifyCode = Second_Verify.CreateRandomCode(iVerifyCodeLength);
            if (Second_Verify.CreateImage(strVerifyCode))
                pbVerifyCode.Image = Second_Verify.img;
            else
                MessageBox.Show(Second_Verify.pic_e.Message, "创建图片错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            String strenter = VerifyCode.Text.Trim();
            String strcorrect = strVerifyCode.Trim();
            if (strenter == "")
            {
                label5.Text = "";
                return;
            }
            strenter = strenter.ToUpper();
            if (strenter == strcorrect) label5.Text = "√";
            else label5.Text = "✖";
        }

        private void login_btn_Click(object sender, EventArgs e)
        {
            string lname = Security_Test.SafeSQL(usrname.Text.Trim());
            string lpass = Security_Test.SafeSQL(passwd.Text.Trim());
            lpass = CryptoClass.SHA256String(lpass);
            string connString = "Data Source =.; Initial Catalog = FileManage; Integrated Security = True";
            SqlConnection conn = new SqlConnection(connString);
            String sql = "select count(*) from UserList where UserName=@UserName and UserPass=@UserPass";
            try
            {
                if (lname.ToLower().Trim() != "admin")
                    throw new Exception("用户名或密码错误!");
                if (label5.Text != "√") throw new Exception("验证码不正确！");
                conn.Open();
                SqlCommand comm = new SqlCommand(sql, conn);
                comm.Parameters.AddWithValue("@UserName", lname);
                comm.Parameters.AddWithValue("@UserPass", lpass);
                int num = (int)comm.ExecuteScalar();
                if (num == 1)
                {
                    //login successful 


                    MainForm Form_m = new MainForm(this,sserver);
                    Form_m.Show();
                    this.Visible = false;
                }
                else
                {
                    //login failed
                    throw new Exception("用户名或密码错误!");
                    
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "登录失败！", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                conn.Close();
                usrname.Clear();
                passwd.Clear();
                VerifyCode.Clear();
                UpdateVerifyCode();
            }
        }

        private void pbVerifyCode_Click(object sender, EventArgs e)
        {
            UpdateVerifyCode();
        }

        private void VerifyCode_TextChanged(object sender, EventArgs e)
        {
            String strenter = VerifyCode.Text.Trim();
            String strcorrect = strVerifyCode.Trim();
            if (strenter == "")
            {
                label5.Text = "";
                return;
            }
            strenter = strenter.ToUpper();
            if (strenter == strcorrect) label5.Text = "√";
            else label5.Text = "✖";
        }
    }
}
