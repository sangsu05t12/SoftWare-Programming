using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp4
{
    public partial class InformationUI : Form
    {
        LiquorsDB LQ = new LiquorsDB();
        UserDB user = new UserDB();
        public InformationUI()
        {
            InitializeComponent();
        }
        public InformationUI(string data)
        {
            InitializeComponent();
            LQ.liquors_Name = data;
        }

        private void InformationUI_Load(object sender, EventArgs e)
        {
            ViewInformationSystem VS = new ViewInformationSystem();
            VS.ViewInformation(this, LQ.liquors_Name);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                checkBox1.BackgroundImage = Properties.Resources.CHECK;
            }
            else
            {
                checkBox1.BackgroundImage = Properties.Resources.UNCHECK;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LiquorReviewUI LR = new LiquorReviewUI(LQ.liquors_Name);
            LR.StartPosition = FormStartPosition.Manual;
            LR.Location = new Point(520, 150);
            LR.Show();
        }

        private void checkBox1_Click(object sender, EventArgs e)
        {
            LikeSystem LS = new LikeSystem();
            user.user_ID = MainUI.rid;
            if (LS.CheckLike(user.user_ID) == true)
            {
                if (checkBox1.Checked == true)
                {
                    LS.CreateLike(LQ.liquors_Name, user.user_ID);
                }
                else
                {
                    LS.DeLike(LQ.liquors_Name, user.user_ID);
                }
            }
            else {
                if (checkBox1.Checked == true)
                {
                    MessageBox.Show("좋아요는 16개까지 누를수 있습니다.");
                    checkBox1.Checked = false;
                }
                else
                {
                    LS.DeLike(LQ.liquors_Name, user.user_ID);
                }
            }
        }
    }
}
