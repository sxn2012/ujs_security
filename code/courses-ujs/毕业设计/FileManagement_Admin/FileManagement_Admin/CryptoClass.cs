using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace FileManagement_Admin
{
    public class CryptoClass
    {
        public static string key = "123abc788def432112aaa678fff54091";
        public static string AesEncrypt(string str, string key)//加密
        {
            if (string.IsNullOrEmpty(str)) return null;
            Byte[] bs = Encoding.UTF8.GetBytes(str);

            RijndaelManaged rm = new RijndaelManaged
            {
                Key = Encoding.UTF8.GetBytes(key),
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };

            ICryptoTransform ct = rm.CreateEncryptor();
            Byte[] result = ct.TransformFinalBlock(bs, 0, bs.Length);

            return Convert.ToBase64String(result, 0, result.Length);
        }

        public static string AesDecrypt(string str, string key)//解密
        {
            if (string.IsNullOrEmpty(str)) return null;
            Byte[] bs = Convert.FromBase64String(str);

            RijndaelManaged rm = new RijndaelManaged
            {
                Key = Encoding.UTF8.GetBytes(key),
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };

            ICryptoTransform ct = rm.CreateDecryptor();
            Byte[] result = ct.TransformFinalBlock(bs, 0, bs.Length);

            return Encoding.UTF8.GetString(result);
        }

        public static string SHA256String(string str)//对字符串做SHA-256
        {
            try
            {
                byte[] bd = Encoding.UTF8.GetBytes(str);
                SHA256Managed Sha256 = new SHA256Managed();
                byte[] by = Sha256.ComputeHash(bd);
                return BitConverter.ToString(by).Replace("-", "").ToLower();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static string SHA256File(string filepath)//对文件做SHA-256
        {
            try
            {
                FileStream fs = new FileStream(filepath, FileMode.Open);
                SHA256Managed Sha256 = new SHA256Managed();
                byte[] by = Sha256.ComputeHash(fs);
                return BitConverter.ToString(by).Replace("-", "").ToLower();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
