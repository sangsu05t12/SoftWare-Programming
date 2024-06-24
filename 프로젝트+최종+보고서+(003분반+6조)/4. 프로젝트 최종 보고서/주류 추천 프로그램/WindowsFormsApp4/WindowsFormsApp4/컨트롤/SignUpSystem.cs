using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp4
{
    internal class SignUpSystem
    {
        UserDB user = new UserDB();
        public bool CheckID(string data) // 중복확인 메소드
        {
            user.user_ID = data;
            DBConnection dbc = new DBConnection(); // DB 연결 및 DB 기능 사용을 위한 객체 생성
            OracleConnection conn = dbc.dbConn(); // DB 연결 상태를 저장하는 객체 생성
            OracleDataReader reader = null; // 쿼리문 실행 정보를 저장하는 객체 생성

            string query = "SELECT * FROM USERS WHERE 아이디 = '" + user.user_ID + "'"; // i열의 ID를 알아내는 문장
            reader = dbc.ExecuteSelectDRQuery(query);
            if (reader.Read())
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void JoinData(string id, string pw, string name, string ctg) // 회원 가입 진행 메소드
        {
            DBConnection dbc = new DBConnection();
            OracleConnection conn = dbc.dbConn();
            int result = 0; // 실행결과를 저장하는 변수

            user.user_ID = id; // ID 텍스트 박스의 문장을 저장
            user.user_PW = pw; // PW 텍스트 박스의 문장을 저장
            user.user_Name = name; // 이름 텍스트 박스의 문장을 저장
            user.user_Ctg = ctg;

            string query = "INSERT INTO USERS (아이디, 이름, 비밀번호, 관심카테고리) VALUES ('" + user.user_ID + "', '" + user.user_PW + "', '" + user.user_Name + "', '" + user.user_Ctg + "')";
            // 회원 번호, ID, PW, 비밀번호, 관심카테고리 회원 테이블에 저장하는 문장

            result = dbc.ExecuteUpdateQuery(query); // 쿼리문 실행

            query = "COMMIT"; // 테이블 수정사항을 저장하는 문장
            result = dbc.ExecuteUpdateQuery(query);

            conn.Close();
        }
    }
}
