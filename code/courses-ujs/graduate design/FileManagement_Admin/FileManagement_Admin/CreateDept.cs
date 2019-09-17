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
    public partial class CreateDept : Form
    {
        private MainForm mf = null;
        public CreateDept(MainForm m)
        {
            InitializeComponent();
            BackgroundImage = Image.FromFile("main.jpg");
            mf = m;
        }

        private void admit_btn_Click(object sender, EventArgs e)
        {
            string connString = "Data Source =.; Initial Catalog = FileManage; Integrated Security = True";
            SqlConnection conn = new SqlConnection(connString);
            string sql = "insert into Department(DepName,DepLoc) values(@DepName,@DepLoc)";
            string dname = Security_Test.SafeSQL(depname.Text.Trim());
            string dloc = Security_Test.SafeSQL(dept_loc.Text.Trim());
            dloc = CryptoClass.AesEncrypt(dloc, CryptoClass.key);
            try
            {
                if (dname == null || dname == "") throw new Exception("部门名不能为空！");
                if (dloc == null || dloc == "") throw new Exception("部门地址不能为空！");
                DialogResult result = MessageBox.Show("确定要创建新部门吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                if (result != DialogResult.OK)
                    return;
                conn.Open();
                SqlCommand comm = new SqlCommand(sql, conn);
                comm.Parameters.AddWithValue("@DepName", dname);
                comm.Parameters.AddWithValue("@DepLoc", dloc);
                
                int num = (int)comm.ExecuteNonQuery();
                if (num <= 0)
                {
                    conn.Close();
                    throw new Exception("数据库异常");
                }
                MessageBox.Show("创建部门成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                mf.UpdateData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "创建部门失败！", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                depname.Clear();
                dept_loc.Clear();
                

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
