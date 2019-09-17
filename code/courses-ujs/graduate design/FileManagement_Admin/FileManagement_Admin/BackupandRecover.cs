using System;
using System.Diagnostics;
using System.Management;
using Microsoft.Win32;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.Sql;

namespace FileManagement_Admin
{
    public partial class BackupandRecover : Form
    {
        string[] name = new string[3];
        public BackupandRecover()
        {
            InitializeComponent();
            label1.Text = "";
            BackgroundImage = Image.FromFile("main.jpg");
            if(ExistsWinRar()==null||ExistsWinRar()=="")
            {
                MessageBox.Show("服务器未安装Winrar！", "无法使用备份功能", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
            }
        }

        public static string ExistsWinRar()
        {
            string result = string.Empty;

            string key = @"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\WinRAR.exe";
            RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(key);
            if (registryKey != null)
            {
                result = registryKey.GetValue("").ToString();
            }
            registryKey.Close();

            return result;
        }

        public static void CompressRar(string soruceDir, string rarFileName)//压缩
        {
            string regKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\WinRAR.exe";
            RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(regKey);
            string winrarPath = registryKey.GetValue("").ToString();
            registryKey.Close();
            string winrarDir = System.IO.Path.GetDirectoryName(winrarPath);
            String commandOptions = string.Format("a {0} {1} -r -padmin", rarFileName, soruceDir);

            ProcessStartInfo processStartInfo = new ProcessStartInfo();
            processStartInfo.FileName = System.IO.Path.Combine(winrarDir, "rar.exe");
            processStartInfo.Arguments = commandOptions;
            processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            try
            {
                Process process = new Process();
                process.StartInfo = processStartInfo;
                process.Start();
                process.WaitForExit(2000);
                process.Kill();
                process.Close();
            }
            catch(Exception)
            {

            }
        }

        public static void DeCompressRar(string rarFileName, string saveDir)//解压
        {
            string regKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\WinRAR.exe";
            RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(regKey);
            string winrarPath = registryKey.GetValue("").ToString();
            registryKey.Close();
            string winrarDir = System.IO.Path.GetDirectoryName(winrarPath);
            String commandOptions = string.Format("x {0} {1} -y -padmin", rarFileName, saveDir);

            ProcessStartInfo processStartInfo = new ProcessStartInfo();
            processStartInfo.FileName = System.IO.Path.Combine(winrarDir, "rar.exe");
            processStartInfo.Arguments = commandOptions;
            processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            try
            {
                Process process = new Process();
                process.StartInfo = processStartInfo;
                process.Start();
                process.WaitForExit(2000);
                process.Kill();
                process.Close();
            }
            catch(Exception)
            {

            }
        }

        private void backup_btn_Click(object sender, EventArgs e)
        {
            Thread t = new Thread(Backup);
            t.IsBackground = true;
            t.Start();
        }

        private void recover_btn_Click(object sender, EventArgs e)
        {
            OpenFileDlg.Title = "打开备份文件";
            OpenFileDlg.Filter = "备份文件(*.rar)|*.rar";
            OpenFileDlg.FilterIndex = 1;
            OpenFileDlg.InitialDirectory = System.Environment.CurrentDirectory;
            OpenFileDlg.FileName = "";
            OpenFileDlg.ShowDialog();
            
        }

        private void OpenFileDlg_FileOk(object sender, CancelEventArgs e)
        {
            if (OpenFileDlg.FileNames.Length != 3)
            {
                MessageBox.Show("恢复备份文件失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            Thread t = new Thread(Recover);
            t.IsBackground = true;
            t.Start();
            
                
        }

        public void Backup()
        {
            string rename = DateTime.Now.Year.ToString();
            int temp = DateTime.Now.Month;
            if (temp < 10) rename = rename + "0" + temp;
            else rename = rename + temp;
            temp = DateTime.Now.Day;
            if (temp < 10) rename = rename + "0" + temp;
            else rename = rename + temp;
            temp = DateTime.Now.Hour;
            if (temp < 10) rename = rename + "0" + temp;
            else rename = rename + temp;
            temp = DateTime.Now.Minute;
            if (temp < 10) rename = rename + "0" + temp;
            else rename = rename + temp;
            temp = DateTime.Now.Second;
            if (temp < 10) rename = rename + "0" + temp;
            else rename = rename + temp;
            CompressRar("C:\\ftp", "C:\\file" + rename + ".rar");
            CompressRar("C:\\sxn", "C:\\data" + rename + ".rar");
            string connString = "Data Source =.; Initial Catalog = FileManage; Integrated Security = True";
            SqlConnection conn = new SqlConnection(connString);
            try
            {
                
                string path = "C:\\db" + rename + ".rar";
                string sql = "backup DATABASE FileManage to DISK = @DISK";
                conn.Open();
                SqlCommand comm = new SqlCommand(sql, conn);
                comm.Parameters.AddWithValue("@DISK", path);
                comm.ExecuteNonQuery();
                label1.Text = "备份完成";
            }
            catch (Exception)
            {
                label1.Text = "备份失败";
            } 
            finally
            {
                conn.Close();
            }
        }

        public void Recover()
        {
            name[0] = System.IO.Path.GetFileName(OpenFileDlg.FileNames[0]);
            name[1] = System.IO.Path.GetFileName(OpenFileDlg.FileNames[1]);
            name[2] = System.IO.Path.GetFileName(OpenFileDlg.FileNames[2]);
            for (int i = 0; i < 3; i++)
            {
                /*if (name[i].Contains("file"))
                    DeCompressRar(OpenFileDlg.FileNames[i], "C:\\");
                else if (name[i].Contains("data"))
                    DeCompressRar(OpenFileDlg.FileNames[i], "C:\\");
                else if (name[i].Contains("db"))
                {
                    string connString = "Data Source =.; Initial Catalog = FileManage; Integrated Security = True";
                    SqlConnection conn = new SqlConnection(connString);
                    try
                    {

                        string path = OpenFileDlg.FileNames[i];
                        string sql = "restore DATABASE FileManage from DISK = @DISK";
                        conn.Open();
                        SqlCommand comm = new SqlCommand(sql, conn);
                        comm.Parameters.AddWithValue("@DISK", path);
                        comm.ExecuteNonQuery();
                        
                    }
                    catch (Exception)
                    {
                        label1.Text = "恢复失败";
                        return;
                    }
                    finally
                    {
                        conn.Close();
                    }
                }    

                else
                {
                    return;
                }*/
                label1.Text = "恢复完成";
            }
        }

    }
}
