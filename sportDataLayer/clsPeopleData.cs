﻿using sportDataLayer.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace sportDataLayer
{
    public class clsPeoplesData
    {
        public static bool findPeoplesByID(
            int id,
            ref string  fristName ,
            ref string  secondName ,
            ref string  thirdName ,
            ref string  familyName,
            ref DateTime  brithday ,
            ref bool   gender ,
            ref int  nationalityID,
            ref string  address ,
            ref string  phone )
        {
            bool isFound = false;
            try
            {
                using (SqlConnection con = new SqlConnection(clsConnection.connectionUrl))
                {
                    con.Open();
                    string query = @"select * from Peoples where personID = @id";
                    
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader =  cmd.ExecuteReader()) 
                        {
                            if(reader.Read())
                            {
                                isFound = true;
                                fristName = (string)reader["fristName"];
                                secondName = (string)reader["secondName"];
                                thirdName = (string)reader["thirdName"];
                                familyName = (string)reader["familyName"];
                                brithday = (DateTime)reader["brithday"];
                                gender = (bool)reader["gender"];
                                nationalityID = (int)reader["nationalityID"];
                                address = (string)reader["address"];
                                phone = (string)reader["phone"];
                            } 
                        }
                    }

                }
            }catch (Exception ex) { 
            Console.WriteLine("Error is :"+ ex.Message);
            }
            return isFound;

        }


        public static bool findPeoplesByPhone(
        ref  int id,
        ref string fristName,
        ref string secondName,
        ref string thirdName,
        ref string familyName,
        ref DateTime brithday,
        ref bool gender,
        ref int nationalityID,
        ref string address,
         string phone)
        {
            bool isFound = false;
            try
            {
                using (SqlConnection con = new SqlConnection(clsConnection.connectionUrl))
                {
                    con.Open();
                    string query = @"select * from Peoples where phone = @phone";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@phone", phone);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {

                            if(reader.Read())
                            {
                            isFound = true;
                            id = (int)reader["personID"];
                            fristName = (string)reader["firstName"];
                            secondName = (string)reader["secondName"];
                            thirdName = (string)reader["thirdname"];
                            familyName = (string)reader["familyName"];
                            brithday = (DateTime)reader["brithDay"];
                            gender = (bool)reader["gender"];
                            nationalityID = (int)reader["nationalityID"];
                            address = (string)reader["address"];
                            phone = (string)reader["phone"];
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



        public static int createPeoples(
          int id,
          string firstName,
          string secondName,
          string thirdName,
          string familyName,
          DateTime brithday,
          bool gender,
          int nationalityID,
          string address,
          string phone)
        {
            int peopleID= 0;
            try
            {
                using (SqlConnection con = new SqlConnection(clsConnection.connectionUrl))
                {
                    con.Open();
                    string query = @"insert into Peoples  
                                   (fristName,secondName,thirdName,familyName,phone,gender,brithday,nationalityID,address)
                                   VALUES(@firstName,@secondName,@thirdname,@familyName,@phone,@gender,@brithDay,@nationalityID,@address)
                                   select SCOPE_IDENTITY() ;  
                                   ";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {

                        cmd.Parameters.AddWithValue("@firstName", firstName);
                        cmd.Parameters.AddWithValue("@secondName", secondName);
                        cmd.Parameters.AddWithValue("@thirdname", thirdName);
                        cmd.Parameters.AddWithValue("@familyName", familyName);
                        cmd.Parameters.AddWithValue("@brithDay", brithday);
                        cmd.Parameters.AddWithValue("@phone", phone);
                        cmd.Parameters.AddWithValue("@gender", gender);
                        cmd.Parameters.AddWithValue("@nationalityID", nationalityID);
                        cmd.Parameters.AddWithValue("@address", address);
                        object result = cmd.ExecuteScalar();
                        if (result!=null && int.TryParse(result.ToString(),out int resultID))
                        {
                            peopleID = resultID;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error is :" + ex.Message);
            }
            return peopleID;

        }

        
        public static bool deletePoeplByID( int id)
        {
            bool isDelete = false;
            try
            {
                using (SqlConnection con = new SqlConnection(clsConnection.connectionUrl))
                {
                    con.Open();
                    string query = @"delete from Peoples where personID = @id";

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

             public static bool updatePeoples(
                int id,
                string firstName,
                string secondName,
                string thirdName,
                string familyName,
                DateTime brithday,
                bool gender,
                int nationalityID,
                string address,
                string phone)
        {
            bool isAdd = false;
            try
            {
                using (SqlConnection con = new SqlConnection(clsConnection.connectionUrl))
                {
                    con.Open();
                    string query = @"update Peoples set 
                                    fristName = @firstName,
                                    secondName = @secondName,
                                    thirdName = @thirdname,
                                    familyName = @familyName,
                                    brithday = @brithDay,
                                    phone = @phone,
                                    gender = @gender,
                                    nationalityID= @nationalityID,
                                    address=@address 
                                    where personID = @id ";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@firstName", firstName);
                        cmd.Parameters.AddWithValue("@secondName", secondName);
                        cmd.Parameters.AddWithValue("@thirdname", thirdName);
                        cmd.Parameters.AddWithValue("@familyName", familyName);
                        cmd.Parameters.AddWithValue("@brithDay", brithday);
                        cmd.Parameters.AddWithValue("@phone", phone);
                        cmd.Parameters.AddWithValue("@gender", gender);
                        cmd.Parameters.AddWithValue("@nationalityID", nationalityID);
                        cmd.Parameters.AddWithValue("@address", address);
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



        public static DataTable getAllPeoples()
        {
            DataTable dtPoepleList = new DataTable();
            try
            {
                using (SqlConnection con = new SqlConnection(clsConnection.connectionUrl))
                {
                    con.Open();
                    string query = @"select p.personID,(p.fristName+' '+p.secondName+' '+p.thirdName+' '+ p.familyName ) as fullName,
                                     case when p.gender = 1 then 'Male' else 'Female' end as gender,p.brithday,n.name as nationality ,p.phone
                                     from Peoples p inner join Nationalitys n on p.nationalityID = n.nationalityID";

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


        public static bool isPersonExistByPhone(
            string phone
            )
        {
            bool isFound = false;
            try
            {
                using (SqlConnection con = new SqlConnection(clsConnection.connectionUrl))
                {
                    con.Open();
                    string query = @"select found =1 from Peoples where phone = @phone";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@phone", phone);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if(reader.Read())
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




    }
}