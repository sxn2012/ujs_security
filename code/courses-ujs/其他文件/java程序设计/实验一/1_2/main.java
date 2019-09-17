//package a1_2;

public class main
{
    public static long fun(int n)
    {
        long s=1;
        for(int i=1;i<=n;i++)
        {
            s=s*i;
        }
        return s;
    }
    public static void main(String []args)
    {
        long s=0;
        for(int i=1;i<=7;i++)
        {
            s=s+fun(i);
        }
        System.out.println("1!+2!+3!+...+7!="+s);
    }
}
