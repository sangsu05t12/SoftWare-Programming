using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;
using System.Xml.Linq;

namespace WindowsFormsApp4
{
    public partial class SignUPUI : Form
    {
        public SignUPUI()
        {
            InitializeComponent();
        }

        SignUpSystem SU = new SignUpSystem();

        private void button3_Click(object sender, EventArgs e) // 취소 버튼
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)  // 중복 확인 버튼
        {
            if (textBox1.Text == "") MessageBox.Show("아이디를 입력하세요.");
            else
            {
                if (SU.CheckID(textBox1.Text) == true) // 중복확인 메소드의 반환값이 true이면 중복이 안된다는 것이므로 사용가능 메세지 창을 띄움
                    MessageBox.Show("사용가능한 아이디입니다!", "아이디 사용 가능", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                else // 그렇지 않다면 중복된 아이디 메세지 창을 띄움
                    MessageBox.Show("중복된 아이디입니다!", "아이디 중복", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)  // 가입하기
        {
            if (textBox1.Text == "") MessageBox.Show("아이디를 입력하세요.");
            else if (textBox2.Text == "") MessageBox.Show("이름을 입력하세요.");
            else if (textBox3.Text == "") MessageBox.Show("비밀번호를 입력하세요.");
            else if (comboBox1.Text == "") MessageBox.Show("관심 카테고리를 입력하세요.");
            else
            {
                if (SU.CheckID(textBox1.Text) == true) // 중복확인 메소드가 true일 때 실행되는 조건문
                {
                    SU.JoinData(textBox1.Text, textBox2.Text, textBox3.Text, comboBox1.Text); // CheckID()와 CheckPW()가 모두 true이면 회원가입 메소드 실행
                    MessageBox.Show("회원가입이 완료되었습니다!", "회원가입 완료", MessageBoxButtons.OK, MessageBoxIcon.Asterisk); // 회원가입 완료 메세지 창
                    Close(); // 회원가입 폼을 닫음
                }
                else MessageBox.Show("중복확인을 해주세요!", "중복 미확인", MessageBoxButtons.OK, MessageBoxIcon.Error); // 아니면 중복확인 오류
            }
        }

        private void SignUPUI_Load(object sender, EventArgs e)
        {
            this.Activate();
            this.textBox1.Focus();
        }
    }
}

