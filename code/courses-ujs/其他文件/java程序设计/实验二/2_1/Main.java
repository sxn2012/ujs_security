//package a2_1;
import java.util.Scanner;
import java.util.regex.*;
public class Main
{
    public static void main(String []args)
    {
        System.out.print("请输入字符串：");
        Scanner sc=new Scanner(System.in);
        String s1=sc.nextLine();
        Matcher match_num=Pattern.compile("\\d").matcher(s1);
        int count_num=0;
        while(match_num.find()) count_num++;
        Matcher match_alp=Pattern.compile("\\p{Alpha}").matcher(s1);
        int count_alp=0;
        while(match_alp.find()) count_alp++;
        Matcher match_space=Pattern.compile("[ ]").matcher(s1);
        int count_space=0;
        while(match_space.find()) count_space++;
        int count_other=0;
        count_other=s1.length()-count_num-count_alp-count_space;
        System.out.println("共有"+count_num+"个数字"+count_alp+"个字母"+count_space+"个空格"+count_other+"个其余字符");
    }
}
