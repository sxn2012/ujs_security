using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileManagement_Admin
{
    public partial class AboutMeForm : Form
    {
        public AboutMeForm()
        {
            InitializeComponent();
            BackgroundImage = Image.FromFile("main.jpg");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
