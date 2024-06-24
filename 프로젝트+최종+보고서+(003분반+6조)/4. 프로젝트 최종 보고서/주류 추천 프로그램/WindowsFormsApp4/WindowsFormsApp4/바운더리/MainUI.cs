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
using System.Xml.Linq;

namespace WindowsFormsApp4
{
    public partial class MainUI : Form
    {
        static public string rid = null;
        System.Windows.Forms.PictureBox PB;
        System.Windows.Forms.Label lb1;
        System.Windows.Forms.Label lb2;
        System.Windows.Forms.Label lb3;
        LiquorsDB LQ = new LiquorsDB();
        UserDB user = new UserDB();
        public MainUI(string id)
        {
            InitializeComponent();
            user.user_ID = id;
            rid = id;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SearchUI searchUI = new SearchUI();
            searchUI.StartPosition = FormStartPosition.Manual;
            searchUI.Location = new Point(500, 150);
            searchUI.Show();
        }
     
        private void button7_Click(object sender, EventArgs e)
        {
            LQ.liquors_Ctg = "맥주";
            ctgSearchUI ctg = new ctgSearchUI(LQ.liquors_Ctg);
            ctg.StartPosition = FormStartPosition.Manual;
            ctg.Location = new Point(520, 150);
            ctg.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            LQ.liquors_Ctg = "소주";
            ctgSearchUI ctg = new ctgSearchUI(LQ.liquors_Ctg);
            ctg.StartPosition = FormStartPosition.Manual;
            ctg.Location = new Point(520, 150);
            ctg.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            LQ.liquors_Ctg = "양주";
            ctgSearchUI ctg = new ctgSearchUI(LQ.liquors_Ctg);
            ctg.StartPosition = FormStartPosition.Manual;
            ctg.Location = new Point(520, 150);
            ctg.Show();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            LQ.liquors_Ctg = "전통주";
            ctgSearchUI ctg = new ctgSearchUI(LQ.liquors_Ctg);
            ctg.StartPosition = FormStartPosition.Manual;
            ctg.Location = new Point(520, 150);
            ctg.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DisplayUI dis = new DisplayUI();
            dis.StartPosition = FormStartPosition.Manual;
            dis.Location = new Point(520, 150);
            dis.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            CombineUI Cbu = new CombineUI();
            Cbu.StartPosition = FormStartPosition.Manual;
            Cbu.Location = new Point(520, 150);
            Cbu.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ReviewSearchUI review = new ReviewSearchUI();
            review.StartPosition = FormStartPosition.Manual;
            review.Location = new Point(520, 150);
            review.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            MyAccountUI MU = new MyAccountUI();
            MU.StartPosition = FormStartPosition.Manual;
            MU.Location = new Point(570, 250);
            MU.Show();
        }

        private void MainUI_Load(object sender, EventArgs e)  // MainUI 바운더리에 Load 메소드 추가하기
        {
            DBConnection dbc = new DBConnection(); // DB 연결 및 DB 기능 사용을 위한 객체 생성
            OracleConnection conn = dbc.dbConn(); // DB 연결 상태를 저장하는 객체 생성
            OracleDataReader reader = null; // 쿼리문 실행 정보를 저장하는 객체 생성
            string query = "SELECT * FROM LIQUOR WHERE 주류카테고리 = (SELECT 관심카테고리 FROM USERS WHERE 아이디 = '" + user.user_ID + "')";
            reader = dbc.ExecuteSelectDRQuery(query); // 쿼리문을 실행하는 문장
            if (reader.Read()) LQ.liquors_URL = (byte[])reader.GetOracleBlob(3).Value;
            ImageConverter imageConverter = new ImageConverter();
            Image imgg = (Image)imageConverter.ConvertFrom(LQ.liquors_URL);

            PB = new PictureBox();   // PB 동적 생성
            PB.Location = new Point(50, 70);
            PB.Parent = this;
            PB.Size = new Size(130, 130);
            PB.Image = imgg;
            PB.SizeMode = PictureBoxSizeMode.Zoom;
            this.Controls.Add(PB);
            PB.BringToFront();

            LQ.liquors_Name = reader.GetString(0);
            lb1 = new Label();
            lb1.Location = new Point(185, 110);
            lb1.Parent = this;
            lb1.Size = new Size(180, 19);
            lb1.Text = "주류명 : " + LQ.liquors_Name.ToString();
            lb1.Font = new Font("굴림", 8, FontStyle.Bold);
            lb1.BringToFront();

            LQ.liquors_Ctg = reader.GetString(4);
            lb2 = new Label();
            lb2.Location = new Point(185, 140);
            lb2.Parent = this;
            lb2.Size = new Size(130, 19);
            lb2.Text = "종류 : " + LQ.liquors_Ctg.ToString();
            lb2.Font = new Font("굴림", 8, FontStyle.Bold);
            lb2.BringToFront();

            LQ.liquors_Alc = reader.GetString(1);
            lb3 = new Label();
            lb3.Location = new Point(185, 170);
            lb3.Parent = this;
            lb3.Size = new Size(130, 19);
            lb3.Text = "도수 : " + LQ.liquors_Alc.ToString() + "%";
            lb3.Font = new Font("굴림", 8, FontStyle.Bold);
            lb3.BringToFront();
        }

    }
}
