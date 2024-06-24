using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp4
{
    public partial class CreateReviewUI : Form
    {
        ReviewSystem RS = new ReviewSystem();
        public CreateReviewUI()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
                MessageBox.Show("주류명을 입력해주세요.");
            else if (textBox3.Text == "")
                MessageBox.Show("내용을 입력해주세요.");
            else
            {
                RS.CreateReview(this);
            }
        }

        private void CreateReviewUI_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 4;
        }
    }
}


