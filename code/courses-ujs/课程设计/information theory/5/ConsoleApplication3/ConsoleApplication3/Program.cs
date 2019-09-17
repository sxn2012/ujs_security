using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;
using System.Web;

namespace ConsoleApplication3
{
    class Program
    {
        public static string str;
        public static int[] code_1;
        public static string[] code_2;
        public static string code_3;
        public static string code_4;
        public static string recv_0;
        public static string recv_1;
        public static string recv_2;
        public static int[] recv_3;
        public static string final_str;
        public static int maxlen ;
        public static string[] TAB0 = new string[] { "0", "11", "101", "1001", "10001", "100001", "1000001", "10000001", "100000001", "1000000001", "10000000001", "100000000001", "1000000000001", "10000000000001", "100000000000001", "100000000000001", "100000000000000" };//0游程编码表
        public static string[] TAB1 = new string[] { "00", "10", "010", "110", "0110", "1110", "01110", "11110", "011110", "111110", "0111111", "1111111", "01111101", "01111100", "11111100", "111111011", "111111010" };//1游程编码表
        
        public static string[] S_TAB = new string[] { "000", "001", "010", "011", "100", "101", "110", "111" };//简化译码表（第一行）
        public static string[] E_TAB = new string[] { "0000000", "1000000", "0000010", "0100000", "0000001", "0000100", "0001000", "0010000" };//简化译码表（第二行）
        static void Encode1()//游程编码
        {
            char[] str_arr = str.ToCharArray();
            code_1 = new int[str_arr.Length];
            int temp = 0;
            int count=0;
            int i = 0;
            if (str_arr[0] != '0')//保证开始时为0游程，如果不是补0
                code_1[i++] = 0;
            while(temp<str_arr.Length-1)
            {
                if(str_arr[temp]!=str_arr[temp+1])//不相同
                {
                    code_1[i++] = count + 1;//统计相同的数字的个数
                    count = 0;//计数器清0
                    temp++;
                    continue;
                }
                temp++;
                count++;
            }
            if(count>0)
                code_1[i++]=count+1;
            code_1[i++] = -1;//EOF
        }
        
        static void Encode2()//霍夫曼编码
        {
            int len = code_1.Length;
            code_2 = new string[len];
            for (int i = 0; i < len; i++)//计算实际长度
                if (code_1[i] == -1)
                {
                    len = i;
                    break;
                }
            for (int i = 0; i < len; i++)
                if (i % 2 == 0)
                    code_2[i] = TAB0[code_1[i]];//查0游程编码表
                else
                    code_2[i] = TAB1[code_1[i]];//查1游程编码表
            

        }

