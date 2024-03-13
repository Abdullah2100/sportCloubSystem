using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace sportDataLayer
{
    public class clsMemberSubscriptionsData
    {
        static string connectionUrl = ConfigurationManager.ConnectionStrings["conncetionUrl"].ConnectionString;

        public static bool findMemberSubscriptionByID(
             int id,
            ref int memberID,
            ref int CoachsTraingingID,
            ref decimal fee,
            ref DateTime startDate,
            ref DateTime endDate
            )
        {
            bool isFound = false;
            try
            {
                using (SqlConnection con = new SqlConnection(connectionUrl))
                {
                    con.Open();
                    string query = @"select * from MemberSubscriptions where memberSubscriptionID = @id";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                isFound = true;
                                memberID = (int)reader["memberID"];
                                CoachsTraingingID = (int)reader["CoachsTraingingID"];
                                fee = (decimal)reader["fee"];
                                startDate = (DateTime)reader["startDate"];
                                endDate = (DateTime)reader["endDate"];
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

        public static bool findMemberSubscriptionByMemberID(
                 ref int id,
                  int memberID,
                 ref int CoachsTraingingID,
                 ref decimal fee,
                 ref DateTime startDate,
                 ref DateTime endDate
                 )
        {
            bool isFound = false;
            try
            {
                using (SqlConnection con = new SqlConnection(connectionUrl))
                {
                    con.Open();
                    string query = @"select * from MemberSubscriptions where memberID = @memberID";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@memberID", memberID);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                isFound = true;
                                id = (int)reader["memberSubscriptionID"];
                                CoachsTraingingID = (int)reader["CoachsTraingingID"];
                                fee = (decimal)reader["fee"];
                                startDate = (DateTime)reader["startDate"];
                                endDate = (DateTime)reader["endDate"];
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





        public static int createMemberSubscriptions(
              int memberID,
              int CoachsTraingingID,
              decimal fee,
              DateTime startDate,
              DateTime endDate
            )
        {
            int id = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(connectionUrl))
                {
                    con.Open();
                    string query = @"INSERT INTO MemberSubscriptions
                                    (CoachsTraingingID,memberID,fee,startDate,endDate)
                                    VALUES (@CoachsTraingingID,@memberID,@fee,@startDate ,@endDate);
                                    select SCOPE_IDENTITY();";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {

                        cmd.Parameters.AddWithValue("@CoachsTraingingID", CoachsTraingingID);
                        cmd.Parameters.AddWithValue("@memberID", memberID);
                        cmd.Parameters.AddWithValue("@fee", fee);
                        cmd.Parameters.AddWithValue("@startDate", startDate);
                        cmd.Parameters.AddWithValue("@endDate", endDate);

                        object result = cmd.ExecuteScalar();
                        if (result != null && int.TryParse(result.ToString(), out int resultID))
                        {
                            id = resultID;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error is :" + ex.Message);
            }
            return id;

        }


        public static bool deleteMemberSubscription(int id)
        {
            bool isDelete = false;
            try
            {
                using (SqlConnection con = new SqlConnection(connectionUrl))
                {
                    con.Open();
                    string query = @"delete from MemberSubscriptions where memberSubscriptionID = @id";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {

                        cmd.Parameters.AddWithValue("@id", id);
                        int result = cmd.ExecuteNonQuery();
                        if (result > 0)
                        {
                            isDelete = true;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error is :" + ex.Message);
            }
            return isDelete;

        }


        public static bool updateMemberSubscriptoion(
            int id,
            int memberID,
             int CoachsTraingingID,
             decimal fee,
             DateTime startDate,
             DateTime endDate
            )
        {
            bool isAdd = false;
            try
            {
                using (SqlConnection con = new SqlConnection(connectionUrl))
                {
                    con.Open();
                    string query = @"UPDATE MemberSubscriptions
                                     SET CoachsTraingingID = @CoachsTraingingID,memberID = @memberID, 
                                    fee = @fee,startDate = @startDate,endDate = @endDate
                                     WHERE memberSubscriptionID = @id";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@CoachsTraingingID", CoachsTraingingID);
                        cmd.Parameters.AddWithValue("@memberID", memberID);
                        cmd.Parameters.AddWithValue("@fee", fee);
                        cmd.Parameters.AddWithValue("@startDate", startDate);
                        cmd.Parameters.AddWithValue("@endDate", endDate);
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
                Console.WriteLine("Error is :" + ex.Message);
            }
            return isAdd;

        }



        public static DataTable getAllMemberSubscription()
        {
            DataTable dtPoepleList = new DataTable();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionUrl))
                {
                    con.Open();
                    string query = @"select * from MemberSubscritptionView";

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
                Console.WriteLine("Error is :" + ex.Message);
            }
            return dtPoepleList;

        }


        public static bool isMemberSubscriptionExistByID(
            int peopleID
            )
        {
            bool isFound = false;
            try
            {
                using (SqlConnection con = new SqlConnection(connectionUrl))
                {
                    con.Open();
                    string query = @"select found =1 from MemberSubscriptions where memberSubscriptionID = @id";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {

                        cmd.Parameters.AddWithValue("@peopleID", peopleID);
                        object result = cmd.ExecuteScalar();
                        isFound = (bool)result == true ? true : false;

                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error is :" + ex.Message);
            }
            return isFound;

        }


        public static bool isMemberSubscriptionExistByMemberID(int memberID)
        {
            bool isFound = false;
            try
            {
                using (SqlConnection con = new SqlConnection(connectionUrl))
                {
                    con.Open();
                    string query = @"select found =1 from MemberSubscriptions where memberID = @memberID";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@memberID", memberID);
                        object result = cmd.ExecuteScalar();
                        isFound = (bool)result == true ? true : false;

                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error is :" + ex.Message);
            }
            return isFound;

        }

    }
}
