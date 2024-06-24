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

namespace WindowsFormsApp4
{
    internal class SearchSystem
    {
        System.Windows.Forms.PictureBox Btn;
        System.Windows.Forms.Label LB1;
        LiquorsDB LD = new LiquorsDB();
        static public string text = null; // 버튼이름
        public int WIDTH = 6;
        public int HEIGHT = 20;
        public int WIDTH2 = 20;
        public int HEIGHT2 = 170;
        public void pb_click(object sender, EventArgs e)
        {
            Label pb = sender as Label;
            LD.liquors_Name = pb.Text;
            InformationUI CI = new InformationUI(LD.liquors_Name);
            CI.StartPosition = FormStartPosition.Manual;
            CI.Location = new Point(520, 150);
            CI.Show();
        }
        public void Searchsystem(SearchUI SU)
        {
            if (SU.radioButton1.Checked)
            {
                if (SU.textBox1.Text == "") MessageBox.Show("주류 명을 입력하세요!");
                else
                {
                    SU.groupBox1.Controls.Clear();
                    HEIGHT = 20;
                    HEIGHT2 = 170;
                    try
                    {
                        LD.liquors_Name = SU.textBox1.Text;
                        DBConnection dbc = new DBConnection(); // DB 연결 및 DB 기능 사용을 위한 객체 생성
                        OracleConnection conn = dbc.dbConn(); // DB 연결 상태를 저장하는 객체 생성
                        OracleDataReader reader = null; // 쿼리문 실행 정보를 저장하는 객체 생성
                        OracleDataReader reader2 = null; // 쿼리문 실행 정보를 저장하는 객체 생성

                        string query = "SELECT * FROM LIQUOR WHERE 주류명 = '" + LD.liquors_Name + "'";
                        reader = dbc.ExecuteSelectDRQuery(query);
                        reader2 = dbc.ExecuteSelectDRQuery(query);
                        if (reader.Read()) LD.liquors_URL = (byte[])reader.GetOracleBlob(3).Value;
                        ImageConverter imageConverter = new ImageConverter();
                        System.Drawing.Image imgg = (System.Drawing.Image)imageConverter.ConvertFrom(LD.liquors_URL);
                        if (reader2.Read()) LD.liquors_Name = reader2.GetString(0);  // 주류명
                        else
                        {
                            throw new Exception();
                        }
                        Btn = new PictureBox();   // BTN 동적 생성
                        Btn.Location = new Point(WIDTH, HEIGHT);
                        Btn.Parent = SU;
                        Btn.Size = new Size(125, 138);
                        Btn.Image = imgg;
                        Btn.SizeMode = PictureBoxSizeMode.Zoom;
                        LB1 = new Label();
                        LB1.Location = new Point(WIDTH2, HEIGHT2);
                        LB1.Parent = SU;
                        LB1.Size = new Size(35, 13);
                        LB1.Text = LD.liquors_Name.ToString();
                        LB1.AutoSize = true;
                        LB1.Click += new EventHandler(pb_click);
                        SU.groupBox1.Controls.Add(Btn);
                        SU.groupBox1.Controls.Add(LB1);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("해당 술이 없습니다 !!");
                    }
                }
            }

            else if (SU.radioButton2.Checked)
            {
                SU.groupBox1.Controls.Clear();
                HEIGHT = 20;
                HEIGHT2 = 170;
                LD.liquors_Co = SU.comboBox1.SelectedItem.ToString();
                int j = 0;
                DBConnection dbc = new DBConnection(); // DB 연결 및 DB 기능 사용을 위한 객체 생성
                OracleConnection conn = dbc.dbConn(); // DB 연결 상태를 저장하는 객체 생성
                OracleDataReader reader = null; // 쿼리문 실행 정보를 저장하는 객체 생성
                OracleDataReader reader2 = null; // 쿼리문 실행 정보를 저장하는 객체 생성
                string query = "SELECT COUNT (*) FROM LIQUOR WHERE 제조사 = '" + LD.liquors_Co + "'";
                reader = dbc.ExecuteSelectDRQuery(query); // 쿼리문을 실행하는 문장
                if (reader.Read()) j = reader.GetInt32(0);

                query = "SELECT * FROM LIQUOR WHERE 제조사 = '" + LD.liquors_Co + "'"; // i번째 열 레코드의 주류조합명을 알아내는 문장
                reader = dbc.ExecuteSelectDRQuery(query);
                reader2 = dbc.ExecuteSelectDRQuery(query);
                for (int i = 1; i < j + 1; i++)
                {
                    if (reader.Read()) LD.liquors_URL = (byte[])reader.GetOracleBlob(3).Value;
                    ImageConverter imageConverter = new ImageConverter();
                    System.Drawing.Image imgg = (System.Drawing.Image)imageConverter.ConvertFrom(LD.liquors_URL);
                    if (reader2.Read()) LD.liquors_Co = reader2.GetString(0);  // 주류명
                    if (i % 3 == 1)
                    {
                        Btn = new PictureBox();   // BTN 동적 생성
                        Btn.Location = new Point(WIDTH, HEIGHT);
                        Btn.Parent = SU;
                        Btn.Size = new Size(125, 138);
                        Btn.Image = imgg;
                        Btn.SizeMode = PictureBoxSizeMode.Zoom;
                        LB1 = new Label();
                        LB1.Location = new Point(WIDTH2, HEIGHT2);
                        LB1.Parent = SU;
                        LB1.Size = new Size(35, 13);
                        LB1.Text = LD.liquors_Co.ToString();
                        LB1.AutoSize = true;
                        LB1.Click += new EventHandler(pb_click);
                        SU.groupBox1.Controls.Add(Btn);
                        SU.groupBox1.Controls.Add(LB1);

                    }
                    else if (i % 3 == 2)
                    {
                        Btn = new PictureBox();
                        Btn.Location = new Point(WIDTH + 135, HEIGHT);
                        Btn.Parent = SU;
                        Btn.Size = new Size(125, 138);
                        Btn.Image = imgg;
                        Btn.SizeMode = PictureBoxSizeMode.Zoom;
                        LB1 = new Label();
                        LB1.Location = new Point(WIDTH2 + 135, HEIGHT2);
                        LB1.Parent = SU;
                        LB1.Size = new Size(35, 13);
                        LB1.Text = LD.liquors_Co.ToString();
                        LB1.AutoSize = true;
                        LB1.Click += new EventHandler(pb_click);
                        SU.groupBox1.Controls.Add(Btn);
                        SU.groupBox1.Controls.Add(LB1);
                    }
                    else
                    {
                        Btn = new PictureBox();
                        Btn.Location = new Point(WIDTH + 270, HEIGHT);
                        Btn.Parent = SU;
                        Btn.Size = new Size(125, 138);
                        Btn.Image = imgg;
                        Btn.SizeMode = PictureBoxSizeMode.Zoom;
                        LB1 = new Label();
                        LB1.Location = new Point(WIDTH2 + 270, HEIGHT2);
                        LB1.Parent = SU;
                        LB1.Size = new Size(35, 13);
                        LB1.Text = LD.liquors_Co.ToString();
                        LB1.AutoSize = true;
                        LB1.Click += new EventHandler(pb_click);
                        SU.groupBox1.Controls.Add(Btn);
                        SU.groupBox1.Controls.Add(LB1);
                    }
                    if (i % 3 == 0)
                    {
                        HEIGHT += 200;
                        HEIGHT2 += 200;
                    }
                }
            }
        }
        public void SearchLoad(SearchUI SU)
        {
            int j = 0;
            DBConnection dbc = new DBConnection(); // DB 연결 및 DB 기능 사용을 위한 객체 생성
            OracleConnection conn = dbc.dbConn(); // DB 연결 상태를 저장하는 객체 생성
            OracleDataReader reader = null; // 쿼리문 실행 정보를 저장하는 객체 생성
            OracleDataReader reader2 = null; // 쿼리문 실행 정보를 저장하는 객체 생성
            string query = "SELECT COUNT (*) FROM LIQUOR";
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
                if (i % 3 == 1)
                {
                    Btn = new PictureBox();   // BTN 동적 생성
                    Btn.Location = new Point(WIDTH, HEIGHT);
                    Btn.Parent = SU;
                    Btn.Size = new Size(125, 138);
                    Btn.Image = imgg;
                    Btn.SizeMode = PictureBoxSizeMode.Zoom;
                    LB1 = new Label();
                    LB1.Location = new Point(WIDTH2, HEIGHT2);
                    LB1.Parent = SU;
                    LB1.Size = new Size(35, 13);
                    LB1.Text = LD.liquors_Name.ToString();
                    LB1.AutoSize = true;
                    LB1.Click += new EventHandler(pb_click);

                    SU.groupBox1.Controls.Add(Btn);
                    SU.groupBox1.Controls.Add(LB1);

                }
                else if (i % 3 == 2)
                {
                    Btn = new PictureBox();
                    Btn.Location = new Point(WIDTH + 135, HEIGHT);
                    Btn.Parent = SU;
                    Btn.Size = new Size(125, 138);
                    Btn.Image = imgg;
                    Btn.SizeMode = PictureBoxSizeMode.Zoom;
                    LB1 = new Label();
                    LB1.Location = new Point(WIDTH2 + 135, HEIGHT2);
                    LB1.Parent = SU;
                    LB1.Size = new Size(35, 13);
                    LB1.Text = LD.liquors_Name.ToString();
                    LB1.AutoSize = true;
                    LB1.Click += new EventHandler(pb_click);
                    SU.groupBox1.Controls.Add(Btn);
                    SU.groupBox1.Controls.Add(LB1);
                }
                else
                {
                    Btn = new PictureBox();
                    Btn.Location = new Point(WIDTH + 270, HEIGHT);
                    Btn.Parent = SU;
                    Btn.Size = new Size(125, 138);
                    Btn.Image = imgg;
                    Btn.SizeMode = PictureBoxSizeMode.Zoom;
                    LB1 = new Label();
                    LB1.Location = new Point(WIDTH2 + 270, HEIGHT2);
                    LB1.Parent = SU;
                    LB1.Size = new Size(35, 13);
                    LB1.Text = LD.liquors_Name.ToString();
                    LB1.AutoSize = true;
                    LB1.Click += new EventHandler(pb_click);
                    SU.groupBox1.Controls.Add(Btn);
                    SU.groupBox1.Controls.Add(LB1);
                }
                if (i % 3 == 0)
                {
                    HEIGHT += 200;
                    HEIGHT2 += 200;
                }
            }
        }
    }
}
