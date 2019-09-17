using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;


namespace FileManagement_Admin
{
    class Second_Verify
    {
        public static ArgumentException pic_e;
        public static Image img;
        public static string CreateRandomCode(int iLength)//创建验证码
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
                randomCode += code.ToString();
            }
            return randomCode;
        }

        public static bool CreateImage(string strVerifyCode)//  创建验证码图片
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
                    Font f = new System.Drawing.Font(font[findex], 13, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic);//字体样式(参数2为字体大小)
                    Brush b = new System.Drawing.SolidBrush(c[cindex]);
                    Point dot = new Point(16, 16);

                    float angle = rand.Next(-iRandAngle, iRandAngle);//转动的度数
                    graph.TranslateTransform(dot.X, dot.Y);//移动光标到指定位置
                    graph.RotateTransform(angle);
                    graph.DrawString(chars[i].ToString(), f, b, 1, 1, format);

                    graph.RotateTransform(-angle);//转回去
                    graph.TranslateTransform(2, -dot.Y);//移动光标到指定位置
                }
                img = map;
                return true;
            }
            catch (ArgumentException ex)
            {
                pic_e = ex;
                return false;
            }
        }


    }
}
