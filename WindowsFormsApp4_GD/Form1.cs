using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.IO;

namespace WindowsFormsApp4_GD
{
    public partial class Form1 : Form
    {
        //아이디 비번
        static Dictionary<string, string> idnpw = new Dictionary<string, string>();
        //아이디 전번
        static Dictionary<string, string> idnphn = new Dictionary<string, string>();

        public Form1()
        {
            InitializeComponent();

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "파일 열기";
            openFileDialog1.Filter = "텍스트 파일 (*.txt)|*.txt|모든 파일 (*.*)|*.*";

            // 사용자가 파일을 선택하고 확인 버튼을 눌렀는지 확인
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog1.FileName;

                try
                {
                    MessageBox.Show("파일 열기 성공");

                    using (StreamReader sr = new StreamReader(filePath))
                    {
                        string line;
                        while ((line = sr.ReadLine()) != null)  //한줄씩 읽고
                        {
                            string[] temp = line.Split(',');   //,기준으로 스플릿

                            if (temp.Length == 2)
                            {
                                string id = temp[0].Trim();
                                string pw = temp[1].Trim();

                                idnpw[id] = pw;
                                idnphn[id] = null;

                            }
                            else if (temp.Length == 3)
                            {
                                string id = temp[0].Trim();
                                string pw = temp[1].Trim();
                                string phone = temp[2].Trim();

                                idnpw[id] = pw;
                                idnphn[id] = phone;
                            }
                        }
                    }
                }
                catch (IOException ex)
                {
                    MessageBox.Show($"파일을 여는 중 오류 발생: {ex.Message}");
                }
            }
        }

        //로그인 버튼
        private void button2_Click(object sender, EventArgs e)
        {
            if (idnpw.ContainsKey(textBox1.Text))   //아이디 존재
            {
                string id = textBox1.Text;
                idnpw.TryGetValue(id, out var pw);
                if (pw.Equals(textBox2.Text))   //비밀번호 일치
                {
                    idnphn.TryGetValue(id, out var phn);
                    if (phn != null)    //전화번호 존재
                    {
                        MessageBox.Show($"아이디>{id}\n전화번호>{phn}");
                    }
                    else
                    {
                        MessageBox.Show($"아이디>{id}\n전화번호>없음");
                    }
                }
                else
                {
                    MessageBox.Show("비밀번호가 일치하지 않습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("아이디가 존재하지 않습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
