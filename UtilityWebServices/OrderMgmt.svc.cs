using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace UtilityWebServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "OrderMgmt" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select OrderMgmt.svc or OrderMgmt.svc.cs at the Solution Explorer and start debugging.
    public class OrderMgmt : IOrderMgmt
    {

        public int InsertOrder(int user_id)
        {
            int order_id = 0;
           

            GetDBConnectionReference.GetDBConnectionClient gdbcc = new GetDBConnectionReference.GetDBConnectionClient();
            string conString = gdbcc.GetDBConnectionString();
            MySqlConnection db = new MySqlConnection(conString);
            db.Open();

            MySqlCommand query = new MySqlCommand();
            query.Connection = db;
            query.CommandText = "INSERT INTO med_stor.`order` (create_date, status_id, user_id) VALUES (NOW(), @status_id, @user_id)";
            query.Prepare();

            query.Parameters.AddWithValue("@status_id", 1);
            query.Parameters.AddWithValue("@user_id",user_id);

            query.ExecuteNonQuery();
            order_id = unchecked((int)query.LastInsertedId);
            
            db.Close();

            return order_id;
        }

        public int InsertOrdMed(Dictionary<int, int> medList, int order_id)
        {
            int inserted = 0;

            GetDBConnectionReference.GetDBConnectionClient gdbcc = new GetDBConnectionReference.GetDBConnectionClient();
            string conString = gdbcc.GetDBConnectionString();
            MySqlConnection db = new MySqlConnection(conString);
            db.Open();

            MySqlCommand query = new MySqlCommand();
            query.Connection = db;
            
            //medList.ForEach(delegate(meds med)
            foreach(KeyValuePair<int, int> pair in medList)
            {
                query.CommandText = "INSERT INTO med_stor.ord_med (order_id, med_id, amount) VALUES (@order_id, @med_id, @amount)";
                query.Prepare();
                query.Parameters.AddWithValue("@order_id", order_id);
                query.Parameters.AddWithValue("@med_id", pair.Key);
                query.Parameters.AddWithValue("@amount", pair.Value);

                inserted = query.ExecuteNonQuery();

                query.Parameters.Clear();
            };
            

            db.Close();

            return inserted;
        }

        public int CompleteOrder(int order_id)
        {
            int updated = 0;
            GetDBConnectionReference.GetDBConnectionClient gdbcc = new GetDBConnectionReference.GetDBConnectionClient();
            string conString = gdbcc.GetDBConnectionString();
            MySqlConnection db = new MySqlConnection(conString);
            db.Open();

            MySqlCommand query = new MySqlCommand();
            query.Connection = db;
            query.CommandText = "UPDATE `order` SET status_id = 2, complete_date = NOW() WHERE order_id = @order_id";

            query.Prepare();
            query.Parameters.AddWithValue("@order_id", order_id);
            updated = query.ExecuteNonQuery();

            db.Close();
            return updated;
        }

        public List<meds> GetOrdMed(int order_id)
        {
            List<meds> medList = new List<meds>();
            GetDBConnectionReference.GetDBConnectionClient gdbcc = new GetDBConnectionReference.GetDBConnectionClient();
            string conString = gdbcc.GetDBConnectionString();
            MySqlConnection db = new MySqlConnection(conString);
            db.Open();

            MySqlCommand query = new MySqlCommand();
            query.Connection = db;
            query.CommandText = "SELECT med_id, amount FROM ord_med WHERE order_id = @order_id";
            query.Prepare();

            query.Parameters.AddWithValue("@order_id", order_id);
            MySqlDataReader queryResults = query.ExecuteReader();
            while(queryResults.Read())
            {
                meds med = new meds();
                med.med_id = queryResults.GetInt32(0);
                med.amount = queryResults.GetInt32(1);
                medList.Add(med);
            }

            return medList;
        }

        public int DeleteOrder(int order_id)
        {
            int deleted = 0;

            GetDBConnectionReference.GetDBConnectionClient gdbcc = new GetDBConnectionReference.GetDBConnectionClient();
            string conString = gdbcc.GetDBConnectionString();
            MySqlConnection db = new MySqlConnection(conString);
            db.Open();

            MySqlCommand query = new MySqlCommand();
            query.Connection = db;
            query.CommandText = "DELETE FROM med_stor.`order` WHERE order_id = @order_id";
            query.Prepare();

            query.Parameters.AddWithValue("@order_id", order_id);
            deleted = query.ExecuteNonQuery();

            db.Close();

            return deleted;
        }

        public int DeleteOrderUser(int user_id, int order_id)
        {
            int deleted = 0;
            GetDBConnectionReference.GetDBConnectionClient gdbcc = new GetDBConnectionReference.GetDBConnectionClient();
            string conString = gdbcc.GetDBConnectionString();
            MySqlConnection db = new MySqlConnection(conString);
            db.Open();

            MySqlCommand query = new MySqlCommand();
            query.Connection = db;
            query.CommandText = "DELETE FROM med_stor.`order` WHERE order_id = @order_id and user_id = @user_id";
            query.Prepare();

            query.Parameters.AddWithValue("@order_id", order_id);
            query.Parameters.AddWithValue("@user_id", user_id);
            deleted = query.ExecuteNonQuery();

            db.Close();

            return deleted;
        }
        
        public List<Order> GetOrderList()
        {
            List<Order> orderList = new List<Order>();
            GetDBConnectionReference.GetDBConnectionClient gdbcc = new GetDBConnectionReference.GetDBConnectionClient();
            string conString = gdbcc.GetDBConnectionString();
            MySqlConnection db = new MySqlConnection(conString);
            db.Open();

            MySqlCommand query = new MySqlCommand();
            query.Connection = db;
            query.CommandText = "SELECT order_id, create_date, IFNULL(complete_date,'UnKnown'), u.surname, s.description FROM `order` o, user u, ord_status s WHERE u.user_id = o.user_id and s.status_id = o.status_id ORDER BY o.status_id";

            MySqlDataReader queryResults = query.ExecuteReader();
            while (queryResults.Read())
            {
                Order order = new Order();
                order.order_id = queryResults.GetInt32(0);
                order.create_date = queryResults.GetString(1);
                order.complete_date = queryResults.GetString(2);
                order.surname = queryResults.GetString(3);
                order.status = queryResults.GetString(4);

                orderList.Add(order);
            }

            queryResults.Close();
            queryResults = null;
            db.Close();

            return orderList;
        }
        
        public List<Order> GetUserOrderList(int user_id)
        {
            List<Order> orderList = new List<Order>();
            GetDBConnectionReference.GetDBConnectionClient gdbcc = new GetDBConnectionReference.GetDBConnectionClient();
            string conString = gdbcc.GetDBConnectionString();
            MySqlConnection db = new MySqlConnection(conString);
            db.Open();

            MySqlCommand query = new MySqlCommand();
            query.Connection = db;
            query.CommandText = "SELECT order_id, create_date, IFNULL(complete_date,'UnKnown'), u.surname, s.description FROM `order` o, user u, ord_status s WHERE u.user_id = o.user_id AND s.status_id = o.status_id AND o.user_id = @user_id ORDER BY o.status_id";
            query.Prepare();

            query.Parameters.AddWithValue("@user_id", user_id);

            MySqlDataReader queryResults = query.ExecuteReader();
            while (queryResults.Read())
            {
                Order order = new Order();
                order.order_id = queryResults.GetInt32(0);
                order.create_date = queryResults.GetString(1);
                order.complete_date = queryResults.GetString(2);
                order.surname = queryResults.GetString(3);
                order.status = queryResults.GetString(4);

                orderList.Add(order);
            }

            queryResults.Close();
            queryResults = null;
            db.Close();

            return orderList;
        }
               
    }
}
