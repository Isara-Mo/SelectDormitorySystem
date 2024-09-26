using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelectDormitory
{
    internal class Dao
    {
        public SqlConnection Connection()
        {
            string str = "Data Source=Matebook14;Initial Catalog=SelectDormitory;Integrated Security=True;Encrypt=False;Max Pool Size=800;Min Pool Size=10;Connection Timeout=600;";
            SqlConnection sc = new SqlConnection(str);
            sc.Open();  //打开链接
            return sc;
        }
        public SqlCommand Command(string sql)
        {
            SqlCommand sc = new SqlCommand(sql, Connection());
            return sc;
        }
        //返回受影响的行数 用于deleet updata insert
        public int Excute(string sql)
        {
            return Command(sql).ExecuteNonQuery();
        }
        //返回Reader对象 用于select
        public SqlDataReader Read(string sql)
        {
            return Command(sql).ExecuteReader();
        }
    }
}
