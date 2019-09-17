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
    public partial class ModifyDName : Form
    {
        private string olddepname = null;
        private MainForm mf = null;
        public ModifyDName(string dn,MainForm m)
        {
            InitializeComponent();
            BackgroundImage = Image.FromFile("main.jpg");
            olddepname = dn;
            mf = m;
        }

        private void admit_btn_Click(object sender, EventArgs e)
        {
            string connString = "Data Source =.; Initial Catalog = FileManage; Integrated Security = True";
            SqlConnection conn = new SqlConnection(connString);
            string sql = "update Department set DepName=@NewDepName where DepName=@DepName";
            string str = olddepname;
            string ndname = Security_Test.SafeSQL(depname.Text.Trim());
            try
            {
                if (ndname == null || ndname == "") throw new Exception("部门名不能为空！");
                DialogResult result = MessageBox.Show("确定要修改部门名吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                if (result != DialogResult.OK)
                    return;
                conn.Open();
                SqlCommand comm = new SqlCommand(sql, conn);


                comm.Parameters.AddWithValue("@NewDepName", ndname);
                comm.Parameters.AddWithValue("@DepName", str);


                int num = (int)comm.ExecuteNonQuery();
                if (num <= 0)
                {
                    conn.Close();
                    throw new Exception("数据库异常");
                }
                MessageBox.Show("修改部门名成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "修改部门名失败！", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                depname.Clear();
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
