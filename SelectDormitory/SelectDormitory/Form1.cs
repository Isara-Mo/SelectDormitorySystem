using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace SelectDormitory
{
    public partial class Form1 : Form
    {
        string StudentId;
        private Point initialPictureBoxLocation;
        public Form1()
        {
            InitializeComponent();
            // 保存图片的初始位置
            initialPictureBoxLocation = pictureBox1.Location;
        }

        

        //登录事件
        private void button1_Click(object sender, EventArgs e)
        {
            if (Login())
            {
                timer1.Start();
                textBox1.Visible = false;
                textBox2.Visible = false;
                comboBox1.Visible = false;
                button1.Visible = false;
                button2.Visible = false;
                label1.Visible = false;
                label2.Visible = false;
                label3.Visible = false;

            }
            else
            {

            }
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
        private bool Login()
        {
            if (textBox1.Text == "" || textBox2.Text == "" || comboBox1.Text == "")
            {
                MessageBox.Show("输入不完整，请检查", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            string hashedPassword = HashPassword(textBox2.Text);
            if (comboBox1.Text == "学生")
            {
                string sql = "select* from Student where Name ='" + textBox1.Text + "' and Password='" + hashedPassword + "' ";
                Dao dao = new Dao();
                IDataReader dr = dao.Read(sql);
                if (dr.Read())
                {
                    StudentId = dr["id"].ToString();
                    return true;
                }
                else
                {
                    MessageBox.Show("账号或密码错误", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            else if (comboBox1.Text == "管理员")
            {
                string sql = "select* from Admin where Id='" + textBox1.Text + "' and Password='" + hashedPassword + "'";
                Dao dao = new Dao();
                IDataReader dr = dao.Read(sql);
                if (dr.Read())
                {
                    return true;
                }
                else
                {
                    MessageBox.Show("账号或密码错误", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = null;
            textBox2.Text = null;
            comboBox1.Text = null;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (pictureBox1.Location.Y > -250)
            {
                pictureBox1.Location = new Point(pictureBox1.Location.X, pictureBox1.Location.Y - 4);

            }
            else
            {
                if (comboBox1.Text == "管理员")
                {
                    Form2 form2 = new Form2(this);
                    form2.Show();
                    this.Hide();
                }
                else if (comboBox1.Text == "学生")
                {
                    Form3 form3 = new Form3(this,StudentId);
                    form3.Show();
                    this.Hide();
                }
                timer1.Stop();
            }

        }
        public void ResetForm()
        {
            // 将图片移回初始位置
            pictureBox1.Location = new Point(initialPictureBoxLocation.X, initialPictureBoxLocation.Y);

            // 显示或隐藏其他控件，根据需要重置其他属性
            textBox1.Visible = true;
            textBox2.Visible = true;
            comboBox1.Visible = true;
            button1.Visible = true;
            button2.Visible = true;
            label1.Visible = true;
            label2.Visible = true;
            label3.Visible = true;

            
        }
    }
}
