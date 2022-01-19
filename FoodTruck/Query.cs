using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace FoodTruck
{
    public class Query
    {
        private static string connectionString = "Data Source=DESKTOP-7KQ5544;" +
                                        "Initial Catalog=FoodTruck;" +
                                        "Integrated Security=SSPI;";
        public static bool SQLBoolQuery(string query)
        {
            using (SqlConnection con = new SqlConnection())
            {
                con.ConnectionString = connectionString;
                try
                {
                    con.Open();
                    SqlCommand q = new SqlCommand(query, con);
                    using(SqlDataReader r = q.ExecuteReader())
                    {
                        r.Read();
                        return Convert.ToBoolean(r[0]);
                    }
                }
                catch (Exception ex)
                {
                    return false;
                }
                finally
                {
                    con.Close();
                }
            }
        }

        public static string[] SQLArrayQuery(string query)
        {
            string[] array;
            using (SqlConnection con = new SqlConnection())
            {
                con.ConnectionString = connectionString;
                try
                {
                    con.Open();
                    SqlCommand q = new SqlCommand(query, con);
                    using (SqlDataReader r = q.ExecuteReader())
                    {
                        r.Read();
                        array = new string[r.FieldCount];
                        for(int i = 0; i < r.FieldCount; i++)
                        {
                            array[i] = Convert.ToString(r[i]);
                        }
                    }
                }
                catch (Exception ex)
                {
                    array = new string[0];
                }
                finally
                {
                    con.Close();
                }
            }
            return array;
        }
    }
}
