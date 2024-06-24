using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp4
{
    internal class ViewTInformationSystem
    {
        TLiquorDB TB = new TLiquorDB();
        System.Windows.Forms.PictureBox PB;  // 주류 이미지
        System.Windows.Forms.Label lb1; // 전통주명
        System.Windows.Forms.Label lb2; // 도수
        System.Windows.Forms.Label lb3; // 규격
        System.Windows.Forms.Label lb4; // 주원료
        System.Windows.Forms.Label lb5; // 제조사 
        System.Windows.Forms.Label lb6; // 설명
        public void ViewTInformation(TinformationUI TF, string name)
        {
            TB.tliquors_Name = name;
            DBConnection dbc = new DBConnection(); // DB 연결 및 DB 기능 사용을 위한 객체 생성
            OracleConnection conn = dbc.dbConn(); // DB 연결 상태를 저장하는 객체 생성
            OracleDataReader reader = null; // 쿼리문 실행 정보를 저장하는 객체 생성
            string query = "SELECT * FROM TLIQUOR WHERE 전통주명 = '" + TB.tliquors_Name + "'";
            reader = dbc.ExecuteSelectDRQuery(query); // 쿼리문을 실행하는 문장

            if (reader.Read()) TB.tliquors_URL = (byte[])reader.GetOracleBlob(6).Value;
            ImageConverter imageConverter = new ImageConverter();
            System.Drawing.Image imgg = (System.Drawing.Image)imageConverter.ConvertFrom(TB.tliquors_URL);
            PB = new PictureBox();   // PB 동적 생성
            PB.Location = new Point(8, 59);
            PB.Parent = TF;
            PB.Size = new Size(147, 201);
            PB.Image = imgg;
            PB.SizeMode = PictureBoxSizeMode.Zoom;

            TB.tliquors_Name = reader.GetString(0);
            lb1 = new Label();
            lb1.Location = new Point(171, 69);
            lb1.Parent = TF;
            lb1.Size = new Size(130, 19);
            lb1.Text = "전통주명 : " + TB.tliquors_Name.ToString();
            lb1.AutoSize = true;

            TB.tliquors_Alc = reader.GetString(1);
            lb2 = new Label();
            lb2.Location = new Point(171, 108);
            lb2.Size = new Size(130, 19);
            lb2.Parent = TF;
            lb2.Text = "도수 : " + TB.tliquors_Alc.ToString() +"%";
            lb2.AutoSize = true;

            TB.tliquors_Amt = reader.GetString(2);
            lb3 = new Label();
            lb3.Location = new Point(171, 152);
            lb3.Size = new Size(224, 19);
            lb3.Parent = TF;
            lb3.Text = "규격 : " + TB.tliquors_Amt.ToString();
            lb3.AutoSize = true;

            TB.tliquors_MIngr = reader.GetString(3);
            lb4 = new Label();
            lb4.Location = new Point(171, 202);
            lb4.Size = new Size(200, 30);
            lb4.Parent = TF;
            lb4.Text = "주원료 : " + TB.tliquors_MIngr.ToString();
            lb4.AutoSize = false;

            TB.tliquors_Co = reader.GetString(4);
            lb5 = new Label();
            lb5.Location = new Point(171, 245);
            lb5.Size = new Size(130, 19);
            lb5.Parent = TF;
            lb5.Text = "제조사 : " + TB.tliquors_Co.ToString();
            lb5.AutoSize = true;

            TB.tliquors_Exp = reader.GetString(5);
            lb6 = new Label();
            lb6.Location = new Point(12, 303);
            lb6.Size = new Size(400, 300);
            lb6.Parent = TF;
            lb6.Text = "설명\n\n" + TB.tliquors_Exp.ToString();
            lb6.AutoSize = false;


            TF.Controls.Add(PB);
            TF.Controls.Add(lb1);
            TF.Controls.Add(lb2);
            TF.Controls.Add(lb3);
            TF.Controls.Add(lb4);
            TF.Controls.Add(lb5);
            TF.Controls.Add(lb6);
        }
    }
}
