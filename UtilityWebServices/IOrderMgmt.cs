using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace UtilityWebServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IOrderMgmt" in both code and config file together.
    [DataContract]
    public class Order
    {
        [DataMember]
        public int order_id { get; set; }
        [DataMember]
        public string create_date { get; set; }
        [DataMember]
        public string complete_date { get; set; }
        [DataMember]
        public string status { get; set; }
        [DataMember]
        public string surname { get; set; }
    }

    [DataContract]
    public class meds
    {
        [DataMember]
        public int med_id { get; set; }
        [DataMember]
        public int amount { get; set; }
    }
    [ServiceContract]
    public interface IOrderMgmt
    {
        [OperationContract]
        int InsertOrder(int user_id);

        [OperationContract]
        int InsertOrdMed(Dictionary<int, int> medList, int order_id);

        [OperationContract]
        int CompleteOrder(int order_id);

        [OperationContract]
        List<meds> GetOrdMed(int order_id);

        [OperationContract]
        int DeleteOrder(int order_id);

        [OperationContract]
        int DeleteOrderUser(int user_id, int order_id);

        [OperationContract]
        List<Order> GetOrderList();

        [OperationContract]
        List<Order> GetUserOrderList(int user_id);
        
    }
}
