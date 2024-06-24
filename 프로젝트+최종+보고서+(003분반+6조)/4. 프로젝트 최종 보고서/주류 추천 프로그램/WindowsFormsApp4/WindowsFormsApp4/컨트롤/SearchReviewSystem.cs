using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Runtime.Remoting.Messaging;

namespace WindowsFormsApp4
{
    internal class SearchReviewSystem
    {
        System.Windows.Forms.PictureBox PB;
        System.Windows.Forms.Button Btn;
        System.Windows.Forms.Label LB1;
        LiquorsDB LD = new LiquorsDB();
        ReviewDB RD = new ReviewDB(); // 평점
        public int WIDTH = 17; // PB
        public int HEIGHT = 27; // PB
        public int WIDTH2 = 35;  // Btn
        public int HEIGHT2 = 163; // Btn
        public int WIDTH3 = 45; // LB1
        public int HEIGHT3 = 203; // LB1

        public void btn_click(object sender, EventArgs e)
        {
            System.Windows.Forms.Button btn = sender as System.Windows.Forms.Button;
            LD.liquors_Name = btn.Text;
            LiquorReviewUI LR = new LiquorReviewUI(LD.liquors_Name);
            LR.StartPosition = FormStartPosition.Manual;
            LR.Location = new Point(520, 150);
            LR.Show();
        }
        public void SearchReview(ReviewSearchUI RS)
        {
            if (RS.textBox1.Text == "")
            {
                MessageBox.Show("검색어가 없습니다 ! ");
            }
            else
            {
                RS.groupBox1.Controls.Clear();
                HEIGHT3 = 203; // LB1
                HEIGHT2 = 163; // Btn
                HEIGHT = 27; // PB
                LD.liquors_Name = RS.textBox1.Text;
                DBConnection dbc = new DBConnection(); // DB 연결 및 DB 기능 사용을 위한 객체 생성
                OracleConnection conn = dbc.dbConn(); // DB 연결 상태를 저장하는 객체 생성
                OracleDataReader reader = null; // 쿼리문 실행 정보를 저장하는 객체 생성
                OracleDataReader reader2 = null; // 쿼리문 실행 정보를 저장하는 객체 생성

                try
                {
                    string query = "SELECT * FROM LIQUOR WHERE 주류명 = '" + LD.liquors_Name + "'";
                    reader = dbc.ExecuteSelectDRQuery(query);
                    if (reader.Read()) LD.liquors_URL = (byte[])reader.GetOracleBlob(3).Value;
                    else
                    {
                        throw new Exception("해당 검색어는 없습니다.");
                    }
                    ImageConverter imageConverter = new ImageConverter();
                    System.Drawing.Image imgg = (System.Drawing.Image)imageConverter.ConvertFrom(LD.liquors_URL);
                    string query2 = "SELECT NVL(AVG(평점), 0.0) FROM REVIEW WHERE 주류명 = '" + LD.liquors_Name + "'";
                    reader2 = dbc.ExecuteSelectDRQuery(query2);
                    if (reader2.Read()) RD.review_Rate = reader2.GetString(0);
                    PB = new PictureBox();
                    PB.Location = new Point(WIDTH, HEIGHT);
                    PB.Parent = RS;
                    PB.Size = new Size(142, 130);
                    PB.Image = imgg;
                    PB.SizeMode = PictureBoxSizeMode.Zoom;

                    LD.liquors_Name = reader.GetString(0);
                    Btn = new System.Windows.Forms.Button();
                    Btn.Location = new Point(WIDTH2, HEIGHT2);
                    Btn.Parent = RS;
                    Btn.Size = new Size(88, 35);
                    Btn.Text = LD.liquors_Name.ToString();
                    Btn.Click += new EventHandler(btn_click);

                    LB1 = new Label();
                    LB1.Location = new Point(WIDTH3, HEIGHT3);
                    LB1.Parent = RS;
                    LB1.Size = new Size(65, 12);
                    LB1.Text = "평점 : " + RD.review_Rate.ToString();
                    LB1.AutoSize = true;

                    RS.groupBox1.Controls.Add(PB);
                    RS.groupBox1.Controls.Add(Btn);
                    RS.groupBox1.Controls.Add(LB1);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        public void ViewReview(ReviewSearchUI RS)
        {
            int j = 0;
            DBConnection dbc = new DBConnection(); // DB 연결 및 DB 기능 사용을 위한 객체 생성
            OracleConnection conn = dbc.dbConn(); // DB 연결 상태를 저장하는 객체 생성
            OracleDataReader reader = null; // 쿼리문 실행 정보를 저장하는 객체 생성
            OracleDataReader reader2 = null; // 쿼리문 실행 정보를 저장하는 객체 생성
            OracleDataReader reader3 = null; // 쿼리문 실행 정보를 저장하는 객체 생성
            string query = "SELECT COUNT (*) FROM LIQUOR";
            string query2;
            reader = dbc.ExecuteSelectDRQuery(query);

            if (reader.Read()) j = reader.GetInt32(0);

            query = "SELECT * FROM LIQUOR";
            reader = dbc.ExecuteSelectDRQuery(query);
            reader2 = dbc.ExecuteSelectDRQuery(query);
            for (int i = 1; i < j + 1; i++)
            {
                if (reader.Read()) LD.liquors_URL = (byte[])reader.GetOracleBlob(3).Value;
                ImageConverter imageConverter = new ImageConverter();
                System.Drawing.Image imgg = (System.Drawing.Image)imageConverter.ConvertFrom(LD.liquors_URL);
                if (reader2.Read()) LD.liquors_Name = reader2.GetString(0);  // 주류명
                query2 = "SELECT NVL(AVG(평점), 0.0) FROM REVIEW WHERE 주류명 = '" + LD.liquors_Name + "'";
                reader3 = dbc.ExecuteSelectDRQuery(query2);
                    if (reader3.Read()) RD.review_Rate = reader3.GetString(0);
                if (i % 2 == 0)
                {
                    PB = new PictureBox();   // PB 동적 생성
                    PB.Location = new Point(WIDTH + 205, HEIGHT);
                    PB.Parent = RS;
                    PB.Size = new Size(142, 130);
                    PB.Image = imgg;
                    PB.SizeMode = PictureBoxSizeMode.Zoom;
                    Btn = new System.Windows.Forms.Button();   // BTN 동적 생성
                    Btn.Location = new Point(WIDTH2 + 210, HEIGHT2);
                    Btn.Parent = RS;
                    Btn.Size = new Size(88, 35);
                    Btn.Text = LD.liquors_Name.ToString();
                    Btn.Click += new EventHandler(btn_click);
                    LB1 = new Label();
                    LB1.Location = new Point(WIDTH3 + 220, HEIGHT3);
                    LB1.Parent = RS;
                    LB1.Size = new Size(65, 12);
                    LB1.Text = "평점 : " + RD.review_Rate.ToString();  // Revliew 테이블 쿼리문써서 넣기
                    LB1.AutoSize = true;

                    RS.groupBox1.Controls.Add(PB);
                    RS.groupBox1.Controls.Add(Btn);
                    RS.groupBox1.Controls.Add(LB1);

                    HEIGHT += 277;
                    HEIGHT2 += 277;
                    HEIGHT3 += 277;
                }
                else
                {
                    PB = new PictureBox();
                    PB.Location = new Point(WIDTH, HEIGHT);
                    PB.Parent = RS;
                    PB.Size = new Size(142, 130);
                    PB.Image = imgg;
                    PB.SizeMode = PictureBoxSizeMode.Zoom;
                    Btn = new System.Windows.Forms.Button();
                    Btn.Location = new Point(WIDTH2, HEIGHT2);
                    Btn.Parent = RS;
                    Btn.Size = new Size(88, 35);
                    Btn.Text = LD.liquors_Name.ToString();
                    Btn.Click += new EventHandler(btn_click);

                    LB1 = new Label();
                    LB1.Location = new Point(WIDTH3, HEIGHT3);
                    LB1.Parent = RS;
                    LB1.Size = new Size(65, 12);
                    LB1.Text = "평점 : " + RD.review_Rate.ToString();
                    LB1.AutoSize = true;

                    RS.groupBox1.Controls.Add(PB);
                    RS.groupBox1.Controls.Add(Btn);
                    RS.groupBox1.Controls.Add(LB1);
                }
            }
        }
    }
}
