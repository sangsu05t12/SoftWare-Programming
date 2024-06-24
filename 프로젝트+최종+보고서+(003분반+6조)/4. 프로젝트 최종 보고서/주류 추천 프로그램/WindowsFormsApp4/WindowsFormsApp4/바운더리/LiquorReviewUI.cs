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
    public partial class LiquorReviewUI : Form
    {
        ReviewDB RD = new ReviewDB();
        public LiquorReviewUI()
        {
            InitializeComponent();
        }
        public LiquorReviewUI(string text)
        {
            InitializeComponent();
            RD.review_Name = text;
        }

        private void LiquorReviewUI_Load(object sender, EventArgs e)
        {
            ReviewSystem RS = new ReviewSystem();
            RS.showreviewlist(this, RD.review_Name);
        }
    }
}
