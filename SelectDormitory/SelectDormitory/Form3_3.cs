using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SelectDormitory
{

    
    public partial class Form3_3 : Form
    {
        string building, dor, StudentId;
        string building_name, dor_name;
        Form3_2 form3_2;
        int flag;   //表示选宿和查看宿舍两种，选宿为1，查看为0
        public Form3_3(string building,string dor,string StudentId,Form3_2 form3_2)
        {
            InitializeComponent();
            this.building = building;
            this.dor = dor; 
            this.StudentId = StudentId;
            this.form3_2 = form3_2;
            this.flag = 1;  //进行选宿阶段
        }
        public Form3_3(string building, string dor, string StudentId)
        {
            InitializeComponent();
            this.building = building;
            this.dor = dor;
            this.StudentId = StudentId;
            this.flag=0;    //任意阶段查看选宿
        }
        private void button1_Click(object sender, EventArgs e)
        {
            SelectDormitory("Bed1", 1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SelectDormitory("Bed2", 2);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SelectDormitory("Bed3", 3);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SelectDormitory("Bed4", 4);
        }
        //private void button1_Click(object sender, EventArgs e)
        //{
        //    UpdateDormitory();
        //    String sql = "update DorNum set Bed1='"+StudentId+ "'where DormitoryNumber="+dor+" and DormitoryBuilding= "+building+"";
        //    Dao dao = new Dao();
        //    int i = dao.Excute(sql);
        //    if (i > 0)
        //    {
        //        MessageBox.Show("选择床位成功");
        //        Table();
        //        string sql2 = "update DorNum set Num=Num+1 where DormitoryNumber=" + dor + " and DormitoryBuilding= " + building + " ";
        //        dao.Excute(sql2);
        //    }
        //    string sql3 = "update Student set DormitoryNumber='"+dor+ "' where Id='"+StudentId+"'";
        //    dao.Excute(sql3);
        //    string sql4 = "update Student set DormitoryBuilding='" + building + "' where Id='" + StudentId + "'";
        //    dao.Excute(sql4);
        //    string sql5 = "update Student set DormitoryBed=1 where Id='" + StudentId + "'";
        //    dao.Excute(sql5);
        //    Table();
        //}
        //private void button2_Click(object sender, EventArgs e)
        //{
        //    UpdateDormitory();
        //    String sql = "update DorNum set Bed2='" + StudentId + "'where DormitoryNumber=" + dor + " and DormitoryBuilding= " + building + "";
        //    Dao dao = new Dao();
        //    int i = dao.Excute(sql);
        //    if (i > 0)
        //    {
        //        MessageBox.Show("选择床位成功");
        //        Table();
        //        string sql2 = "update DorNum set Num=Num+1 where DormitoryNumber=" + dor + " and DormitoryBuilding= " + building + " ";
        //        dao.Excute(sql2);
        //    }
        //    string sql3 = "update Student set DormitoryNumber='" + dor + "' where Id='" + StudentId + "'";
        //    dao.Excute(sql3);
        //    string sql4 = "update Student set DormitoryBuilding='" + building + "' where Id='" + StudentId + "'";
        //    dao.Excute(sql4);
        //    string sql5 = "update Student set DormitoryBed=2 where Id='" + StudentId + "'";
        //    dao.Excute(sql5);
        //    Table();
        //}

        //private void button3_Click(object sender, EventArgs e)
        //{
        //    UpdateDormitory();
        //    String sql = "update DorNum set Bed3='" + StudentId + "'where DormitoryNumber=" + dor + " and DormitoryBuilding= " + building + "";
        //    Dao dao = new Dao();
        //    int i = dao.Excute(sql);
        //    if (i > 0)
        //    {
        //        MessageBox.Show("选择床位成功");
        //        Table();
        //        string sql2 = "update DorNum set Num=Num+1 where DormitoryNumber=" + dor + " and DormitoryBuilding= " + building + " ";
        //        dao.Excute(sql2);
        //    }
        //    string sql3 = "update Student set DormitoryNumber='" + dor + "' where Id='" + StudentId + "'";
        //    dao.Excute(sql3);
        //    string sql4 = "update Student set DormitoryBuilding='" + building + "' where Id='" + StudentId + "'";
        //    dao.Excute(sql4);
        //    string sql5 = "update Student set DormitoryBed=3 where Id='" + StudentId + "'";
        //    dao.Excute(sql5);
        //    Table();
        //}

        //private void button4_Click(object sender, EventArgs e)
        //{
        //    UpdateDormitory();
        //    String sql = "update DorNum set Bed4='" + StudentId + "'where DormitoryNumber=" + dor + " and DormitoryBuilding= " + building + "";
        //    Dao dao = new Dao();
        //    int i = dao.Excute(sql);
        //    if (i > 0)
        //    {
        //        MessageBox.Show("选择床位成功");
        //        Table();
        //        string sql2 = "update DorNum set Num=Num+1 where DormitoryNumber=" + dor + " and DormitoryBuilding= " + building + " ";
        //        dao.Excute(sql2);
        //    }
        //    string sql3 = "update Student set DormitoryNumber='" + dor + "' where Id='" + StudentId + "'";
        //    dao.Excute(sql3);
        //    string sql4 = "update Student set DormitoryBuilding='" + building + "' where Id='" + StudentId + "'";
        //    dao.Excute(sql4);
        //    string sql5 = "update Student set DormitoryBed=4 where Id='" + StudentId + "'";
        //    dao.Excute(sql5);
        //    Table();
        //}

        private void Form3_3_Load(object sender, EventArgs e)
        {
            Table();
            searchName();
            this.Text = "宿舍楼栋：" + building_name + " 宿舍号: " + dor_name;
        }

        private void Form3_3_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (flag == 1)
            {
                form3_2.refresh();
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void Table()
        {
            Label[] labels = new Label[] {label10, label11, label12, label13 };
            Button[] buttons = new Button[] { button1, button2, button3, button4 };
            for(int i = 1; i < 5; i++)
            {
                buttons[i - 1].Visible = true;
                labels[i - 1].Text = "未选";
            }
            if (flag == 0)          //查看宿舍时不可更改选宿
            {
                button1.Visible = false;
                button2.Visible = false;
                button3.Visible = false;
                button4.Visible = false;
            }
            string sql = "select Bed1,Bed2,Bed3,Bed4 from DorNum where DormitoryNumber='"+dor+"' and DormitoryBuilding='"+building+"'";
            Dao dao = new Dao();
            IDataReader dr = dao.Read(sql);
            if (dr.Read())
            {
                string[] bed=new string[5];
                bed[1] = dr["Bed1"].ToString();
                bed[2] = dr["Bed2"].ToString();
                bed[3] = dr["Bed3"].ToString();
                bed[4] = dr["Bed4"].ToString();
                for(int i = 1; i < 5; i++)
                {
                    if (!string.IsNullOrEmpty(bed[i]))      //不为NULL且不为空字符串
                    {
                        string sql2 = "select Name from Student where Id='" + bed[i] + "'";
                        IDataReader dr2 = dao.Read(sql2);
                        if (dr2.Read())
                        {
                            buttons[i-1].Visible = false;
                            
                        }labels[i-1].Text = dr2["Name"].ToString().Trim()+" 已选";
                        dr2.Close();
                    }
                }
            }
            dr.Close();   //读取完后最好关掉读取，否则会造成资源浪费        
        }
        public void searchName()
        {
            Dao dao = new Dao();
            IDataReader dr;
            string sql = "select Name from BuildingName where BuildingId='" + building + "'";
            dr = dao.Read(sql);
            if (dr.Read())
            {
                building_name = dr["Name"].ToString();
            }
            sql = "select DorName from DorName where Id='" + dor + "'";
            dr = dao.Read(sql);
            if (dr.Read())
            {
                dor_name = dr["DorName"].ToString();
            }
            dr.Close();   //读取完后最好关掉读取，否则会造成资源浪费
        }
        public void SelectDormitory(string bedColumn, int bedNumber)
        {
            UpdateDormitory();
            String sql = $"update DorNum set "+bedColumn+"='" + StudentId + "'where DormitoryNumber=" + dor + " and DormitoryBuilding= " + building + "";
            Dao dao = new Dao();
            int i = dao.Excute(sql);
            if (i > 0)
            {
                MessageBox.Show("选择床位成功");
                Table();
                string sql2 = "update DorNum set Num=Num+1 where DormitoryNumber=" + dor + " and DormitoryBuilding= " + building + " ";
                dao.Excute(sql2);
            }
            string sql3 = "update Student set DormitoryNumber='" + dor + "' where Id='" + StudentId + "'";
            dao.Excute(sql3);
            string sql4 = "update Student set DormitoryBuilding='" + building + "' where Id='" + StudentId + "'";
            dao.Excute(sql4);
            string sql5 = "update Student set DormitoryBed= '"+bedNumber+"' where Id='" + StudentId + "'";
            dao.Excute(sql5);
            Table();
        }
        public void UpdateDormitory()           //如果已经选过了，则清理痕迹
        {
            Dao dao = new Dao();  // 使用封装好的 Dao 类

            // Step 1: 查找 Student 表中的 DormitoryBed、DormitoryNumber 和 DormitoryBuilding
            string selectSql = "SELECT DormitoryBed, DormitoryNumber, DormitoryBuilding FROM Student WHERE Id = '" + StudentId + "'";

            SqlDataReader reader = dao.Read(selectSql);
            if (reader.Read())
            {
                // Step 2: 检查 DormitoryBed 是否为空
                if (reader["DormitoryBed"] != DBNull.Value)
                {
                    int dormitoryBed = Convert.ToInt32(reader["DormitoryBed"]);
                    int dormitoryNumber = Convert.ToInt32(reader["DormitoryNumber"]);
                    int dormitoryBuilding = Convert.ToInt32(reader["DormitoryBuilding"]);
                    reader.Close();  // 关闭读取器

                    // Step 3: 更新 DorNum 表中的 Num 减 1
                    string updateNumSql = "UPDATE DorNum SET Num = Num - 1 WHERE DormitoryNumber = " + dormitoryNumber + " AND DormitoryBuilding = " + dormitoryBuilding;
                    dao.Excute(updateNumSql);  // 执行更新 Num 的 SQL 语句

                    // Step 4: 根据 dormitoryBed 值将对应的 Bedx 设置为 NULL
                    string updateBedSql = "";

                    // 判断 dormitoryBed 的值，选择更新哪一个 Bed 列
                    switch (dormitoryBed)
                    {
                        case 1:
                            updateBedSql = "UPDATE DorNum SET Bed1 = NULL WHERE DormitoryNumber = " + dormitoryNumber + " AND DormitoryBuilding = " + dormitoryBuilding;
                            break;
                        case 2:
                            updateBedSql = "UPDATE DorNum SET Bed2 = NULL WHERE DormitoryNumber = " + dormitoryNumber + " AND DormitoryBuilding = " + dormitoryBuilding;
                            break;
                        case 3:
                            updateBedSql = "UPDATE DorNum SET Bed3 = NULL WHERE DormitoryNumber = " + dormitoryNumber + " AND DormitoryBuilding = " + dormitoryBuilding;
                            break;
                        case 4:
                            updateBedSql = "UPDATE DorNum SET Bed4 = NULL WHERE DormitoryNumber = " + dormitoryNumber + " AND DormitoryBuilding = " + dormitoryBuilding;
                            break;
                        default:
                            Console.WriteLine("DormitoryBed 值无效");
                            return;
                    }
                    // 执行更新 Bedx 的 SQL 语句
                    dao.Excute(updateBedSql);
                }
            }
            else
            {
                Console.WriteLine("未找到对应的 Student 记录");
            }

            reader.Close();  // 确保关闭读取器
        }



    }
}
