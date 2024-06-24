using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace WindowsFormsApp4
{
    public partial class MyAccountUI : Form
    {
        ModifyAccountSystem MA = new ModifyAccountSystem();
        public MyAccountUI()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MA.SaveAccount(this);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MyAccountUI_Load(object sender, EventArgs e)
        {
            MA.LoadAccount(this);
        }
    }
}
