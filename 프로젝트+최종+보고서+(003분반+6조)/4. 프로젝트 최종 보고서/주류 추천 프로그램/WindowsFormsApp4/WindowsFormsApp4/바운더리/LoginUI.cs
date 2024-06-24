using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace WindowsFormsApp4
{
    public partial class LoginUI : Form
    {
        LogInSystem LS = new LogInSystem();
        public LoginUI()
        {
            InitializeComponent();
        }
        static public string rid = null;

        private void button1_Click(object sender, EventArgs e)
        {
            if (LS.LoginData(textBox1, textBox2) == true) // LoginData의 반환 값이 true라면 로그인 성공
            {
                MessageBox.Show("로그인 성공", "로그인 성공", MessageBoxButtons.OK, MessageBoxIcon.Asterisk); // 로그인 성공 메세지 창
                MainUI M = new MainUI(textBox1.Text);
                this.Hide();
                M.StartPosition = FormStartPosition.Manual;
                M.Location = new Point(520, 150);
                M.ShowDialog();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SignUPUI SUI = new SignUPUI();
            SUI.StartPosition = FormStartPosition.Manual;
            SUI.Location = new Point(550, 200);
            SUI.Show();
        }

        private void LoginUI_Load(object sender, EventArgs e)
        {
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(600, 200);
        }
    }
}
