using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Forms;

namespace WindowsFormsApp4
{
    internal class ViewInformationSystem
    {
        System.Windows.Forms.PictureBox PB;
        System.Windows.Forms.Label lb1;
        System.Windows.Forms.Label lb2;
        System.Windows.Forms.Label lb3;
        System.Windows.Forms.Label lb4;
        LiquorsDB LQ = new LiquorsDB();
        UserDB user = new UserDB();
        public void ViewInformation(InformationUI IF, string name)
        {
            LQ.liquors_Name = name;
            DBConnection dbc = new DBConnection(); // DB 연결 및 DB 기능 사용을 위한 객체 생성
            OracleConnection conn = dbc.dbConn(); // DB 연결 상태를 저장하는 객체 생성
            OracleDataReader reader = null; // 쿼리문 실행 정보를 저장하는 객체 생성
            string query = "SELECT * FROM LIQUOR WHERE 주류명 = '" + LQ.liquors_Name + "'";
            reader = dbc.ExecuteSelectDRQuery(query); // 쿼리문을 실행하는 문장
            if (reader.Read()) LQ.liquors_URL = (byte[])reader.GetOracleBlob(3).Value;
            ImageConverter imageConverter = new ImageConverter();
            System.Drawing.Image imgg = (System.Drawing.Image)imageConverter.ConvertFrom(LQ.liquors_URL);

            PB = new PictureBox();   // PB 동적 생성
            PB.Location = new Point(100, 60);
            PB.Parent = IF;
            PB.Size = new Size(210, 258);
            PB.Image = imgg;
            PB.SizeMode = PictureBoxSizeMode.Zoom;

            LQ.liquors_Name = reader.GetString(0);
            lb1 = new Label();
            lb1.Location = new Point(100, 320);
            lb1.Parent = IF;
            lb1.Size = new Size(200, 19);
            lb1.Text = "주류명 : " + LQ.liquors_Name.ToString();
            lb1.AutoSize = false;

            LQ.liquors_Ctg = reader.GetString(4);
            lb2 = new Label();
            lb2.Location = new Point(100, 350);
            lb2.Size = new Size(130, 19);
            lb2.Parent = IF;
            lb2.Text = "종류 : " + LQ.liquors_Ctg.ToString();
            lb2.AutoSize = false;

            LQ.liquors_Co = reader.GetString(2);
            lb3 = new Label();
            lb3.Location = new Point(100, 380);
            lb3.Size = new Size(224, 19);
            lb3.Parent = IF;
            lb3.Text = "제조사명 : " + LQ.liquors_Co.ToString();
            lb3.AutoSize = false;

            LQ.liquors_Alc = reader.GetString(1);
            lb4 = new Label();
            lb4.Location = new Point(100, 410);
            lb4.Size = new Size(130, 19);
            lb4.Parent = IF;
            lb4.Text = "도수 : " + LQ.liquors_Alc.ToString() +"%";
            lb4.AutoSize = false;

            IF.Controls.Add(PB);
            IF.Controls.Add(lb1);
            IF.Controls.Add(lb2);
            IF.Controls.Add(lb3);
            IF.Controls.Add(lb4);

            user.user_ID = MainUI.rid;
            query = "SELECT* FROM LIKEDB WHERE 주류명 = '" + LQ.liquors_Name + "' AND 회원아이디 = (SELECT 아이디 FROM USERS WHERE 아이디 = '" + user.user_ID + "')";
            reader = dbc.ExecuteSelectDRQuery(query);
 
                if (reader.Read())
                {
                    IF.checkBox1.Checked = true;
                }
                else
                {
                    IF.checkBox1.Checked = false;
                }
        }
    }
}
