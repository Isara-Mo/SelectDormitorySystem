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
    public partial class Form3_2 : Form
    {
        string StudentId;
        public Form3_2(string StudentId)
        {
            InitializeComponent();
            this.StudentId = StudentId;
        }

        private void Form3_2_Load(object sender, EventArgs e)
        {
            Table();
        }
        private void Table()
        {

            string sql = "Select Gender,Major,Sleep from Student where Id='"+StudentId+"'";
            Dao dao = new Dao();
            IDataReader dr = dao.Read(sql);
            string gender, major, sleep;
            if (dr.Read())
            {
                gender = dr["Gender"].ToString();
                major = dr["Major"].ToString();
                sleep = dr["Sleep"].ToString();
            }
            else
            {
                MessageBox.Show("未找到该学生的数据");
                return;
            }
            dr.Close();   //读取完后最好关掉读取，否则会造成资源浪费

            string sql2 = "select DormitoryBuilding,DormitoryNumberBegin,DormitoryNumberEnd from Distribute where Gender='"+gender+"' and Major='"+major+"'  and Sleep='"+sleep+"' ";
            Dao dao2 = new Dao();
            IDataReader dr2 = dao2.Read(sql2);
            if (dr2.Read())
            {
                string a;
                int begin, end;
                a = dr2["DormitoryBuilding"].ToString();
                begin = Convert.ToInt32(dr2["DormitoryNumberBegin"]);
                end = Convert.ToInt32(dr2["DormitoryNumberEnd"]);
                sql = "select Name from BuildingName where BuildingId='" + a + "'";
                IDataReader dr4 = dao.Read(sql);
                string building_name="", dor_name="";
                if (dr4.Read())
                {
                    building_name = dr4["Name"].ToString();
                }
                for (int i = begin; i <= end; i++)
                {
                    string sql3 = "select Num from DorNum where DormitoryNumber='"+i+"' and DormitoryBuilding='"+a+"' and Num!=4";
                    Dao dao3 = new Dao();
                    IDataReader dr3 = dao3.Read(sql3);
                    if (dr3.Read())
                    {
                        int num=Convert.ToInt32(dr3["Num"]);
                        int spare = 4 - num;
                        sql = "select DorName from DorName where Id='" + i + "'";
                        dr = dao.Read(sql);
                        if (dr.Read())
                        {
                            dor_name = dr["DorName"].ToString();
                        }
                        string[] str = { a, i.ToString(), spare.ToString() };
                        string[] str1 = { a ,i.ToString() ,building_name, dor_name.ToString(), spare.ToString() };
                        dataGridView1.Rows.Add(str1);
                    }
                    dr3.Close();
                }
            }
            dr2.Close();
        }
        public void refresh()
        {
            dataGridView1.Rows.Clear();
            Table();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            string building,dor,spare;
            building = dataGridView1.SelectedCells[0].Value.ToString();
            dor = dataGridView1.SelectedCells[1].Value.ToString();
            spare = dataGridView1.SelectedCells[2].Value.ToString();

            Form3_3 form3_3 = new Form3_3(building,dor,StudentId,this);
            form3_3.ShowDialog();

            

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
