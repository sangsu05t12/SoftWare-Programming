using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace WindowsFormsApp4
{
    internal class ReviewSystem
    {
        System.Windows.Forms.GroupBox GB;
        System.Windows.Forms.Label LB1;
        System.Windows.Forms.Label LB2;

        public int WIDTH = 15;
        public int HEIGHT = 27;
        public int WIDTH2 = 30;
        public int HEIGHT2 = 39;
        public int WIDTH3 = 30;
        public int HEIGHT3 = 64;
        ReviewDB RD = new ReviewDB(); 
        public void CreateReview(CreateReviewUI CR)
        {
            string o = null;  // 확인용
            RD.review_Ctg = CR.comboBox1.SelectedItem.ToString();
            RD.review_Rate = CR.comboBox2.Text;
            RD.review_Name = CR.textBox2.Text;
            RD.review_Cont = CR.textBox3.Text;
            int result = 0;
            RD.review_ID = MainUI.rid;
            DBConnection dbc = new DBConnection(); // DB 연결 및 DB 기능 사용을 위한 객체 생성
            OracleConnection conn = dbc.dbConn(); // DB 연결 상태를 저장하는 객체 생성
            OracleDataReader reader = null; // 쿼리문 실행 정보를 저장하는 객체 생성
            OracleDataReader reader2 = null; // 쿼리문 실행 정보를 저장하는 객체 생성

            string query3 = "SELECT * FROM REVIEW WHERE 주류명 = '" + RD.review_Name + "' AND 회원아이디 = (SELECT 아이디 FROM USERS WHERE 아이디 = '" + RD.review_ID + "')";
            reader2 = dbc.ExecuteSelectDRQuery(query3);

            string query2 = "SELECT * FROM LIQUOR WHERE 주류카테고리 = '" + RD.review_Ctg + "' AND 주류명 = (SELECT 주류명 FROM LIQUOR WHERE 주류명 = '" + RD.review_Name + "')";
            reader = dbc.ExecuteSelectDRQuery(query2);

            if (reader2.Read())
            {
                MessageBox.Show("해당 주류는 이미 리뷰가 작성되어 있습니다.");
                conn.Close();
            }
            else
            {
                if (reader.Read()) o = reader.GetString(0);
                if (o == null)  // 주류명이 있는지 확인
                {
                    MessageBox.Show("주류 명이 일치하지 않습니다.");
                    conn.Close();
                }
                else
                {
                    string query = "SELECT COUNT (*) FROM REVIEW";
                    reader = dbc.ExecuteSelectDRQuery(query);
                    if (reader.Read()) RD.review_No = reader.GetInt32(0);
                    RD.review_No += 1;
                    query = "INSERT INTO REVIEW (리뷰번호, 주류명, 회원아이디, 내용, 평점, 카테고리) VALUES (" + RD.review_No + ",'" + RD.review_Name + "', '" + RD.review_ID + "', '" + RD.review_Cont + "', '" + RD.review_Rate + "', '" + RD.review_Ctg + "')";
                    result = dbc.ExecuteUpdateQuery(query); // 쿼리문 실행

                    query = "COMMIT"; // 테이블 수정사항을 저장하는 문장
                    result = dbc.ExecuteUpdateQuery(query);

                    conn.Close();

                    MessageBox.Show("리뷰가 작성되었습니다");
                    CR.Close();
                }
            }
        }
        public void UpdateLoad(UpdateReviewUI UR)
        {
            RD.review_Name = ReviewListUI.add;
            RD.review_ID = MainUI.rid;
            DBConnection dbc = new DBConnection(); // DB 연결 및 DB 기능 사용을 위한 객체 생성
            OracleConnection conn = dbc.dbConn(); // DB 연결 상태를 저장하는 객체 생성
            OracleDataReader reader = null; // 쿼리문 실행 정보를 저장하는 객체 생성
            string query = "SELECT * FROM REVIEW WHERE 주류명 = '" + RD.review_Name + "' AND 회원아이디 = (SELECT 아이디 FROM USERS WHERE 아이디 = '" + RD.review_ID + "')";
            reader = dbc.ExecuteSelectDRQuery(query); // 쿼리문을 실행하는 문장
            if (reader.Read()) RD.review_Name = reader.GetString(1);
            RD.review_Ctg = reader.GetString(5);
            RD.review_Rate = reader.GetString(4);
            RD.review_Cont = reader.GetString(3);
            
            if (UR.comboBox1.Items.Equals(RD.review_Rate)) 
                UR.comboBox1.SelectedItem = RD.review_Rate;
            //UR.comboBox1.Text = RD.review_Rate;
            UR.textBox3.Text = RD.review_Cont;
            UR.textBox2.Text = RD.review_Name;
            UR.textBox4.Text = RD.review_Ctg;
        }
        public void UpdateReview(UpdateReviewUI UR)
        {
            RD.review_ID = MainUI.rid;
            RD.review_Rate = UR.comboBox1.Text;
            RD.review_Cont = UR.textBox3.Text;
            RD.review_Name = ReviewListUI.add;
            DBConnection dbc = new DBConnection(); // DB 연결 및 DB 기능 사용을 위한 객체 생성
            OracleConnection conn = dbc.dbConn(); // DB 연결 상태를 저장하는 객체 생성
            OracleDataReader reader = null; // 쿼리문 실행 정보를 저장하는 객체 생성
            int result = 0; // 쿼리문 실행 정보를 저장하는 객체 생성
            if (UR.comboBox1.Text == "")
            {
                MessageBox.Show("평점이 비었습니다 !");
            }
            else if (UR.textBox3.Text == "")
            {
                MessageBox.Show("내용이 비었습니다.!");
            }
            else
            {
                string query = "UPDATE REVIEW SET 평점 = '" + RD.review_Rate + "', 내용 = '" + RD.review_Cont + "' WHERE 회원아이디 ='" + RD.review_ID + "' AND 주류명 = (SELECT 주류명 FROM LIQUOR WHERE 주류명 = '" + RD.review_Name + "')";
                result = dbc.ExecuteUpdateQuery(query); // 쿼리문을 실행하는 문장
                query = "COMMIT"; // 테이블의 변경사항을 저장하는 문장
                reader = dbc.ExecuteSelectDRQuery(query);
                MessageBox.Show("수정이 완료되었습니다.");
                UR.Close();
            }
        }
        public void MyReviewLoad(ReviewListUI ML)
        {
            RD.review_ID = MainUI.rid;
            DBConnection dbc = new DBConnection(); // DB 연결 및 DB 기능 사용을 위한 객체 생성
            OracleConnection conn = dbc.dbConn(); // DB 연결 상태를 저장하는 객체 생성
            OracleDataReader reader = null; // 쿼리문 실행 정보를 저장하는 객체 생성
            string query = "SELECT * FROM REVIEW WHERE 회원아이디 = '" + RD.review_ID + "' ORDER BY 리뷰번호";
            reader = dbc.ExecuteSelectDRQuery(query);

            while (reader.Read()) // 테이블 데이터가 없을 때까지 반복
            {
                ListViewItem lvi = new ListViewItem(); // 리스트뷰에 아이템을 추가하기 위한 리스트뷰아이템 객체 생성
                lvi.Text = reader.GetString(1);
                // 첫 번째 행은 체크박스가 있으므로 두 번째 행부터 추가
                //lvi.SubItems.Add(reader.GetString(1)); // 리뷰 테이블의 주류명을 추가
                lvi.SubItems.Add(reader.GetString(5)); // 리뷰 테이블의 카테고리를  추가
                lvi.SubItems.Add(reader.GetString(4)); // 리뷰 테이블의 평점을 추가

                ML.listView2.Items.Add(lvi); // 리스트뷰에 리스트뷰아이템을 추가
                ML.listView2.EndUpdate(); // 리스트뷰 업데이트를 끝냄
            }
        }
        public void DelReview(ReviewListUI ML)
        {
            RD.review_ID = MainUI.rid;
            DBConnection dbc = new DBConnection(); // DB 연결 및 DB 기능 사용을 위한 객체 생성
            OracleConnection conn = dbc.dbConn(); // DB 연결 상태를 저장하는 객체 생성
            OracleDataReader reader = null; // 쿼리문 실행 정보를 저장하는 객체 생성
            int result = 0;

            if (ML.listView2.SelectedItems.Count == 1)
            {
                System.Windows.Forms.ListView.SelectedListViewItemCollection items = ML.listView2.SelectedItems;
                ListViewItem lvItem = items[0];
                RD.review_Name = lvItem.SubItems[0].Text;
                string query2 = "DELETE FROM REVIEW WHERE 주류명 = '" + RD.review_Name + "' AND 회원아이디 = (SELECT 아이디 FROM USERS WHERE 아이디 = '" + RD.review_ID + "')";
                result = dbc.ExecuteUpdateQuery(query2);
                query2 = "COMMIT";
                reader = dbc.ExecuteSelectDRQuery(query2);
                query2 = "UPDATE REVIEW SET 리뷰번호 = ROWNUM";
                result = dbc.ExecuteUpdateQuery(query2);
                query2 = "COMMIT";
                reader = dbc.ExecuteSelectDRQuery(query2);
            }

            string query = "SELECT * FROM REVIEW WHERE 회원아이디 = '" + RD.review_ID + "' ORDER BY 리뷰번호";
            reader = dbc.ExecuteSelectDRQuery(query);

            ML.listView2.Items.Clear();
            while (reader.Read()) // 테이블 데이터가 없을 때까지 반복
            {
                ListViewItem lvi = new ListViewItem(); // 리스트뷰에 아이템을 추가하기 위한 리스트뷰아이템 객체 생성
                lvi.Text = reader.GetString(1);
                // 첫 번째 행은 체크박스가 있으므로 두 번째 행부터 추가
                //lvi.SubItems.Add(reader.GetString(1)); // 리뷰 테이블의 주류명을 추가
                lvi.SubItems.Add(reader.GetString(5)); // 리뷰 테이블의 카테고리를  추가
                lvi.SubItems.Add(reader.GetString(4)); // 리뷰 테이블의 평점을 추가

                ML.listView2.Items.Add(lvi); // 리스트뷰에 리스트뷰아이템을 추가
                ML.listView2.EndUpdate(); // 리스트뷰 업데이트를 끝냄
            }
        }
        public void showreviewlist(LiquorReviewUI LR, string name)
        {
            RD.review_Name = name;
            LR.groupBox1.Text = RD.review_Name + " 리뷰 모아보기";
            int num = 0;
            DBConnection dbc = new DBConnection(); // DB 연결 및 DB 기능 사용을 위한 객체 생성
            OracleConnection conn = dbc.dbConn(); // DB 연결 상태를 저장하는 객체 생성
            OracleDataReader reader = null; // 쿼리문 실행 정보를 저장하는 객체 생성

            string query = "SELECT COUNT (*) FROM REVIEW WHERE 주류명 = '" + RD.review_Name + "'";
            reader = dbc.ExecuteSelectDRQuery(query);
            if (reader.Read()) num = reader.GetInt32(0);

            query = "SELECT * FROM REVIEW WHERE 주류명 = '" + RD.review_Name + "'";
            reader = dbc.ExecuteSelectDRQuery(query);

            for (int i = 1; i < num + 1; i++)
            {
                if (reader.Read()) RD.review_Rate = reader.GetString(4);
                GB = new System.Windows.Forms.GroupBox();
                GB.Location = new Point(WIDTH, HEIGHT);
                GB.Parent = LR;
                GB.Size = new Size(360, 120);
                LB1 = new Label();
                LB1.Location = new Point(WIDTH2, HEIGHT + 30);
                LB1.Parent = LR;
                LB1.Size = new Size(55, 15);
                LB1.Text = "평점 : " + RD.review_Rate.ToString();
                LB1.AutoSize = true;

                RD.review_Cont = reader.GetString(3);
                LB2 = new Label();
                LB2.Location = new Point(WIDTH3, HEIGHT + 50);
                LB2.Parent = LR;
                LB2.Size = new Size(330, 60);
                LB2.Text = "내용 : " + RD.review_Cont.ToString();
                LB2.AutoSize = false;

                LR.groupBox1.Controls.Add(LB1);
                LR.groupBox1.Controls.Add(LB2);
                LR.groupBox1.Controls.Add(GB);


                HEIGHT += 130;
            }
        }
    }
}
