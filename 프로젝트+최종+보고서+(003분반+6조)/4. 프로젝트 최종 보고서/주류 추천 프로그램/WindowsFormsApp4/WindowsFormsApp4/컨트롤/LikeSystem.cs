using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WindowsFormsApp4
{
    internal class LikeSystem
    {
        public void CreateLike(string name, string id)
        {
            LikeDB LK = new LikeDB();
            LK.ID = id;
            LK.Name = name;
            int count = 0;
            DBConnection dbc = new DBConnection(); // DB 연결 및 DB 기능 사용을 위한 객체 생성
            OracleConnection conn = dbc.dbConn(); // DB 연결 상태를 저장하는 객체 생성
            OracleDataReader reader = null; // 쿼리문 실행 정보를 저장하는 객체 생성
            int result = 0;
            string query = "SELECT COUNT(*) FROM LIKEDB";
            reader = dbc.ExecuteSelectDRQuery(query);
            if (reader.Read()) count = reader.GetInt32(0);
            LK.No = count + 1;
            query = "INSERT INTO LIKEDB VALUES (" + LK.No + ", '" + LK.Name + "', '" + LK.ID + "')";
            result = dbc.ExecuteUpdateQuery(query);
            query = "COMMIT";
            reader = dbc.ExecuteSelectDRQuery(query);
        }
        public void DeLike(string name, string id) 
        {
            LikeDB LK = new LikeDB();
            LK.ID = id;
            LK.Name = name;
            int count = 0;
            DBConnection dbc = new DBConnection(); // DB 연결 및 DB 기능 사용을 위한 객체 생성
            OracleConnection conn = dbc.dbConn(); // DB 연결 상태를 저장하는 객체 생성
            OracleDataReader reader = null; // 쿼리문 실행 정보를 저장하는 객체 생성
            int result = 0;
            string query = "SELECT COUNT (*) FROM LIKEDB";
            reader = dbc.ExecuteSelectDRQuery(query);
            if (reader.Read()) count = reader.GetInt32(0);
            query = "DELETE FROM LIKEDB WHERE 주류명 = '" + LK.Name + "' AND 회원아이디 = '" + LK.ID + "'";
            result = dbc.ExecuteUpdateQuery(query);
            query = "COMMIT";
            reader = dbc.ExecuteSelectDRQuery(query);

            query = "UPDATE LIKEDB SET 좋아요번호 = ROWNUM";
            result = dbc.ExecuteUpdateQuery(query);
            query = "COMMIT";
            reader = dbc.ExecuteSelectDRQuery(query);
        }
        public bool CheckLike(string id)
        {
            LikeDB LK = new LikeDB();
            LK.ID = id;
            int count = 0;
            DBConnection dbc = new DBConnection(); // DB 연결 및 DB 기능 사용을 위한 객체 생성
            OracleConnection conn = dbc.dbConn(); // DB 연결 상태를 저장하는 객체 생성
            OracleDataReader reader = null; // 쿼리문 실행 정보를 저장하는 객체 생성
            string query = "SELECT COUNT (*) FROM LIKEDB WHERE 회원아이디 = '" + LK.ID + "'";
            reader = dbc.ExecuteSelectDRQuery(query);
            if (reader.Read()) count = reader.GetInt32(0);

            if (count == 16)
                return false;
            else return true;
        }
    }
}
