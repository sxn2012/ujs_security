//package a1_6;

import java.util.Scanner;

public class main
{
    public static int F(int n)
    {
        if(n==0) return 0;
        else if(n==1) return 1;
        else return F(n-1)+F(n-2);
    }
    public static void main(String []args)
    {
        Scanner sc=new Scanner(System.in);
        System.out.print("请输入月份数：");
        int n=sc.nextInt();
        System.out.println("兔子个数："+2*F(n));
    }
}
