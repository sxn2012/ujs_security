using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace FileManagement
{
    public class CryptoClass
    {
        public static string key = "123abc788def432112aaa678fff54091";//密钥
        public static string AesEncrypt(string str, string key)//加密
        {
            if (string.IsNullOrEmpty(str)) return null;
            Byte[] bs = Encoding.UTF8.GetBytes(str);//string转换成byte数组
            RijndaelManaged rm = new RijndaelManaged
            {
                Key = Encoding.UTF8.GetBytes(key),//密钥
                Mode = CipherMode.ECB,//分组模式
                Padding = PaddingMode.PKCS7//填充模式
            };
            ICryptoTransform ct = rm.CreateEncryptor();
            Byte[] result = ct.TransformFinalBlock(bs, 0, bs.Length);//加密后的byte数组
            return Convert.ToBase64String(result, 0, result.Length);//base64编码转换成string
        }

        public static string AesDecrypt(string str, string key)//解密
        {
            if (string.IsNullOrEmpty(str)) return null;
            Byte[] bs = Convert.FromBase64String(str);//string数组base64解码转换成byte数组
            RijndaelManaged rm = new RijndaelManaged
            {
                Key = Encoding.UTF8.GetBytes(key),//密钥
                Mode = CipherMode.ECB,//分组模式
                Padding = PaddingMode.PKCS7//填充模式
            };
            ICryptoTransform ct = rm.CreateDecryptor();
            Byte[] result = ct.TransformFinalBlock(bs, 0, bs.Length);//解密后的byte数组
            return Encoding.UTF8.GetString(result);//byte数组转换成string
        }

        public static byte[] ReadFileToByte(string file)//文件转化为byte数组
        {
            FileStream fs = null;
            byte[] fb = new byte[0];
            try
            {
                fs = new FileStream(file, FileMode.Open, FileAccess.Read);//打开文件
                BinaryReader r = new BinaryReader(fs);
                r.BaseStream.Seek(0, SeekOrigin.Begin);//将文件指针设置到文件开始处
                fb = r.ReadBytes((int)r.BaseStream.Length);//读文件
                return fb;//返回读取结果
            }
            catch
            {
                return fb;
            }
            finally
            {
                if (fs != null)
                    fs.Close();//关闭文件
            }
        }


        public static bool WriteByteToFile(byte[] fb, string file)//byte数组转化为文件
        {
            FileStream fs = null;
            try
            {
                fs = new FileStream(file, FileMode.Create);//打开文件
                fs.Write(fb, 0, fb.Length);//写文件
            }
            catch
            {
                return false;//写入失败
            }
            finally
            {
                if (fs != null)
                    fs.Close();//关闭文件
            }
            return true;//写入成功
        }

        public static byte[] AesEncryptByte(byte[] fb, string key)//byte数组加密
        {
            RijndaelManaged rm = new RijndaelManaged
            {
                Key = Encoding.UTF8.GetBytes(key),//密钥
                Mode = CipherMode.ECB,//分组模式
                Padding = PaddingMode.PKCS7//填充模式
            };
            ICryptoTransform ct = rm.CreateEncryptor();
            return ct.TransformFinalBlock(fb, 0, fb.Length);//返回加密结果
        }

        public static byte[] AesDecryptByte(byte[] fb, string key)//byte数组解密
        {
            RijndaelManaged rm = new RijndaelManaged
            {
                Key = Encoding.UTF8.GetBytes(key),//密钥
                Mode = CipherMode.ECB,//分组模式
                Padding = PaddingMode.PKCS7//填充模式
            };
            ICryptoTransform cTransform = rm.CreateDecryptor();
            return cTransform.TransformFinalBlock(fb, 0, fb.Length);//返回解密结果
        }


        public static void EncryptFile(string origin,string encrypted)//文件加密
        {
            try
            {
                byte[] bt = ReadFileToByte(origin);//转换成byte数组
                bt = AesEncryptByte(bt, key);//加密
                WriteByteToFile(bt, encrypted);//写文件
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "文件加密异常！", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public static void DecryptFile(string origin, string decrypted)//文件解密
        {
            try
            {
                byte[] bt = ReadFileToByte(origin);//转换成byte数组
                bt = AesDecryptByte(bt, key);//解密
                WriteByteToFile(bt, decrypted);//写文件
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "文件解密异常！", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
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
            catch(Exception)
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
                fs.Close();
                return BitConverter.ToString(by).Replace("-", "").ToLower();
            }
            catch(Exception)
            {
                return null;
            }
        }
    }
}
