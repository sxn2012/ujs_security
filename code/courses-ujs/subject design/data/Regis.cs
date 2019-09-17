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
namespace MagazineOrdering
{
    public partial class Regis : Form
    {
        public Regis()
        {
            InitializeComponent();
        }

        protected override void WndProc(ref Message msg)
        {

            const int WM_SYSCOMMAND = 0x0112;

            const int SC_CLOSE = 0xF060;

            if (msg.Msg == WM_SYSCOMMAND && ((int)msg.WParam == SC_CLOSE))
            {
                // 点击winform右上关闭按钮 

                this.Close();


            }
            base.WndProc(ref msg);
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_admit_Click(object sender, EventArgs e)
        {
            string connString = "Data Source =.; Initial Catalog = Magazine_Ordering; Integrated Security = True";
            SqlConnection conn = new SqlConnection(connString);
            string lname = usr.Text.Trim();
            string lpass = pwd.Text.Trim();
            string lloc = loc.Text.Trim();
            String sql = String.Format("select count(*) from Userlist where username='{0}' ", lname);
            try
            {
                if (lname == null || lname == "")
                    throw new Exception("用户名不能为空！");
                if (lpass == null || lpass == "")
                    throw new Exception("密码不能为空！");
                if (lloc == null || lloc == "")
                    throw new Exception("地址不能为空！");
                conn.Open();
                SqlCommand comm = new SqlCommand(sql, conn);
                int num = (int)comm.ExecuteScalar();
                if (num > 0)
                {
                    //MessageBox.Show("该用户已存在");
                    throw new Exception("该用户已存在");
                }
                    
                else
                {
                    conn.Close();
                    sql = String.Format("insert into userlist(username,password,access,location) values('{0}','{1}',3,'{2}')", lname, lpass,lloc);
                    conn.Open();
                    comm = new SqlCommand(sql, conn);
                    num = 0;
                    num = comm.ExecuteNonQuery();
                    if (num > 0)
                        MessageBox.Show("注册成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        throw new Exception("数据库异常!");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "注册失败！", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                conn.Close();
                this.Close();
            }
        }
    }
}
