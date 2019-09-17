using System;
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
using System.Data.SqlClient;
using System.Data.Sql;
using System.Diagnostics;
using System.Media;

namespace FileManagement
{
    public partial class MainForm : Form
    {
        public FtpWeb f = null;//ftp连接
        private LoginForm lf = null;//登录窗口
        public Socket_Send _sc = null;//socket发送
        private string updatefile_version = null;//修改的版本号
        private string updatefile_name = null;//修改过的文件名
        private string modify_path = null;
        public string u_name = null;//登陆的用户名
        private string vername_temp = null;//版本号
        private double process_percent = 0;//进度条百分比
        private Stack<string> s1 = new Stack<string>();
        public int status_flag = 0;//状态（下载、上传、无状态）
        public string r_path = null;//服务器端路径
        public string l_path = null;//本地路径
        public string u_dep = null;//用户所属部门
        public int countnum = 0;//计数器
        public int loginflag = 1;//登陆标志
        public DateTime lastclicktime;
        public MainForm(LoginForm ll,FtpWeb ftp,Socket_Send sclient,string uname)
        {
            InitializeComponent();
            BackgroundImage = Image.FromFile("main.jpg");
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;
            lf = ll;
            f = ftp;
            _sc = sclient;
            u_name = uname;
            f.mf = this;
            lastclicktime = DateTime.Now;
            progress_updown.Value = 0;//进度条清零
            label_updown.Text = "";
            label_percent.Text = "";

            FileView.View = View.SmallIcon;
            FileView.SmallImageList = DocImageList;
            FileView.MultiSelect = false;

            VersionView.View = View.SmallIcon;
            VersionView.SmallImageList = DocImageList;
            VersionView.MultiSelect = false;

            //隐藏
            label_updown.Visible = false;
            label_percent.Visible = false;
            progress_updown.Visible = false;
            
            CompleteModifyFile.Enabled = false;
            GiveUpModifyFile.Enabled = false;

            loginflag = 1;

            Thread t = new Thread(Counting);
            t.IsBackground = true;
            t.Start();

            //获取登陆用户的所属部门
            string connString = "Data Source =101.132.111.23; Initial Catalog = FileManage; User ID = userlogin; Pwd = User123456";
            SqlConnection conn = new SqlConnection(connString);
            String sql = "select UserDep from UserList where Username=@Username";
            try
            {
                conn.Open();
                SqlCommand comm = new SqlCommand(sql, conn);
                comm.Parameters.AddWithValue("@Username", u_name);
                
                u_dep = (string)comm.ExecuteScalar();
                u_dep = u_dep.Trim();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "数据库异常！", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                
            }
            finally
            {
                conn.Close();
            }
            
            
            UpdateFileinfo();
        }

        private void Counting()
        {
            while(true)
            {
                try
                {
                    countnum++;
                    //长时间无动作自动退出登陆
                    if (countnum >= 150 && loginflag == 1 && (updatefile_name == null || updatefile_name == ""))
                    {
                        _sc.Sending(u_name + "用户自动退出登陆");
                        countnum = 0;
                        this.Close();
                        lf.ShowOverTiming();
                        return;
                    }
                    timelabel.BeginInvoke(new MethodInvoker(() => timelabel.Text = DateTime.Now.ToString()));

                }
                catch(Exception)
                {

                }
                Thread.Sleep(1000);
            }
        }


