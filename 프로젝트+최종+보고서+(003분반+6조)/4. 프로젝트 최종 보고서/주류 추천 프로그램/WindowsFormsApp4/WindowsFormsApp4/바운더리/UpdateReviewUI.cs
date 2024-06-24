using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp4
{
    public partial class UpdateReviewUI : Form
    {
        ReviewSystem RS = new ReviewSystem();
        public UpdateReviewUI()
        {
            InitializeComponent();
        }

        private void UpdateReviewUI_Load(object sender, EventArgs e)
        {
            RS.UpdateLoad(this);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RS.UpdateReview(this);
        }
    }
}
