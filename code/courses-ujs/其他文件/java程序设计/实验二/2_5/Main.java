//package a2_5;
import java.util.*;
public class Main
{

    public static void main(String []args)
    {

        int []a=new int[20];
        int []b=new int[20];
        double []n=new double[20];
        a[0]=2;
        b[0]=1;
        n[0]=(double)a[0]/(double)b[0];
        for(int i=1;i<20;i++)
        {
            a[i]=a[i-1]+b[i-1];
            b[i]=a[i-1];
            n[i]=(double)a[i]/(double)b[i];
        }
        double sum=0;
        for (int i=0;i<20;i++)
            sum=sum+n[i];
        System.out.println("前20项之和为："+sum);
    }
}
