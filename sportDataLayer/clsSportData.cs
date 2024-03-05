using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace sportDataLayer
{
    public class clsSportData
    {
        static string connectionUrl = ConfigurationManager.ConnectionStrings["conncetionUrl"].ConnectionString;


        //Find Sport
        public static bool findSportByID(int id, ref string name, ref DateTime createdDate, ref bool isActive)
        {
            bool isFound = false;
            try

            {
                using (SqlConnection con = new SqlConnection(connectionUrl))
                {
                    con.Open();
                    string query = @"select top 1 * FROM Sports where sportID = @id ";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                isFound = true;
                                name = (string)reader["name"];
                                createdDate = (DateTime)reader["createdDate"];
                                isActive = (bool)reader["isActive"];

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


        public static bool findSportByName(ref int id, string name, ref DateTime createdDate, ref bool isActive)
        {
            bool isFound = false;
            try

            {
                using (SqlConnection con = new SqlConnection(connectionUrl))
                {
                    con.Open();
                    string query = @"select top 1 * FROM Sports where name = @name ";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@name", name);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                isFound = true;
                                id = (int)reader["sportID"];
                                createdDate = (DateTime)reader["createdDate "];
                                isActive = (bool)reader["isActive"];

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




        // Curde Operation

        public static bool addNewSport(string name)
        {
            bool isAdd = false;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionUrl))
                {
                    con.Open();
                    string query = @"insert into Sports (name) values (@name) ";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@name", name);
                        int reader = cmd.ExecuteNonQuery();
                        if (reader > 0)
                        {
                            isAdd = true;
                        }
                    }
                }
            }
            catch (Exception ex)

            {
                Console.WriteLine("Error is :" + ex.Message);
                isAdd = false;
            }
            return isAdd;
        }

        public static bool updateSport(int id, string name, bool isActive)
        {
            bool isAdd = false;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionUrl))
                {
                    con.Open();
                    string query = @"update Sports set name =@name ,isActive =@isActive where sportID = @id ";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@name", name);
                        cmd.Parameters.AddWithValue("@isActive", isActive);
                        int reader = cmd.ExecuteNonQuery();
                        if (reader > 0)
                        {
                            isAdd = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error is :" + ex.Message);
                isAdd = false;
            }
            return isAdd;
        }

        public static bool updateSportState(int id, bool isActive)
        {
            bool isAdd = false;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionUrl))
                {
                    con.Open();
                    string query = @"update Sports set isActive =@isActive where sportID = @id ";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@id", id);

                        cmd.Parameters.AddWithValue("@isActive", isActive);

                        int reader = cmd.ExecuteNonQuery();
                        if (reader > 0)
                        {
                            isAdd = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error is :" + ex.Message);
                isAdd = false;
            }
            return isAdd;
        }

        public static bool deleteSport(int id)
        {
            bool isAdd = false;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionUrl))
                {

                    con.Open();
                    string query = @"DELETE FROM Sports WHERE sportID= @id ";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@id", id);

                        int reader = cmd.ExecuteNonQuery();
                        if (reader > 0)
                        {
                            isAdd = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error is :" + ex.Message);
                isAdd = false;
            }
            return isAdd;
        }

        public static DataTable getAllSport()
        {
            DataTable dtSportList = new DataTable();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionUrl))
                {
                    con.Open();
                    string query = @"select * FROM Sports ";
                    using (SqlCommand cmd = new SqlCommand(query, con))

                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())

                        {
                            if (reader.HasRows)
                            {
                                dtSportList.Load(reader);
                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine("Error is :" + ex.Message);
            }
            return dtSportList;
        }



        public static DataTable getAllActiveSportName()
        {
            DataTable dtSportList = new DataTable();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionUrl))
                {
                    con.Open();
                    string query = @"select name FROM Sports  where isActive = 1";
                    using (SqlCommand cmd = new SqlCommand(query, con))

                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())

                        {
                            if (reader.HasRows)
                            {
                                dtSportList.Load(reader);
                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine("Error is :" + ex.Message);
            }
            return dtSportList;
        }


        //isExist 
        public static bool isExistByName(string name)

        {
            bool isExist = false;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionUrl))
                {
                    con.Open();
                    string query = @"select find=1 from Sports where name = @name ";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@name", name);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                isExist = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)

            {
                Console.WriteLine("Error is :" + ex.Message);
            }
            return isExist;
        }
        public static bool isSportActiveByID(int id)
        {
            bool isExist = false;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionUrl))
                {
                    con.Open();
                    string query = @"select find=1 from Sports where sportID = @id and isActive =1  ";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                isExist = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)

            {
                Console.WriteLine("Error is :" + ex.Message);
            }
            return isExist;
        }
        public static bool isSportActiveByName(string name)
        {
            bool isExist = false;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionUrl))
                {
                    con.Open();
                    string query = @"select find=1 from Sports where name = @name and isActive =1  ";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@name", name);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                isExist = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)

            {
                Console.WriteLine("Error is :" + ex.Message);
            }
            return isExist;
        }

        public static bool changeTheSportState(int id, bool state)
        {
            bool isAdd = false;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionUrl))
                {
                    con.Open();
                    string query = @"update Sports set isActive =@isActive where sportID = @id ";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@id", id);

                        cmd.Parameters.AddWithValue("@isActive", state);

                        int reader = cmd.ExecuteNonQuery();
                        if (reader > 0)
                        {
                            isAdd = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error is :" + ex.Message);
                isAdd = false;
            }
            return isAdd;
        }
    }
}
