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
    public partial class FindPassWord : Form
    {
        public FindPassWord()
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

        private void btn_admit_Click(object sender, EventArgs e)
        {
            string connString = "Data Source =.; Initial Catalog = Magazine_Ordering; Integrated Security = True";
            SqlConnection conn = new SqlConnection(connString);
            string lname = usr.Text.Trim();
            string lpass = pass.Text.Trim();
            string lloc = loc.Text.Trim();
            String sql = String.Format("select count(*) from Userlist where username='{0}' and access='3' and location='{1}' ", lname,lloc);
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
                if (num <= 0)
                {
                    
                    throw new Exception("未找到该用户，请检查输入！");
                }

                else
                {
                    conn.Close();
                    sql = String.Format("update Userlist set  password='{1}'  where username='{0}' and access='3' ", lname, lpass);
                    conn.Open();
                    comm = new SqlCommand(sql, conn);
                    num = 0;
                    num = comm.ExecuteNonQuery();
                    if (num > 0)
                    {
                        MessageBox.Show("重置密码成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    else
                        throw new Exception("管理员账户无法重置密码!");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "重置密码失败！", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                conn.Close();
                
            }
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
