using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp4
{
    internal class IngrCombine
    {
        CombineDB CD = new CombineDB();
        System.Windows.Forms.Label lbl1;
        System.Windows.Forms.Label lbl2;
        System.Windows.Forms.Label lbl3;
        public int WIDTH = 72;
        public int HEIGHT = 45;
        public void ViewIngr(CombineInformation CI, string name)
        {
            CD.combine_Name = name;  // 버튼 이름 가져오기
            DBConnection dbc = new DBConnection(); // DB 연결 및 DB 기능 사용을 위한 객체 생성
            OracleConnection conn = dbc.dbConn(); // DB 연결 상태를 저장하는 객체 생성
            OracleDataReader reader = null; // 쿼리문 실행 정보를 저장하는 객체 생성
            string query = "SELECT * FROM LIQUOR_COMBINE WHERE 주류조합명 = '" + CD.combine_Name + "'"; // 주류조합 테이블의 열 개수를 알아내는 문장
            reader = dbc.ExecuteSelectDRQuery(query);
            if (reader.Read()) CD.combine_Ingr1 = reader.GetString(2);
            CD.combine_Ingr2 = reader.GetString(3);
            CD.combine_Ingr3 = reader.GetString(4);

            lbl1 = new System.Windows.Forms.Label();
            lbl1.Location = new Point(WIDTH, HEIGHT+10);
            lbl1.Parent = CI;
            lbl1.Size = new Size(45, 15);
            lbl1.Text = "재료 1 : " + CD.combine_Ingr1.ToString();
            lbl1.AutoSize = true;
            lbl2 = new System.Windows.Forms.Label();
            lbl2.Location = new Point(WIDTH, HEIGHT + 54);
            lbl2.Parent = CI;
            lbl2.Size = new Size(45, 15);
            lbl2.Text = "재료 2 : " + CD.combine_Ingr2.ToString();
            lbl2.AutoSize = true;
            lbl3 = new System.Windows.Forms.Label();
            lbl3.Location = new Point(WIDTH, HEIGHT + 94);
            lbl3.Parent = CI;
            lbl3.Size = new Size(45, 15);
            lbl3.Text = "재료 3 : " + CD.combine_Ingr3.ToString();
            lbl3.AutoSize = true;
            CI.Controls.Add(lbl1);
            CI.Controls.Add(lbl2);
            CI.Controls.Add(lbl3);
        }
    }
}
