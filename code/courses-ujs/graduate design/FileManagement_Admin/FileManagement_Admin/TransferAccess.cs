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
    public partial class TransferAccess : Form
    {
        private string dname = null;
        private string sname = null;
        public TransferAccess(string dn, string sn)
        {
            InitializeComponent();
            BackgroundImage = Image.FromFile("main.jpg");
            dname = dn;
            sname = sn;
            label4.Text = sname.Trim();
            label5.Text = "管理";
            string connString = "Data Source =.; Initial Catalog = FileManage; Integrated Security = True";
            SqlConnection conn = new SqlConnection(connString);
            String sql = "select DepName from Department";
            try
            {
                deptname.Items.Clear();
                conn.Open();
                SqlCommand comm = new SqlCommand(sql, conn);
                SqlDataReader reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    deptname.Items.Add(reader[0].ToString().Trim());
                }
                conn.Close();
               

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

        private void admit_btn_Click(object sender, EventArgs e)
        {
            string connString = "Data Source =.; Initial Catalog = FileManage; Integrated Security = True";
            SqlConnection conn = new SqlConnection(connString);
            string sql = "update AccessList set DepName=@newDepName where DepName=@oldDepName and SubName=@SubName and privilege='2'";

            
            string dept = deptname.SelectedItem.ToString().Trim();
            
            
            try
            {
                if (dept == null || dept == "") throw new Exception("请选择一个部门！");
                
                DialogResult result = MessageBox.Show("确定要转移管理权吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                if (result != DialogResult.OK)
                    return;
                conn.Open();
                SqlCommand comm = new SqlCommand(sql, conn);
                comm.Parameters.AddWithValue("@newDepName", dept);
                comm.Parameters.AddWithValue("@SubName", sname);
                comm.Parameters.AddWithValue("@oldDepName", dname);
                int num = (int)comm.ExecuteNonQuery();
                if (num <= 0)
                {

                    throw new Exception("数据库异常");
                }
                
                MessageBox.Show("管理权转移成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "管理权转移失败！", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

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
