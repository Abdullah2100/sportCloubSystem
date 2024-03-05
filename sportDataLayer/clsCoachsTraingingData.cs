using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
namespace sportDataLayer
{
    public class clsCoachsTraingingData
    {
        static string connectionUrl = ConfigurationManager.ConnectionStrings["conncetionUrl"].ConnectionString;
        public static bool findCoachsTrainging(
             int id,
             ref int coacheID,
             ref int sportID,
             ref bool isAvaliable,
             ref string dayilyStartAt,
             ref string dayilyEndAt,
             ref int trainingDay,
             ref decimal fee
             )

        {
            bool isFound = false;
            string query = @"select * from CoachesTrainging where CoachsTraingingID = @id";

            try
            {

                using (SqlConnection con = new SqlConnection(connectionUrl))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                isFound = true;
                                sportID = (int)reader["sportID"];
                                coacheID = (int)reader["coacheID"];
                                isAvaliable = (bool)reader["isAvaliable"];
                                dayilyStartAt = (string)reader["dayilyStartAt"];
                                dayilyEndAt = (string)reader["dayilyEndAt"];
                                trainingDay = (int)reader["trainingDay"];
                                fee = (decimal)reader["fee"];

                            }
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                clsAppEventHandler.createNewEventLog(ex.Message);
                Console.WriteLine("errror is : " + ex.Message);
            }




            return isFound;

        }


        public static bool findCoachsTrainging(
             ref int id,
              int coacheID,
             ref int sportID,
             ref bool isAvaliable,
             ref string dayilyStartAt,
             ref string dayilyEndAt,
             ref int trainingDay,
             ref decimal fee
             )

        {
            bool isFound = false;
            string query = @"select * from CoachesTrainging where coacheID = @icoacheIDd";

            try
            {

                using (SqlConnection con = new SqlConnection(connectionUrl))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@coacheID", coacheID);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                isFound = true;
                                id = (int)reader["CoachsTraingingID"];
                                sportID = (int)reader["sportID"];
                                isAvaliable = (bool)reader["isAvaliable"];
                                dayilyStartAt = (string)reader["dayilyStartAt"];
                                dayilyEndAt = (string)reader["dayilyEndAt"];
                                trainingDay = (int)reader["trainingDay"];
                                fee = (decimal)reader["fee"];

                            }
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                clsAppEventHandler.createNewEventLog(ex.Message);
                Console.WriteLine("errror is : " + ex.Message);
            }




