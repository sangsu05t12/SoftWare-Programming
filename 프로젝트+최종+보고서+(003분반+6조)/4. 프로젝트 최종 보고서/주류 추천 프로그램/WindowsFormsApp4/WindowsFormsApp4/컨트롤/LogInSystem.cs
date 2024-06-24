using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp4
{
    internal class LogInSystem
    {
        UserDB user = new UserDB();
        public bool LoginData(TextBox textBox1, TextBox textBox2) // 로그인 성공, 실패를 수행하는 메소드
        {
            int num = 0; // 회원 테이블의 레코드 수를 저장할 변수
            user.user_ID = textBox1.Text;
            user.user_PW = textBox2.Text;
            string eid = null; // 회원 테이블에 있는 ID를 저장하기 위한 변수
            string epw = null; // 회원 테이블에 있는 PW를 저장하기 위한 변수
            bool bi = false; // ID 일치, 불일치 정보를 저장하는 변수
            bool bp = false; // PW 일치, 불일치 정보를 저장하는 변수

            DBConnection dbc = new DBConnection(); // DB 연결 및 DB 기능 사용을 위한 객체 생성
            OracleConnection conn = dbc.dbConn(); // DB 연결 상태를 저장하는 객체 생성
            OracleDataReader reader = null; // 쿼리문 실행 정보를 저장하는 객체 생성

            string query = "SELECT COUNT (*) FROM USERS"; // 회원 테이블의 열 개수를 알아내는 문장
            reader = dbc.ExecuteSelectDRQuery(query); // 쿼리문을 실행하는 문장

            if (reader.Read()) num = reader.GetInt32(0); // 회원 테이블의 열 개수 저장

            query = "SELECT 아이디 FROM USERS WHERE 아이디 = " + "'" + user.user_ID + "'"; // i번째 열 레코드의 ID를 알아내는 문장
            reader = dbc.ExecuteSelectDRQuery(query);
            if (reader.Read()) eid = reader.GetString(0); // 알아낸 ID를 저장

            query = "SELECT 비밀번호 FROM USERS WHERE 비밀번호 = " + "'" + user.user_PW + "'"; // i번째 열 레코드의 PW를 알아내는 문장
            reader = dbc.ExecuteSelectDRQuery(query);
            if (reader.Read()) epw = reader.GetString(0); // 알아낸 PW를 저장

            if (textBox1.Text == "") // ID를 입력하지 않았다면 실행되는 조건문
            {
                MessageBox.Show("아이디를 입력하세요!", "아이디 입력 오류", MessageBoxButtons.OK, MessageBoxIcon.Error); // ID 입력 오류 메세지 창
                textBox1.Focus();
                return false;
            }

            else if (textBox2.Text == "") // PW를 입력하지 않았다면 실행되는 조건문
            {
                MessageBox.Show("비밀번호를 입력하세요!", "비밀번호 입력 오류", MessageBoxButtons.OK, MessageBoxIcon.Error); // PW 입력 오류 메세지 창
                textBox2.Focus();
                return false;
            }

            else
            {
                if (user.user_ID.Equals(eid)) // 테이블의 ID와 입력받은 ID가 일치하면 bi에 true 저장
                {
                    bi = true;

                    if (user.user_PW.Equals(epw)) // 테이블의 PW와 입력받은 PW가 일치하면 bp에 true 저장
                    {
                        bp = true;
                    }
                }
            }

            if (bi)
            {
                if (bp) // bi와 bp가 true이면 ID와 PW가 일치하는 것이므로 true 반환
                {
                    return true;
                }

                else // bi는 true이나 bp가 false이면 비밀번호 불일치 메세지 창을 띄움
                {
                    MessageBox.Show("비밀번호가 일치하지 않습니다!", "비밀번호 불일치", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox2.Focus();
                    return false;
                }
            }

            else // bi가 false이면 존재하지 않는 ID 메세지 창을 띄움
            {
                MessageBox.Show("존재하지 않는 아이디입니다!", "존재하지 않는 아이디", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Focus();
                return false;
            }
        }
    }
}
