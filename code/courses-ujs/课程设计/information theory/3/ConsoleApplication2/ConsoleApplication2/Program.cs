using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleApplication2
{
    class Program
    {
        public static double[] ps;//信源符号概率分布
        public static double[,] p;//传递矩阵
        public static double[,] phi;//中间量
        public static int r;//信源数目
        public static int s;//信宿数目
        public static double C;//信道容量
        public static double C_last;//上次计算出的信道容量
        public static double dieta = 0.01;//允许的误差
        static void Calculate()
        {
            //初始化数据
            for (int i = 0; i < r; i++)
                ps[i] = 1.0 / (double)r;//赋值
            
            //迭代计算
            bool flag = true;//标志
            phi = new double[20, 20];
            while(flag)
            {
                //计算phi(j,i)
                double temp;
                for(int j=0;j<s;j++)
                {
                    temp = 0.0;
                    for (int i = 0; i < r; i++)
                        temp = temp + ps[i] * p[i, j];//分母
                    for (int i = 0; i < r; i++)
                        phi[j,i] = (ps[i] * p[i, j]) / temp;//计算结果
                }
                //计算ps(i)
                double pup = 0.0;//分子
                double pdown = 0.0;//分母
                //下面计算分母
                bool flag_1 = true;
                for(int i=0;i<r;i++)
                {
                    temp = 0.0;
                    for (int j = 0; j < s; j++)
                        if (phi[j, i] != 0)
                            temp = temp + p[i, j] * (Math.Log(phi[j, i])/Math.Log(Math.E));
                        else if (p[i, j] == 0 && phi[j, i] == 0)
                            temp = temp + 0.0;
                        else if(p[i,j]!=0&&phi[j,i]==0)
                            flag_1 = false;
                    if (flag_1)
                        pdown = pdown + Math.Exp(temp);
                    else
                        pdown = pdown + 0.0;
                }
                //下面计算分子
                for(int i=0;i<r;i++)
                {
                    bool flag_2 = true;
                    temp = 0.0;
                    for(int j=0;j<s;j++)
                    {
                        if (phi[j, i] != 0)
                            temp = temp + p[i, j] * (Math.Log(phi[j, i])/Math.Log(Math.E));
                        else if (p[i, j] == 0 && phi[j, i] == 0)
                            temp = temp + 0.0;
                        else if (p[i, j] != 0 && phi[j, i] == 0)
                            flag_2 = false;
                    }
                    if (flag_2)
                        pup = Math.Exp(temp) ;
                    else
                        pup = 0.0;
                    ps[i] = pup / pdown;//最终结果
                }

                C = Math.Log(pdown)/Math.Log(2);//信道容量迭代结果
               
                if (Math.Abs(C - C_last) / C <= dieta) flag = false;//小于允许误差 退出迭代计算
                C_last = C;//更新上次计算的信道容量的值
            }
        }
        static void Main(string[] args)
        {
            StreamReader sr = new StreamReader(@"G:\subject\csharp\ConsoleApplication2\in3.txt");
            StreamWriter sw = new StreamWriter(@"G:\subject\csharp\ConsoleApplication2\out3.txt", true);
            ps = new double[20];
            p = new double[20, 20];
            string line;
            int i = 0;
            int j = 0;
            r = 0;
            s = 0;
            while(true)
            {
                line = sr.ReadLine();
                if (line==null)
                {
                    r = i;
                    Calculate();
                    sw.WriteLine("信道容量为:" + C);
                    break;
                }
                if (line == "")
                {
                    r = i;
                    Calculate();
                    sw.WriteLine("信道容量为:" + C);
                    i = 0;
                    j = 0;
                    p = new double[20, 20];
                    continue;
                }   
                string[] temp1 = line.Split(',');
                s = temp1.Length;
                for (j = 0; j < s; j++)
                    p[i, j] = double.Parse(temp1[j]);
                i++;
                
            }
            sr.Close();
            sw.Close();
        }
    }
}
