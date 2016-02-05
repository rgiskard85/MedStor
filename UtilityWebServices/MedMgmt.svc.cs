using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace UtilityWebServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "MedMgmt" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select MedMgmt.svc or MedMgmt.svc.cs at the Solution Explorer and start debugging.
    public class MedMgmt : IMedMgmt
    {
        public int InsertMedicine(string description, int quantity, decimal price)
        {
            int inserted = 0;
                        
            GetDBConnectionReference.GetDBConnectionClient gdbcc = new GetDBConnectionReference.GetDBConnectionClient();
            string conString = gdbcc.GetDBConnectionString();
            MySqlConnection db = new MySqlConnection(conString);
            db.Open();

            MySqlCommand query = new MySqlCommand();
            query.Connection = db;
            query.CommandText = "INSERT INTO med_stor.medicine (description, quantity, price) VALUES(@description, @quantity, @price)";
            query.Prepare();

            query.Parameters.AddWithValue("@description", description);
            query.Parameters.AddWithValue("@quantity", quantity);
            query.Parameters.AddWithValue("@price", price);
            inserted = query.ExecuteNonQuery();

            db.Close();

            // Returns 0 if failed, 1 if succeeded
            return inserted;
        }

        public int UpdateMedicine(int med_id, string description, int quantity, decimal price)
        {
            int updated = 0;
            GetDBConnectionReference.GetDBConnectionClient gdbcc = new GetDBConnectionReference.GetDBConnectionClient();
            string conString = gdbcc.GetDBConnectionString();
            MySqlConnection db = new MySqlConnection(conString);
            db.Open();

            MySqlCommand query = new MySqlCommand();
            query.Connection = db;
            query.CommandText = "UPDATE med_stor.medicine set description = @description, quantity = @quantity, price = @price WHERE med_id = @med_id";
            query.Prepare();

            query.Parameters.AddWithValue("@description", description);
            query.Parameters.AddWithValue("@quantity", quantity);
            query.Parameters.AddWithValue("@price", price);
            query.Parameters.AddWithValue("@med_id", med_id);

            db.Close();

            // Returns 0 if failed, 1 if succeeded
            updated = query.ExecuteNonQuery();

            return updated;
        }

        public int SetMedicineQuantity(int med_id, int quantity)
        {
            int updated = 0;

            GetDBConnectionReference.GetDBConnectionClient gdbcc = new GetDBConnectionReference.GetDBConnectionClient();
            string conString = gdbcc.GetDBConnectionString();
            MySqlConnection db = new MySqlConnection(conString);
            db.Open();

            MySqlCommand query = new MySqlCommand();
            query.Connection = db;
            query.CommandText = "UPDATE med_stor.medicine SET quantity = @quantity WHERE med_id = @med_id";
            query.Prepare();

            query.Parameters.AddWithValue("@quantity", quantity);
            query.Parameters.AddWithValue("@med_id", med_id);
            updated = query.ExecuteNonQuery();

            db.Close();

            // Returns 0 if failed, 1 if succeeded
            return updated;
        }

        public int GetMedicineQuantity(int med_id)
        {
            int quantity = 0;
            GetDBConnectionReference.GetDBConnectionClient gdbcc = new GetDBConnectionReference.GetDBConnectionClient();
            string conString = gdbcc.GetDBConnectionString();
            MySqlConnection db = new MySqlConnection(conString);
            db.Open();

            MySqlCommand query = new MySqlCommand();
            query.Connection = db;
            query.CommandText = "SELECT quantity from med_stor.medicine WHERE med_id = @med_id";
            query.Prepare();

            query.Parameters.AddWithValue("@med_id", med_id);
            MySqlDataReader queryResults = query.ExecuteReader();
            if (queryResults.Read())
            {
                quantity = queryResults.GetInt32(0);
            }
            queryResults.Close();
            queryResults = null;
            db.Close();

            return quantity;
        }
        
        public List<Medicine> GetMedicineList()
        {
            List<Medicine> mList = new List<Medicine>();

            GetDBConnectionReference.GetDBConnectionClient gdbcc = new GetDBConnectionReference.GetDBConnectionClient();
            string conString = gdbcc.GetDBConnectionString();
            MySqlConnection db = new MySqlConnection(conString);
            db.Open();

            MySqlCommand query = new MySqlCommand();
            query.Connection = db;
            query.CommandText = "SELECT med_id, description, quantity, price FROM med_stor.medicine";
            query.Prepare();

            MySqlDataReader queryResults = query.ExecuteReader();
            while (queryResults.Read())
            {
                Medicine med = new Medicine();
                med.med_id = queryResults.GetInt32(0);
                med.description = queryResults.GetString(1);
                med.quantity = queryResults.GetInt32(2);
                med.price = queryResults.GetDecimal(3);

                mList.Add(med);
            }

            queryResults.Close();
            queryResults = null;
            db.Close();

            return mList;
        }
        
        public int MedicineExists(string description)
        {
            int med_id = 0;
            GetDBConnectionReference.GetDBConnectionClient gdbcc = new GetDBConnectionReference.GetDBConnectionClient();
            string conString = gdbcc.GetDBConnectionString();
            MySqlConnection db = new MySqlConnection(conString);
            db.Open();

            MySqlCommand query = new MySqlCommand();
            query.Connection = db;
            query.CommandText = "SELECT med_id FROM med_stor.medicine WHERE description = @description";
            query.Prepare();

            query.Parameters.AddWithValue("@description", description);
            MySqlDataReader queryResults = query.ExecuteReader();
            if (queryResults.Read())
            {
                med_id = queryResults.GetInt32(0);
            }

            queryResults.Close();
            queryResults = null;
            db.Close();
            
            return med_id;
        }
    }
}
