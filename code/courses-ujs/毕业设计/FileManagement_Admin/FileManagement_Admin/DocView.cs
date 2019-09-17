using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.Sql;

namespace FileManagement_Admin
{
    public partial class DocView : Form
    {
        public DocView()
        {
            InitializeComponent();
            BackgroundImage = Image.FromFile("main.jpg");
            FileView.View = View.SmallIcon;
            FileView.SmallImageList = DocImageList;
            FileView.MultiSelect = false;

            VersionView.View = View.SmallIcon;
            VersionView.SmallImageList = DocImageList;
            VersionView.MultiSelect = false;
            UpdateFileinfo();
        }

        public void UpdateFileinfo()//更新文件信息
        {
            //清空
            FileView.Clear();

            string connString = "Data Source =.; Initial Catalog = FileManage; Integrated Security = True";
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
                    FileView.Columns.Add("文件名", 100, HorizontalAlignment.Center);
                    FileView.Columns.Add("文件主题", 100, HorizontalAlignment.Center);
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

        private void UpdateVersionInfo(string version)//更新文件版本信息
        {
            VersionView.Clear();
            string str = version;
            string connString = "Data Source =.; Initial Catalog = FileManage; Integrated Security = True";
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
                        string[] strtemp = stext.Split('.');
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
                    VersionView.Columns.Add("文件名", 100, HorizontalAlignment.Center);
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

        private void FileView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (FileView.SelectedItems.Count != 1)
            {

                return;
            }
            foreach (ListViewItem lvi in FileView.SelectedItems)
            {
                string str = (FileView.View == View.SmallIcon) ? lvi.Text : lvi.SubItems[0].Text.Trim();
                UpdateVersionInfo(str);
            }
        }

        private void ViewFileSummary_SmallIcon_Click(object sender, EventArgs e)
        {
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

        private void ViewFileSummary_LargeIcon_Click(object sender, EventArgs e)
        {
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
    }
}
