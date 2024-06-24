using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp4
{
    internal class DBConnection
    {
        // DB 연결 설정 문자열
        string oradb = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=182.211.7.65)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=xe))); User Id=C##lss;Password=1234;";

        OracleConnection conn = null; // DB 연결 상태 저장 변수

        public OracleConnection dbConn() // DB 연결 메소드
        {
            try
            {
                conn = new OracleConnection(oradb);
                conn.Open();
                Console.WriteLine("DB 접속 성공");
            }

            catch (Exception e) { Console.WriteLine("DB 접속 실패 : " + e.Message); }

            return conn;
        }

        public OracleDataReader ExecuteSelectDRQuery(string query) // SELECT문 실행 메소드
        {
            OracleDataReader dr = null; // 명령 실행 결과 저장 변수
            try
            {
                OracleCommand cmd = new OracleCommand(); // 명령 실행을 위한 변수
                cmd.Connection = conn; // 명령 실행을 위해 DB 연결
                cmd.CommandText = query; // 전달받은 명령문을 DB에 저장
                dr = cmd.ExecuteReader(); // 명령 실행 후 실행 결과 저장
            }

            catch (Exception e) // 예외 발생 시 예외 문장 출력
            {
                Console.WriteLine(e.ToString());
            }

            return dr; // 명령 실행 결과 반환
        }

        public int ExecuteUpdateQuery(string query) // INSERT, DELETE, UPDATE문 실행 메소드
        {
            int result = 0;

            try
            {
                OracleCommand cmd = new OracleCommand(); // 명령 실행을 위한 변수
                cmd.Connection = conn; // 명령 실행을 위해 DB 연결
                cmd.CommandText = query; // 전달받은 명령문을 DB에 저장
                result = cmd.ExecuteNonQuery(); // 명령 실행 후 실행 결과 저장
            }

            catch (Exception e) // 예외 발생 시 예외 문장 출력
            {
                Console.WriteLine(e.ToString());
            }

            return result; // 명령 실행 결과 반환
        }
    }
}