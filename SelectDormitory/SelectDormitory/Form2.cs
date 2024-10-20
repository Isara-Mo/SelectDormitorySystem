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

    public partial class Form2 : Form
    {
        Form1 form1;
        const int totalDormitoriesPerBuilding = 100; // 每栋楼90间宿舍
        const int peoplePerDormitory = 4;           // 每间宿舍可容纳4人
        const int buildingsPerGender = 2;           // 男生2栋楼，女生2栋楼

        public Form2(Form1 form1)
        {
            InitializeComponent();
            this.form1= form1;
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            form1.Show();
            this.Hide();
            form1.ResetForm();
        }

        private void 开放学生信息填写ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sql = "update Admin set model =1";
            Dao dao = new Dao();
            int i=dao.Excute(sql);
            if (i > 0)
            {
                MessageBox.Show("已经开放学生填写信息");
            }
            Table();

        }

        private void 关闭学生信息填写ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sqlcheck = "SELECT model FROM Admin WHERE model = 1";
            Dao dao = new Dao();
            IDataReader dr = dao.Read(sqlcheck);
            if (dr.Read())
            {
                dr.Close();
                string sql = "update Admin set model =2";
                dao.Excute(sql);

                //读取分别读取男生人数a1，女生人数a1来划分楼栋
                string sql2 = "SELECT " +
                             "(SELECT COUNT(*) FROM Student WHERE Gender = N'男') AS MaleCount, " +
                             "(SELECT COUNT(*) FROM Student WHERE Gender = N'女') AS FemaleCount";

                SqlDataReader reader = dao.Read(sql2);
                int maleCount = 0;
                int femaleCount = 0;
                if (reader.Read())
                {
                    maleCount = Convert.ToInt32(reader["MaleCount"]);
                    femaleCount = Convert.ToInt32(reader["FemaleCount"]);
                }
                reader.Close();
                string sql3 = "delete from Distribute"; ////删除原有的分布
                dao.Excute(sql3);

                //统计男生中，每个专业，Sleep为早睡、晚睡和NULL的分别由多少人
                int[,,] statsArray = GetSleepStatistics();
                //读取每种专业人数来分配宿舍区间。在区间内再根据早睡晚睡划分区间
                AllocateDormitories(statsArray);
                //将信息传入分配表
                MessageBox.Show("已经关闭学生填写信息，为学生分配宿舍。");
            }
            else
            {
                MessageBox.Show("请先开放学生填写信息！");
            }
            Table();


        }
        public int[,,] GetSleepStatistics()             //返回数组a[性别][专业][早晚睡]  代表不同分区人数
        {

            // 假设有2种性别，4个专业，并统计三类睡眠状态：早睡、晚睡、NULL
            int[,,] sleepStatistics = new int[2, 4, 3];

            Dao dao = new Dao();
            // 构建 SQL 更新语句，将 Sleep 列为 NULL 的记录更新为 '未填写'
            string updateSql = "UPDATE Student SET Sleep = N'未填写' WHERE Sleep IS NULL";
            dao.Excute(updateSql);

            // 打印操作结果到控制台
            Console.WriteLine("已将 Student 表中所有 Sleep 为 NULL 的记录更新为 '未填写'");

            string sql = @"
                SELECT 
                    Gender,
                    Major,
                    SUM(CASE WHEN Sleep = N'早睡' THEN 1 ELSE 0 END) AS EarlySleepCount,
                    SUM(CASE WHEN Sleep = N'晚睡' THEN 1 ELSE 0 END) AS LateSleepCount,
                    SUM(CASE WHEN Sleep = N'未填写' THEN 1 ELSE 0 END) AS NullSleepCount
                FROM Student
                GROUP BY Gender, Major";

            SqlDataReader reader = dao.Read(sql);

            while (reader.Read())
            {
                // 读取性别的值 (0: 男, 1: 女)
                int gender = reader["Gender"].ToString().Trim() == "男" ? 0 : 1;

                // 读取专业的值（假设专业为1-4）
                int major = Convert.ToInt32(reader["Major"]) - 1;

                // 存储每个性别、专业的睡眠统计数据到三维数组
                sleepStatistics[gender, major, 0] = Convert.ToInt32(reader["EarlySleepCount"]);  // 早睡人数
                sleepStatistics[gender, major, 1] = Convert.ToInt32(reader["LateSleepCount"]);   // 晚睡人数
                sleepStatistics[gender, major, 2] = Convert.ToInt32(reader["NullSleepCount"]);   // 未填写人数
            }
            reader.Close();
            // 返回三维数组

      

            return sleepStatistics;
        }
        public void AllocateDormitories(int[,,] statsArray)
        {
            int currentDormitoryNumberMale = 1;  // 男生当前分配的宿舍号起始值
            int currentDormitoryNumberFemale = 1;  // 女生当前分配的宿舍号起始值

            // 遍历性别、专业、睡眠类型
            for (int gender = 0; gender < 2; gender++)
            {
                int buildingStart = gender == 0 ? 1 : 3; // 男生从1栋开始，女生从3栋开始
                int currentDormitoryNumber = gender == 0 ? currentDormitoryNumberMale : currentDormitoryNumberFemale;

                for (int major = 0; major < 4; major++) // 假设专业是1到4
                {
                    for (int sleepType = 0; sleepType < 3; sleepType++) // 早睡、晚睡、未填写
                    {
                        int studentCount = statsArray[gender, major, sleepType];
                        if (studentCount > 0)
                        {
                            // 计算需要多少个宿舍
                            int requiredDorms = (int)Math.Ceiling(studentCount / (double)peoplePerDormitory);

                            // 计算分配宿舍的起始和结束宿舍号
                            int dormitoryBegin = currentDormitoryNumber;
                            int dormitoryEnd = dormitoryBegin + requiredDorms - 1;

                            // 确保宿舍号不会超过当前楼栋的最大宿舍号
                            while (dormitoryEnd > totalDormitoriesPerBuilding)
                            {
                                // 如果超出当前楼栋，分配到下一栋
                                buildingStart++;
                                dormitoryBegin = 1;
                                dormitoryEnd = requiredDorms;
                            }
                            // 将分配结果插入到 Distribute 表中
                            InsertDistributeData(buildingStart, dormitoryBegin, dormitoryEnd, gender, major, sleepType);

                            // 更新当前宿舍号到下一个未分配的宿舍
                            currentDormitoryNumber = dormitoryEnd + 1;
                        }
                    }
                }

                // 更新当前分配进度
                if (gender == 0)
                    currentDormitoryNumberMale = currentDormitoryNumber;
                else
                    currentDormitoryNumberFemale = currentDormitoryNumber;
            }
        }
        private void InsertDistributeData(int building, int dormitoryBegin, int dormitoryEnd, int gender, int major, int sleepType)
        {
            string genderStr = gender == 0 ? "男" : "女";
            string sleepStr = "";

            // 如果是早睡或晚睡，则根据 sleepType 设置值；如果是空值（NULL），则设置为 "未填写"
            if (sleepType == 0)
                sleepStr = "早睡";
            else if (sleepType == 1)
                sleepStr = "晚睡";
            else
                sleepStr = "未填写";  // 如果 Sleep 是空，则设置为 "未填写"

            // 打印分配结果到控制台（如果 Sleep 是空，则显示 "未填写"）
            Console.WriteLine($"性别: {genderStr}, 专业: {major + 1}, 睡眠类型: {sleepStr}, 分配楼栋: {building}, 起始宿舍: {dormitoryBegin}, 结束宿舍: {dormitoryEnd}");

            // 构建插入 SQL 语句
            string insertSql = $@"
                INSERT INTO Distribute (DormitoryBuilding, DormitoryNumberBegin, DormitoryNumberEnd, Gender, Major, Sleep)
                VALUES ({building}, {dormitoryBegin}, {dormitoryEnd}, N'{genderStr}', {major + 1}, N'{sleepStr}')";
            Dao dao = new Dao();      
            dao.Excute(insertSql);
        }

        private void 开放学生选宿ToolStripMenuItem1_Click(object sender, EventArgs e)
        {

            string sqlcheck = "SELECT model FROM Admin WHERE model = 2";
            Dao dao = new Dao();
            IDataReader dr = dao.Read(sqlcheck);
            if (dr.Read())
            {
                dr.Close();
                string sql = "update Admin set model =3";
                int i = dao.Excute(sql);
                if (i > 0)
                {
                    MessageBox.Show("已经开放学生选择宿舍");
                }

            }
            else
            {
                MessageBox.Show("请先关闭信息填写！");
            }
            Table();
        }

        private void 关闭学生选宿ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            string sqlcheck = "SELECT model FROM Admin WHERE model = 3";
            Dao dao = new Dao();
            IDataReader dr = dao.Read(sqlcheck);
            if (dr.Read())
            {
                dr.Close();
                string sql = "update Admin set model =4";
                int i = dao.Excute(sql);
                if (i > 0)
                {
                    MessageBox.Show("已经关闭学生选择宿舍");
                }
            }
            else
            {
                MessageBox.Show("还未开启学生选宿！");
            }
            Table();

        }
        public void AssignDormForUnassignedStudents()
        {
            Dao dao = new Dao();

            // Step 1: 查找 DormitoryBed 为空的学生
            string sql = "SELECT Id, Gender, Major, Sleep FROM Student WHERE DormitoryBed IS NULL";
            SqlDataReader reader = dao.Read(sql);

            while (reader.Read())
            {
                string studentId = reader["Id"].ToString();
                string gender = reader["Gender"].ToString();
                string major = reader["Major"].ToString();
                string sleep = reader["Sleep"].ToString();

                // Step 2: 根据 Gender, Major, Sleep 找到分配的宿舍区间
                string distributeSql = $@"
                    SELECT DormitoryBuilding, DormitoryNumberBegin, DormitoryNumberEnd
                    FROM Distribute
                    WHERE Gender = N'{gender}' AND Major = {major} AND Sleep = N'{sleep}'";

                SqlDataReader distributeReader = dao.Read(distributeSql);

                if (distributeReader.Read())
                {
                    int dormitoryBuilding = Convert.ToInt32(distributeReader["DormitoryBuilding"]);
                    int dormitoryNumberBegin = Convert.ToInt32(distributeReader["DormitoryNumberBegin"]);
                    int dormitoryNumberEnd = Convert.ToInt32(distributeReader["DormitoryNumberEnd"]);

                    // Step 3: 查找空闲床位
                    string dormNumSql = $@"
                        SELECT DormitoryNumber, Bed1, Bed2, Bed3, Bed4 
                        FROM DorNum 
                        WHERE DormitoryBuilding = {dormitoryBuilding} 
                        AND DormitoryNumber BETWEEN {dormitoryNumberBegin} AND {dormitoryNumberEnd}
                        AND Num < 4";

                    SqlDataReader dormNumReader = dao.Read(dormNumSql);

                    if (dormNumReader.Read())
                    {
                        int dormitoryNumber = Convert.ToInt32(dormNumReader["DormitoryNumber"]);
                        string bed1 = dormNumReader["Bed1"].ToString();
                        string bed2 = dormNumReader["Bed2"].ToString();
                        string bed3 = dormNumReader["Bed3"].ToString();
                        string bed4 = dormNumReader["Bed4"].ToString();

                        // 找到空闲的床位
                        string bedColumn = "";
                        if (string.IsNullOrEmpty(bed1)) bedColumn = "Bed1";
                        else if (string.IsNullOrEmpty(bed2)) bedColumn = "Bed2";
                        else if (string.IsNullOrEmpty(bed3)) bedColumn = "Bed3";
                        else if (string.IsNullOrEmpty(bed4)) bedColumn = "Bed4";

                        if (!string.IsNullOrEmpty(bedColumn))
                        {
                            // Step 4: 更新 DorNum 表中的床位和 Num
                            string updateDormSql = $@"
                                UPDATE DorNum 
                                SET {bedColumn} = '{studentId}', Num = Num + 1 
                                WHERE DormitoryBuilding = {dormitoryBuilding} 
                                AND DormitoryNumber = {dormitoryNumber}";
                            dao.Excute(updateDormSql);

                            // Step 5: 更新 Student 表中的床位和宿舍信息
                            string updateStudentSql = $@"
                                UPDATE Student 
                                SET DormitoryBed = {bedColumn.Last()}, DormitoryBuilding = {dormitoryBuilding}, DormitoryNumber = {dormitoryNumber} 
                                WHERE Id = '{studentId}'";
                            dao.Excute(updateStudentSql);
                        }
                    }
                    dormNumReader.Close();
                }
                distributeReader.Close();
            }
            reader.Close();
        }

        private void 为未选宿同学分配宿舍ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sql = "SELECT model FROM Admin WHERE model = 4";
            Dao dao = new Dao();
            IDataReader dr = dao.Read(sql);
            if (dr.Read())
            {
                dr.Close();
                AssignDormForUnassignedStudents();
                MessageBox.Show("为未选宿的同学分配宿舍！");
                sql = "update Admin set model =5";
                dao.Excute(sql);
            }
            else
            {
                MessageBox.Show("请先停止学生选宿！");
            }
            Table();
        }

        private void 重置学生选宿情况ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //情况学生表选宿情况
            string sql1 = "UPDATE Student SET DormitoryBuilding = NULL, DormitoryNumber = NULL, DormitoryBed = NULL,Sleep= NULL;";
            //情况宿舍表选宿情况
            string sql2 = "UPDATE DorNum SET Bed1 = NULL, Bed2 = NULL, Bed3 = NULL,Bed4=null,Num=0";
            //情况选宿分配表
            string sql3 = "delete from Distribute";
            //重置管理员model
            string sql4 = "update Admin set model =0";
            Dao dao= new Dao();
            dao.Excute(sql1);
            dao.Excute(sql2);
            dao.Excute(sql3);
            dao.Excute(sql4);
            MessageBox.Show("已重置学生选宿情况");
            Table();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            Table();
        }
        private void Table()
        {
            string sql = "SELECT model FROM Admin WHERE Id = 1";
            Dao dao = new Dao();
            IDataReader dr = dao.Read(sql);
            if (dr.Read())
            {
                string a;
                a = dr["model"].ToString().Trim();
                if (a == "0")
                {
                    label1.Text = "等待管理员开放作息填写";
                    label2.Text = "";
                }
                else if (a == "1")
                {
                    label1.Text = "正在进行学生信息填写";
                    label2.Text = "等待下一步操作:关闭学生信息填写";
                }
                else if (a == "2")
                {
                    label1.Text = "已关闭学生信息填写";
                    label2.Text = "等待下一步操作:开放学生选宿";
                }
                else if (a == "3")
                {
                    label1.Text = "正在进行学生选宿";
                    label2.Text = "等待下一步操作:关闭学生选宿";
                }
                else if(a == "4")
                {
                    label1.Text = "已关闭选宿";
                    label2.Text = "可为未选宿同学分配宿舍";
                }
                else
                {
                    label2.Text = "已为未选宿同学分配宿舍";
                }

            }
        }
    }
}
