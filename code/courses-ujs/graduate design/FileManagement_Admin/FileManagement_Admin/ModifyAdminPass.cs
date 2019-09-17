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
    public partial class ModifyAdminPass : Form
    {
        private MainForm mf = null;
        public ModifyAdminPass(MainForm m)
        {
            InitializeComponent();
            BackgroundImage = Image.FromFile("main.jpg");
            mf = m;
        }

        private void admit_btn_Click(object sender, EventArgs e)
        {
            string lold = Security_Test.SafeSQL(origin.Text.Trim());
            lold = CryptoClass.SHA256String(lold);
            string connString = "Data Source =.; Initial Catalog = FileManage; Integrated Security = True";
            SqlConnection conn = new SqlConnection(connString);
            string sql = "update UserList set UserPass=@UserPass where UserName='admin' and UserPass=@oldPass";
            
            try
            {
                string pwd = Security_Test.SafeSQL(passwd.Text.Trim());
                if (!Security_Test.IsSecurePWD(pwd)) throw new Exception("密码不符合要求！密码应为6-30位的字母和数字");
                pwd = CryptoClass.SHA256String(pwd);
                string pwdadmit = Security_Test.SafeSQL(passwdadmit.Text.Trim());
                pwdadmit = CryptoClass.SHA256String(pwdadmit);
                if (pwd == null || pwd == "") throw new Exception("密码不能为空！");
                if (pwd != pwdadmit) throw new Exception("两次密码输入不一致！");
                if (pwd == lold) throw new Exception("新密码不能与原密码相同！");
                DialogResult result = MessageBox.Show("确定要修改管理员密码吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                if (result != DialogResult.OK)
                    return;
                conn.Open();
                SqlCommand comm = new SqlCommand(sql, conn);
                comm.Parameters.AddWithValue("@UserPass", pwd);
                comm.Parameters.AddWithValue("@oldPass", lold);


                int num = (int)comm.ExecuteNonQuery();
                if (num <= 0)
                {
                    conn.Close();
                    throw new Exception("原密码错误！");
                }
                MessageBox.Show("修改管理员密码成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MessageBox.Show("修改管理员密码后需要重新登陆！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                mf.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "修改管理员密码失败！", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                origin.Clear();
                passwd.Clear();
                passwdadmit.Clear();
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
