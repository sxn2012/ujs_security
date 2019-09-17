using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace FileManagement
{
    public partial class VerifyID : Form
    {
        private string lemail = null;
        private int remain = 120;//冻结发邮件按钮时长
        //验证码的长度
        private const int iVerifyCodeLength = 6;
        //验证码
        private String strVerifyCode = "";
        private String Emailcode = "";
        private Form f = null;//下一步打开的窗口
        public VerifyID(string emailaddr, Form p)
        {
            InitializeComponent();
            BackgroundImage = Image.FromFile("main.jpg");
            lemail = emailaddr;
            label2.Text = emailaddr;
            label6.Text = "";
            f = p;
            UpdateVerifyCode();
        }

        public void UpdateVerifyCode()//更新验证码
        {
            strVerifyCode = Second_Verify.CreateRandomCode(iVerifyCodeLength);
            if (Second_Verify.CreateImage(strVerifyCode))
                pbVerifyCode.Image = Second_Verify.img;
            else
                MessageBox.Show(Second_Verify.pic_e.Message, "创建图片错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

        }

        private string CreateEmailCode(int iLength)//创建验证码
        {
            int rand;
            char code;
            string randomCode = String.Empty;
            //生成一定长度的验证码
            System.Random random = new Random();
            for (int i = 0; i < iLength; i++)
            {
                rand = random.Next();//随机选择字母/数字
                if (rand % 2 == 0)
                {//字母随机生成
                    code = (char)('A' + (char)(rand % 26));
                    if (code == 'O' || code == 'I' || code == 'S') code = 'F';//避免难以分辨的字母出现
                }
                else
                {//数字随机生成
                    code = (char)('0' + (char)(rand % 10));
                    if (code == '1' || code == '7') code = '8';//避免难以分辨的数字出现
                }
                Emailcode += code.ToString();
            }
            return Emailcode;
        }


        private void SendEmialBtnChanging()
        {
            while (remain >= 0)
            {
                if (remain > 0)
                    btn_email.Text = remain + "秒后再试";
                else
                {
                    btn_email.Text = "发送邮件或短信";
                    btn_email.Enabled = true;
                }
                remain--;
                Thread.Sleep(1000);
            }
            remain = 120;
        }

        private void SendCode()
        {
            
            if (Second_Verify.SendMsg(lemail, Emailcode))
                MessageBox.Show("发送成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                MessageBox.Show(Second_Verify.ex.Message, "发送出错！", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
        }

        private void btn_email_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (label6.Text != "√")
            {
                MessageBox.Show("验证码错误！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }    
            btn_email.LinkVisited = true;
            Emailcode = "";
            CreateEmailCode(iVerifyCodeLength);
            
            if (lemail != null && lemail != ""&& Security_Test.IsValidContact(lemail))
            {
                SendCode();

                btn_admit.Enabled = true;
                btn_email.Enabled = false;
                Thread t1 = new Thread(SendEmialBtnChanging);
                t1.IsBackground = true;
                t1.Start();
            }
            else
            {
                MessageBox.Show("联系方式不合法！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
            }
        }

        private void pbVerifyCode_Click(object sender, EventArgs e)
        {
            UpdateVerifyCode();
            String strenter = VerifyCode.Text.Trim();
            String strcorrect = strVerifyCode.Trim();
            if (strenter == "")
            {
                label6.Text = "";
                return;
            }
            strenter = strenter.ToUpper();
            if (strenter == strcorrect) label6.Text = "√";
            else label6.Text = "✖";
        }

        private void VerifyCode_TextChanged(object sender, EventArgs e)
        {
            String strenter = VerifyCode.Text.Trim();
            String strcorrect = strVerifyCode.Trim();
            if (strenter == "")
            {
                label6.Text = "";
                return;
            }
            strenter = strenter.ToUpper();
            if (strenter == strcorrect) label6.Text = "√";
            else label6.Text = "✖";
        }

        private void btn_admit_Click(object sender, EventArgs e)
        {
            String everify = EmailVerifyCode.Text.ToUpper().Trim();
            try
            {
                if (label6.Text != "√")
                    throw new Exception("验证码错误！");
                if (everify != Emailcode)
                    throw new Exception("邮箱验证码错误！");
                if (DateTime.Compare(DateTime.Now, Second_Verify.ExpiredDate) >= 0)
                    throw new Exception("邮箱验证码已过期，请重新发送！");
                f.Visible = true;

            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.Message, "身份验证失败！", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                
                this.Close();
            }
        }


    }
}
