//package a1_1;
import java.util.Scanner;
public class main
{
    public static void main(String []args)
    {
        Scanner sc=new Scanner(System.in);
        System.out.print("请输入两个数字：");
        int x=sc.nextInt();
        int y=sc.nextInt();
        int temp=0;
        System.out.println("交换前：x="+x+",y="+y);
        temp=x;
        x=y;
        y=temp;
        System.out.println("交换后：x="+x+",y="+y);
    }
}
