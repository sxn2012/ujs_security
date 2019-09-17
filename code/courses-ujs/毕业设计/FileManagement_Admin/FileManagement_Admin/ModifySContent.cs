using System;
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
    public partial class ModifySContent : Form
    {
        private string subname = null;
        private MainForm mf = null;
        public ModifySContent(string sn,MainForm m)
        {
            InitializeComponent();
            BackgroundImage = Image.FromFile("main.jpg");
            subname = sn;
            mf = m;
        }

        private void admit_btn_Click(object sender, EventArgs e)
        {
            string connString = "Data Source =.; Initial Catalog = FileManage; Integrated Security = True";
            SqlConnection conn = new SqlConnection(connString);
            string sql = "update SubjectList set SubContent=@SubContent where SubName=@SubName";
            string str = subname;
            string scon = Security_Test.SafeSQL(sub_con.Text.Trim());
            scon = CryptoClass.AesEncrypt(scon, CryptoClass.key);
            try
            {
                if (scon == null || scon == "") throw new Exception("主题内容不能为空！");
                DialogResult result = MessageBox.Show("确定要修改主题内容吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                if (result != DialogResult.OK)
                    return;
                conn.Open();
                SqlCommand comm = new SqlCommand(sql, conn);


                comm.Parameters.AddWithValue("@SubContent", scon);
                comm.Parameters.AddWithValue("@SubName", str);


                int num = (int)comm.ExecuteNonQuery();
                if (num <= 0)
                {
                    conn.Close();
                    throw new Exception("数据库异常");
                }
                MessageBox.Show("修改主题内容成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "修改主题内容失败！", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                sub_con.Clear();
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
