using sportDataLayer.Util;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sportDataLayer
{
    public  class clsMemberData
    {
        public static bool findMemberByPersonID(
            ref int id,
            int personID,
            ref bool isActive
    )
        {
            bool isFound = false;
            try
            {
                using (SqlConnection con = new SqlConnection(clsConnection.connectionUrl))
                {
                    con.Open();
                    string query = @"select * from Members where personID = @personID";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@personID", personID);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                isFound = true;
                                id = (int)reader["memberID"];
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

        public static bool findMemberByID(
     
            int id,
            ref int personID,
            ref bool isActive
    )
        {
            bool isFound = false;
            try
            {
                using (SqlConnection con = new SqlConnection(clsConnection.connectionUrl))
                {
                    con.Open();
                    string query = @"select * from Members where memberID = @id";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                isFound = true;
                                personID = (int)reader["personID"];
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







        public static int createMember(
             int personID,
             bool isActive
            )
        {
            int id = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(clsConnection.connectionUrl))
                {
                    con.Open();
                    string query = @"insert into Members (personID,isActive)values (@personID,@isActive);
                                     select SCOPE_IDENTITY();";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {

                        cmd.Parameters.AddWithValue("@personID", personID);
                        cmd.Parameters.AddWithValue("@isActive", isActive);
                      
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


        public static bool deleteMemberByID(int id)
        {
            bool isDelete = false;
            try
            {
                using (SqlConnection con = new SqlConnection(clsConnection.connectionUrl))
                {
                    con.Open();
                    string query = @"delete from Members where memberID = @id";

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


        public static bool updateMember(
          
            int id,
             int personID,
             bool isActive
            )
        {
            bool isAdd = false;
            try
            {
                using (SqlConnection con = new SqlConnection(clsConnection.connectionUrl))
                {
                    con.Open();
                    string query = @"update Members set
                                     personID = @personID,
                                     isActive = @isActive
                                     where memberID = @id
";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@personID", personID);
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
                Console.WriteLine("Error is :" + ex.Message);
            }
            return isAdd;

        }



        public static DataTable getAllMember()
        {
            DataTable dtPoepleList = new DataTable();
            try
            {
                using (SqlConnection con = new SqlConnection(clsConnection.connectionUrl))
                {
                    con.Open();
                    string query = @"
                                     select m.memberID ,p.personID,
                                     (p.fristName+' '+p.secondName+' '+p.thirdName+' '+ p.familyName ) as fullName,
                                     case when p.gender = 1 then 'Male' else 'Female' end as gender
                                     ,p.brithday,n.name as nationality ,p.phone , m.isActive as isActive
                                     from Peoples p inner join Nationalitys n on p.nationalityID = n.nationalityID
									 inner join Members m 
									 on m.personID = p.personID
";

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


        public static bool isMemberExistByPeopleID(
            int peopleID 
            )
        {
            bool isFound = false;
            try
            {
                using (SqlConnection con = new SqlConnection(clsConnection.connectionUrl))
                {
                    con.Open();
                    string query = @"select found =1 from Members where phone = @peopleID";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@peopleID", peopleID);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                                isFound = true;
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


        public static bool isMemberExistByID(int id )
        {
            bool isFound = false;
            try
            {
                using (SqlConnection con = new SqlConnection(clsConnection.connectionUrl))
                {
                    con.Open();
                    string query = @"select found =1 from Members where memberID = @id";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                                isFound = true;
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

        public static bool UpdatememberState(

                int id,
                 bool isActive
                )
        {
            bool isAdd = false;
            try
            {
                using (SqlConnection con = new SqlConnection(clsConnection.connectionUrl))
                {
                    con.Open();
                    string query = @"update Members set
                                     isActive = @isActive
                                     where memberID = @id
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
                Console.WriteLine("Error is :" + ex.Message);
            }
            return isAdd;

        }



        public static bool isMemberActivaeByID(
            int id
             )
        {
            bool isFound = false;
            try
            {
                using (SqlConnection con = new SqlConnection(clsConnection.connectionUrl))
                {
                    con.Open();
                    string query = @"select found = 1 from Members where memberID = @id and isActive = 1 ";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                isFound = true;
                                                    
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


    }
}
