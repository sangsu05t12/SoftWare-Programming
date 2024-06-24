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
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;

namespace WindowsFormsApp4
{
    public partial class ReviewSearchUI : Form
    {
        SearchReviewSystem SR = new SearchReviewSystem();

        public ReviewSearchUI()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ReviewListUI reviewListUI = new ReviewListUI();
            reviewListUI.StartPosition = FormStartPosition.Manual;
            reviewListUI.Location = new Point(520, 150);
            reviewListUI.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            CreateReviewUI createReviewUI = new CreateReviewUI();
            createReviewUI.StartPosition = FormStartPosition.Manual;
            createReviewUI.Location = new Point(525, 150);
            createReviewUI.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SR.SearchReview(this);
        }

        private void ReviewSearchUI_Load(object sender, EventArgs e)
        {
            SR.ViewReview(this);
        }
    }
}
