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
    public partial class CreateSub : Form
    {
        private MainForm mf = null;
        public CreateSub(MainForm m)
        {
            InitializeComponent();
            BackgroundImage = Image.FromFile("main.jpg");
            mf = m;
        }

        private void admit_btn_Click(object sender, EventArgs e)
        {
            string connString = "Data Source =.; Initial Catalog = FileManage; Integrated Security = True";
            SqlConnection conn = new SqlConnection(connString);
            string sql = "insert into SubjectList(SubName,SubContent) values(@SubName,@SubContent)";
            string sname = Security_Test.SafeSQL(subname.Text.Trim());
            string scon = Security_Test.SafeSQL(sub_con.Text.Trim());
            scon = CryptoClass.AesEncrypt(scon, CryptoClass.key);
            try
            {
                if (sname == null || sname == "") throw new Exception("主题名不能为空！");
                if (scon == null || scon == "") throw new Exception("主题内容不能为空！");
                DialogResult result = MessageBox.Show("确定要创建新主题吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                if (result != DialogResult.OK)
                    return;
                conn.Open();
                SqlCommand comm = new SqlCommand(sql, conn);
                comm.Parameters.AddWithValue("@SubName", sname);
                comm.Parameters.AddWithValue("@SubContent", scon);

                int num = (int)comm.ExecuteNonQuery();
                if (num <= 0)
                {
                    conn.Close();
                    throw new Exception("数据库异常");
                }
                MessageBox.Show("创建主题成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                mf.UpdateData();
                if (!Directory.Exists("C:\\ftp\\" + sname))
                {
                    Directory.CreateDirectory("C:\\ftp\\" + sname);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "创建主题失败！", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                subname.Clear();
                sub_con.Clear();


                return;
            }
            finally
            {

                conn.Close();

            }
            this.Close();
        }
    }
}
