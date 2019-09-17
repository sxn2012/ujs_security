//package a1_4;

public class main
{
    public static boolean IsFlowerNum(int n)
    {
        int x=0,y=0,z=0;
        x=n%10;
        y=(n/10)%10;
        z=(n/100)%10;
        if(x*x*x+y*y*y+z*z*z==n)return true;
        else return false;
    }
    public static void main(String []args)
    {
        System.out.print("100-999之间的水仙花数为：");
        for(int i=100;i<=999;i++)
        {
            if(IsFlowerNum(i))
            {
                System.out.print(i+" ");
            }
        }
        System.out.println("\n");
    }
}
