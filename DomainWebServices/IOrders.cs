using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DomainWebServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IOrders" in both code and config file together.
    
    [ServiceContract]
    public interface IOrders
    {
        [OperationContract]
        int CreateOrder(Dictionary<int, int> meds, int user_id);

        [OperationContract]
        List<OrderMgmtReference.Order> ViewOrdersCond(int user_id);

        [OperationContract]
        Dictionary<int, string> DeleteOrderCond(int user_id, int order_id);

        [OperationContract]
        int SingleOrderPlausibility(int order_id);

        [OperationContract]
        Dictionary<OrderMgmtReference.Order, int> MultiOrderPlausibility();

        [OperationContract]
        Dictionary<int, string> OrderConfirmation(int order_id);
    }
}
