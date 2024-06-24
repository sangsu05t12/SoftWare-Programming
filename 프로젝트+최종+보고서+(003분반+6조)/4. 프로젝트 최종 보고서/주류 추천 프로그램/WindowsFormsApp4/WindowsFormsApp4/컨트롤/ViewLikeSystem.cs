using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace WindowsFormsApp4
{
    internal class ViewLikeSystem
    {
        System.Windows.Forms.Button Btn;
        public int WIDTH = 40;
        public int HEIGHT = 46;
        LiquorsDB LB = new LiquorsDB();
        LikeDB LK = new LikeDB();
        public void ViewLike(DisplayUI DP)
        {
            LK.ID = MainUI.rid;
            int num = 0 ;
            string[] text = new string[17];
            DBConnection dbc = new DBConnection(); // DB 연결 및 DB 기능 사용을 위한 객체 생성
            OracleConnection conn = dbc.dbConn(); // DB 연결 상태를 저장하는 객체 생성
            OracleDataReader reader = null; // 쿼리문 실행 정보를 저장하는 객체 생성

            int count = 1;
            string query = "SELECT COUNT (*) FROM LIKEDB WHERE 회원아이디 = '" + LK.ID + "'";
            reader = dbc.ExecuteSelectDRQuery(query);
            if (reader.Read()) num = reader.GetInt32(0);

            string query2 = "SELECT 주류명 FROM LIKEDB WHERE 회원아이디 = '" + LK.ID + "'";
            reader = dbc.ExecuteSelectDRQuery(query2);
            while (reader.Read())
            {
                text[count] = reader.GetString(0);
                count++;
            }
            for (int i = 1; i < num + 1; i++)
            {
                LB.liquors_Name = text[i];
                query = "SELECT * FROM LIQUOR WHERE 주류명 = '" + LB.liquors_Name + "'";
                reader = dbc.ExecuteSelectDRQuery(query);
                reader.Read();
                LB.liquors_URL = (byte[])reader.GetOracleBlob(3).Value;
                ImageConverter imageConverter = new ImageConverter();
                System.Drawing.Image imgg = (System.Drawing.Image)imageConverter.ConvertFrom(LB.liquors_URL);
                Btn = new Button();   // BTN 동적 생성
                Btn.Location = new Point(WIDTH, HEIGHT);
                Btn.Size = new Size(60, 70);
                Btn.BackgroundImage = imgg;
                Btn.BackgroundImageLayout = ImageLayout.Zoom;
                Btn.FlatStyle = FlatStyle.Flat;
                Btn.Click += new EventHandler(btn_click);
                LB.liquors_Name = reader.GetString(0);
                Btn.Name = LB.liquors_Name;
                DP.pictureBox1.Controls.Add(Btn);

                WIDTH += 80;

                if (i % 4 == 0)
                {
                    WIDTH = 40;
                    HEIGHT += 116;
                }
            }
        }
        public void btn_click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            LB.liquors_Name = btn.Name;
            InformationUI CI = new InformationUI(LB.liquors_Name);
            CI.Show();
        }
    }
}
