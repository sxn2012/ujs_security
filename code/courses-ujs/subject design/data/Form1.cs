using System;
using System.IO;
using System.Threading;
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

namespace MagazineOrdering
{
    public partial class Login_Form : Form
    {
        //验证码的长度
        private const int iVerifyCodeLength = 4;
        //验证码
        private String strVerifyCode = "";
        public Login_Form()//构造函数
        {
            InitializeComponent();
            UsrImg.BackgroundImage = LeftSideList.Images[0];
            PassImg.BackgroundImage = LeftSideList.Images[1];
            VerifyImg.BackgroundImage = LeftSideList.Images[2];
            TitileImg.BackgroundImage = LeftSideList.Images[3];
            UpdateVerifyCode();

        }

        public void Clear_all()//清除所有输入内容及状态图片
        {
            usr.Clear();
            pwd.Clear();
            VerifyCode.Clear();
            VerifyStatus.BackgroundImage = null;
        }
        protected override void WndProc(ref Message msg)//关闭事件响应函数
        {
            
            const int WM_SYSCOMMAND = 0x0112;
            
            const int SC_CLOSE = 0xF060;

            if (msg.Msg == WM_SYSCOMMAND && ((int)msg.WParam == SC_CLOSE))
            {
                // 点击winform右上关闭按钮 
                DialogResult result = MessageBox.Show("确定要退出吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                if (result == DialogResult.OK)
                    this.Close();
                else
                    return;
                
                
            }
            base.WndProc(ref msg);
        }


        public void UpdateVerifyCode()//更新验证码
        {
            strVerifyCode = CreateRandomCode(iVerifyCodeLength);
            CreateImage(strVerifyCode);
        }
        private string CreateRandomCode(int iLength)//创建验证码
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
                    if (code == 'O' || code == 'I') code = 'F';//避免难以分辨的字母出现
                }
                else
                {//数字随机生成
                    code = (char)('0' + (char)(rand % 10));
                    
                }
                randomCode += code.ToString();
            }
            return randomCode;
        }

        private void CreateImage(string strVerifyCode)//  创建验证码图片
        {
            try
            {
                int iRandAngle = 45;    //随机转动角度
                int iMapWidth = (int)(strVerifyCode.Length * 21);//图片宽度
                Bitmap map = new Bitmap(iMapWidth, 28);     //创建图片背景

                Graphics graph = Graphics.FromImage(map);
                graph.Clear(Color.AliceBlue);//清除画面，填充背景
                graph.DrawRectangle(new Pen(Color.Black, 0), 0, 0, map.Width - 1, map.Height - 1);//画一个边框
                graph.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;//模式

                Random rand = new Random();
                //背景噪点生成
                Pen blackPen = new Pen(Color.LightGray, 0);
                for (int i = 0; i < 500; i++)
                {
                    int x = rand.Next(0, map.Width);
                    int y = rand.Next(0, map.Height);
                    graph.DrawRectangle(blackPen, x, y, 1, 1);
                }

                //验证码旋转，防止机器识别
                char[] chars = strVerifyCode.ToCharArray();//拆散字符串成单字符数组
                //文字居中
                StringFormat format = new StringFormat(StringFormatFlags.NoClip);
                format.Alignment = StringAlignment.Center;
                format.LineAlignment = StringAlignment.Center;
                //定义颜色
                Color[] c = { Color.Gold, Color.Red, Color.Blue, Color.Green, Color.Orange, Color.Brown, Color.Cyan, Color.Purple };
                //定义字体
                string[] font = { "Verdana", "Microsoft Sans Serif", "Comic Sans MS", "Arial" };
                for (int i = 0; i < chars.Length; i++)
                {
                    int cindex = rand.Next(8);
                    int findex = rand.Next(4);
                    Font f = new System.Drawing.Font(font[findex], 13, System.Drawing.FontStyle.Bold|System.Drawing.FontStyle.Italic);//字体样式(参数2为字体大小)
                    Brush b = new System.Drawing.SolidBrush(c[cindex]);
                    Point dot = new Point(16, 16);

                    float angle = rand.Next(-iRandAngle, iRandAngle);//转动的度数
                    graph.TranslateTransform(dot.X, dot.Y);//移动光标到指定位置
                    graph.RotateTransform(angle);
                    graph.DrawString(chars[i].ToString(), f, b, 1, 1, format);

                    graph.RotateTransform(-angle);//转回去
                    graph.TranslateTransform(2, -dot.Y);//移动光标到指定位置
                }
                pbVerifyCode.Image = map;

            }
            catch (ArgumentException)
            {
                MessageBox.Show("创建图片错误。");
            }
        }
        private void btn_exit_Click(object sender, EventArgs e)//退出按钮事件响应函数
        {
            
            DialogResult result = MessageBox.Show("确定要退出吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
            if (result == DialogResult.OK)
                this.Close();
            else
                return;
              
            
        }

        private void btn_login_Click(object sender, EventArgs e)//登录按钮事件响应函数
        {
            string connString = "Data Source =.; Initial Catalog = Magazine_Ordering; Integrated Security = True";
            SqlConnection conn = new SqlConnection(connString);
            string lname = usr.Text.Trim();
            string lpass = pwd.Text.Trim();
            String sql = String.Format("select count(*) from Userlist where username='{0}' and password='{1}'", lname,lpass);
            try
            {
                if(lname==null||lname=="")
                    throw new Exception("用户名不能为空！");
                if(lpass==null||lpass=="")
                    throw new Exception("密码不能为空！");
                if (VerifyCode.Text.Trim() == null || VerifyCode.Text.Trim() == "")
                    throw new Exception("验证码不能为空！");
                if (VerifyCode.Text.ToLower().Trim() != strVerifyCode.ToLower().Trim())
                {
                    VerifyCode.Clear();
                    throw new Exception("验证码错误！");
                }
                conn.Open();
                SqlCommand comm = new SqlCommand(sql, conn);
                int num = (int)comm.ExecuteScalar();
                if (num == 1)
                {
                    conn.Close();
                    sql = String.Format("select access from Userlist where username='{0}'", lname);
                    conn.Open();
                    comm = new SqlCommand(sql, conn);
                    num = (int)comm.ExecuteScalar();
                    if (num == 0)
                    {
                        //MessageBox.Show("管理员登陆成功！");
                        Admin ad = new Admin(this);
                        this.Visible = false;
                        ad.Show();
                    }
                    else
                    {
                        //MessageBox.Show("普通用户登陆成功！");
                        OrdinaryUser od = new OrdinaryUser(this,lname);
                        this.Visible = false;
                        od.Show();
                    }
                }
                   
                else
                {
                    //MessageBox.Show("用户名或密码错误！");
                    VerifyCode.Clear();
                    usr.Clear();
                    pwd.Clear();
                    throw new Exception("用户名或密码错误！");
                }
                    
            }
            catch(Exception ex)
            {
                UpdateVerifyCode();
                MessageBox.Show(ex.Message, "登录失败！", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                conn.Close();
            }
            
        }

        private void btn_Register_Click(object sender, EventArgs e)//注册按钮事件响应函数
        {
            Regis rform = new Regis();
            rform.Show();
        }

        private void btn_Findpass_Click(object sender, EventArgs e)//找回密码按钮事件响应函数
        {
            FindPassWord fpw = new FindPassWord();
            fpw.Show();
        }

        private void pbVerifyCode_Click(object sender, EventArgs e)//点击图片事件响应函数
        {
            UpdateVerifyCode();//更新验证码
        }

        private void VerifyCode_TextChanged(object sender, EventArgs e)//显示验证码的对错
        {
            if (VerifyCode.Text.ToLower().Trim() == null || VerifyCode.Text.ToLower().Trim()=="")
            {
                VerifyStatus.BackgroundImage = null;
                return;
            }
            if(VerifyCode.Text.ToLower().Trim() == strVerifyCode.ToLower().Trim())
            {
                VerifyStatus.BackgroundImage = StatusList.Images[0];
            }
            else
            {
                VerifyStatus.BackgroundImage = StatusList.Images[1];
            }
        }
    }
}
