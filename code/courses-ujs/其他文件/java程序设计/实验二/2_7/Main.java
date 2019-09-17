//package a2_7;
import java.util.*;
public class Main
{
    public static void main(String []args)
    {
        for (int i=1;i<=6;i++)
        {
            for(int j=1;j<=6-i;j++)
                System.out.print(" ");
            System.out.print("* ");
            for(int j=1;j<=i-2;j++)
                System.out.print("  ");
            if(i!=1)
                System.out.println("*");
            else
                System.out.println();
        }
        for (int i=5;i>=1;i--)
        {
            for(int j=1;j<=6-i;j++)
                System.out.print(" ");
            System.out.print("* ");
            for(int j=1;j<=i-2;j++)
                System.out.print("  ");
            if(i!=1)
                System.out.println("*");
            else
                System.out.println();
        }

        for (int i=1;i<=6;i++)
        {
            for(int j=1;j<=6-i;j++)
                System.out.print(" ");
            System.out.print("* ");
            for(int j=1;j<=i-2;j++)
                System.out.print("* ");
            if(i!=1)
                System.out.println("*");
            else
                System.out.println();
        }
        for (int i=5;i>=1;i--)
        {
            for(int j=1;j<=6-i;j++)
                System.out.print(" ");
            System.out.print("* ");
            for(int j=1;j<=i-2;j++)
                System.out.print("* ");
            if(i!=1)
                System.out.println("*");
            else
                System.out.println();
        }
    }
}
