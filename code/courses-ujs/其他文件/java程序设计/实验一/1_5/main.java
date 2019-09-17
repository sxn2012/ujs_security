//package a1_5;

import java.util.Scanner;

public class main
{
    public static int gcd(int x,int y)
    {
        if(y==0)
            return x;
        else
            return gcd(y,x%y);
    }
    public static void main(String []args)
    {
        Scanner sc=new Scanner(System.in);
        System.out.print("请输入两个数：");
        int a=sc.nextInt();
        int b=sc.nextInt();
        if(b>a)
        {
            int temp=a;
            a=b;
            b=temp;
        }
        int r1=gcd(a,b);
        int r2=a*b/gcd(a,b);
        System.out.println("最大公约数为："+r1);
        System.out.println("最小公倍数为："+r2);
    }
}