        static void Encode3()//加密编码
        {
            
            string str="";
            int i=0;
            while(code_2[i]!=null&&code_2[i]!="")
                str=str+code_2[i++];//转换成字符串
            string key = "abcabc";//设置密钥
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();//des加密
            byte[] inputByteArray;
            inputByteArray = Encoding.Default.GetBytes(str);
            des.Key = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(key, "md5").Substring(0, 8));
            des.IV = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(key, "md5").Substring(0, 8));
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            //cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            code_3= ret.ToString();//以字符串形式存储
            
            
        }
        static void Encode4()//信道编码
        {
            code_4 = "";
            char[] lastcode = code_3.ToCharArray();
            int[] m = new int[] { 0, 0, 0, 0 };
            int[] c = new int[] { 0, 0, 0, 0, 0, 0, 0 };
            for(int i=0;i<lastcode.Length;i++)
            {
                //16进制转换成2进制
                if (lastcode[i] >= '0' && lastcode[i] <= '9')
                {
                    int temp = lastcode[i] - '0';
                    m[3] = temp % 2;
                    m[2] = temp / 2 % 2;
                    m[1] = temp / 4 % 2;
                    m[0] = temp / 8 % 2;
                }
                else if (lastcode[i] >= 'A' && lastcode[i] <= 'F')
                {
                    int temp = lastcode[i] - 'A' + 10;
                    m[3] = temp % 2;
                    m[2] = temp / 2 % 2;
                    m[1] = temp / 4 % 2;
                    m[0] = temp / 8 % 2;
                }
                else continue;
                //c=m*G,计算出结果
                c[0] = m[0];
                c[1] = m[1];
                c[2] = (m[0] + m[2]) % 2;
                c[3] = (m[0] + m[1] + m[3]) % 2;
                c[4] = (m[1] + m[2]) % 2;
                c[5] = (m[2] + m[3]) % 2;
                c[6] = m[3];
                for (int j = 0; j < 7; j++)
                    code_4 = code_4 + c[j];//以字符串形式存储
            }
        }

        static void SendString()//模拟信道
        {
            char[] str_temp = code_4.ToCharArray();
            int iSeed = 10;
            Random ro = new Random(iSeed);//产生随机种子
            long tick = DateTime.Now.Ticks;
            Random ran = new Random((int)(tick & 0xffffffffL) | (int)(tick >> 32));
            int len = str_temp.Length;
            for(int i=0;i<len;i++)
            {
                
                int num = ran.Next() % 100;//生成10以内的随机数
                if(num==2)//如果随机数等于某个数（比如2），表明产生了错误，这样错误概率正好是1/10
                {
                    //产生错误，0变成1，1变成0
                    if (str_temp[i] == '0') str_temp[i] = '1';
                    else if (str_temp[i] == '1') str_temp[i] = '0';
                }
            }
            recv_0 = new string(str_temp);//按字符串形式存储
        }

        static void Decode1()//信道译码
        {
            int []c=new int[7];
            int[] s = new int[3];
            char[] m = recv_0.ToCharArray();
            int[] o = new int[4];
            int len = m.Length;
            string s1 = "";
            recv_1="";
            for(int i=0;i<len;i=i+7)
            {
                for (int j = i; j < i + 7; j++)
                    c[j - i] = m[j] - '0';
                //计算伴随式
                s[0] = (c[2] + c[3] + c[4] + c[6]) % 2;
                s[1] = (c[1] + c[2] + c[3] + c[5]) % 2;
                s[2] = (c[0] + c[1] + c[2] + c[4]) % 2;
                string temp = "";
                temp = temp + s[0];
                temp = temp + s[1];
                temp = temp + s[2];
                for(int j=0;j<=7;j++)
                    if(temp==S_TAB[j])
                    {
                        char[] t1 = E_TAB[j].ToCharArray();//查表，纠正错误
                        int[] t1_1 = new int[7];
                        for (int k = 0; k < 7; k++)
                            t1_1[k] = t1[k] - '0';
                        for (int k = 0; k < 7; k++)
                            c[k] = (c[k] + t1_1[k]) % 2;
                        break;
                    }
                //恢复4位信息元
                o[0] = c[0];
                o[1] = c[1];
                o[2] = (c[0] + c[2]) % 2;
                o[3] = c[6];
                for (int j = 0; j < 4; j++)
                    str = str + o[j];
            }
            //转换成16进制
            char[] str_arr = str.ToCharArray();
            int f=0;
            for (int i = 0; i < str_arr.Length; i = i + 4)//4位分组
            {
                if (i + 1 >= str_arr.Length) f = (str_arr[i] - '0') * 8;
                else if (i + 2 >= str_arr.Length) f = (str_arr[i] - '0') * 8 + (str_arr[i + 1] - '0') * 4;
                else if (i + 3 >= str_arr.Length) f = (str_arr[i] - '0') * 8 + (str_arr[i + 1] - '0') * 4 + (str_arr[i + 2] - '0') * 2;
                else
                    f = (str_arr[i] - '0') * 8 + (str_arr[i + 1] - '0') * 4 + (str_arr[i + 2] - '0') * 2 + (str_arr[i + 3] - '0') * 1;
                if (f < 10)
                    recv_1 = recv_1 + f;
                else
                {
                    char cc=' ';
                    switch(f)
                    {
                        case 10: cc = 'A'; break;
                        case 11: cc = 'B'; break;
                        case 12: cc = 'C'; break;
                        case 13: cc = 'D'; break;
                        case 14: cc = 'E'; break;
                        case 15: cc = 'F'; break;
                    }
                    recv_1 = recv_1 + cc;
                }
            }
        }

        static void Decode2()//解密译码
        {
            
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();//des解密
            int len;
            string key = "abcabc";//设置密钥
            len = recv_1.Length / 2;
            byte[] inputByteArray = new byte[len];
            int x, i;
            for (x = 0; x < len; x++)
            {
                i = Convert.ToInt32(recv_1.Substring(x * 2, 2), 16);
                inputByteArray[x] = (byte)i;
            }
            des.Key = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(key, "md5").Substring(0, 8));
            des.IV = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(key, "md5").Substring(0, 8));
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            //cs.FlushFinalBlock();
            string temp = Encoding.Default.GetString(ms.ToArray());
            
           
            char []tt=temp.ToCharArray();
            len = tt.Length;
            //转换成2进制
            for(i=0;i<len;i++)
            {
                string s1 = "";
                int t1 = (int)tt[i];
                while(t1>0)
                {
                    int t2 = t1 % 2;
                    t1 = t1 / 2;
                    s1 = t2 + s1;
                }
                recv_2 = recv_2 + s1;//以字符串形式存储
            }
            
            
           
        }

        static void Decode3()//霍夫曼译码
        {
            recv_3 = new int[recv_2.Length];
            int cur_sta = 0;//当前游程
            int pos = 0;
            int i, j;
            char[] s_arr = recv_2.ToCharArray();
            for(i=0;i<s_arr.Length;)
            {
                string s_cur = "";
                for(j=i;j<s_arr.Length;j++)
                {
                    s_cur = s_cur + s_arr[j];
                    bool flag=false;
                    if (cur_sta == 0)
                    {
                        for (int k = 0; k < TAB0.Length; k++)//查表
                            if (s_cur == TAB0[k])//找到了对应的码字
                            {
                                recv_3[pos++] = k;//记录
                                flag = true;
                                cur_sta = 1;//下一个是1游程
                            }
                    }
                    else
                    {
                        for (int k = 0; k < TAB1.Length; k++)//查表
                            if (s_cur == TAB1[k])//找到了对应的码字
                            {
                                recv_3[pos++] = k;//记录
                                flag = true;
                                cur_sta = 0;//下一个是0游程
                            }
                    }
                    if(flag)
                    {
                        i = j + 1;//改变位置
                        break;
                    }
                }
                if (j >= s_arr.Length) break;//超出范围 退出循环
            }
            recv_3[pos++] = -1;//EOF
        }

        static void Decode4()//游程译码
        {
            char cur ;//当前字符
           
            
            char[] temp = new char[maxlen];
            int pos = 0;
            int loc = 0;
            int len = recv_3.Length;
            for(pos=0;pos<len;pos++)
            {
                if (pos % 2 == 0) cur = '0';//0游程
                else cur = '1';//1游程
                for (int i = 0; i < recv_3[pos]; i++)
                {
                    if (loc >= maxlen)
                    {
                        final_str = new string(temp);
                        return;
                    }
                    temp[loc++] = cur;//按照个数加入字符串
                }

            }
            final_str = new string(temp);
            
        }
        static void Main(string[] args)
        {
            StreamReader sr = new StreamReader(@"G:\subject\csharp\ConsoleApplication3\in5.txt");
            StreamWriter sw = new StreamWriter(@"G:\subject\csharp\ConsoleApplication3\out5.txt", true);
            string line = sr.ReadLine();//读入数据
            

            while(line!=null&&line!="")
            {
                str = line;
                maxlen = str.Length;
                Encode1();//游程编码
                sw.Write("游程编码结果：" );
                for (int i = 0; i < code_1.Length; i++)
                {
                    if (code_1[i] == -1) break;
                    sw.Write(code_1[i]+" ");
                }
                Encode2();//霍夫曼编码
                sw.Write(" 霍夫曼编码结果:");
                for (int i = 0; i < code_2.Length;i++ )
                {
                    if (code_2[i] == null || code_2[i] == "") break;
                    sw.Write(code_2[i] + " ");
                }
                Encode3();//加密编码
                sw.Write(" 加密编码结果：" + code_3);
                Encode4();//循环码信道编码
                sw.Write(" 信道编码结果：" + code_4);
                SendString();//模拟信道传输
                sw.Write(" 模拟接收串：" + recv_0);
                Decode1();//循环码信道译码
                sw.Write(" 信道译码结果：" + recv_1);
                Decode2();//解密译码
                sw.Write(" 解密译码结果：" + recv_2);
                Decode3();//霍夫曼译码
                sw.Write(" 霍夫曼译码结果：");
                for (int i = 0; i < recv_3.Length;i++ )
                {
                    if (recv_3[i] == -1) break;
                    sw.Write(recv_3[i]+" ");
                }
                Decode4();//游程译码
                sw.WriteLine(" 游程译码结果：" + final_str);
                line = sr.ReadLine();//读入下一个数据
            }
            sr.Close();
            sw.Close();
        }
    }
}
