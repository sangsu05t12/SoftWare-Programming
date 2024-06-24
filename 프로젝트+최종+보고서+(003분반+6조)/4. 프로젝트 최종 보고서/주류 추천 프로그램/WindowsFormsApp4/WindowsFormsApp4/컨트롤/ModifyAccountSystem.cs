using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp4
{
    internal class ModifyAccountSystem
    {
        UserDB user = new UserDB();
        public void LoadAccount(MyAccountUI MA)
        {
            user.user_ID = MainUI.rid;
            DBConnection dbc = new DBConnection(); // DB 연결 및 DB 기능 사용을 위한 객체 생성
            OracleConnection conn = dbc.dbConn(); // DB 연결 상태를 저장하는 객체 생성
            OracleDataReader reader = null; // 쿼리문 실행 정보를 저장하는 객체 생성
            string query = "SELECT * FROM USERS WHERE 아이디 ='" + user.user_ID + "'";
            reader = dbc.ExecuteSelectDRQuery(query); // 쿼리문을 실행하는 문장
            if (reader.Read()) user.user_Name = reader.GetString(1);
            user.user_PW = reader.GetString(2);
            user.user_Ctg = reader.GetString(3);

            MA.textBox1.Text = user.user_ID;
            MA.textBox2.Text = user.user_Name;
            MA.textBox3.Text = user.user_PW;
            MA.comboBox1.SelectedItem = user.user_Ctg;
        }
        public void SaveAccount(MyAccountUI MA)
        {
            user.user_ID = LoginUI.rid;
            user.user_PW = MA.textBox3.Text;
            user.user_Ctg = MA.comboBox1.Text;
            DBConnection dbc = new DBConnection(); // DB 연결 및 DB 기능 사용을 위한 객체 생성
            OracleConnection conn = dbc.dbConn(); // DB 연결 상태를 저장하는 객체 생성
            OracleDataReader reader = null; // 쿼리문 실행 정보를 저장하는 객체 생성
            int result = 0; // 쿼리문 실행 정보를 저장하는 객체 생성
            string query = "UPDATE USERS SET  비밀번호 = '" + user.user_PW + "', 관심카테고리 = '" + user.user_Ctg + "' WHERE 아이디 = '" + user.user_ID + "'";

            result = dbc.ExecuteUpdateQuery(query); // 쿼리문을 실행하는 문장

            query = "COMMIT"; // 테이블의 변경사항을 저장하는 문장
            reader = dbc.ExecuteSelectDRQuery(query);

            MessageBox.Show("수정이 완료되었습니다.");
            MA.Close();
        }
    }
}
