using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace FileManagement_Admin
{
    public class Security_Test
    {
        public static bool IsSafetyFile(string fname)
        {
            if (fname.Contains(".exe") || fname.Contains(".cmd") || fname.Contains(".bat") || fname.Contains(".vbs") || fname.Contains(".vbe") || fname.Contains(".js") || fname.Contains(".wsh") || fname.Contains(".wsf"))
                return false;
            else
                return true;
        }

        public static bool IsSecurePWD(String pass)
        {
            var regex = new Regex(@"(?=.*[0-9]) (?=.*[a-zA-Z]) .{6,30}", RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace);
            if (regex.IsMatch(pass))
                return true;
            else
                return false;
        }

        public static bool IsEmailAddr(String emailaddr)
        {
            Regex regex = new Regex("^\\s*([A-Za-z0-9_-]+(\\.\\w+)*@(\\w+\\.)+\\w{2,5})\\s*$");
            if (regex.IsMatch(emailaddr))
                return true;
            else
                return false;
        }

        public static string SafeSQL(string str)
        {
            if (str == String.Empty)
                return String.Empty;
            str = str.Replace("'", " ");//单引号
            str = str.Replace(";", " ");//分号
            str = str.Replace(",", " ");//逗号
            str = str.Replace("?", " ");//问号
            str = str.Replace("!", " ");//感叹号
            str = str.Replace("<", " ");//小于
            str = str.Replace(">", " ");//大于
            str = str.Replace("(", " ");//左括号
            str = str.Replace(")", " ");//右括号
            str = str.Replace("[", " ");//方括号左
            str = str.Replace("]", " ");//方括号右
            //特殊字符
            str = str.Replace("@", " ");
            str = str.Replace("^", " ");
            str = str.Replace("=", " ");
            str = str.Replace("+", " ");
            str = str.Replace("*", " ");
            str = str.Replace("&", " ");
            str = str.Replace("#", " ");
            str = str.Replace("%", " ");
            str = str.Replace("$", " ");
            str = str.Replace("|", " ");
            str = str.Replace("\r\n", " ");
            str = str.Replace("\t", " ");
            str = str.Replace("0x", " ");
            str = str.Replace("0X", " ");
            //数据库关键字过滤
            string[] pattern = { "select", "insert", "delete", "from", "count", "drop", "update", "truncate", "asc", "mid", "char", "xp_cmdshell", "exec", "netlocalgroup administrators", "net user", "or", "and", "not", "table" };
            string temp = str.ToLower();
            for (int i = 0; i < pattern.Length; i++)
            {
                temp = temp.Replace(pattern[i].ToString(), " ");
            }
            if (temp == str.ToLower()) //没有进行过滤
                return str;
            else return temp;//返回不正确的字符串，防止SQL注入
        }

    }
}
