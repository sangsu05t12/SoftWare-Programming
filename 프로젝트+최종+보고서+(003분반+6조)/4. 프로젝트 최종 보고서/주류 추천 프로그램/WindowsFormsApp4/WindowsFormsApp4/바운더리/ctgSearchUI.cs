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
    public partial class ctgSearchUI : Form
    {
        LiquorsDB LQ = new LiquorsDB();
        public ctgSearchUI(string ctg)
        {
            InitializeComponent();
            LQ.liquors_Ctg = ctg;
        }

        private void ctgSearchUI_Load(object sender, EventArgs e)
        {
            CtgSearchSystem CTG = new CtgSearchSystem();
            CTG.CtgSearch(this, LQ.liquors_Ctg);
        }
    
    }
}
