using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.Sql;

namespace FileManagement
{
    public partial class LoginForm : Form
    {
        public Socket_Send sclient = null;
        //验证码的长度
        private const int iVerifyCodeLength = 6;
        //验证码
        private String strVerifyCode = "";
        //密码错误的用户名
        private string Err_uname = "";
        private int Err_time = 0;//错误次数
        public LoginForm()
        {
            InitializeComponent();
            BackgroundImage = Image.FromFile("main.jpg");
            sclient = new Socket_Send("101.132.111.23", 8888);
            UpdateVerifyCode();
            RegNewUsr.LinkVisited = false;
            FindPwd.LinkVisited = false;
            label5.Text = "";
        }

        public void ShowOverTiming()
        {
            MessageBox.Show("长时间无操作，用户已退出登录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
            this.UseWaitCursor = true;//显示漏斗光标
            this.Refresh();
            string lname = Security_Test.SafeSQL(usrname.Text.Trim());
            string lpass = Security_Test.SafeSQL(passwd.Text.Trim());
            lpass = CryptoClass.SHA256String(lpass);
            //string laddr = ftpaddr.Text.Trim();
            string connString = "Data Source =101.132.111.23; Initial Catalog = FileManage; User ID = userlogin; Pwd = User123456";
            SqlConnection conn = new SqlConnection(connString);
            String sql = "select count(*) from UserList where Username=@Username and UserPass=@UserPass";
            try
            {
                if (lname == null || lname == "") throw new Exception("用户名不能为空！");
                if (lpass == null || lpass == "") throw new Exception("密码不能为空！");
                if (label5.Text != "√") throw new Exception("验证码不正确！");
                //if (laddr == null || laddr == "") throw new Exception("服务器地址不能为空！");
                conn.Open();
                SqlCommand comm = new SqlCommand(sql, conn);
                comm.Parameters.AddWithValue("@Username", lname);
                comm.Parameters.AddWithValue("@UserPass", lpass);
                int num = (int)comm.ExecuteScalar();
                
                if(num==1&&lname!="admin")
                {
                    //login successful
                    Err_time = 0;
                    Err_uname = "";
                    conn.Close();
                    sql = "select UserStatus from UserSafety where Username=@Username";
                    conn.Open();
                    SqlCommand comm1 = new SqlCommand(sql, conn);
                    comm1.Parameters.AddWithValue("@Username", lname);
                    num = int.Parse(comm1.ExecuteScalar().ToString().Trim());
                    if (num >= 0)
                    {
                        
                        FtpWeb ftp = new FtpWeb("101.132.111.23", "", "", "");
                        MainForm Form_m = new MainForm(this, ftp, sclient, lname);
                        
                        if (num ==1)//未初始化
                        {
                            
                            this.Visible = false;
                            EditEmail ee = new EditEmail(lname, Form_m, this);
                            ee.Show();
                        }
                        else if (num == 0)//状态正常
                        {
                            conn.Close();
                            sql = "Update UserSafety Set UserStatus='2' where UserName=@UserName";
                            conn.Open();
                            SqlCommand comm2 = new SqlCommand(sql, conn);
                            comm2.Parameters.AddWithValue("@Username", lname);
                            num = (int)comm2.ExecuteNonQuery();
                            if (num <= 0)
                                throw new Exception("数据库异常！");
                            sclient.Sending(lname + "用户登陆成功");
                            this.Visible = false;
                            Form_m.Show();
                            Form_m.loginflag = 1;
                        }
                        else//该账号在异地已登陆
                        {
                            throw new Exception("该账户存在风险，无法登陆！");
                        }
                        
                    }
                    else
                        throw new Exception("该用户已被锁定！请向系统管理员申请解锁！");
                     


                    
                }
                else
                {
                    //login failed
                    conn.Close();
                    if (Err_uname == lname)
                    {
                        Err_time++;
                    }
                    else
                    {
                        Err_uname = lname;
                        Err_time = 1;
                    }
                    if (Err_time >= 3)
                    {
                        conn.Close();
                        sql = "Update UserSafety Set UserStatus='-1' where UserName=@UserName";
                        conn.Open();
                        SqlCommand comm1 = new SqlCommand(sql, conn);
                        comm1.Parameters.AddWithValue("@Username", lname);
                        comm1.ExecuteNonQuery();//执行不成功时返回为0，表示无此用户，不处理
                        //复位
                        Err_uname = "";
                        Err_time = 0;
                    }
                    
                    throw new Exception("用户名或密码错误!");
                    
                }
            }
            catch (Exception ex)
            {
                if (lname != null && lname != "")
                    sclient.Sending(lname + "用户登陆失败");
                MessageBox.Show(ex.Message, "登录失败！", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                
               
            }
            finally
            {
                conn.Close();
                usrname.Clear();
                passwd.Clear();
                VerifyCode.Clear();
                UpdateVerifyCode();
                this.UseWaitCursor = false;//不显示漏斗光标
                this.Refresh();
            }
            
        }

        private void RegNewUsr_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RegNewUsr.LinkVisited = true;
            if (MessageBox.Show("请向系统管理员申请注册账号！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) == DialogResult.OK)
                RegNewUsr.LinkVisited = false;
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

        private void pbVerifyCode_Click(object sender, EventArgs e)
        {
            UpdateVerifyCode();
        }

        private void FindPwd_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ResetPass rp = new ResetPass();
            rp.Show();
        }

        
    }
}
