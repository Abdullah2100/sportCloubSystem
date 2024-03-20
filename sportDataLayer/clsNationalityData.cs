using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace sportDataLayer
{
    public class clsNationalityData
    {
        static string connectionUrl = ConfigurationManager.ConnectionStrings["conncetionUrl"].ConnectionString;

        public static bool findNationalityByID(int id, ref int addBy, ref string name)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionUrl))
                {
                    con.Open();
                    string query = @"select * from Nationalitys where nationalityID = @id";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                isFound = true;
                                name = (string)reader["name"];
                                addBy = (int)reader["addBy"];
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error is :" + ex.Message);
            }
            return isFound;
        }

        public static bool findNationalityByName(ref int id, ref int addBy, string name)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionUrl))
                {
                    con.Open();
                    string query = @"select * from Nationalitys where name =@name";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@name", name);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                isFound = true;
                                id = (int)reader["nationalityID"];
                                addBy = (int)reader["addBy"];
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error is :" + ex.Message);
            }
            return isFound;
        }


        public static DataTable getAllNationality()
        {
            DataTable dtNaionalties = new DataTable();

            try
            {
                using (SqlConnection con = new SqlConnection(connectionUrl))
                {
                    con.Open();
                    string query = @"select * from Nationalitys";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {

                            if (reader.HasRows)
                                dtNaionalties.Load(reader);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error is :" + ex.Message);
            }
            return dtNaionalties;
        }



        public static bool UpdateNationality(int id, string name)
        {
            bool isUpdate = false;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionUrl))
                {
                    con.Open();
                    string query = @"Update   Nationalitys  set   name =@name where nationalityID = id";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@name", name);
                        cmd.Parameters.AddWithValue("@id", id);
                        int result = cmd.ExecuteNonQuery();
                        if (result > 0)
                        {
                            isUpdate = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error is :" + ex.Message);
            }
            return isUpdate;
        }


    }
}