        public void UpdateFileinfo()//更新文件信息
        {
            //清空
            FileView.Clear();
            
            string connString = "Data Source =101.132.111.23; Initial Catalog = FileManage; User ID = userlogin; Pwd = User123456";
            SqlConnection conn = new SqlConnection(connString);
            String sql = "select FilesName,FileSubject,LatestVersion from FileList";
            try
            {
                conn.Open();
                SqlCommand comm = new SqlCommand(sql, conn);
                SqlDataReader reader = comm.ExecuteReader();
                if (FileView.View == View.SmallIcon || FileView.View == View.LargeIcon)
                {
                    while (reader.Read())
                    {
                        ListViewItem item = new ListViewItem();
                        item.SubItems.Clear();
                        string str = reader[0].ToString().Trim();
                        string[] strtemp = str.Split('.');
                        string ext = "." + strtemp[strtemp.Length - 1];
                        if (!DocImageList.Images.Keys.Contains(ext))
                        {
                            string fname = strtemp[strtemp.Length - 1] + ".gif";
                            fname = "filetype\\" + fname;
                            if (!File.Exists(fname)) fname = "filetype\\unknown.gif";
                            DocImageList.Images.Add(ext, Image.FromFile(fname));
                        }
                        ListViewItem lvi = new ListViewItem();
                        lvi.Text = str;
                        lvi.ImageIndex = DocImageList.Images.Keys.IndexOf(ext);
                        FileView.Items.Add(lvi);
                    }
                }
                else
                {
                    FileView.Columns.Add("文档名", 100, HorizontalAlignment.Center);
                    FileView.Columns.Add("文档主题", 100, HorizontalAlignment.Center);
                    FileView.Columns.Add("最新版本", 100, HorizontalAlignment.Center);
                    while (reader.Read())
                    {
                        string str = reader[0].ToString().Trim();
                        string[] strtemp = str.Split('.');
                        string ext = "." + strtemp[strtemp.Length - 1];
                        if (!DocImageList.Images.Keys.Contains(ext))
                        {
                            string fname = strtemp[strtemp.Length - 1] + ".gif";
                            fname = "filetype\\" + fname;
                            if (!File.Exists(fname)) fname = "filetype\\unknown.gif";
                            DocImageList.Images.Add(ext, Image.FromFile(fname));
                        }
                        ListViewItem item = new ListViewItem();
                        item.SubItems.Clear();
                        item.ImageIndex = DocImageList.Images.Keys.IndexOf(ext);
                        item.SubItems[0].Text = reader[0].ToString().Trim();
                        item.SubItems.Add(reader[1].ToString().Trim());
                        item.SubItems.Add(reader[2].ToString().Trim());
                        FileView.Items.Add(item);
                    }
                }
                
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "数据库异常！", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                conn.Close();
            }
        }
        protected override void WndProc(ref Message msg)//关闭事件响应函数
        {

            const int WM_SYSCOMMAND = 0x0112;

            const int SC_CLOSE = 0xF060;

            if (msg.Msg == WM_SYSCOMMAND && ((int)msg.WParam == SC_CLOSE))
            {
                
                // 点击winform右上关闭按钮 
                if (updatefile_name != null && updatefile_name != "")
                {
                    MessageBox.Show("还有文档未修改完成！", "无法退出登陆！", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                DialogResult result = MessageBox.Show("确定要退出登陆吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                if (result == DialogResult.OK)
                {
                    _sc.Sending(u_name + "用户退出登录");
                    this.Close();
                }
                else
                    return;


            }
            base.WndProc(ref msg);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            countnum = 0;//清零
            this.lf.Visible = true;
            string connString = "Data Source =101.132.111.23; Initial Catalog = FileManage; User ID = userlogin; Pwd = User123456";
            SqlConnection conn = new SqlConnection(connString);
            try
            {
                string sql = "Update UserSafety Set UserStatus='0' where UserName=@UserName and UserStatus='2'";
                conn.Open();
                SqlCommand comm = new SqlCommand(sql, conn);
                comm.Parameters.AddWithValue("@Username", u_name);
                comm.ExecuteNonQuery();//返回值为0时不做处理
                
            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.Message, "退出登录失败！", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                conn.Close();
                loginflag = 0;
            }
        }

        public void EnableUpdownProgress()
        {
            //显示之前重置
            progress_updown.Value = 0;//进度条清零
            label_updown.Text = "";
            label_percent.Text = "";
            //显示进度
            label_updown.Visible = true;
            label_percent.Visible = true;
            progress_updown.Visible = true;
        }

        public void Delete_File()//删除文件线程
        {
            if (r_path != null && r_path != "")//删除单个文件版本
            {
                r_path = r_path.Trim();
                f.Delete(r_path);
                f.Delete(r_path + "_verify");
            }
            else//删除批量文件
            {
                while (s1.Count != 0)
                {
                    string str = s1.Pop().Trim();
                    f.Delete(str);
                    f.Delete(str + "_verify");
                }
                s1.Clear();
            }
            SystemSounds.Beep.Play();//删除完成，播放声音
            UpdateFileinfo();//更新文件信息
            if (vername_temp != null && vername_temp != "")
            {
                UpdateVersionInfo(vername_temp);
                vername_temp = null;
            }
            else
            {
                VersionView.Clear();
            }
        }

        public void Upload_File()//上传文件线程
        {
            
            process_percent = 0;
            //上传
            l_path = l_path.Trim();
            r_path = r_path.Trim();
            string n_path = l_path + ".en";
            n_path = n_path.Trim();
            label_updown.Text = "加密中...";
            CryptoClass.EncryptFile(l_path, n_path);
            f.Upload(n_path, r_path);
            label_updown.Text = "验证中...";
            string SHA256code = CryptoClass.SHA256File(l_path);
            string verifypath1 = l_path + "_verify";
            try
            {
                SHA256code = CryptoClass.AesEncrypt(SHA256code, CryptoClass.key);
                string verifypath2=r_path+"_verify";
                StreamWriter sw = new StreamWriter(verifypath1, false);
                sw.WriteLine(SHA256code);
                sw.Close();
                f.Upload(verifypath1, verifypath2);
            }
            catch (Exception)
            {
                label_updown.Text = "文档验证失败";
                this.UseWaitCursor = false;//不显示漏斗光标
                this.Refresh();
                return;
            }
            finally
            {
                File.Delete(verifypath1);
            }
            Thread.Sleep(400);
            File.Delete(n_path);
            SystemSounds.Beep.Play();//上传完成，播放声音
            label_updown.Text = "已完成";
            label_percent.Text = "100.00%";
            progress_updown.Value = 100;//进度条满格
            UpdateFileinfo();//更新文件信息
            this.UseWaitCursor = false;//不显示漏斗光标
            this.Refresh();
            if (vername_temp != null && vername_temp != "")
            {
                UpdateVersionInfo(vername_temp);
                vername_temp = null;
            }
            //进度显示完毕，继续隐藏
            Thread.Sleep(3000);
            label_updown.Visible = false;
            label_percent.Visible = false;
            progress_updown.Visible = false;
            modify_path = null;//修改后的文件路径复位
        }

        public void Download_File()//下载文件线程
        {
            
            process_percent = 0;
            l_path = l_path.Trim();
            r_path = r_path.Trim();
            string n_path = l_path + ".en";
            n_path = n_path.Trim();
            f.Download(r_path, n_path);
            label_updown.Text = "解密中...";
            CryptoClass.DecryptFile(n_path, l_path);
            label_updown.Text = "验证中...";
            string SHA256code = CryptoClass.SHA256File(l_path);
            string verifypath1 = r_path + "_verify";
            string verifypath2 = l_path + "_verify";
            try
            {
                
                f.Download(verifypath1, verifypath2);
                StreamReader sr = new StreamReader(verifypath2);
                string SHA256Verify = sr.ReadLine();
                SHA256Verify = CryptoClass.AesDecrypt(SHA256Verify, CryptoClass.key);
                sr.Close();
                if (SHA256code != SHA256Verify)
                    throw new Exception();

            }
            catch (Exception)
            {
                label_updown.Text = "文档验证失败";
                this.UseWaitCursor = false;//不显示漏斗光标
                this.Refresh();
                return;
            }
            
            Thread.Sleep(400);
            File.Delete(n_path);
            File.Delete(verifypath2);
            SystemSounds.Beep.Play();//下载完成，播放声音
            label_updown.Text = "已完成";
            label_percent.Text = "100.00%";
            progress_updown.Value = 100;//进度条满格
            UpdateFileinfo();//更新文件信息
            this.UseWaitCursor = false;//不显示漏斗光标
            this.Refresh();
            //打开文件
            ProcessStartInfo psi = new ProcessStartInfo(l_path);
            Process pro = new Process();
            pro.StartInfo = psi;
            pro.Start();
            //进度显示完毕，继续隐藏
            Thread.Sleep(3000);
            label_updown.Visible = false;
            label_percent.Visible = false;
            progress_updown.Visible = false;
        }

        public void ChangeUpDownProgress(double changenum)//处理进度条
        {

            if (status_flag == 1 && (label_updown.Text.Trim() == null || label_updown.Text.Trim() == "")) label_updown.Text = "上传中...";
            else if (status_flag == 2 && (label_updown.Text.Trim() == null || label_updown.Text.Trim() == "")) label_updown.Text = "下载中...";
            
            progress_updown.Value = (int)changenum;//进度条进度显示
            if (changenum - process_percent > 1.75)//刷新需要一定时间，差距过小时不刷新防止屏幕闪烁
            {
                if (changenum > 98.25) return;
                string str = changenum.ToString("f2");
                if (changenum < 10)
                    str = "0" + str;
                str = str + "%";
                label_percent.Text = str;//显示百分比数字
                process_percent = changenum;
            }
        }
        private void FileView_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (FileView.SelectedItems.Count != 1)
            {
               
                return;
            }
            foreach (ListViewItem lvi in FileView.SelectedItems)
            {
                string str = (FileView.View == View.SmallIcon || FileView.View == View.LargeIcon) ? lvi.Text : lvi.SubItems[0].Text.Trim();
                UpdateVersionInfo(str);
            }
            
        }

        private void UpdateVersionInfo(string version)//更新文件版本信息
        {
            VersionView.Clear();
            string str = version;
            string connString = "Data Source =101.132.111.23; Initial Catalog = FileManage; User ID = userlogin; Pwd = User123456";
            SqlConnection conn = new SqlConnection(connString);
            String sql = "select FilesName,FVersion,Modifytime,FileRemoteName from FileVersion where FilesName=@FilesName";
            try
            {
                conn.Open();
                SqlCommand comm = new SqlCommand(sql, conn);
                comm.Parameters.AddWithValue("@FilesName", str);
                SqlDataReader reader = comm.ExecuteReader();
                if (VersionView.View == View.SmallIcon || VersionView.View == View.LargeIcon)
                {
                    while (reader.Read())
                    {
                        ListViewItem item = new ListViewItem();
                        item.SubItems.Clear();

                        string stext = reader[3].ToString().Trim();
                        string[] strtemp = str.Split('.');
                        string ext = "." + strtemp[strtemp.Length - 1];
                        if (!DocImageList.Images.Keys.Contains(ext))
                        {
                            string fname = strtemp[strtemp.Length - 1] + ".gif";
                            fname = "filetype\\" + fname;
                            if (!File.Exists(fname)) fname = "filetype\\unknown.gif";
                            DocImageList.Images.Add(ext, Image.FromFile(fname));
                        }
                        ListViewItem lvi = new ListViewItem();
                        lvi.Text = stext;
                        lvi.ImageIndex = DocImageList.Images.Keys.IndexOf(ext);
                        VersionView.Items.Add(lvi);
                    }
                }
                else
                {
                    VersionView.Columns.Add("文档名", 100, HorizontalAlignment.Center);
                    VersionView.Columns.Add("版本号", 60, HorizontalAlignment.Center);
                    VersionView.Columns.Add("修改时间", 200, HorizontalAlignment.Center);
                    while (reader.Read())
                    {
                        string stext = reader[0].ToString().Trim();
                        string[] strtemp = stext.Split('.');
                        string ext = "." + strtemp[strtemp.Length - 1];
                        if (!DocImageList.Images.Keys.Contains(ext))
                        {
                            string fname = strtemp[strtemp.Length - 1] + ".gif";
                            fname = "filetype\\" + fname;
                            if (!File.Exists(fname)) fname = "filetype\\unknown.gif";
                            DocImageList.Images.Add(ext, Image.FromFile(fname));
                        }
                        ListViewItem item = new ListViewItem();
                        item.SubItems.Clear();
                        item.ImageIndex = DocImageList.Images.Keys.IndexOf(ext);
                        item.SubItems[0].Text = reader[0].ToString().Trim();
                        item.SubItems.Add(reader[1].ToString().Trim());
                        item.SubItems.Add(reader[2].ToString().Trim());


                        VersionView.Items.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "数据库异常！", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                conn.Close();
            }
        }

        private void SaveFileDlg_FileOk(object sender, CancelEventArgs e)//将选定的文件下载至本地
        {
            this.UseWaitCursor = true;//显示漏斗光标
            this.Refresh();
            countnum = 0;//清零
            string savingpath = SaveFileDlg.FileName.ToString();//保存路径
            //查询服务器端的文件名
            string connString = "Data Source =101.132.111.23; Initial Catalog = FileManage; User ID = userlogin; Pwd = User123456";
            SqlConnection conn = new SqlConnection(connString);
            String sql = "select FileRemoteName from FileList,FileVersion where FileList.FilesName=@FilesName and FileList.FilesName=FileVersion.FilesName and LatestVersion=FVersion";
            string str = "";
            try
            {
                conn.Open();
                SqlCommand comm = new SqlCommand(sql, conn);
                foreach (ListViewItem lvi in FileView.SelectedItems)
                {
                    str = (FileView.View == View.SmallIcon || FileView.View == View.LargeIcon) ? lvi.Text : lvi.SubItems[0].Text.Trim();
                    comm.Parameters.AddWithValue("@FilesName", str);
                }
                string remotepath = (string)comm.ExecuteScalar();//获取服务器端文件名
                if (!Security_Test.IsSafetyFile(System.IO.Path.GetFileName(remotepath).Trim())) throw new Exception("禁止下载此文件！");
                conn.Close();
                sql = "select FileSubject from FileList where FilesName=@FilesName";
                conn.Open();
                SqlCommand comm1 = new SqlCommand(sql, conn);
                comm1.Parameters.AddWithValue("@FilesName", str);
                string filesub = comm1.ExecuteScalar().ToString().Trim();
                
                //下载文件
                status_flag = 2;//下载文件标志
                r_path = filesub + "\\" + remotepath;
                l_path = savingpath;
                EnableUpdownProgress();
                Thread t = new Thread(Download_File);
                t.IsBackground = true;
                t.Start();
                _sc.Sending(u_name + "用户浏览文档" + str + "成功");
                
            }
            catch(Exception ex)
            {
                _sc.Sending(u_name + "用户浏览文档" + str + "失败");
                MessageBox.Show(ex.Message, "下载文档失败！", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                conn.Close();
                
            }
        }
     

        private void UpdateFileDlg_FileOk(object sender, CancelEventArgs e)//将修改后的文件上传至服务端
        {
            this.UseWaitCursor = true;//显示漏斗光标
            this.Refresh();
            countnum = 0;//清零
            string filepath = UpdateFileDlg.FileName;//文件路径
            string filename = System.IO.Path.GetFileNameWithoutExtension(updatefile_name).Trim();
            string connString = "Data Source =101.132.111.23; Initial Catalog = FileManage; User ID = userlogin; Pwd = User123456";
            SqlConnection conn = new SqlConnection(connString);
            string fileext = System.IO.Path.GetExtension(UpdateFileDlg.FileName).Trim();
            string fileversion = updatefile_version.Replace('.', '_');
            string filername = filename + "_" + fileversion + fileext;//文件重命名
            
            try
            {
                if (!Security_Test.IsSafetyFile(System.IO.Path.GetFileName(filepath).Trim())) throw new Exception("禁止上传此文件！");
                string temp = System.IO.Path.GetExtension(updatefile_name).Trim();
                if (temp.ToUpper() != fileext.ToUpper()) throw new Exception("修改文档不能改变原文档扩展名！");
                EnableUpdownProgress();
                string sql = "select FileSubject from FileList where FilesName=@FilesName";
                conn.Open();
                SqlCommand comm2 = new SqlCommand(sql, conn);
                comm2.Parameters.AddWithValue("@FilesName", updatefile_name);
                string filesub = comm2.ExecuteScalar().ToString().Trim();
                //上传文件
                l_path = filepath;
                r_path = filesub + "\\" + filername;
                status_flag = 1;//上传文件标志
                vername_temp = updatefile_name.Trim();//更新版本参数
                Thread t = new Thread(Upload_File);
                t.IsBackground = true;
                t.Start();
                //更新修改标志
                conn.Close();
                sql = "update FileList set LatestVersion=@LatestVersion, FileStatus='0' where FilesName=@FilesName";
                conn.Open();
                SqlCommand comm = new SqlCommand(sql, conn);
                comm.Parameters.AddWithValue("@FilesName", updatefile_name);
                comm.Parameters.AddWithValue("@LatestVersion", updatefile_version);

                int num = comm.ExecuteNonQuery();
                if (num <= 0)
                    throw new Exception("数据库异常");
                conn.Close();
                //更新文件版本号
                sql = "insert into FileVersion(FilesName,FVersion,FileRemoteName,Modifytime) values(@FilesName,@FVersion,@FileRemoteName,@Modifytime)";
                conn.Open();
                SqlCommand comm1 = new SqlCommand(sql, conn);
                comm1.Parameters.AddWithValue("@FilesName", updatefile_name);
                comm1.Parameters.AddWithValue("@FVersion", updatefile_version);
                comm1.Parameters.AddWithValue("@FileRemoteName", filername);
                comm1.Parameters.AddWithValue("@Modifytime", DateTime.Now.ToString());
                num = comm1.ExecuteNonQuery();
                if (num <= 0)
                    throw new Exception("数据库异常");
                //显示菜单
                StartModifyFile.Enabled = true;
                GiveUpModifyFile.Enabled = false;
                CompleteModifyFile.Enabled = false;
                _sc.Sending(u_name + "用户修改文档" + updatefile_name + "完成");
                

            }
            catch (Exception ex)
            {
                this.UseWaitCursor = false;//不显示漏斗光标
                this.Refresh();
                _sc.Sending(u_name + "用户修改文档" + updatefile_name + "失败");
                MessageBox.Show(ex.Message, "修改文档失败！", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                conn.Close();
                updatefile_name = null;//修改完成
               
            }
        }

        private void UpdateFile_auto()
        {
            this.UseWaitCursor = true;//显示漏斗光标
            this.Refresh();
            countnum = 0;//清零
            string filepath = modify_path;//文件路径
            string filename = System.IO.Path.GetFileNameWithoutExtension(updatefile_name).Trim();
            string connString = "Data Source =101.132.111.23; Initial Catalog = FileManage; User ID = userlogin; Pwd = User123456";
            SqlConnection conn = new SqlConnection(connString);
            string fileext = System.IO.Path.GetExtension(modify_path).Trim();
            string fileversion = updatefile_version.Replace('.', '_');
            string filername = filename + "_" + fileversion + fileext;//文件重命名

            try
            {
                if (!Security_Test.IsSafetyFile(System.IO.Path.GetFileName(filepath).Trim())) throw new Exception("禁止上传此文件！");
                string temp = System.IO.Path.GetExtension(updatefile_name).Trim();
                if (temp.ToUpper() != fileext.ToUpper()) throw new Exception("修改文档不能改变原文档扩展名！");
                EnableUpdownProgress();
                string sql = "select FileSubject from FileList where FilesName=@FilesName";
                conn.Open();
                SqlCommand comm2 = new SqlCommand(sql, conn);
                comm2.Parameters.AddWithValue("@FilesName", updatefile_name);
                string filesub = comm2.ExecuteScalar().ToString().Trim();
                //上传文件
                l_path = filepath;
                r_path = filesub + "\\" + filername;
                status_flag = 1;//上传文件标志
                vername_temp = updatefile_name.Trim();//更新版本参数
                Thread t = new Thread(Upload_File);
                t.IsBackground = true;
                t.Start();
                //更新修改标志
                conn.Close();
                sql = "update FileList set LatestVersion=@LatestVersion, FileStatus='0' where FilesName=@FilesName";
                conn.Open();
                SqlCommand comm = new SqlCommand(sql, conn);
                comm.Parameters.AddWithValue("@FilesName", updatefile_name);
                comm.Parameters.AddWithValue("@LatestVersion", updatefile_version);

                int num = comm.ExecuteNonQuery();
                if (num <= 0)
                    throw new Exception("数据库异常");
                conn.Close();
                //更新文件版本号
                sql = "insert into FileVersion(FilesName,FVersion,FileRemoteName,Modifytime) values(@FilesName,@FVersion,@FileRemoteName,@Modifytime)";
                conn.Open();
                SqlCommand comm1 = new SqlCommand(sql, conn);
                comm1.Parameters.AddWithValue("@FilesName", updatefile_name);
                comm1.Parameters.AddWithValue("@FVersion", updatefile_version);
                comm1.Parameters.AddWithValue("@FileRemoteName", filername);
                comm1.Parameters.AddWithValue("@Modifytime", DateTime.Now.ToString());
                num = comm1.ExecuteNonQuery();
                if (num <= 0)
                    throw new Exception("数据库异常");
                //显示菜单
                StartModifyFile.Enabled = true;
                GiveUpModifyFile.Enabled = false;
                CompleteModifyFile.Enabled = false;
                _sc.Sending(u_name + "用户修改文档" + updatefile_name + "完成");


            }
            catch (Exception ex)
            {
                this.UseWaitCursor = false;//不显示漏斗光标
                this.Refresh();
                _sc.Sending(u_name + "用户修改文档" + updatefile_name + "失败");
                MessageBox.Show(ex.Message, "修改文档失败！", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                conn.Close();
                updatefile_name = null;//修改完成

            }
        }


        private void SaveVersionDlg_FileOk(object sender, CancelEventArgs e)//将选定的文件版本下载到本地
        {
            this.UseWaitCursor = true;//显示漏斗光标
            this.Refresh();
            countnum = 0;//清零
            string savingpath = SaveVersionDlg.FileName.ToString();//本地路径
            //获取服务器端文件名
            string connString = "Data Source =101.132.111.23; Initial Catalog = FileManage; User ID = userlogin; Pwd = User123456";
            SqlConnection conn = new SqlConnection(connString);
            String sql = "";
            string str = "";
            string fve = "";
            try
            {
                
                foreach (ListViewItem lvi in VersionView.SelectedItems)
                {
                    
                    if (VersionView.View == View.SmallIcon || VersionView.View == View.LargeIcon)
                    {
                        sql = "select FilesName,FVersion from FileVersion where FileRemoteName=@FileRemoteName";
                        conn.Open();
                        SqlCommand comm_ = new SqlCommand(sql, conn);
                        comm_.Parameters.AddWithValue("@FileRemoteName", lvi.Text.Trim());
                        SqlDataReader reader = comm_.ExecuteReader();
                        while (reader.Read())
                        {
                            str = reader[0].ToString().Trim();
                            fve = reader[1].ToString().Trim();
                        }
                        conn.Close();
                    }
                    else
                    {
                        str = lvi.SubItems[0].Text.Trim();
                        fve = lvi.SubItems[1].Text.Trim();
                    }
                    
                }
                sql = "select FileRemoteName from FileVersion where FilesName=@FilesName and FVersion=@FVersion";
                conn.Open();
                SqlCommand comm = new SqlCommand(sql, conn);
                comm.Parameters.AddWithValue("@FilesName", str);
                comm.Parameters.AddWithValue("@FVersion", fve);
                string remotepath = (string)comm.ExecuteScalar();//获得服务器端文件名
                if (!Security_Test.IsSafetyFile(System.IO.Path.GetFileName(remotepath).Trim())) throw new Exception("禁止下载此文件！");
                EnableUpdownProgress();
                conn.Close();
                sql = "select FileSubject from FileList where FilesName=@FilesName";
                conn.Open();
                SqlCommand comm1 = new SqlCommand(sql, conn);
                comm1.Parameters.AddWithValue("@FilesName", str);
                string filesub = comm1.ExecuteScalar().ToString().Trim();
                //下载文件
                status_flag = 2;//下载文件标志
                r_path = filesub + "\\" + remotepath;
                l_path = savingpath;
                Thread t = new Thread(Download_File);
                t.IsBackground = true;
                t.Start();
                _sc.Sending(u_name + "用户浏览文档版本" + str + "(" + fve + ")" + "成功");
            }
            catch (Exception ex)
            {
                this.UseWaitCursor = false;//不显示漏斗光标
                this.Refresh();
                _sc.Sending(u_name + "用户浏览文档版本" + str + "(" + fve + ")" + "失败");
                MessageBox.Show(ex.Message, "下载文档失败！", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                conn.Close();
                
            }
        }

        

        private void ViewVersion_Click(object sender, EventArgs e)//查看版本
        {
            if (DateTime.Compare(lastclicktime.AddSeconds(1), DateTime.Now) > 0) //限制点击频率
            {
                MessageBox.Show("操作过于频繁！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
                lastclicktime = DateTime.Now;
            countnum = 0;//清零
            if (updatefile_name != null && updatefile_name != "")
            {
                MessageBox.Show("还有文档未修改完成！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (VersionView.SelectedItems.Count != 1)//未选定版本信息
            {
                MessageBox.Show("请选择一条信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //查看用户是否有浏览权限
            string connString = "Data Source =101.132.111.23; Initial Catalog = FileManage; User ID = userlogin; Pwd = User123456";
            SqlConnection conn = new SqlConnection(connString);
            String sql = "select count(*) from AccessList where DepName=@DepName and SubName in (select FileSubject from FileList where FilesName=@FilesName) and (privilege='1' or privilege='2')";
            string str = "";
            string strver = "";
            try
            {
                
                foreach (ListViewItem lvi in VersionView.SelectedItems)
                {
                    if (VersionView.View == View.SmallIcon || VersionView.View == View.LargeIcon)
                    {
                        sql = "select FilesName,FVersion from FileVersion where FileRemoteName=@FileRemoteName";
                        conn.Open();
                        SqlCommand comm_ = new SqlCommand(sql, conn);
                        comm_.Parameters.AddWithValue("@FileRemoteName", lvi.Text.Trim());
                        SqlDataReader reader = comm_.ExecuteReader();
                        while (reader.Read())
                        {
                            str = reader[0].ToString().Trim();
                            strver = reader[1].ToString().Trim();
                        }
                        conn.Close();
                    }
                    else
                    {
                        str = lvi.SubItems[0].Text.Trim();
                        strver = lvi.SubItems[1].Text.Trim();
                    }
                    
                }
                sql = "select count(*) from AccessList where DepName=@DepName and SubName in (select FileSubject from FileList where FilesName=@FilesName) and (privilege='1' or privilege='2')";
                conn.Open();
                SqlCommand comm = new SqlCommand(sql, conn);
                comm.Parameters.AddWithValue("@DepName", u_dep);
                comm.Parameters.AddWithValue("@FilesName", str);
                int num = (int)comm.ExecuteScalar();
                if (num != 1)//用户没有权限
                {
                    throw new Exception("用户没有浏览版本所需的权限！");
                }

            }
            catch (Exception ex)
            {
                _sc.Sending(u_name + "用户浏览文档版本" + str + "(" + strver + ")" + "失败");
                MessageBox.Show(ex.Message, "浏览版本失败！", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                
                return;
            }
            finally
            {
                conn.Close();
            }
            SaveVersionDlg.Title = "下载需要浏览的文档版本";
            SaveVersionDlg.Filter = "文本文件(*.txt)|*.txt|所有文件(*.*)|*.*";
            SaveVersionDlg.FilterIndex = 1;
            SaveVersionDlg.FileName = "";
            //弹出对话框，要求用户选择文件保存的路径
            SaveVersionDlg.ShowDialog();
        }

        private void DeleteVersion_Click(object sender, EventArgs e)//删除文件版本
        {
            if (DateTime.Compare(lastclicktime.AddSeconds(1), DateTime.Now) > 0) //限制点击频率
            {
                MessageBox.Show("操作过于频繁！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
                lastclicktime = DateTime.Now;
            countnum = 0;//清零
            if (updatefile_name != null && updatefile_name != "")
            {
                MessageBox.Show("还有文档未修改完成！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (VersionView.SelectedItems.Count != 1)//未选择文件版本信息
            {
                MessageBox.Show("请选择一条信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //删除确认
            DialogResult result = MessageBox.Show("确定要删除该文档版本吗？\n注意该操作无法恢复！", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
            if (result != DialogResult.OK)
                return;
            //查看用户是否有管理权限
            string connString = "Data Source =101.132.111.23; Initial Catalog = FileManage; User ID = userlogin; Pwd = User123456";
            SqlConnection conn = new SqlConnection(connString);
            double lver = 0;
            double dver = 0;
            String sql = "";
            string str = "";
            string strver = "";
            string filesub = "";
            try
            {
                foreach (ListViewItem lvi in VersionView.SelectedItems)
                {
                    if (VersionView.View == View.SmallIcon || VersionView.View == View.LargeIcon)
                    {
                        sql = "select FilesName,FVersion from FileVersion where FileRemoteName=@FileRemoteName";
                        conn.Open();
                        SqlCommand comm_ = new SqlCommand(sql, conn);
                        comm_.Parameters.AddWithValue("@FileRemoteName", lvi.Text.Trim());
                        SqlDataReader reader = comm_.ExecuteReader();
                        while (reader.Read())
                        {
                            str = reader[0].ToString().Trim();
                            strver = reader[1].ToString().Trim();
                        }
                        conn.Close();
                    }
                    else
                    {
                        str = lvi.SubItems[0].Text.Trim();
                        strver = lvi.SubItems[1].Text.Trim();
                    }
                    dver = double.Parse(strver);
                }
                sql = "select count(*) from AccessList where DepName=@DepName and SubName in (select FileSubject from FileList where FilesName=@FilesName) and privilege='2'";
                conn.Open();
                SqlCommand comm = new SqlCommand(sql, conn);
                comm.Parameters.AddWithValue("@DepName", u_dep);
                comm.Parameters.AddWithValue("@FilesName", str);
                int num = (int)comm.ExecuteScalar();
                if (num != 1)//用户没有权限
                {
                    throw new Exception("用户没有删除版本所需的权限！");
                }
                conn.Close();
                //查看当前数据库中存储的文件的最新版本号
                sql = "select LatestVersion from FileList where FilesName=@FilesName";
                conn.Open();
                SqlCommand comm3 = new SqlCommand(sql, conn);
                comm3.Parameters.AddWithValue("@FilesName", str);
                lver = double.Parse(comm3.ExecuteScalar().ToString().Trim());//最新版本号
                if (lver == dver)//禁止删除正在修改的版本
                {
                    conn.Close();
                    sql = "select FileStatus from FileList where FilesName=@FilesName";
                    conn.Open();
                    SqlCommand comm1 = new SqlCommand(sql, conn);
                    comm1.Parameters.AddWithValue("@FilesName", str);
                    int modify_flag = int.Parse(comm1.ExecuteScalar().ToString().Trim());
                    if (modify_flag != 0)//文件正在被修改
                        throw new Exception("该文件被占用！");
                    
                }
                conn.Close();
                sql = "select FileSubject from FileList where FilesName=@FilesName";
                conn.Open();
                SqlCommand comm4 = new SqlCommand(sql, conn);
                comm4.Parameters.AddWithValue("@FilesName", str);
                filesub = comm4.ExecuteScalar().ToString().Trim();
            }
            catch (Exception ex)
            {
                _sc.Sending(u_name + "用户删除文档版本" + str + "(" + strver + ")" + "失败");
                MessageBox.Show(ex.Message, "删除版本失败！", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                return;
            }
            finally
            {
                conn.Close();
            }
            //查看要删除的文件版本在服务器端的文件名
            sql = "select FileRemoteName from FileVersion where FilesName=@FilesName and FVersion=@FVersion";
            string st = str;
            string fve = strver;
            try
            {
                conn.Open();
                SqlCommand comm = new SqlCommand(sql, conn);
                
                
                comm.Parameters.AddWithValue("@FilesName", st);
                comm.Parameters.AddWithValue("@FVersion", fve);
                
                SqlDataReader reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    vername_temp = st.Trim();//更新的文件版本参数
                    //删除文件
                    r_path = filesub + "\\" + reader[0].ToString().Trim();
                    Thread t = new Thread(Delete_File);
                    t.IsBackground = true;
                    t.Start();
                }
                conn.Close();

                //从版本信息表中删除该信息
                sql = "delete from FileVersion where FilesName=@FilesName and FVersion=@FVersion";
                conn.Open();
                SqlCommand comm1 = new SqlCommand(sql, conn);
                comm1.Parameters.AddWithValue("@FilesName", st);
                comm1.Parameters.AddWithValue("@FVersion", fve);
                int num = (int)comm1.ExecuteNonQuery();
                if (num <= 0)
                    throw new Exception("数据库异常");
                conn.Close();
            
                if (lver == dver)//如果文件目前的最新版本号正好为当前已经删除的文件
                {
                    //查看该文件是否有其他版本
                    sql = "select count(*) from FileVersion where FilesName=@FilesName";
                    conn.Open();
                    SqlCommand comm6 = new SqlCommand(sql, conn);
                    comm6.Parameters.AddWithValue("@FilesName", st);
                    int flag = (int)comm6.ExecuteScalar();
                    conn.Close();
                    if (flag == 0)//没有其它版本，该文件所有版本已全部删除
                    {
                        //将文件表中的该文件信息删除
                        sql = "delete from FileList where FilesName=@FilesName";
                        conn.Open();
                        SqlCommand comm2 = new SqlCommand(sql, conn);
                        comm2.Parameters.AddWithValue("@FilesName", st);
                        num = (int)comm2.ExecuteNonQuery();
                        if (num <= 0)
                            throw new Exception("数据库异常");
                        conn.Close();
                        return;
                    }
                    //该文件还有其他未删除的版本
                    //倒序查看其他未删除的版本号
                    sql = "select FVersion from FileVersion where FilesName=@FilesName order by FVersion DESC";
                    conn.Open();
                    SqlCommand comm4 = new SqlCommand(sql, conn);
                    comm4.Parameters.AddWithValue("@FilesName", st);
                    string temp = (string)comm4.ExecuteScalar();//找到目前删除了该版本后文件剩余的最新版本
                    conn.Close();
                    //修改文件信息表中存储的文件最新版本号
                    sql = "update FileList set LatestVersion=@LatestVersion where FilesName=@FilesName";
                    conn.Open();
                    SqlCommand comm5 = new SqlCommand(sql, conn);
                    comm5.Parameters.AddWithValue("@LatestVersion", temp);
                    comm5.Parameters.AddWithValue("@FilesName", st);
                    int num_ = comm5.ExecuteNonQuery();
                    if (num_ <= 0)
                        throw new Exception("数据库异常");
                    conn.Close();

                }
                _sc.Sending(u_name + "用户删除文档版本" + str + "(" + strver + ")" + "成功");
            }
            catch (Exception ex)
            {
                _sc.Sending(u_name + "用户删除文档版本" + str + "(" + strver + ")" + "失败");
                MessageBox.Show(ex.Message, "删除版本失败！", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                conn.Close();
            }
        }

        
        private void ViewFile_Click(object sender, EventArgs e)//查看文件
        {
            if (DateTime.Compare(lastclicktime.AddSeconds(1), DateTime.Now) > 0) //限制点击频率
            {
                MessageBox.Show("操作过于频繁！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
                lastclicktime = DateTime.Now;
            countnum = 0;//清零
            if (updatefile_name != null && updatefile_name != "")
            {
                MessageBox.Show("还有文档未修改完成！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (FileView.SelectedItems.Count != 1)//没有选择文件信息
            {
                MessageBox.Show("请选择一条信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //查看用户是否有浏览文件的权限
            string connString = "Data Source =101.132.111.23; Initial Catalog = FileManage; User ID = userlogin; Pwd = User123456";
            SqlConnection conn = new SqlConnection(connString);
            String sql = "select count(*) from AccessList where DepName=@DepName and SubName in (select FileSubject from FileList where FilesName=@FilesName) and (privilege='1' or privilege='2')";
            string str = "";
            try
            {
                conn.Open();
                SqlCommand comm = new SqlCommand(sql, conn);
                comm.Parameters.AddWithValue("@DepName", u_dep);
                foreach (ListViewItem lvi in FileView.SelectedItems)
                {
                    str = (FileView.View == View.SmallIcon || FileView.View == View.LargeIcon) ? lvi.Text : lvi.SubItems[0].Text.Trim();
                    comm.Parameters.AddWithValue("@FilesName", str);
                }
                int num = (int)comm.ExecuteScalar();
                if (num != 1)//没有权限
                {
                    throw new Exception("用户没有浏览文档所需的权限！");
                }

            }
            catch (Exception ex)
            {
                _sc.Sending(u_name + "用户浏览文档" + str + "失败");
                MessageBox.Show(ex.Message, "浏览文档失败！", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                
                return;
            }
            finally
            {
                conn.Close();
            }
            SaveFileDlg.Title = "下载需要浏览的文档";
            SaveFileDlg.Filter = "文本文件(*.txt)|*.txt|所有文件(*.*)|*.*";
            SaveFileDlg.FilterIndex = 1;
            SaveFileDlg.FileName = "";
            //弹出对话框，让用户选择下载文件的保存路径
            SaveFileDlg.ShowDialog();
        }

        private void DeleteFile_Click(object sender, EventArgs e)//删除文件
        {
            if (DateTime.Compare(lastclicktime.AddSeconds(1), DateTime.Now) > 0) //限制点击频率
            {
                MessageBox.Show("操作过于频繁！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
                lastclicktime = DateTime.Now;
            countnum = 0;//清零
            if (updatefile_name != null && updatefile_name != "")
            {
                MessageBox.Show("还有文档未修改完成！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (FileView.SelectedItems.Count != 1)//没有选择文件信息
            {
                MessageBox.Show("请选择一条信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //删除确认
            DialogResult result = MessageBox.Show("确定要删除该文档吗？\n注意该操作无法恢复！", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
            if (result != DialogResult.OK)
                return;
            //查看用户是否有管理权限
            string connString = "Data Source =101.132.111.23; Initial Catalog = FileManage; User ID = userlogin; Pwd = User123456";
            SqlConnection conn = new SqlConnection(connString);
            String sql = "select count(*) from AccessList where DepName=@DepName and SubName in (select FileSubject from FileList where FilesName=@FilesName) and privilege='2'";
            string str = "";
            string filesub = "";
            try
            {
                conn.Open();
                SqlCommand comm = new SqlCommand(sql, conn);
                comm.Parameters.AddWithValue("@DepName", u_dep);
                
                foreach (ListViewItem lvi in FileView.SelectedItems)
                {
                    str = (FileView.View == View.SmallIcon || FileView.View == View.LargeIcon) ? lvi.Text : lvi.SubItems[0].Text.Trim();
                    comm.Parameters.AddWithValue("@FilesName", str);
                }
                int num = (int)comm.ExecuteScalar();
                if (num != 1)//没有权限
                {
                    throw new Exception("用户没有删除文档所需的权限！");
                }
                conn.Close();
                sql = "select FileStatus from FileList where FilesName=@FilesName";
                conn.Open();
                SqlCommand comm1 = new SqlCommand(sql, conn);
                comm1.Parameters.AddWithValue("@FilesName", str);
                int modify_flag = int.Parse(comm1.ExecuteScalar().ToString().Trim());
                if (modify_flag != 0)//文件正在被修改
                    throw new Exception("该文件被占用！");
                conn.Close();
                sql = "select FileSubject from FileList where FilesName=@FilesName";
                conn.Open();
                SqlCommand comm2 = new SqlCommand(sql, conn);
                comm2.Parameters.AddWithValue("@FilesName", str);
                filesub = comm2.ExecuteScalar().ToString().Trim();
            }
            catch (Exception ex)
            {
                _sc.Sending(u_name + "用户删除文档" + str + "失败");
                MessageBox.Show(ex.Message, "删除文档失败！", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            finally
            {
                conn.Close();
            }
            //查看要删除的文件在服务器上的文件名
            sql = "select FileRemoteName from FileList,FileVersion where FileList.FilesName=@FilesName and FileList.FilesName=FileVersion.FilesName";
            string temp = "";
            try
            {
                conn.Open();
                SqlCommand comm = new SqlCommand(sql, conn);
                foreach (ListViewItem lvi in FileView.SelectedItems)
                {
                    temp = (FileView.View == View.SmallIcon || FileView.View == View.LargeIcon) ? lvi.Text : lvi.SubItems[0].Text.Trim();
                    comm.Parameters.AddWithValue("@FilesName", temp);
                }
                s1.Clear();
                SqlDataReader reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    //删除文件
                    r_path = filesub + "\\" + reader[0].ToString().Trim();
                    s1.Push(r_path);
                    r_path = null;
                }
                Thread t = new Thread(Delete_File);
                t.IsBackground = true;
                t.Start();
                conn.Close();
                //删除该文件的所有版本信息
                sql = "delete from FileVersion where FilesName=@FilesName";
                conn.Open();
                SqlCommand comm1 = new SqlCommand(sql, conn);
                comm1.Parameters.AddWithValue("@FilesName", temp);
                int num = (int)comm1.ExecuteNonQuery();
                if (num <= 0)
                    throw new Exception("数据库异常");
                conn.Close();
                //删除该文件在文件信息表中的信息
                sql = "delete from FileList where FilesName=@FilesName";
                conn.Open();
                SqlCommand comm2 = new SqlCommand(sql, conn);
                comm2.Parameters.AddWithValue("@FilesName", temp);
                num = (int)comm2.ExecuteNonQuery();
                if (num <= 0)
                    throw new Exception("数据库异常");
                _sc.Sending(u_name + "用户删除文档" + str + "成功");
            }
            catch (Exception ex)
            {
                _sc.Sending(u_name + "用户删除文档" + str + "失败");
                MessageBox.Show(ex.Message, "删除文档失败！", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                conn.Close();
            }
        }

        private void StartModifyFile_Click(object sender, EventArgs e)//开始修改文件
        {
            if (DateTime.Compare(lastclicktime.AddSeconds(1), DateTime.Now) > 0) //限制点击频率
            {
                MessageBox.Show("操作过于频繁！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
                lastclicktime = DateTime.Now;
            countnum = 0;//清零
            if (FileView.SelectedItems.Count != 1)//没有选择文件信息
            {
                MessageBox.Show("请选择一条信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //查看用户是否有管理权限
            string connString = "Data Source =101.132.111.23; Initial Catalog = FileManage; User ID = userlogin; Pwd = User123456";
            SqlConnection conn = new SqlConnection(connString);
            String sql = "select count(*) from AccessList where DepName=@DepName and SubName in (select FileSubject from FileList where FilesName=@FilesName) and privilege='2'";
            string str = "";
            try
            {
                conn.Open();
                SqlCommand comm = new SqlCommand(sql, conn);
                comm.Parameters.AddWithValue("@DepName", u_dep);
                foreach (ListViewItem lvi in FileView.SelectedItems)
                {
                    str = (FileView.View == View.SmallIcon || FileView.View == View.LargeIcon) ? lvi.Text : lvi.SubItems[0].Text.Trim();
                    comm.Parameters.AddWithValue("@FilesName", str);
                }
                int num = (int)comm.ExecuteScalar();
                if (num != 1)//没有权限
                {
                    throw new Exception("用户没有修改文档所需的权限！");
                }

            }
            catch (Exception ex)
            {
                _sc.Sending(u_name + "用户修改文档" + str + "失败");
                MessageBox.Show(ex.Message, "修改文档失败！", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                updatefile_name = null;
                return;
            }
            finally
            {
                conn.Close();
            }
            //查看文件是否正在被修改
            sql = "select FileStatus from FileList where FilesName=@FilesName";
            string temp = "";
            try
            {
                conn.Open();
                SqlCommand comm = new SqlCommand(sql, conn);
                foreach (ListViewItem lvi in FileView.SelectedItems)
                {
                    temp = (FileView.View == View.SmallIcon || FileView.View == View.LargeIcon) ? lvi.Text : lvi.SubItems[0].Text.Trim();
                    
                    comm.Parameters.AddWithValue("@FilesName", temp);
                }
                int modify_flag = int.Parse(comm.ExecuteScalar().ToString().Trim());
                if (modify_flag != 0)//文件正在被修改
                    throw new Exception("该文件被占用！");
                

                

                ModifyFileDlg.Title = "下载需要修改的文档";
                ModifyFileDlg.Filter = "文本文件(*.txt)|*.txt|所有文件(*.*)|*.*";
                ModifyFileDlg.FilterIndex = 1;
                ModifyFileDlg.FileName = "";
                ModifyFileDlg.ShowDialog();
            }
            catch (Exception ex)
            {
                _sc.Sending(u_name + "用户修改文档" + str + "失败");
                MessageBox.Show(ex.Message, "修改文档失败！", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                conn.Close();
            }
        }

        private void CompleteModifyFile_Click(object sender, EventArgs e)//完成文件修改
        {
            if (DateTime.Compare(lastclicktime.AddSeconds(1), DateTime.Now) > 0) //限制点击频率
            {
                MessageBox.Show("操作过于频繁！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
                lastclicktime = DateTime.Now;
            countnum = 0;//清零
            if (FileView.SelectedItems.Count != 1)//没有选择文件信息
            {
                MessageBox.Show("请选择一条信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string str = "";
            try
            {
                //查看是否有对该文件进行修改操作
                foreach (ListViewItem lvi in FileView.SelectedItems)
                {
                    str = (FileView.View == View.SmallIcon || FileView.View == View.LargeIcon) ? lvi.Text : lvi.SubItems[0].Text.Trim();

                    if (updatefile_name != str || modify_path == null)
                        throw new Exception("没有对选定的文档进行修改操作，无法完成修改操作！");

                }


                DialogResult result = MessageBox.Show("修改文档时是否对文档进行过移动或重命名的操作？", "请确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    UpdateFileDlg.Title = "上传修改完成的文档";
                    UpdateFileDlg.Filter = "文本文件(*.txt)|*.txt|所有文件(*.*)|*.*";
                    UpdateFileDlg.FilterIndex = 1;
                    UpdateFileDlg.FileName = "";
                    //弹出框让用户选择修改好的文件
                    UpdateFileDlg.ShowDialog();
                }
                else
                    UpdateFile_auto();
                
            }
            catch (Exception ex)
            {
                _sc.Sending(u_name + "用户修改文档" + str + "失败");
                MessageBox.Show(ex.Message, "完成修改失败！", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                
            }
        }

        private void GiveUpModifyFile_Click(object sender, EventArgs e)//放弃修改文件
        {
            if (DateTime.Compare(lastclicktime.AddSeconds(1), DateTime.Now) > 0) //限制点击频率
            {
                MessageBox.Show("操作过于频繁！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
                lastclicktime = DateTime.Now;
            countnum = 0;//清零
            if (FileView.SelectedItems.Count != 1)//没有选择文件信息
            {
                MessageBox.Show("请选择一条信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //放弃修改确认
            DialogResult result = MessageBox.Show("确定要放弃修改吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
            if (result != DialogResult.OK)
                return;
            //重置修改标记
            string connString = "Data Source =101.132.111.23; Initial Catalog = FileManage; User ID = userlogin; Pwd = User123456";
            SqlConnection conn = new SqlConnection(connString);
            String sql = "update FileList set FileStatus='0' where FilesName=@FilesName";
            string str = "";
            try
            {
                conn.Open();
                SqlCommand comm = new SqlCommand(sql, conn);
                foreach (ListViewItem lvi in FileView.SelectedItems)
                {
                    str = (FileView.View == View.SmallIcon || FileView.View == View.LargeIcon) ? lvi.Text : lvi.SubItems[0].Text.Trim();
                   
                    if (updatefile_name != str)//正在修改的文件名不符
                        throw new Exception("没有对选定的文档进行修改操作，无法放弃修改");
                    comm.Parameters.AddWithValue("@FilesName", str);
                }
                int num = comm.ExecuteNonQuery();
                if (num <= 0)
                    throw new Exception("数据库异常");
                updatefile_name = null;
                File.Delete(modify_path);
                modify_path = null;
                //显示菜单
                StartModifyFile.Enabled = true;
                GiveUpModifyFile.Enabled = false;
                CompleteModifyFile.Enabled = false;
                _sc.Sending(u_name + "用户已放弃修改文档" + str);
            }
            catch (Exception ex)
            {
                _sc.Sending(u_name + "用户放弃修改文档" + str + "失败");
                MessageBox.Show(ex.Message, "放弃修改失败！", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                conn.Close();
            }
        }

        private void UploadFile_Click(object sender, EventArgs e)//上传文件
        {
            if (DateTime.Compare(lastclicktime.AddSeconds(1), DateTime.Now) > 0) //限制点击频率
            {
                MessageBox.Show("操作过于频繁！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
                lastclicktime = DateTime.Now;
            countnum = 0;//清零
            if (updatefile_name != null && updatefile_name != "")
            {
                MessageBox.Show("还有文档未修改完成！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            SubjectChoice sc = new SubjectChoice(this);
            sc.Show();
            
            
        }

        private void FileView_MouseUp(object sender, MouseEventArgs e)
        {
            countnum = 0;//清零
            if (FileView.SelectedItems.Count != 1)//未选定信息
            {
                if (e.Button == MouseButtons.Right)//鼠标右键
                {
                    UploadMenu.Show(FileView, e.Location);//显示上传文件菜单
                }

            }
            else//选定一条信息
            {
                if (e.Button == MouseButtons.Right)//鼠标右键
                {
                    FileMenu.Show(FileView, e.Location);//显示文件属性菜单
                }
            }
        }

        private void VersionView_MouseUp(object sender, MouseEventArgs e)
        {
            countnum = 0;//清零
            if (VersionView.SelectedItems.Count == 1)//选定一条信息
            {
                if (e.Button == MouseButtons.Right)//鼠标右键
                {
                    VersionMenu.Show(VersionView, e.Location);//显示版本属性信息
                }

            }
            
        }

        private void ViewFileSummary_SmallIcon_Click(object sender, EventArgs e)
        {
            if (DateTime.Compare(lastclicktime.AddSeconds(1), DateTime.Now) > 0) //限制点击频率
            {
                MessageBox.Show("操作过于频繁！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
                lastclicktime = DateTime.Now;
            countnum = 0;//清零
            FileView.Clear();
            VersionView.Clear();
            FileView.View = View.SmallIcon;
            FileView.SmallImageList = DocImageList;
            FileView.MultiSelect = false;

            VersionView.View = View.SmallIcon;
            VersionView.SmallImageList = DocImageList;
            VersionView.MultiSelect = false;
            UpdateFileinfo();
        }

        private void ViewFileDetail_Click(object sender, EventArgs e)
        {
            if (DateTime.Compare(lastclicktime.AddSeconds(1), DateTime.Now) > 0) //限制点击频率
            {
                MessageBox.Show("操作过于频繁！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
                lastclicktime = DateTime.Now;
            countnum = 0;//清零
            FileView.Clear();
            VersionView.Clear();
            FileView.GridLines = true;//表格是否显示网格线
            FileView.FullRowSelect = true;//是否选中整行
            FileView.View = View.Details;//设置显示方式
            FileView.Scrollable = true;//是否自动显示滚动条
            FileView.MultiSelect = false;//是否可以选择多行

            VersionView.GridLines = true;//表格是否显示网格线
            VersionView.FullRowSelect = true;//是否选中整行
            VersionView.View = View.Details;//设置显示方式
            VersionView.Scrollable = true;//是否自动显示滚动条
            VersionView.MultiSelect = false;//是否可以选择多行
            UpdateFileinfo();
        }

        private void ModifyPwd_Click(object sender, EventArgs e)
        {
            if (DateTime.Compare(lastclicktime.AddSeconds(1), DateTime.Now) > 0) //限制点击频率
            {
                MessageBox.Show("操作过于频繁！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
                lastclicktime = DateTime.Now;
            countnum = 0;//清零
            if (updatefile_name != null && updatefile_name != "")
            {
                MessageBox.Show("还有文档未修改完成！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            ModifyPass mp = new ModifyPass(this);
            
        }

        private void LogOut_Click(object sender, EventArgs e)
        {
            if (DateTime.Compare(lastclicktime.AddSeconds(1), DateTime.Now) > 0) //限制点击频率
            {
                MessageBox.Show("操作过于频繁！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
                lastclicktime = DateTime.Now;
            if (updatefile_name != null && updatefile_name != "")
            {
                MessageBox.Show("还有文档未修改完成！", "无法退出登陆！", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            DialogResult result = MessageBox.Show("确定要退出登陆吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
            if (result == DialogResult.OK)
            {

                this.Close();
            }
            
        }

        private void Aboutme_Click(object sender, EventArgs e)
        {
            if (DateTime.Compare(lastclicktime.AddSeconds(1), DateTime.Now) > 0) //限制点击频率
            {
                MessageBox.Show("操作过于频繁！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
                lastclicktime = DateTime.Now;
            countnum = 0;//清零
            AboutMeForm am = new AboutMeForm(this);
            am.Show();
        }

        private void ViewFileSummary_LargeIcon_Click(object sender, EventArgs e)
        {
            if (DateTime.Compare(lastclicktime.AddSeconds(1), DateTime.Now) > 0) //限制点击频率
            {
                MessageBox.Show("操作过于频繁！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
                lastclicktime = DateTime.Now;
            countnum = 0;//清零
            FileView.Clear();
            VersionView.Clear();
            FileView.View = View.LargeIcon;
            FileView.LargeImageList = DocImageList;
            FileView.MultiSelect = false;

            VersionView.View = View.LargeIcon;
            VersionView.LargeImageList = DocImageList;
            VersionView.MultiSelect = false;
            UpdateFileinfo();
        }

        private void AccRollback_Click(object sender, EventArgs e)
        {
            if (DateTime.Compare(lastclicktime.AddSeconds(1), DateTime.Now) > 0) //限制点击频率
            {
                MessageBox.Show("操作过于频繁！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
                lastclicktime = DateTime.Now;
            countnum = 0;
            if (updatefile_name != null && updatefile_name != "")
            {
                MessageBox.Show("还有文档未修改完成！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            DialogResult result = MessageBox.Show("确定要还原账户吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
            if (result != DialogResult.OK)
                return;
            string connString = "Data Source =101.132.111.23; Initial Catalog = FileManage; User ID = userlogin; Pwd = User123456";
            SqlConnection conn = new SqlConnection(connString);
            string sql = "Update UserSafety Set UserStatus='1' where UserName=@UserName and UserStatus='2'";
            try
            {
                conn.Open();
                SqlCommand comm = new SqlCommand(sql, conn);
                
                comm.Parameters.AddWithValue("@UserName", u_name);

                int num = (int)comm.ExecuteNonQuery();
                if (num <= 0)
                    throw new Exception("用户账户状态异常！");
                _sc.Sending(u_name + "用户还原账户成功");
                MessageBox.Show("还原用户账户成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MessageBox.Show("还原账户后需要重新登陆！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                _sc.Sending(u_name + "用户还原账户失败");
                MessageBox.Show(ex.Message, "还原用户账户失败！", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                conn.Close();
                
            }
        }

        private void ModifyFileDlg_FileOk(object sender, CancelEventArgs e)
        {
            this.UseWaitCursor = true;//显示漏斗光标
            this.Refresh();
            countnum = 0;//清零
            string savingpath = ModifyFileDlg.FileName.ToString();//保存路径
            //查询服务器端的文件名
            string connString = "Data Source =101.132.111.23; Initial Catalog = FileManage; User ID = userlogin; Pwd = User123456";
            SqlConnection conn = new SqlConnection(connString);
            String sql = "select FileRemoteName from FileList,FileVersion where FileList.FilesName=@FilesName and FileList.FilesName=FileVersion.FilesName and LatestVersion=FVersion";
            string str = "";
            try
            {
                conn.Open();
                SqlCommand comm = new SqlCommand(sql, conn);
                foreach (ListViewItem lvi in FileView.SelectedItems)
                {
                    str = (FileView.View == View.SmallIcon || FileView.View == View.LargeIcon) ? lvi.Text : lvi.SubItems[0].Text.Trim();
                    comm.Parameters.AddWithValue("@FilesName", str);
                }
                string remotepath = (string)comm.ExecuteScalar();//获取服务器端文件名
                if (!Security_Test.IsSafetyFile(System.IO.Path.GetFileName(remotepath).Trim())) throw new Exception("禁止下载此文件！");
                conn.Close();
                //查看该文件的最新版本
                sql = "select LatestVersion from FileList where FilesName=@FilesName";
                conn.Open();
                SqlCommand comm2 = new SqlCommand(sql, conn);

                comm2.Parameters.AddWithValue("@FilesName", str);
                double lver = double.Parse(comm2.ExecuteScalar().ToString().Trim());
                lver = lver + 0.5;
                updatefile_version = lver.ToString("f1");//设置修改后的版本号
                conn.Close();
                //设置文件正在修改的标记
                sql = "update FileList set FileStatus='1' where FilesName=@FilesName";
                conn.Open();
                SqlCommand comm1 = new SqlCommand(sql, conn);
                comm1.Parameters.AddWithValue("@FilesName", str);
                int num = comm1.ExecuteNonQuery();
                if (num <= 0)
                    throw new Exception("数据库异常");
                updatefile_name = str;
                conn.Close();
                sql = "select FileSubject from FileList where FilesName=@FilesName";
                conn.Open();
                SqlCommand comm3 = new SqlCommand(sql, conn);
                comm3.Parameters.AddWithValue("@FilesName", str);
                string filesub = comm3.ExecuteScalar().ToString().Trim();
                //下载文件
                status_flag = 2;//下载文件标志
                r_path = filesub + "\\" + remotepath;
                l_path = savingpath;
                EnableUpdownProgress();
                Thread t = new Thread(Download_File);
                t.IsBackground = true;
                t.Start();
                
                StartModifyFile.Enabled = false;//一个文件修改完成之前禁止修改另一个文件
                GiveUpModifyFile.Enabled = true;
                CompleteModifyFile.Enabled = true;
                _sc.Sending(u_name + "用户修改文档" + str + "开始");
                modify_path = savingpath;//保存文件路径
                MessageBox.Show("修改文档过程中，请勿移动文档位置或进行文档删除、重命名等操作\r\n修改完成后，请先关闭文件，再点击修改完成按钮！\r\n如果要放弃修改，请先关闭文件，再点击放弃修改按钮！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception ex)
            {
                modify_path = null;
                _sc.Sending(u_name + "用户修改文档" + str + "失败");
                MessageBox.Show(ex.Message, "下载文档失败！", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                conn.Close();

            }
        }

        

        

        

        
    }
}
