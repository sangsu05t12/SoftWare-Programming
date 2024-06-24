using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp4
{
    internal class CombineSystem
    {
        System.Windows.Forms.PictureBox PB;
        System.Windows.Forms.Button Btn;
        public int WIDTH = 24; // PB
        public int WIDTH2 = 40;  // Btn
        public int HEIGHT = 39; // PB
        public int HEIGHT2 = 200; // Btn
        CombineDB CD = new CombineDB();

        public void btn_click(object sender, EventArgs e)    // CombineInformation 바운더리 호출 
        {
            System.Windows.Forms.Button btn = sender as System.Windows.Forms.Button;
            CD.combine_Name = btn.Text;
            CombineInformation CI = new CombineInformation(CD.combine_Name);
            CI.StartPosition = FormStartPosition.Manual;
            CI.Location = new Point(600, 250);
            CI.Show();
        }

        public void LoadCombine(CombineUI CB)
        {
            int num = 0;
            DBConnection dbc = new DBConnection(); // DB 연결 및 DB 기능 사용을 위한 객체 생성
            OracleConnection conn = dbc.dbConn(); // DB 연결 상태를 저장하는 객체 생성
            OracleDataReader reader = null; // 쿼리문 실행 정보를 저장하는 객체 생성
            OracleDataReader reader2 = null; // 쿼리문 실행 정보를 저장하는 객체 생성
            string query = "SELECT COUNT (*) FROM LIQUOR_COMBINE"; // 주류조합 테이블의 열 개수를 알아내는 문장
            reader = dbc.ExecuteSelectDRQuery(query); // 쿼리문을 실행하는 문장
            if (reader.Read()) num = reader.GetInt32(0); // 주류조합 테이블의 열 개수 저장
            for (CD.combine_No = 1; CD.combine_No < num + 1; CD.combine_No++)
            {
                string query2 = "SELECT 주류조합사진 FROM LIQUOR_COMBINE WHERE 주류조합번호 = " + CD.combine_No.ToString();
                query = "SELECT 주류조합명 FROM LIQUOR_COMBINE WHERE 주류조합번호 = " + CD.combine_No.ToString(); // i번째 열 레코드의 주류조합명을 알아내는 문장
                reader = dbc.ExecuteSelectDRQuery(query);
                reader2 = dbc.ExecuteSelectDRQuery(query2);
                if (reader.Read()) CD.combine_Name = reader.GetString(0);
                if (reader2.Read()) CD.combine_URL = (byte[])reader2.GetOracleBlob(0).Value;
                ImageConverter imageConverter = new ImageConverter();
                System.Drawing.Image imgg = (System.Drawing.Image)imageConverter.ConvertFrom(CD.combine_URL);

                if (CD.combine_No % 2 == 0)
                {
                    PB = new PictureBox();   // PB 동적 생성
                    PB.Location = new Point(WIDTH + 205, HEIGHT);
                    PB.Parent = CB;
                    PB.Size = new Size(150, 150);
                    PB.Image = imgg;
                    PB.SizeMode = PictureBoxSizeMode.Zoom;
                    Btn = new System.Windows.Forms.Button();   // BTN 동적 생성
                    Btn.Location = new Point(WIDTH2 + 205, HEIGHT2);
                    Btn.Parent = CB;
                    Btn.Size = new Size(100, 40);
                    Btn.Text = CD.combine_Name.ToString();
                    Btn.Click += new EventHandler(btn_click);
                    CB.groupBox1.Controls.Add(PB);
                    CB.groupBox1.Controls.Add(Btn);

                    HEIGHT += 250;
                    HEIGHT2 += 250;
                }
                else
                {
                    PB = new PictureBox();
                    PB.Location = new Point(WIDTH, HEIGHT);
                    PB.Parent = CB;
                    PB.Size = new Size(150, 150);
                    PB.Image = imgg;
                    PB.SizeMode = PictureBoxSizeMode.Zoom;
                    Btn = new System.Windows.Forms.Button();
                    Btn.Location = new Point(WIDTH2, HEIGHT2);
                    Btn.Parent = CB;
                    Btn.Size = new Size(100, 40);
                    Btn.Text = CD.combine_Name.ToString();
                    Btn.Click += new EventHandler(btn_click);
                    CB.groupBox1.Controls.Add(PB);
                    CB.groupBox1.Controls.Add(Btn);
                }
            }
        }
        public void SearchCombine(CombineUI CB)
        {
            if (CB.textBox1.Text == "") MessageBox.Show("검색어가 없습니다 !");
            else
            {
                CB.groupBox1.Controls.Clear(); // 전체 다 지우기
                HEIGHT = 39; // PB
                HEIGHT2 = 200; // Btn
                DBConnection dbc = new DBConnection(); // DB 연결 및 DB 기능 사용을 위한 객체 생성
                OracleConnection conn = dbc.dbConn(); // DB 연결 상태를 저장하는 객체 생성
                OracleDataReader reader = null; // 쿼리문 실행 정보를 저장하는 객체 생성
                OracleDataReader reader2 = null; // 쿼리문 실행 정보를 저장하는 객체 생성
                string query = null;
                int num = 0;
                try
                {
                    if (CB.comboBox1.SelectedItem.ToString() == "조합 이름")
                    {
                        CD.combine_Name = CB.textBox1.Text;
                        query = "SELECT * FROM LIQUOR_COMBINE WHERE 주류조합명 = '" + CD.combine_Name + "'";
                        reader = dbc.ExecuteSelectDRQuery(query);  // 이미지
                        reader2 = dbc.ExecuteSelectDRQuery(query); // 이름
                        if (reader.Read()) CD.combine_URL = (byte[])reader.GetOracleBlob(5).Value;
                        ImageConverter imageConverter = new ImageConverter();
                        System.Drawing.Image imgg = (System.Drawing.Image)imageConverter.ConvertFrom(CD.combine_URL);
                        if (reader2.Read()) CD.combine_Name = reader2.GetString(1);
                        else
                        {
                            throw new Exception("해당 검색의 술은 없습니다.");
                        }
                        PB = new PictureBox();   // PB 동적 생성
                        PB.Location = new Point(WIDTH, HEIGHT);
                        PB.Parent = CB;
                        PB.Size = new Size(150, 150);
                        PB.Image = imgg;
                        PB.SizeMode = PictureBoxSizeMode.Zoom;
                        Btn = new System.Windows.Forms.Button();   // BTN 동적 생성
                        Btn.Location = new Point(WIDTH2, HEIGHT2);
                        Btn.Parent = CB;
                        Btn.Size = new Size(100, 40);
                        Btn.Text = CD.combine_Name.ToString();
                        Btn.Click += new EventHandler(btn_click);
                        CB.groupBox1.Controls.Add(PB);
                        CB.groupBox1.Controls.Add(Btn);
                    }

                    else if (CB.comboBox1.SelectedItem.ToString() == "조합 재료")
                    {
                        CD.combine_Ingr1 = CB.textBox1.Text;
                        CD.combine_Ingr2 = CB.textBox1.Text;
                        CD.combine_Ingr3 = CB.textBox1.Text;
                        query = "SELECT COUNT (*) FROM LIQUOR_COMBINE WHERE 재료1 = '" + CD.combine_Ingr1 + "' OR 재료2 = '" + CD.combine_Ingr2
                        + "' OR 재료3 = '" + CD.combine_Ingr3 + "'";
                        reader = dbc.ExecuteSelectDRQuery(query);
                        if (reader.Read()) num = reader.GetInt32(0); // 주류조합 테이블의 열 개수 저장

                        query = "SELECT * FROM LIQUOR_COMBINE WHERE 재료1 = '" + CD.combine_Ingr1 + "' OR 재료2 = '" + CD.combine_Ingr2
                        + "' OR 재료3 = '" + CD.combine_Ingr3 + "'";
                        reader = dbc.ExecuteSelectDRQuery(query);  // 이미지
                        reader2 = dbc.ExecuteSelectDRQuery(query); // 이름
                        if (num == 0)
                        {
                            throw new Exception("해당 검색의 재료에 해당하는 술은 없습니다.");
                        }
                        for (CD.combine_No = 1; CD.combine_No < num + 1; CD.combine_No++)
                        {
                            if (reader.Read()) CD.combine_URL = (byte[])reader.GetOracleBlob(5).Value;
                            ImageConverter imageConverter = new ImageConverter();
                            System.Drawing.Image imgg = (System.Drawing.Image)imageConverter.ConvertFrom(CD.combine_URL);
                            if (reader2.Read()) CD.combine_Name = reader2.GetString(1);

                            if (CD.combine_No % 2 == 0)
                            {
                                PB = new PictureBox();   // PB 동적 생성
                                PB.Location = new Point(WIDTH + 205, HEIGHT);
                                PB.Parent = CB;
                                PB.Size = new Size(150, 150);
                                PB.Image = imgg;
                                PB.SizeMode = PictureBoxSizeMode.Zoom;
                                Btn = new System.Windows.Forms.Button();   // BTN 동적 생성
                                Btn.Location = new Point(WIDTH2 + 205, HEIGHT2);
                                Btn.Parent = CB;
                                Btn.Size = new Size(100, 40);
                                Btn.Text = CD.combine_Name.ToString();
                                Btn.Click += new EventHandler(btn_click);
                                CB.groupBox1.Controls.Add(PB);
                                CB.groupBox1.Controls.Add(Btn);

                                HEIGHT += 250;
                                HEIGHT2 += 250;
                            }
                            else
                            {
                                PB = new PictureBox();
                                PB.Location = new Point(WIDTH, HEIGHT);
                                PB.Parent = CB;
                                PB.Size = new Size(150, 150);
                                PB.Image = imgg;
                                PB.SizeMode = PictureBoxSizeMode.Zoom;
                                Btn = new System.Windows.Forms.Button();
                                Btn.Location = new Point(WIDTH2, HEIGHT2);
                                Btn.Parent = CB;
                                Btn.Size = new Size(100, 40);
                                Btn.Text = CD.combine_Name.ToString();
                                Btn.Click += new EventHandler(btn_click);
                                CB.groupBox1.Controls.Add(PB);
                                CB.groupBox1.Controls.Add(Btn);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
