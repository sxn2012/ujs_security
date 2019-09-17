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
    public partial class ModifyUPass : Form
    {
        private string username = null;
        public ModifyUPass(string uname)
        {
            InitializeComponent();
            BackgroundImage = Image.FromFile("main.jpg");
            username = uname;
        }

        private void admit_btn_Click(object sender, EventArgs e)
        {
            string connString = "Data Source =.; Initial Catalog = FileManage; Integrated Security = True";
            SqlConnection conn = new SqlConnection(connString);
            string sql = "update UserList set UserPass=@UserPass where UserName=@UserName";
            
            try
            {
                string str = username;
                string pwd = Security_Test.SafeSQL(passwd.Text.Trim());
                if (!Security_Test.IsSecurePWD(pwd)) throw new Exception("密码不符合要求！密码应为6-30位的字母和数字");
                pwd = CryptoClass.SHA256String(pwd);
                string pwdadmit = Security_Test.SafeSQL(passwdadmit.Text.Trim());
                pwdadmit = CryptoClass.SHA256String(pwdadmit);
                if (pwd == null || pwd == "") throw new Exception("密码不能为空！");
                if (pwd != pwdadmit) throw new Exception("两次密码输入不一致！");
                DialogResult result = MessageBox.Show("确定要重置密码吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                if (result != DialogResult.OK)
                    return;
                conn.Open();
                SqlCommand comm = new SqlCommand(sql, conn);
                comm.Parameters.AddWithValue("@UserPass", pwd);
                comm.Parameters.AddWithValue("@UserName", str);


                int num = (int)comm.ExecuteNonQuery();
                if (num <= 0)
                {
                    conn.Close();
                    throw new Exception("数据库异常");
                }
                MessageBox.Show("重置密码成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "重置密码失败！", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
