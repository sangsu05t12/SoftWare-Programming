using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace WindowsFormsApp4
{
    public partial class TinformationUI : Form
    {
        TLiquorDB TQ = new TLiquorDB();
        public TinformationUI(string text)
        {
            InitializeComponent();
            TQ.tliquors_Name = text;
        }

        private void TinformationUI_Load(object sender, EventArgs e)
        {
            ViewTInformationSystem VT = new ViewTInformationSystem();
            VT.ViewTInformation(this, TQ.tliquors_Name);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
