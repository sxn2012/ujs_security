using System;
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

namespace FileManagement
{
    public partial class SubjectChoice : Form
    {
        private MainForm mf = null;
        private string choosingsub = null;
        public SubjectChoice(MainForm m)
        {
            InitializeComponent();
            BackgroundImage = Image.FromFile("main.jpg");
            mf = m;
            choosingsub = null;
            //文件主题初始化
            string connString = "Data Source =101.132.111.23; Initial Catalog = FileManage; User ID = userlogin; Pwd = User123456";
            SqlConnection conn = new SqlConnection(connString);
            String sql = "select SubName,SubContent from SubjectList";
            try
            {
                SubjectChoose.Items.Clear();
                conn.Open();
                SqlCommand comm = new SqlCommand(sql, conn);
                SqlDataReader reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    string str = reader[0].ToString().Trim();
                    string temp = CryptoClass.AesDecrypt(reader[1].ToString().Trim(),CryptoClass.key);
                    SubjectChoose.Items.Add(str+"-内容："+temp);
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

        private void btn_admit_Click(object sender, EventArgs e)
        {
            
            mf.countnum = 0;//清零
            if (SubjectChoose.SelectedIndex < 0)//没有选择文件主题
            {
                MessageBox.Show("请选择文档的主题", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            choosingsub = SubjectChoose.SelectedItem.ToString().Trim().Split('-')[0].Trim();//选定文件的主题
            //查看用户对该主题的文件是否有管理权限
            string connString = "Data Source =101.132.111.23; Initial Catalog = FileManage; User ID = userlogin; Pwd = User123456";
            SqlConnection conn = new SqlConnection(connString);
            String sql = "select count(*) from AccessList where DepName=@DepName and SubName=@SubName and privilege='2'";
            try
            {
                conn.Open();
                SqlCommand comm = new SqlCommand(sql, conn);
                comm.Parameters.AddWithValue("@DepName", mf.u_dep);
                comm.Parameters.AddWithValue("@SubName", choosingsub);
                int num = (int)comm.ExecuteScalar();
                if (num != 1 )//没有权限
                {
                    throw new Exception("用户没有上传文档所需的权限！");
                }
                
            }
            catch (Exception ex)
            {
                mf._sc.Sending(mf.u_name + "用户上传文档失败");
                MessageBox.Show(ex.Message, "上传文档失败！", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
                return;
            }
            finally
            {
                conn.Close();
            }


            OpenFileDlg.Title = "上传文档";
            OpenFileDlg.Filter = "文本文件(*.txt)|*.txt|所有文件(*.*)|*.*";
            OpenFileDlg.FilterIndex = 1;
            OpenFileDlg.FileName = "";
            OpenFileDlg.ShowDialog();//弹出框让用户选择上传的文件
            this.Close();
        }

        private void OpenFileDlg_FileOk(object sender, CancelEventArgs e)//上传文件
        {
            
            mf.countnum = 0;//清零
            string filepath = OpenFileDlg.FileName.Trim();//文件路径
            string filename = System.IO.Path.GetFileNameWithoutExtension(OpenFileDlg.FileName).Trim();
            string fileext = System.IO.Path.GetExtension(OpenFileDlg.FileName).Trim();
            string fileversion = "1_0";
            string filername = filename + "_" + fileversion + fileext;//文件在服务器端的命名
            string filesub = choosingsub;
            string filefullname = System.IO.Path.GetFileName(OpenFileDlg.FileName).Trim();
            string connString = "Data Source =101.132.111.23; Initial Catalog = FileManage; User ID = userlogin; Pwd = User123456";
            SqlConnection conn = new SqlConnection(connString);
            try
            {
                if (!Security_Test.IsSafetyFile(System.IO.Path.GetFileName(filepath).Trim())) throw new Exception("禁止上传此文件！");
                mf.EnableUpdownProgress();
                //上传文件
                mf.status_flag = 1;//上传文件标志
                mf.l_path = filepath;
                mf.r_path = choosingsub + "\\" + filername;
                Thread t = new Thread(mf.Upload_File);
                t.IsBackground = true;
                t.Start();
                //将文件信息插入文件信息表
                string sql = "insert into FileList(FilesName,FileSubject,LatestVersion,FileStatus) values(@FilesName,@FileSubject,'1.0','0')";
                conn.Open();
                SqlCommand comm = new SqlCommand(sql, conn);
                comm.Parameters.AddWithValue("@FilesName", filefullname);
                comm.Parameters.AddWithValue("@FileSubject", filesub);

                int num = comm.ExecuteNonQuery();
                if (num <= 0)
                    throw new Exception("数据库异常");
                conn.Close();
                //将文件版本信息插入文件版本信息表
                sql = "insert into FileVersion(FilesName,FVersion,FileRemoteName,Modifytime) values(@FilesName,'1.0',@FileRemoteName,@Modifytime)";
                conn.Open();
                SqlCommand comm1 = new SqlCommand(sql, conn);
                comm1.Parameters.AddWithValue("@FilesName", filefullname);
                comm1.Parameters.AddWithValue("@FileRemoteName", filername);
                comm1.Parameters.AddWithValue("@Modifytime", DateTime.Now.ToString());
                num = comm1.ExecuteNonQuery();
                if (num <= 0)
                    throw new Exception("数据库异常");
                mf._sc.Sending(mf.u_name + "用户上传文档成功");
                

            }
            catch (Exception ex)
            {
                mf._sc.Sending(mf.u_name + "用户上传文档失败");
                MessageBox.Show(ex.Message, "上传文档失败！", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                conn.Close();
                
                
            }
        }

    }
}
