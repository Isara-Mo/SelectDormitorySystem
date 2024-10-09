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
    public partial class Form3_1 : Form
    {
        string StudentId;
        public Form3_1(string StudentId)
        {
            InitializeComponent();
            this.StudentId = StudentId;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            string sql="update Student set Sleep ='"+comboBox1.Text+"' where Id='"+StudentId+"'";
            Dao dao = new Dao();
            int i=dao.Excute(sql);
            if (i > 0)
            {
                MessageBox.Show("设置成功");
            }
            else
            {
                MessageBox.Show("设置失败");
            }

        }
    }
}
