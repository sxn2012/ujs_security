using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileManagement_Admin
{
    public partial class SystemLog : Form
    {
        public SystemLog()
        {
            InitializeComponent();
            BackgroundImage = Image.FromFile("main.jpg");
            UpdateLoginfo();
            Thread t = new Thread(AutoClearLog);
            t.IsBackground = true;
            t.Start();
        }

        private void UpdateLoginfo()
        {
            try
            {
                LogBox.Text = "";
                StreamReader sr = new StreamReader("sys.log");
                string line1 = sr.ReadLine();
                string line2 = sr.ReadLine();
                while (line1 != null && line2 != null && line1 != "" && line2 != "")
                {
                    LogBox.Text = line1 + " " + line2 + "\r\n" + LogBox.Text;
                    line1 = sr.ReadLine();
                    line2 = sr.ReadLine();
                }
                sr.Close();
            }
            catch (Exception ex)
            {
                LogBox.Clear();
                MessageBox.Show(ex.Message, "日志文件异常！", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void UpdateView_Click(object sender, EventArgs e)
        {
            UpdateLoginfo();
        }

        private void ClearView_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("确定要清空日志显示吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
            if (result != DialogResult.OK)
                return;
            LogBox.Clear();
        }

        private void DeleteView_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("确定要删除日志文件吗？\n注意该操作无法恢复！", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
            if (result != DialogResult.OK)
                return;
            try
            {
                FileStream fs = new FileStream("sys.log", FileMode.Truncate, FileAccess.ReadWrite);//清空文件
                fs.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "日志文件异常！", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            LogBox.Clear();
            
        }

        private void ViewHistoryLog_Click(object sender, EventArgs e)
        {
            OpenLogFileDlg.Title = "打开历史日志文件";
            OpenLogFileDlg.Filter = "日志文件(*.log)|*.log";
            OpenLogFileDlg.FilterIndex = 1;
            OpenLogFileDlg.InitialDirectory = System.Environment.CurrentDirectory;
            OpenLogFileDlg.FileName = "";
            OpenLogFileDlg.ShowDialog();
        }

        private void OpenLogFileDlg_FileOk(object sender, CancelEventArgs e)
        {
            try
            {
                
                string str = System.IO.Path.GetFileName(OpenLogFileDlg.FileName);
                string[] s = str.Split('.');
                if (!Regex.IsMatch(s[0].Trim(), @"^(?<year>\d{4})(?<month>\d{2})(?<day>\d{2})((20|21|22|23|[0-1]?\d)([0-5]?\d)([0-5]?\d))$") || s.Length != 2 || s[1] != "log")
                {
                    throw new Exception("日志文件有误！");
                }
                LogBox.Text = "";
                StreamReader sr = new StreamReader(OpenLogFileDlg.FileName.Trim());
                string line1 = sr.ReadLine();
                string line2 = sr.ReadLine();
                while (line1 != null && line2 != null && line1 != "" && line2 != "")
                {
                    LogBox.Text = line1 + " " + line2 + "\r\n" + LogBox.Text;
                    line1 = sr.ReadLine();
                    line2 = sr.ReadLine();
                }
                sr.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "显示历史日志失败！", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                
            }
        }

        private void AutoClearLog()//自动清理不是今年的日志文件
        {
            string str = System.Environment.CurrentDirectory;
            string[] strfile = Directory.GetFiles(str, "*.log");
            for (int i = 0; i < strfile.Length; i++ )
            {
                if (strfile[i].Split('.')[0].Length != 14)
                    continue;
                if (int.Parse(strfile[i].Substring(0, 4)) != DateTime.Now.Year)//年份不一致
                    File.Delete(strfile[i]);
            }
        }

        
    }
}
