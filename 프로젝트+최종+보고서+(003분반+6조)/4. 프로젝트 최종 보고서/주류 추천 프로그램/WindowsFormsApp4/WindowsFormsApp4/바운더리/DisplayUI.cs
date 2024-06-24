using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using Oracle.ManagedDataAccess.Client;

namespace WindowsFormsApp4
{
    public partial class DisplayUI : Form
    {
        public DisplayUI()
        {
            InitializeComponent();
        }
        private void DisplayUI_Load(object sender, EventArgs e)
        {
            ViewLikeSystem VL = new ViewLikeSystem();
            VL.ViewLike(this);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pictureBox1.Controls.Clear();
            DisplayUI_Load(sender,e);
        }
    }
}
