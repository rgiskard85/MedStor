using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace UtilityWebServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "UserMgmt" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select UserMgmt.svc or UserMgmt.svc.cs at the Solution Explorer and start debugging.
    public class UserMgmt : IUserMgmt
    {
        public bool UserExists(string username)
        {
            GetDBConnectionReference.GetDBConnectionClient gdbcc = new GetDBConnectionReference.GetDBConnectionClient();
            string conString = gdbcc.GetDBConnectionString();
            MySqlConnection db = new MySqlConnection(conString);
            db.Open();

            MySqlCommand query = new MySqlCommand();
            query.Connection = db;
            query.CommandText = "SELECT * FROM user WHERE username = @username";
            query.Prepare();

            query.Parameters.AddWithValue("@username", username);
            MySqlDataReader queryResults = query.ExecuteReader();
            if(queryResults.Read())
            {
                queryResults.Close();
                queryResults = null;
                db.Close();
                return true;
            }
            else
            {
                queryResults.Close();
                queryResults = null;
                db.Close();
                return false;
            }            
        }

        public int GetExistingUserId(string username, string password)
        {
            int user_id = 0;
            EncryptDataReference.EncryptDataClient edc = new EncryptDataReference.EncryptDataClient();
            string encPassword = edc.Encrypt(password);

            GetDBConnectionReference.GetDBConnectionClient gdbcc = new GetDBConnectionReference.GetDBConnectionClient();
            string conString = gdbcc.GetDBConnectionString();
            MySqlConnection db = new MySqlConnection(conString);
            db.Open();

            MySqlCommand query = new MySqlCommand();
            query.Connection = db;
            query.CommandText = "SELECT user_id FROM user WHERE username =@username and password = @encPassword";
            query.Prepare();

            query.Parameters.AddWithValue("@username", username);
            query.Parameters.AddWithValue("@encPassword", encPassword);
            MySqlDataReader queryResults = query.ExecuteReader();
            if (queryResults.Read())
            {
                user_id = queryResults.GetInt32(0);
            }
            queryResults.Close();
            queryResults = null;
            db.Close();

            // Returns non-zero if username&password are correct
            return user_id;
        }

        public int InsertUser(string username, string name, string surname, string email, string phone, string city, string password)
        {
            int inserted = 0;

            EncryptDataReference.EncryptDataClient edc = new EncryptDataReference.EncryptDataClient();
            string encPassword = edc.Encrypt(password);

            GetDBConnectionReference.GetDBConnectionClient gdbcc = new GetDBConnectionReference.GetDBConnectionClient();
            string conString = gdbcc.GetDBConnectionString();
            MySqlConnection db = new MySqlConnection(conString);
            db.Open();

            MySqlCommand query = new MySqlCommand();
            query.Connection = db;
            query.CommandText = "INSERT INTO med_stor.user (username, name, surname, email, phone, city, password) VALUES(@username, @name, @surname, @email, @phone, @city, @encPassword)";
            query.Prepare();

            query.Parameters.AddWithValue("@username", username);
            query.Parameters.AddWithValue("@name", name);
            query.Parameters.AddWithValue("@surname", surname);
            query.Parameters.AddWithValue("@email", email);
            query.Parameters.AddWithValue("@phone", phone);
            query.Parameters.AddWithValue("@city", city);
            query.Parameters.AddWithValue("@encPassword", encPassword);
            inserted = query.ExecuteNonQuery();

            db.Close();
            
            // Returns 0 if failed, 1 if succeeded
            return inserted;
        }

        public int UpdateUser(int user_id, string name, string surname, string email, string phone, string city, string password)
        {
            int updated = 0;
            EncryptDataReference.EncryptDataClient edc = new EncryptDataReference.EncryptDataClient();
            string encPassword = edc.Encrypt(password);

            GetDBConnectionReference.GetDBConnectionClient gdbcc = new GetDBConnectionReference.GetDBConnectionClient();
            string conString = gdbcc.GetDBConnectionString();
            MySqlConnection db = new MySqlConnection(conString);
            db.Open();

            MySqlCommand query = new MySqlCommand();
            query.Connection = db;
            query.CommandText = "UPDATE med_stor.user set name=@name, surname=@surname, email=@email, phone=@phone, city=@city, password=@encPassword where user_id=@user_id";
            query.Prepare();
                        
            query.Parameters.AddWithValue("@name", name);
            query.Parameters.AddWithValue("@surname", surname);
            query.Parameters.AddWithValue("@email", email);
            query.Parameters.AddWithValue("@phone", phone);
            query.Parameters.AddWithValue("@city", city);
            query.Parameters.AddWithValue("@encPassword", encPassword);
            query.Parameters.AddWithValue("@user_id", user_id);
            updated = query.ExecuteNonQuery();

            db.Close();

            return updated;
        }

        public User GetUserInfo(int user_id)
        {
            User user = new User();

            GetDBConnectionReference.GetDBConnectionClient gdbcc = new GetDBConnectionReference.GetDBConnectionClient();
            string conString = gdbcc.GetDBConnectionString();
            MySqlConnection db = new MySqlConnection(conString);
            db.Open();

            MySqlCommand query = new MySqlCommand();
            query.Connection = db;
            query.CommandText = "SELECT user_id, username, name, surname, email, phone, city, r.title FROM user u, role r WHERE u.role_id = r.role_id AND USER_ID = @user_id";
            query.Prepare();

            query.Parameters.AddWithValue("@user_id", user_id);
            
            MySqlDataReader queryResults = query.ExecuteReader();
            if (queryResults.Read())
            {
                user.user_id = queryResults.GetInt32(0);
                user.username = queryResults.GetString(1);
                user.name = queryResults.GetString(2);
                user.surname = queryResults.GetString(3);
                user.email = queryResults.GetString(4);
                user.phone = queryResults.GetString(5);
                user.city = queryResults.GetString(6);
                user.title = queryResults.GetString(7);                
            }
            queryResults.Close();
            queryResults = null;
            db.Close();
            
            return user;
        }

        public List<User> GetUserList()
        {
            List<User> uList = new List<User>();

            GetDBConnectionReference.GetDBConnectionClient gdbcc = new GetDBConnectionReference.GetDBConnectionClient();
            string conString = gdbcc.GetDBConnectionString();
            MySqlConnection db = new MySqlConnection(conString);
            db.Open();

            MySqlCommand query = new MySqlCommand();
            query.Connection = db;
            query.CommandText = "SELECT user_id, username, name, surname, email, phone, city, title FROM user u, role r WHERE u.role_id = r.role_id";
            query.Prepare();

            MySqlDataReader queryResults = query.ExecuteReader();
            while (queryResults.Read())
            {
                User user = new User();
                user.user_id = queryResults.GetInt32(0);
                user.username = queryResults.GetString(1);
                user.name = queryResults.GetString(2);
                user.surname = queryResults.GetString(3);
                user.email = queryResults.GetString(4);
                user.phone = queryResults.GetString(5);
                user.city = queryResults.GetString(6);
                user.title = queryResults.GetString(7);

                uList.Add(user);
            }

            queryResults.Close();
            queryResults = null;
            db.Close();
            
            return uList;
        }        
    }
}
