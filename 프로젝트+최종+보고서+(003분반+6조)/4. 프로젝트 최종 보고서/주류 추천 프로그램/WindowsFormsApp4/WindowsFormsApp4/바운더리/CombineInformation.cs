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

namespace WindowsFormsApp4
{
    public partial class CombineInformation : Form
    {
        CombineDB CD = new CombineDB();
        public CombineInformation(string name)
        {
            InitializeComponent();
            CD.combine_Name = name;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CombineInformation_Load(object sender, EventArgs e)
        {
           IngrCombine Ing = new IngrCombine();
            Ing.ViewIngr(this, CD.combine_Name);
        }
    }
}
