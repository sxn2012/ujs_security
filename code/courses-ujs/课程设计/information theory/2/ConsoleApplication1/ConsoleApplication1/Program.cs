using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleApplication1
{
    class Program
    {
        public static int[] result_array ;//编码结果
        public static string[] temparr;//字典
        public static string ori;//译码结果
        static void EnCode(string w)//编码
        {
            char []w_array=w.ToCharArray();
            int len = w_array.Length;
            string result = "";
            //建立初始化字典
            char ca ;
            //string temp1 = "";
            temparr = new string[len];
            int flag = 0;
            for (int i = 0; i < 26;i++ )//遍历a到z
            {
                ca = (char)('a' + i);
                if(w.IndexOf(ca)!=-1)//如果字符串中存在该字母
                {
                    temparr[flag++] = ca.ToString();//加入到字典中
                }
            }
            result_array = new int[len];
            int k = 0;
            int f2 = 0;
            Stack<int> s1 = new Stack<int>();
            //逐位编码
            for (int i = 0; i < len; )
            {
                string cur = "";
                cur = cur + w_array[i];
                for(int j=i+1;;j++)
                {
                    for (k = 0; k < temparr.Length; k++)
                        if (temparr[k]!=null&&temparr[k] == cur) break;//查找字典中有无此字符串
                    if (k==temparr.Length)//字典中没有这个字符串         
                    {
                        k = s1.Pop();//导出前一个字典中存在的字符串的索引
                        result_array[f2++] = k+1;//导出到输出码中
                        temparr[flag++] = cur;//字典中增添该项
                        break;//进行下一项的编码
                    }
                    else//字典中有这个字符串
                        s1.Push(k);//记录下词条的索引
                    if(j>=len)//范围越界
                    {
                        for (k = 0; k < temparr.Length; k++)
                            if (temparr[k] != null && temparr[k] == cur) break;//查找当前字符串在字典中是否存在
                        if(k==temparr.Length)//不存在
                        {
                            k = s1.Pop();//导出前一个索引值
                            result_array[f2++] = k + 1;//记录到输出码中
                            temparr[flag++] = cur;//字典中增添该项
                        }
                        else//存在
                        {
                            result_array[f2++] = k + 1;//索引值导出到输出码中
                        }
                        result_array[f2++] = -1;//结束标记eof
                        i = len;//改变i值，以便退出外层循环
                        break;//退出内层循环
                    }
                    else//范围不越界
                        cur = cur + w_array[j];//形成新词条
                    i++;//改变指向下一个待编码字符的指针
                }
            }
            return;
            
        }

        static void DeCode(int[]c)//解码
        {
            int len = c.Length;
            ori = "";
            for (int i = 0; i < len; i++)//遍历码字
            {
                if (c[i] == -1) break;//结束标志，退出循环
                ori = ori + temparr[c[i] - 1];//在字典中查找该编码对应的字符串
            }
            return;
            
        }
        static void Main(string[] args)
        {
            StreamReader sr = new StreamReader(@"G:\subject\csharp\ConsoleApplication1\in2.txt");//打开文件以读入
            StreamWriter sw = new StreamWriter(@"G:\subject\csharp\ConsoleApplication1\out2.txt", true);//打开文件以写入
            char[,] wordlist=new char[20,100];//二维数组，存储输入序列
            int[,] codelist = new int[20, 100];//二维数组，存储编码序列
            char[,] relist = new char[20, 100];//二维数组，存储译码序列
            string line = sr.ReadLine();
            int i = 0;
            int j = 0;
            int len1 = 0;
            while(line!=null&&line!="")
            {
                string[] temp1 = line.Split(',');//逗号分割
                j=0;
                len1 = temp1.Length;
                string temp2 = "";
                for (j = 0; j < len1; j++)
                {
                    wordlist[i, j] = Char.Parse(temp1[j]);//字符串转化为int数组
                    temp2 = temp2 + wordlist[i, j];//拼接字符串
                    
                }
                string temp3="";
                EnCode(temp2);
                for(j=0;j<len1;j++)//遍历字符
                {
                    if (result_array[j] == -1)
                    {
                        temp3 = temp3 + "EOF";
                        break;//遇到结束标志退出循环
                    }
                    codelist[i, j] = result_array[j];//保存编码结果
                    temp3 = temp3 + result_array[j] +",";
                }
                string temp4="";
                DeCode(result_array);
                char []t=ori.ToCharArray();
          
                for (j = 0; j < len1;j++ )//遍历字符
                {
                    relist[i, j] = t[j];//保存译码结果
                    temp4 = temp4 + t[j];
                }
                sw.Write("第" + (i + 1) + "组编码结果：");
                sw.Write(temp3);
                sw.Write("\t译码结果：");
                sw.WriteLine(temp4);
                line = sr.ReadLine();
                i++;
            }
            sr.Close();
            sw.Close();
        }
    }
}
