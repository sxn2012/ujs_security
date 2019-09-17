using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.Sql;

namespace FileManagement_Admin
{
    public partial class ModifySName : Form
    {
        private string oldsubname = null;
        private MainForm mf = null;
        public ModifySName(string sn,MainForm m)
        {
            InitializeComponent();
            BackgroundImage = Image.FromFile("main.jpg");
            oldsubname = sn;
            mf = m;
        }

        private void admit_btn_Click(object sender, EventArgs e)
        {
            string connString = "Data Source =.; Initial Catalog = FileManage; Integrated Security = True";
            SqlConnection conn = new SqlConnection(connString);
            string sql = "update SubjectList set SubName=@NewSubName where SubName=@SubName";
            string str = oldsubname;
            string nsname = Security_Test.SafeSQL(subname.Text.Trim());
            try
            {
                if (nsname == null || nsname == "") throw new Exception("主题名不能为空！");
                DialogResult result = MessageBox.Show("确定要修改主题名吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                if (result != DialogResult.OK)
                    return;
                conn.Open();
                SqlCommand comm = new SqlCommand(sql, conn);


                comm.Parameters.AddWithValue("@NewSubName", nsname);
                comm.Parameters.AddWithValue("@SubName", str);


                int num = (int)comm.ExecuteNonQuery();
                if (num <= 0)
                {
                    conn.Close();
                    throw new Exception("数据库异常");
                }
                if (Directory.Exists("C:\\ftp\\" + oldsubname))
                {
                    Directory.Move("C:\\ftp\\" + oldsubname, "C:\\ftp\\" + nsname);
                }
                
                MessageBox.Show("修改主题名成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "修改主题名失败！", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                subname.Clear();
                return;
            }
            finally
            {
                conn.Close();
                mf.UpdateData();
            }
            this.Close();
        }
    }
}
