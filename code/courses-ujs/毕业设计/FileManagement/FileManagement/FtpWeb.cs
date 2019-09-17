using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Globalization;
using System.Net;
using System.IO;
using System.Windows.Forms;

namespace FileManagement
{
    public class FtpWeb
    {
        string ftpServerIP;
        string ftpRemotePath;
        string ftpUserID;
        string ftpPassword;
        string ftpURI;
        public MainForm mf = null;
        // 连接FTP
        //FtpRemotePath指定FTP连接成功后的当前目录, 如果不指定即默认为根目录
        public FtpWeb(string FtpServerIP, string FtpRemotePath, string FtpUserID, string FtpPassword)
        {
            ftpServerIP = FtpServerIP;
            ftpRemotePath = FtpRemotePath;
            ftpUserID = FtpUserID;
            ftpPassword = FtpPassword;
            ftpURI = "ftp://" + ftpServerIP + "/" + ftpRemotePath + "/";
        }

        // 上传
        public void Upload(string localpath, string urlpath)
        {
            FileInfo fileInf = new FileInfo(localpath);
            long len = fileInf.Length;//文件大小
            string uri = ftpURI + urlpath;
            FtpWebRequest reqFTP;
            reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));
            reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
            reqFTP.KeepAlive = false;
            reqFTP.Method = WebRequestMethods.Ftp.UploadFile;
            reqFTP.UseBinary = true;
            reqFTP.ContentLength = fileInf.Length;
            int buffLength = 2048;
            byte[] buff = new byte[buffLength];
            int contentLen;
            FileStream fs = fileInf.OpenRead();

