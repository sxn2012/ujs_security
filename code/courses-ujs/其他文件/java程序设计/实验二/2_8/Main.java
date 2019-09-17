//package a2_8;
import java.util.*;
public class Main
{
    public static void main(String []args)
    {
        int y1,m1,d1,y2,m2,d2;
        System.out.print("请输入两个日期（yyyy/mm/dd):");
        Scanner sc=new Scanner(System.in);
        y1=sc.nextInt();
        m1=sc.nextInt();
        d1=sc.nextInt();
        y2=sc.nextInt();
        m2=sc.nextInt();
        d2=sc.nextInt();
        Calendar cal=Calendar.getInstance();
        cal.set(y1,m1-1,d1);
        long t1=cal.getTimeInMillis();
        cal.set(y2,m2-1,d2);
        long t2=cal.getTimeInMillis();
        long subday=Math.abs(t1-t2)/(1000*60*60*24);
        System.out.println("相隔"+subday+"天");
    }
}
