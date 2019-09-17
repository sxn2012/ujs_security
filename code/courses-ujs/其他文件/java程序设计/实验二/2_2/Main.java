//package a2_2;
import java.util.*;
public class Main
{
    public static boolean IsCompleteNum(int n)
    {
        int sum=0;
        for (int i=1;i<n;i++)
            if (n%i==0) sum=sum+i;
        return sum==n;
    }
    public static void main(String []args)
    {
        System.out.print("1000以内的完数：");
        for (int i=1;i<1000;i++)
            if(IsCompleteNum(i))
                System.out.print(i+" ");
        System.out.println();
    }
}
