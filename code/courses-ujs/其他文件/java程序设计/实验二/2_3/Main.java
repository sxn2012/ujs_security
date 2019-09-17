//package a2_3;
import java.util.*;
public class Main
{
    public static void main(String []args)
    {
        double h=100.0;
        double sum=h;
        for(int i=0;i<10;i++)
        {
            h=h/2;
            sum=sum+2*h;
        }
        System.out.println("共经过"+sum+"米，第10次反弹高度为："+h+"米");
    }
}