            try
            {
                Stream strm = reqFTP.GetRequestStream();
                contentLen = fs.Read(buff, 0, buffLength);
                int present_len = contentLen;//当前为止已读入的字节数
                
                while (contentLen != 0)
                {
                    strm.Write(buff, 0, contentLen); 
                    contentLen = fs.Read(buff, 0, buffLength);
                    mf.ChangeUpDownProgress((long)present_len * 100L / (double)len);//进度条处理
                    present_len = present_len + contentLen;
                    
                }
                strm.Close();
                fs.Close();
                mf.ChangeUpDownProgress(100);//进度条满格
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "上传文件异常！", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //Console.WriteLine(ex.ToString());
            }
        }

        // 下载
        public void Download(string urlpath, string localpath)
        {
            FtpWebRequest reqFTP;
            long len = GetFileSize(urlpath);//文件大小
            try
            {
                FileStream outputStream = new FileStream(localpath, FileMode.Create);
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftpURI + urlpath));
                reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                Stream ftpStream = response.GetResponseStream();
                long cl = response.ContentLength;
                int bufferSize = 2048;
                int readCount;
                byte[] buffer = new byte[bufferSize];
                readCount = ftpStream.Read(buffer, 0, bufferSize);
                int present_len = readCount;//当前为止已读入的字节数
                while (readCount > 0)
                {
                    outputStream.Write(buffer, 0, readCount);
                    readCount = ftpStream.Read(buffer, 0, bufferSize);
                    mf.ChangeUpDownProgress((long)present_len * 100L / (double)len);//进度条处理
                    present_len = present_len + readCount;
                }
                ftpStream.Close();
                outputStream.Close();
                response.Close();
                mf.ChangeUpDownProgress(100);//进度条满格
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "下载文件异常！", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //Console.WriteLine(ex.ToString());
            }
        }

        // 删除文件
        public void Delete(string urlpath)
        {
            try
            {
                string uri = ftpURI + urlpath;
                FtpWebRequest reqFTP;
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));
                reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                reqFTP.KeepAlive = false;
                reqFTP.Method = WebRequestMethods.Ftp.DeleteFile;
                string result = String.Empty;
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                long size = response.ContentLength;
                Stream datastream = response.GetResponseStream();
                StreamReader sr = new StreamReader(datastream);
                result = sr.ReadToEnd();
                sr.Close();
                datastream.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "删除文件异常！", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //Console.WriteLine(ex.ToString());
            }
        }


        // 删除文件夹
        public void RemoveDirectory(string urlpath)
        {
            try
            {
                string uri = ftpURI + urlpath;
                FtpWebRequest reqFTP;
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));
                reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                reqFTP.KeepAlive = false;
                reqFTP.Method = WebRequestMethods.Ftp.RemoveDirectory;
                string result = String.Empty;
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                long size = response.ContentLength;
                Stream datastream = response.GetResponseStream();
                StreamReader sr = new StreamReader(datastream);
                result = sr.ReadToEnd();
                sr.Close();
                datastream.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "删除文件夹异常！", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //Console.WriteLine(ex.ToString());
            }
        }

        //获取指定目录下明细(包含文件和文件夹)
        public string[] GetFilesDetailList(string urlpath)
        {
            string[] downloadFiles;
            try
            {
                bool getin = false;
                string uri = ftpURI + urlpath;
                StringBuilder result = new StringBuilder();
                FtpWebRequest ftp;
                ftp = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));
                ftp.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                ftp.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                WebResponse response = ftp.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.Default);
                string line = reader.ReadLine();
                while (line != null)
                {
                    getin = true;
                    result.Append(line);
                    result.Append("\n");
                    line = reader.ReadLine();
                }
                if (getin)
                    result.Remove(result.ToString().LastIndexOf("\n"), 1);
                reader.Close();
                response.Close();
                return result.ToString().Split('\n');
            }
            catch (Exception ex)
            {
                downloadFiles = null;
                MessageBox.Show(ex.Message, "获取明细异常！", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //Console.WriteLine(ex.ToString());
                return downloadFiles;
            }
        }

        // 获取指定目录下文件列表(仅文件)
        public string[] GetFileList(string urlpath, string mask)
        {
            string[] downloadFiles;
            StringBuilder result = new StringBuilder();
            FtpWebRequest reqFTP;
            try
            {
                string uri = ftpURI + urlpath;
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                reqFTP.Method = WebRequestMethods.Ftp.ListDirectory;
                WebResponse response = reqFTP.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.Default);
                string line = reader.ReadLine();
                while (line != null)
                {
                    if (mask.Trim() != string.Empty && mask.Trim() != "*.*")
                    {
                        string mask_ = mask.Substring(0, mask.IndexOf("*"));
                        if (line.Substring(0, mask_.Length) == mask_)
                        {
                            result.Append(line);
                            result.Append("\n");
                        }
                    }
                    else
                    {
                        result.Append(line);
                        result.Append("\n");
                    }
                    line = reader.ReadLine();
                }
                result.Remove(result.ToString().LastIndexOf('\n'), 1);
                reader.Close();
                response.Close();
                return result.ToString().Split('\n');
            }
            catch (Exception ex)
            {
                downloadFiles = null;
                if (ex.Message.Trim() != "远程服务器返回错误: (550) 文件不可用(例如，未找到文件，无法访问文件)。")
                {
                    MessageBox.Show(ex.Message, "获取明细异常！", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    //Console.WriteLine(ex.ToString());
                }
                return downloadFiles;
            }
        }


        // 获取指定目录下所有的文件夹列表(仅文件夹)
        public string[] GetDirectoryList(string urlpath)
        {
            string[] drectory = GetFilesDetailList(urlpath);
            string m = string.Empty;
            foreach (string str in drectory)
            {
                if (str == "")
                    continue;
                int dirPos = str.IndexOf("<DIR>");
                if (dirPos > 0)
                {
                    /*判断 Windows 风格*/
                    m += str.Substring(dirPos + 5).Trim() + "\n";
                }
                else if (str.Trim().Substring(0, 1).ToUpper() == "D")
                {
                    /*判断 Unix 风格*/
                    string dir = str.Substring(54).Trim();
                    if (dir != "." && dir != "..")
                    {
                        m += dir + "\n";
                    }
                }
            }
            if (m[m.Length - 1] == '\n')
                m.Remove(m.Length - 1);
            char[] n = new char[] { '\n' };
            return m.Split(n);   //这样最后一个始终是空格了
        }

        /// 判断指定目录下是否存在指定的子目录
        // RemoteDirectoryName指定的目录名
        public bool DirectoryExist(string urlpath, string RemoteDirectoryName)
        {
            string[] dirList = GetDirectoryList(urlpath);
            foreach (string str in dirList)
            {
                if (str.Trim() == RemoteDirectoryName.Trim())
                {
                    return true;
                }
            }
            return false;
        }


        // 判断指定目录下是否存在指定的文件
        //远程文件名
        public bool FileExist(string urlpath, string RemoteFileName)
        {
            string[] fileList = GetFileList(urlpath, "*.*");
            foreach (string str in fileList)
            {
                if (str.Trim() == RemoteFileName.Trim())
                {
                    return true;
                }
            }
            return false;
        }

        // 创建文件夹
        public void MakeDir(string urlpath)
        {
            FtpWebRequest reqFTP;
            try
            {
                // dirName = name of the directory to create.
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftpURI + urlpath));
                reqFTP.Method = WebRequestMethods.Ftp.MakeDirectory;
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                Stream ftpStream = response.GetResponseStream();
                ftpStream.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "创建文件夹异常！", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //Console.WriteLine(ex.ToString());
            }
        }

        // 获取指定文件大小
        public long GetFileSize(string urlpath)
        {
            FtpWebRequest reqFTP;
            long fileSize = 0;
            try
            {
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftpURI + urlpath));
                reqFTP.Method = WebRequestMethods.Ftp.GetFileSize;
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                Stream ftpStream = response.GetResponseStream();
                fileSize = response.ContentLength;
                ftpStream.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "获取文件大小异常！", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //Console.WriteLine(ex.ToString());
            }
            return fileSize;
        }

        // 改名
        public void ReName(string urlpath, string newname)
        {
            FtpWebRequest reqFTP;
            try
            {
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftpURI + urlpath));  //源路径
                reqFTP.Method = WebRequestMethods.Ftp.Rename;
                reqFTP.RenameTo = newname; //新名称
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                Stream ftpStream = response.GetResponseStream();
                ftpStream.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "文件重命名异常！", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //Console.WriteLine(ex.ToString());
            }
        }


        // 移动文件
        public void MovieFile(string urlpath, string newname)
        {
            ReName(urlpath, newname);
        }

        // 切换当前目录
        /// <param name="IsRoot">true 绝对路径   false 相对路径</param>
        public void GotoDirectory(string DirectoryName, bool IsRoot)
        {
            if (IsRoot)
            {
                ftpRemotePath = DirectoryName;
            }
            else
            {
                ftpRemotePath += DirectoryName + "/";
            }
            ftpURI = "ftp://" + ftpServerIP + "/" + ftpRemotePath + "/";
        }

    }
}
