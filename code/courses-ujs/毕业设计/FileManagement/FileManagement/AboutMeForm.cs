using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileManagement
{
    public partial class AboutMeForm : Form
    {
        private MainForm mf = null;
        public AboutMeForm(MainForm m)
        {
            InitializeComponent();
            BackgroundImage = Image.FromFile("main.jpg");
            mf = m;
        }

        private void btn_admit_Click(object sender, EventArgs e)
        {
            mf.countnum = 0;//清零
            this.Close();
        }
    }
}
