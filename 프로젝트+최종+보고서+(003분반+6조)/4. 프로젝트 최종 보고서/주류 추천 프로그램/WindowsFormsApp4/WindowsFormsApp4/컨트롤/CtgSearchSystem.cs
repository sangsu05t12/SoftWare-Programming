using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace WindowsFormsApp4
{
    internal class CtgSearchSystem
    {
        System.Windows.Forms.PictureBox PB;
        System.Windows.Forms.Label LB;
        TLiquorDB TQ = new TLiquorDB();
        LiquorsDB LQ = new LiquorsDB();
        public int WIDTH = 6;
        public int HEIGHT = 20;
        public int WIDTH2 = 20;
        public int HEIGHT2 = 170;
        public void pb2_click(object sender, EventArgs e)  // 전통주 클릭 핸들러
        {
            Label pb = sender as Label;
            TQ.tliquors_Name = pb.Text;
            TinformationUI CI = new TinformationUI(TQ.tliquors_Name);
            CI.StartPosition = FormStartPosition.Manual;
            CI.Location = new Point(525, 150);
            CI.Show();
        }
        public void pb_click(object sender, EventArgs e)
        {
            Label pb = sender as Label;
            LQ.liquors_Name = pb.Text;
            InformationUI CI = new InformationUI(LQ.liquors_Name);
            CI.StartPosition = FormStartPosition.Manual;
            CI.Location = new Point(525, 150);
            CI.Show();
        }
        public void CtgSearch(ctgSearchUI CS, string ctg)
        {
            LQ.liquors_Ctg = ctg;
            DBConnection dbc = new DBConnection(); // DB 연결 및 DB 기능 사용을 위한 객체 생성
            OracleConnection conn = dbc.dbConn(); // DB 연결 상태를 저장하는 객체 생성
            OracleDataReader reader = null; // 쿼리문 실행 정보를 저장하는 객체 생성
            OracleDataReader reader2 = null; // 쿼리문 실행 정보를 저장하는 객체 생성
            OracleDataReader reader3 = null; // 쿼리문 실행 정보를 저장하는 객체 생성
            if (LQ.liquors_Ctg == "전통주")
            {
                string query = "SELECT * FROM TLIQUOR";
                reader = dbc.ExecuteSelectDRQuery(query);
                reader2 = dbc.ExecuteSelectDRQuery(query);
                int num = 0;
                string query2 = "SELECT COUNT (*) FROM TLIQUOR";
                reader3 = dbc.ExecuteSelectDRQuery(query2); // 쿼리문을 실행하는 문장
                if (reader3.Read()) num = reader3.GetInt32(0); // 주류조합 테이블의 열 개수 저장

                for (int i = 1; i < num + 1; i++)
                {
                    if (reader.Read()) LQ.liquors_Name = reader.GetString(0);
                    if (reader2.Read()) LQ.liquors_URL = (byte[])reader2.GetOracleBlob(6).Value;
                    ImageConverter imageConverter = new ImageConverter();
                    System.Drawing.Image imgg = (System.Drawing.Image)imageConverter.ConvertFrom(LQ.liquors_URL);
                    if (i % 3 == 1)
                    {
                        PB = new PictureBox();   // BTN 동적 생성
                        PB.Location = new Point(WIDTH, HEIGHT);
                        PB.Parent = CS;
                        PB.Size = new Size(125, 138);
                        PB.Image = imgg;
                        PB.SizeMode = PictureBoxSizeMode.Zoom;
                        LB = new Label();
                        LB.Location = new Point(WIDTH2, HEIGHT2);
                        LB.Parent = CS;
                        LB.Size = new Size(60, 13);
                        LB.Text = LQ.liquors_Name.ToString();
                        LB.AutoSize = false;
                        LB.Click += new EventHandler(pb2_click);
                        CS.panel1.Controls.Add(PB);
                        CS.panel1.Controls.Add(LB);

                    }
                    else if (i % 3 == 2)
                    {
                        PB = new PictureBox();
                        PB.Location = new Point(WIDTH + 135, HEIGHT);
                        PB.Parent = CS;
                        PB.Size = new Size(125, 138);
                        PB.Image = imgg;
                        PB.SizeMode = PictureBoxSizeMode.Zoom;
                        LB = new Label();
                        LB.Location = new Point(WIDTH2 + 135, HEIGHT2);
                        LB.Parent = CS;
                        LB.Size = new Size(60, 13);
                        LB.Text = LQ.liquors_Name.ToString();
                        LB.AutoSize = false;
                        LB.Click += new EventHandler(pb2_click);
                        CS.panel1.Controls.Add(PB);
                        CS.panel1.Controls.Add(LB);
                    }
                    else
                    {
                        PB = new PictureBox();
                        PB.Location = new Point(WIDTH + 270, HEIGHT);
                        PB.Parent = CS;
                        PB.Size = new Size(125, 138);
                        PB.Image = imgg;
                        PB.SizeMode = PictureBoxSizeMode.Zoom;
                        LB = new Label();
                        LB.Location = new Point(WIDTH2 + 270, HEIGHT2);
                        LB.Parent = CS;
                        LB.Size = new Size(90, 13);
                        LB.Text = LQ.liquors_Name.ToString();
                        LB.AutoSize = false;
                        LB.Click += new EventHandler(pb2_click);
                        CS.panel1.Controls.Add(PB);
                        CS.panel1.Controls.Add(LB);
                    }

                    if (i % 3 == 0)
                    {
                        HEIGHT += 200;
                        HEIGHT2 += 200;
                    }

                }
            }
            else
            {
                string query = "SELECT * FROM LIQUOR WHERE 주류카테고리 = '" + LQ.liquors_Ctg + "'";
                reader = dbc.ExecuteSelectDRQuery(query);
                reader2 = dbc.ExecuteSelectDRQuery(query);
                int num = 0;
                string query2 = "SELECT COUNT (*) FROM LIQUOR WHERE 주류카테고리 = '" + LQ.liquors_Ctg + "'";
                reader3 = dbc.ExecuteSelectDRQuery(query2); // 쿼리문을 실행하는 문장
                if (reader3.Read()) num = reader3.GetInt32(0); // 주류조합 테이블의 열 개수 저장
                for (int i = 1; i < num + 1; i++)
                {
                    if (reader.Read()) LQ.liquors_Name = reader.GetString(0);
                    if (reader2.Read()) LQ.liquors_URL = (byte[])reader2.GetOracleBlob(3).Value;
                    ImageConverter imageConverter = new ImageConverter();
                    System.Drawing.Image imgg = (System.Drawing.Image)imageConverter.ConvertFrom(LQ.liquors_URL);
                    if (i % 3 == 1)
                    {
                        PB = new PictureBox();   // BTN 동적 생성
                        PB.Location = new Point(WIDTH, HEIGHT);
                        PB.Parent = CS;
                        PB.Size = new Size(125, 138);
                        PB.Image = imgg;
                        PB.SizeMode = PictureBoxSizeMode.Zoom;
                        LB = new Label();
                        LB.Location = new Point(WIDTH2, HEIGHT2);
                        LB.Parent = CS;
                        LB.Size = new Size(90, 13);
                        LB.Text = LQ.liquors_Name.ToString();
                        LB.AutoSize = true;
                        LB.Click += new EventHandler(pb_click);

                        CS.panel1.Controls.Add(PB);
                        CS.panel1.Controls.Add(LB);

                    }
                    else if (i % 3 == 2)
                    {
                        PB = new PictureBox();
                        PB.Location = new Point(WIDTH + 135, HEIGHT);
                        PB.Parent = CS;
                        PB.Size = new Size(125, 138);
                        PB.Image = imgg;
                        PB.SizeMode = PictureBoxSizeMode.Zoom;
                        LB = new Label();
                        LB.Location = new Point(WIDTH2 + 125, HEIGHT2);
                        LB.Parent = CS;
                        LB.Size = new Size(90, 13);
                        LB.Text = LQ.liquors_Name.ToString();
                        LB.AutoSize = true;
                        LB.Click += new EventHandler(pb_click);
                        CS.panel1.Controls.Add(PB);
                        CS.panel1.Controls.Add(LB);
                    }
                    else
                    {
                        PB = new PictureBox();
                        PB.Location = new Point(WIDTH + 270, HEIGHT);
                        PB.Parent = CS;
                        PB.Size = new Size(125, 138);
                        PB.Image = imgg;
                        PB.SizeMode = PictureBoxSizeMode.Zoom;
                        LB = new Label();
                        LB.Location = new Point(WIDTH2 + 260, HEIGHT2);
                        LB.Parent = CS;
                        LB.Size = new Size(90, 13);
                        LB.Text = LQ.liquors_Name.ToString();
                        LB.AutoSize = true;
                        LB.Click += new EventHandler(pb_click);
                        CS.panel1.Controls.Add(PB);
                        CS.panel1.Controls.Add(LB);
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
}
