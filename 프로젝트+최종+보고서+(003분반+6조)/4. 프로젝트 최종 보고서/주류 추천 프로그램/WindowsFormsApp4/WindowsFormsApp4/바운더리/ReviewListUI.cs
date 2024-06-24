using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp4
{
    public partial class ReviewListUI : Form
    {
        ReviewSystem RS = new ReviewSystem();
        public ReviewListUI()
        {
            InitializeComponent();
        }
        static public string add = "";
        static public string id = MainUI.rid;
        private void ReviewListUI_Load(object sender, EventArgs e)
        {
            RS.MyReviewLoad(this);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listView2.SelectedItems.Count == 1)
            {
                System.Windows.Forms.ListView.SelectedListViewItemCollection items = listView2.SelectedItems;
                ListViewItem lvItem = items[0];
                add = lvItem.SubItems[0].Text;
                UpdateReviewUI CRT = new UpdateReviewUI();
                CRT.StartPosition = FormStartPosition.Manual;
                CRT.Location = new Point(525, 150);
                CRT.Show();
            }
            else if (listView2.SelectedItems.Count == 0)
            {
                MessageBox.Show("항목을 선택하세요 !");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RS.DelReview(this);
        }
    }
}
