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
    public partial class ModifyDLoc : Form
    {
        private string depname = null;
        private MainForm mf = null;
        public ModifyDLoc(string dn,MainForm m)
        {
            InitializeComponent();
            BackgroundImage = Image.FromFile("main.jpg");
            depname = dn;
            mf = m;
        }

        private void admit_btn_Click(object sender, EventArgs e)
        {
            string connString = "Data Source =.; Initial Catalog = FileManage; Integrated Security = True";
            SqlConnection conn = new SqlConnection(connString);
            string sql = "update Department set DepLoc=@DepLoc where DepName=@DepName";
            string str = depname;
            string loc = Security_Test.SafeSQL(dept_loc.Text.Trim());
            loc = CryptoClass.AesEncrypt(loc, CryptoClass.key);
            try
            {
                if (loc == null || loc == "") throw new Exception("部门地址不能为空！");
                DialogResult result = MessageBox.Show("确定要修改部门地址吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                if (result != DialogResult.OK)
                    return;
                conn.Open();
                SqlCommand comm = new SqlCommand(sql, conn);


                comm.Parameters.AddWithValue("@DepLoc", loc);
                comm.Parameters.AddWithValue("@DepName", str);


                int num = (int)comm.ExecuteNonQuery();
                if (num <= 0)
                {
                    conn.Close();
                    throw new Exception("数据库异常");
                }
                MessageBox.Show("修改部门地址成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "修改部门地址失败！", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dept_loc.Clear();
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
