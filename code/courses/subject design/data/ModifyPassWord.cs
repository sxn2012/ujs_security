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
    public partial class ModifyPassWord : Form
    {
        Admin add_ = null;
        public ModifyPassWord(Admin ad_form)
        {
            add_ = ad_form;
            InitializeComponent();
        }
        private void ModifyPassWord_Closing(object sender, FormClosingEventArgs e)//关闭窗口消息
        {
            
        }

        private void btn_admit_Click(object sender, EventArgs e)
        {
            string connString = "Data Source =.; Initial Catalog = Magazine_Ordering; Integrated Security = True";
            SqlConnection conn = new SqlConnection(connString);
            string lname = usr.Text.Trim();
            string lpass = pass.Text.Trim();
            string mpass = newpass.Text.Trim();
            String sql = String.Format("select count(*) from Userlist where username='{0}' and access='0' and password='{1}' ", lname, lpass);
            try
            {
                if (lname == null || lname == "")
                    throw new Exception("用户名不能为空！");
                if (lpass == null || lpass == "")
                    throw new Exception("原密码不能为空！");
                if (mpass == null || mpass == "")
                    throw new Exception("密码不能为空！");
                conn.Open();
                SqlCommand comm = new SqlCommand(sql, conn);
                int num = (int)comm.ExecuteScalar();
                if (num <= 0)
                {

                    throw new Exception("用户名或原密码错误！");
                }

                else
                {
                    conn.Close();
                    sql = String.Format("update Userlist set  password='{1}'  where username='{0}'", lname, mpass);
                    conn.Open();
                    comm = new SqlCommand(sql, conn);
                    num = 0;
                    num = comm.ExecuteNonQuery();
                    if (num > 0)
                    {
                        MessageBox.Show("修改密码成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        MessageBox.Show("修改密码后必须重新登录！", "注意", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        
                        this.Close();
                        add_.Close();
                    }
                    else
                        throw new Exception("数据库异常!");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "修改密码失败！", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
