using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SelectDormitory
{
    public partial class Form3 : Form
    {
        Form1 form1;
        string StudentId;
        public Form3(Form1 form1,string StudentId)
        {
            InitializeComponent();
            this.form1 = form1;
            this.StudentId = StudentId;

        }

        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            form1.Show();
            this.Hide();
            form1.ResetForm();
        }

        private void 填写作息习惯ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sqlcheck = "SELECT model FROM Admin WHERE model = 1";
            Dao dao = new Dao();
            IDataReader dr = dao.Read(sqlcheck);
            if (dr.Read())
            {
                dr.Close();
                Form3_1 form3_1 = new Form3_1(StudentId);
                form3_1.ShowDialog();
            }
            else
            {
                MessageBox.Show("目前未开放填写！");
            }

        }

        private void 选择宿舍ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sqlcheck = "SELECT model FROM Admin WHERE model = 3";
            Dao dao = new Dao();
            IDataReader dr = dao.Read(sqlcheck);
            if (dr.Read())
            {
                dr.Close();
                Form3_2 form3_2 = new Form3_2(StudentId);
                form3_2.ShowDialog();
            }
            else
            {
                MessageBox.Show("目前未开放选择宿舍！");
            }
        }

        private void 查看自己的宿舍ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sqlcheck = "SELECT DormitoryBuilding,DormitoryNumber,DormitoryBed FROM Student WHERE Id='" + StudentId+ "' and DormitoryBed is not null";
            Dao dao = new Dao();
            IDataReader dr = dao.Read(sqlcheck);
            if (dr.Read())
            {
                string a, b, c;
                a = dr["DormitoryBuilding"].ToString();
                b = dr["DormitoryNumber"].ToString();
                dr.Close();
                Form3_3 form3_3 = new Form3_3(a,b,StudentId);
                form3_3.ShowDialog();
            }
            else
            {
                MessageBox.Show("还未被分配宿舍！");
            }
        }

        private void 修改密码ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4(StudentId,this);
            form4.ShowDialog();
        }
        private void Table()
        {
            string sql = "SELECT model FROM Admin WHERE Id = 1";
            Dao dao= new Dao();
            IDataReader dr = dao.Read(sql);
            if (dr.Read())
            {
                string a;
                a = dr["model"].ToString().Trim();
                if (a == "0")
                {
                    label1.Text = "请等待管理员开放作息填写";
                }
                else if(a == "1")
                {
                    label1.Text = "请进行作息填写";
                }
                else if (a == "2")
                {
                    label1.Text = "请等待管理员开放选宿";
                }
                else if (a == "3")
                {
                    label1.Text = "请进行选宿";
                }
                else
                {
                    label1.Text = "已关闭选宿，请查看自己的宿舍情况";
                }
   
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            Table();
        }
    }
}
