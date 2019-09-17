//package a2_6;
import java.util.*;
public class Main
{
    public static int fac(int a)
    {
        if (a==0) return 1;
        else if (a==1) return 1;
        else return fac(a-1)*a;
    }
    public static int Choose(int n,int r)
    {
        return fac(n)/(fac(r)*fac(n-r));
    }
    public static void main(String args[])
    {
        for (int i=0;i<=6;i++)
        {
            for(int k=6-i;k>0;k--)
                System.out.print(" ");
            for (int j=0;j<=i;j++)
            {
                System.out.print(Choose(i,j));
                System.out.print(" ");
            }
            System.out.println();
        }
    }
}