            return isFound;

        }


        public static int createCoachesTraineing(

             int coacheID,
             int sportID,
             bool isAvaliable,
             string dayilyStartAt,
             string dayilyEndAt,
             int trainingDay,
             decimal fee
            )
        {
            int id = 0;
            string query = @"
                            insert into CoachesTrainging                        
                            (coacheID,sportID,isAvaliable,dayilyStartAt,dayilyEndAt,trainingDay,fee)
                            values(@coachID,@sportID,@isAvaliable,@dayilyStartAt,@dayilyEndAt,@trainingDay,@fee) ;
                            select SCOPE_IDENTITY()";

            try
            {

                using (SqlConnection con = new SqlConnection(connectionUrl))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@coachID", coacheID);
                        cmd.Parameters.AddWithValue("@sportID", sportID);
                        cmd.Parameters.AddWithValue("@isAvaliable", isAvaliable);
                        cmd.Parameters.AddWithValue("@dayilyStartAt", dayilyStartAt);
                        cmd.Parameters.AddWithValue("@dayilyEndAt", dayilyEndAt);
                        cmd.Parameters.AddWithValue("@trainingDay", trainingDay);
                        cmd.Parameters.AddWithValue("@fee", fee);
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
                clsAppEventHandler.createNewEventLog(ex.Message);
                Console.WriteLine("errror is : " + ex.Message);
            }

            return id;
        }

        public static bool updateCoachTraining(
                 int id,
                 int coachID,
                 int sportID,
                 bool isAvaliable,
                 string dayilyStartAt,
                 string dayilyEndAt,
                 int trainingDay,
                 decimal fee
                )
        {
            bool isUpdate = false;
            string query = @"
                            update CoachesTrainging set
                            coacheID = @coachID,
                            sportID=@sportID,
                            isAvaliable =@isAvaliable,
                            dayilyStartAt=@dayilyStartAt,
                            dayilyEndAt = @dayilyEndAt,
                            trainingDay = @trainingDay,
                            fee = @fee
                            where CoachsTraingingID = @id";
            try
            {

                using (SqlConnection con = new SqlConnection(connectionUrl))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@coachID", coachID);
                        cmd.Parameters.AddWithValue("@sportID", sportID);
                        cmd.Parameters.AddWithValue("@isAvaliable", isAvaliable);
                        cmd.Parameters.AddWithValue("@dayilyStartAt", dayilyStartAt);
                        cmd.Parameters.AddWithValue("@dayilyEndAt", dayilyEndAt);
                        cmd.Parameters.AddWithValue("@trainingDay", trainingDay);
                        cmd.Parameters.AddWithValue("@fee", fee);
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
                clsAppEventHandler.createNewEventLog(ex.Message);
                Console.WriteLine("errror is : " + ex.Message);
            }

            return isUpdate;
        }

        public static bool changeCoachTraingingState(
             int id,
             bool isAvaliable
            )
        {
            bool isUpdate = false;
            string query = @"update CoachesTrainging set isAvaliable =@isAvaliable where CoachsTraingingID = @id";
            try
            {

                using (SqlConnection con = new SqlConnection(connectionUrl))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@isAvaliable", isAvaliable);
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
                clsAppEventHandler.createNewEventLog(ex.Message);
                Console.WriteLine("errror is : " + ex.Message);
            }

            return isUpdate;
        }



        public static bool deleteCoachTraining(
                int id
                             )
        {
            bool isDelete = false;
            string query = @"delete from CoachesTrainging where CoachsTraingingID = @id";
            try
            {

                using (SqlConnection con = new SqlConnection(connectionUrl))
                {
                    con.Open();
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
                clsAppEventHandler.createNewEventLog(ex.Message);
                Console.WriteLine("errror is : " + ex.Message);
            }

            return isDelete;
        }


        public static DataTable getAllcoachTraining()
        {
            bool isDelete = false;
            DataTable dtCaochesTraingin = new DataTable();
            try
            {

                using (SqlConnection con = new SqlConnection(connectionUrl))
                {
                    con.Open();
                    string query = @"select * from CoachesTraingView";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {

                        dtCaochesTraingin.Load(cmd.ExecuteReader());

                    }
                }

            }
            catch (Exception ex)
            {
                clsAppEventHandler.createNewEventLog(ex.Message);
                Console.WriteLine("errror is : " + ex.Message);
            }

            return dtCaochesTraingin;
        }

        public static DataTable getAllcoachTraining(int id)
        {
            DataTable dtCaochesTraingin = new DataTable();
            try
            {

                using (SqlConnection con = new SqlConnection(connectionUrl))
                {
                    con.Open();
                    string query = @"select * from CoachesTraingView where CoachsTraingingID = @id";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.Add("@id", id);

                        dtCaochesTraingin.Load(cmd.ExecuteReader());

                    }
                }

            }
            catch (Exception ex)
            {
                clsAppEventHandler.createNewEventLog(ex.Message);
                Console.WriteLine("errror is : " + ex.Message);
            }

            return dtCaochesTraingin;
        }



        public static bool isCoachTrainingByCoach(int coachID)
        {
            bool isFound = false;
            string query = @"select  found=1 CoachesTrainging where coachID = @coachID ";
            try
            {

                using (SqlConnection con = new SqlConnection(connectionUrl))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@coachID", coachID);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {

                            if (reader.HasRows)
                            {
                                isFound = true;
                            }

                        }

                    }
                }

            }
            catch (Exception ex)
            {
                clsAppEventHandler.createNewEventLog(ex.Message);
                Console.WriteLine("errror is : " + ex.Message);
            }

            return isFound;
        }


        public static bool isCoachTrainingActive(int id)
        {
            bool isActive = false;
            string query = @"select found=1 from CoachesTrainging where CoachsTraingingID = @id and isAvaliable =1 ";
            try
            {

                using (SqlConnection con = new SqlConnection(connectionUrl))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {

                            if (reader.HasRows)
                            {
                                isActive = true;
                            }

                        }

                    }
                }

            }
            catch (Exception ex)
            {
                clsAppEventHandler.createNewEventLog(ex.Message);
                Console.WriteLine("errror is : " + ex.Message);
            }

            return isActive;
        }


    }
}
