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
using System.Linq.Expressions;
using static System.Net.Mime.MediaTypeNames;

namespace WindowsFormsApp4
{
    public partial class SearchUI : Form
    {
        SearchSystem SS = new SearchSystem();
        public SearchUI()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SS.Searchsystem(this);
        }

        private void SearchUI_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            SS.SearchLoad(this);
        }
    }
}
