using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace sportDataLayer
{
    public class clsCoachData
    {
        static string connectionUrl = ConfigurationManager.ConnectionStrings["conncetionUrl"].ConnectionString;

        public static bool findCoachByID
            (
            int id,
            ref int personID,
            ref int? addBy,
            ref DateTime startTraingDate,
            ref DateTime? endTraingDate,
            ref bool isActive)
        {
            bool isFound = false;
            try
            {


                using (SqlConnection con = new SqlConnection(connectionUrl))
                {


                    con.Open();
                    string query = @"select * from Coaches where coacheID = @id";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                isFound = true;
                                personID = (int)reader["personID"];
                                addBy = (int?)reader["addBy"];
                                startTraingDate = (DateTime)reader["startTraingDate"];
                                if (reader["endTraingDate"] == DBNull.Value)
                                    endTraingDate = null;
                                else
                                    endTraingDate = (DateTime)reader["endTraingDate"];
                                isActive = (bool)reader["isActive"];
                            }
                        }

                    }


                }

            }
            catch (Exception ex)
            {
                clsAppEventHandler.createNewEventLog(ex.Message);
                Console.WriteLine("Error is : " + ex.Message);
            }
            return isFound;

        }


        public static bool findCoachByPersonID
        (
            ref int id,
            int personID,
            ref int? addBy,
            ref DateTime startTraingDate,
            ref DateTime? endTraingDate,
            ref bool isActive)
        {
            bool isFound = false;
            try
            {


                using (SqlConnection con = new SqlConnection(connectionUrl))
                {


                    con.Open();
                    string query = @"select * from Coaches where personID = @personID";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@personID", personID);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                isFound = true;
                                id = (int)reader["coacheID"];
                                addBy = (int?)reader["addBy"];
                                startTraingDate = (DateTime)reader["startTraingDate"];
                                if (reader["endTraingDate"] == DBNull.Value)
                                    endTraingDate = null;
                                else
                                    endTraingDate = (DateTime)reader["endTraingDate"];
                                isActive = (bool)reader["isActive"];
                            }
                        }

                    }


                }

            }
            catch (Exception ex)
            {
                clsAppEventHandler.createNewEventLog(ex.Message);

                Console.WriteLine("Error is : " + ex.Message);
            }
            return isFound;

        }


        public static int createCoach
           (
               int personID,
                int? addBy,
                DateTime startTraingDate,
                DateTime? endTraingDate,
                bool isActive)
        {
            int id = 0;
            try
            {


                using (SqlConnection con = new SqlConnection(connectionUrl))
                {


                    con.Open();
                    string query = @"
                                  INSERT INTO Coaches (personID,addBy,startTraingDate,isActive)
                                  VALUES(@personID,@addBy,@startTraingDate,@isActive);
                                  select SCOPE_IDENTITY();";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@personID", personID);
                        cmd.Parameters.AddWithValue("@startTraingDate", personID);
                        cmd.Parameters.AddWithValue("@isActive", personID);
                        if (addBy == null)
                            cmd.Parameters.AddWithValue("@addBy", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@addBy", addBy);
                        object result = cmd.ExecuteScalar();
                        if (result != null && int.TryParse(result.ToString(), out int cachID))
                        {
                            id = cachID;
                        }

                    }


                }

            }
            catch (Exception ex)
            {
                clsAppEventHandler.createNewEventLog(ex.Message);

                Console.WriteLine("Error is : " + ex.Message);
            }
            return id;

        }


        public static bool updateCoach
                (
                    int id,
                    int personID,
                    int? addBy,
                     DateTime startTraingDate,
                     DateTime? endTraingDate,
                     bool isActive)
        {
            bool isUpdated = false;
            try
            {


                using (SqlConnection con = new SqlConnection(connectionUrl))
                {


                    con.Open();
                    string query = @"
                                  Update Coaches set personID = @personID
                                  ,startTraingDate = @startTraingDate,
                                   addBy = @addBy
                                   isActive =@isActive
                                   where coacheID = @id;
                                   ";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@personID", personID);
                        cmd.Parameters.AddWithValue("@startTraingDate", personID);
                        cmd.Parameters.AddWithValue("@isActive", personID);
                        if (addBy == null)
                            cmd.Parameters.AddWithValue("@addBy", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@addBy", addBy); int result = cmd.ExecuteNonQuery();
                        if (result > 0)
                        {
                            isUpdated = true;
                        }

                    }


                }

            }
            catch (Exception ex)
            {
                clsAppEventHandler.createNewEventLog(ex.Message);

                Console.WriteLine("Error is : " + ex.Message);
            }
            return isUpdated;

        }



        public static bool deleteCoach(int id)
        {
            bool isUpdated = false;
            try
            {


                using (SqlConnection con = new SqlConnection(connectionUrl))
                {


                    con.Open();
                    string query = @"delete from Coaches where coacheID == @id;
                                   ";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@id", id);

                        int result = cmd.ExecuteNonQuery();
                        if (result > 0)
                        {
                            isUpdated = true;
                        }

                    }


                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error is : " + ex.Message);
                clsAppEventHandler.createNewEventLog(ex.Message);

            }
            return isUpdated;

        }

        public static DataTable getAllCoaches()
        {
            DataTable dtPoepleList = new DataTable();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionUrl))
                {
                    con.Open();
                    string query = @"select * from Coaches_view";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                                dtPoepleList.Load(reader);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                clsAppEventHandler.createNewEventLog(ex.Message);

                Console.WriteLine("Error is :" + ex.Message);
            }
            return dtPoepleList;

        }

        public static DataTable getAllActiveCoachsName()
        {
            DataTable dtPoepleList = new DataTable();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionUrl))
                {
                    con.Open();
                    string query = @" select (p.fristName+' '+p.secondName+' '+p.thirdName+' '+ p.familyName ) as fullName
                                      from Peoples p inner join Nationalitys n on p.nationalityID = n.nationalityID
                                      inner join Coaches c on c.personID = p.personID  where isActive =1 ";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                                dtPoepleList.Load(reader);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                clsAppEventHandler.createNewEventLog(ex.Message);

                Console.WriteLine("Error is :" + ex.Message);
            }
            return dtPoepleList;

        }



        public static bool UpdateCoachState(

        int id,
         bool isActive
        )
        {
            bool isAdd = false;
            try
            {
                using (SqlConnection con = new SqlConnection(connectionUrl))
                {
                    con.Open();
                    string query = @"update Coaches set
                                     isActive = @isActive
                                     where coacheID = @id
";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@isActive", isActive);
                        int result = cmd.ExecuteNonQuery();
                        if (result > 0)
                        {
                            isAdd = true;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                clsAppEventHandler.createNewEventLog(ex.Message);

                Console.WriteLine("Error is :" + ex.Message);
            }
            return isAdd;

        }




        public static bool isCoachActive(int id)
        {
            bool isUpdated = false;
            try
            {


                using (SqlConnection con = new SqlConnection(connectionUrl))
                {


                    con.Open();
                    string query = @"select found =1 from Coaches where coacheID = @id and isActive =1 ";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@id", id);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {

                            if (reader.Read())
                            {
                                isUpdated = true;
                            }
                        }

                    }


                }

            }
            catch (Exception ex)
            {
                clsAppEventHandler.createNewEventLog(ex.Message);

                Console.WriteLine("Error is : " + ex.Message);
            }
            return isUpdated;

        }


        public static bool isCoachExistByID(int id)
        {
            bool isUpdated = false;
            try
            {


                using (SqlConnection con = new SqlConnection(connectionUrl))
                {


                    con.Open();
                    string query = @"select found =1 from Coaches where coacheID = @id ";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@id", id);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {

                            if (reader.Read())
                            {
                                isUpdated = true;
                            }
                        }

                    }


                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error is : " + ex.Message);
                clsAppEventHandler.createNewEventLog(ex.Message);

            }
            return isUpdated;

        }


        public static bool isCoachExistByPersonID(int personID)
        {
            bool isUpdated = false;
            try
            {


                using (SqlConnection con = new SqlConnection(connectionUrl))
                {


                    con.Open();
                    string query = @"select found =1 from Coaches where personID = @personID ";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@personID", personID);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {

                            if (reader.Read())
                            {
                                isUpdated = true;
                            }
                        }

                    }


                }

            }
            catch (Exception ex)
            {
                clsAppEventHandler.createNewEventLog(ex.Message);

                Console.WriteLine("Error is : " + ex.Message);
            }
            return isUpdated;

        }


    }
}
