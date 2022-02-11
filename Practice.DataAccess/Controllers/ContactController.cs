using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Practice.DataAccess.Models;

namespace Practice.DataAccess.Controllers
{
	public class ContactController
	{
        public static int CreateContact(string firstName, string lastName, string phoneNumber, string emailAddress, string address, IPeopleInfoConfigManager configManager)
        {
            string sqlConnectionString = configManager.PeopleInfoConnection;
            int ContactId = 0;
            string insertSqlCommand = @"INSERT INTO CONTACTS
                                                   (FIRSTNAME,
                                                    LASTNAME,
                                                    PHONENUMBER,
                                                    EMAILADDRESS,
                                                    ADDRESS)
                                             OUTPUT INSERTED.CONTACTID
                                             VALUES
                                                   (@FIRSTNAME,
                                                    @LASTNAME,
                                                    @PHONENUMBER,
                                                    @EMAILADDRESS,
                                                    @ADDRESS)";
            using (SqlConnection sqlConnection = new SqlConnection(sqlConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(insertSqlCommand, sqlConnection))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("@FIRSTNAME", firstName));
                    sqlCommand.Parameters.Add(new SqlParameter("@LASTNAME", lastName));
                    sqlCommand.Parameters.Add(new SqlParameter("@PHONENUMBER", phoneNumber));
                    sqlCommand.Parameters.Add(new SqlParameter("@EMAILADDRESS", emailAddress));
                    sqlCommand.Parameters.Add(new SqlParameter("@ADDRESS", address));

                    sqlCommand.Connection.Open();
                    ContactId = (int)sqlCommand.ExecuteScalar();
                    sqlCommand.Connection.Close();
                }
            }
            return ContactId;
        }

        public static int UpdateContact(int contactID, string firstName, string lastName, string phoneNumber, string emailAddress, string address, IPeopleInfoConfigManager configManager)
        {
            string sqlConnectionString = configManager.PeopleInfoConnection;
            string updateSqlCommand = @"UPDATE CONTACTS
                                               SET FIRSTNAME =    @FIRSTNAME,
                                                   LASTNAME =     @LASTNAME,
                                                   PHONENUMBER =  @PHONENUMBER,
                                                   EMAILADDRESS = @EMAILADDRESS,
                                                   ADDRESS =      @ADDRESS
                                             WHERE CONTACTID =    @CONTACTID";
            using (SqlConnection sqlConnection = new SqlConnection(sqlConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(updateSqlCommand, sqlConnection))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("@FIRSTNAME", firstName));
                    sqlCommand.Parameters.Add(new SqlParameter("@LASTNAME", lastName));
                    sqlCommand.Parameters.Add(new SqlParameter("@PHONENUMBER", phoneNumber));
                    sqlCommand.Parameters.Add(new SqlParameter("@EMAILADDRESS", emailAddress));
                    sqlCommand.Parameters.Add(new SqlParameter("@ADDRESS", address));
                    sqlCommand.Parameters.Add(new SqlParameter("@CONTACTID", contactID));

                    sqlCommand.Connection.Open();
                    sqlCommand.ExecuteNonQuery();
                    sqlCommand.Connection.Close();
                }
            }
            return contactID;
        }

        public static bool DeleteContact(int contactID, IPeopleInfoConfigManager configManager)
        {
            string sqlConnectionString = configManager.PeopleInfoConnection;
            string deleteSqlCommand = @"DELETE FROM CONTACTS WHERE CONTACTID = @CONTACTID";
            using (SqlConnection sqlConnection = new SqlConnection(sqlConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(deleteSqlCommand, sqlConnection))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("@CONTACTID", contactID));

                    sqlCommand.Connection.Open();
                    sqlCommand.ExecuteNonQuery();
                    sqlCommand.Connection.Close();
                }
            }
            return true;
        }

        public static List<ContactModel>? GetAllContacts(IPeopleInfoConfigManager configManager)
        {
            string sqlConnectionString = configManager.PeopleInfoConnection;

            List<ContactModel> contactsList = new List<ContactModel>();

            string querySql = "SELECT * FROM CONTACTS ORDER BY CONTACTID DESC";
            using (SqlConnection sqlConnection = new SqlConnection(sqlConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(querySql, sqlConnection))
                {
                    using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
                    {
                        using (DataTable dataTable = new DataTable())
                        {
                            sqlDataAdapter.Fill(dataTable);

                            foreach (DataRow dataRow in dataTable.Rows)
                            {
                                ContactModel contactModel = new ContactModel();

                                contactModel.ContactId = Convert.ToInt32(dataRow["CONTACTID"]);
                                contactModel.FirstName = dataRow["FIRSTNAME"]?.ToString() ?? "";
                                contactModel.LastName = dataRow["LASTNAME"]?.ToString() ?? "";
                                contactModel.PhoneNumber = dataRow["PHONENUMBER"]?.ToString() ?? "";
                                contactModel.EmailAddress = dataRow["EMAILADDRESS"]?.ToString() ?? "";
                                contactModel.Address = dataRow["ADDRESS"]?.ToString() ?? "";

                                contactsList.Add(contactModel);
                            }
                        }
                    }
                }
            }

            return contactsList;
        }

        public static ContactModel? GetContactByID(int contactID, IPeopleInfoConfigManager configManager)
        {
            string sqlConnectionString = configManager.PeopleInfoConnection;
            ContactModel contact = new ContactModel();

            string querySql = "SELECT * FROM CONTACTS WHERE CONTACTID = @CONTACTID";
            using (SqlConnection sqlConnection = new SqlConnection(sqlConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(querySql, sqlConnection))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("@CONTACTID", contactID));
                    sqlConnection.Open();
                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();

                            contact.ContactId = Convert.ToInt32(reader["CONTACTID"]);
                            contact.FirstName = reader["FIRSTNAME"]?.ToString() ?? "";
                            contact.LastName = reader["LASTNAME"]?.ToString() ?? "";
                            contact.PhoneNumber = reader["PHONENUMBER"]?.ToString() ?? "";
                            contact.EmailAddress = reader["EMAILADDRESS"]?.ToString() ?? "";
                            contact.Address = reader["ADDRESS"]?.ToString() ?? "";
                        }
                        else
                        {
                            throw new Exception("No rows found.");
                        }
                    }

                    sqlConnection.Close();
                }
            }
            return contact;
        }
    }
}

