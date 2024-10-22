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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SelectDormitory
{
    public partial class Form4 : Form
    {
        String StudentId;
        Form3 form3;
        public Form4(string StudentId,Form3 form3)
        {
            InitializeComponent();
            this.StudentId = StudentId;
            this.form3 = form3;
        }

        private void Form4_FormClosed(object sender, FormClosedEventArgs e)
        {

        }
        private string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "")
            {
                MessageBox.Show("输入不完整，请检查", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (textBox1.Text.Length >20 || textBox2.Text.Length>20 || textBox3.Text.Length>20)
            {
                MessageBox.Show("输入的密码太长，请检查", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (textBox1.Text.Length < 2 || textBox2.Text.Length <2 || textBox3.Text.Length <2)
            {
                MessageBox.Show("输入的密码太短，请检查", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (textBox3.Text != textBox2.Text)
            {
                MessageBox.Show("两次密码输入不一致，请检查", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                string opw, npw, tpw;
                opw=textBox1.Text.ToString();
                npw=textBox2.Text.ToString();
                tpw=textBox3.Text.ToString();
                string hashed_opw = HashPassword(opw);
                string hashed_npw = HashPassword(npw);
                string sql = "select * from Student where Id='" + StudentId + "' and Password='" + hashed_opw + "'";
                Dao dao = new Dao();
                IDataReader dr = dao.Read(sql);
                if (dr.Read())
                {
                    sql = "update Student set Password='" + hashed_npw + "'where Id='" + StudentId + "'";
                    int i=dao.Excute(sql);
                    if (i > 0)
                    {
                        MessageBox.Show("修改成功");
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("原始密码输入错误！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
    }
}
