using Oracle.ManagedDataAccess.Client;
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
using System.Collections;
using static System.Net.Mime.MediaTypeNames;

namespace WindowsFormsApp4
{
    public partial class CombineUI : Form
    {
        CombineSystem CS = new CombineSystem();
        public CombineUI()
        {
            InitializeComponent();
        }
        private void CombineUI_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            CS.LoadCombine(this);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CS.SearchCombine(this);
        }
    }
}