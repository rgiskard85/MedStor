using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DomainWebServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Orders" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Orders.svc or Orders.svc.cs at the Solution Explorer and start debugging.
    public class Orders : IOrders
    {

        public int CreateOrder(Dictionary<int, int> meds, int user_id)
        {            
            OrderMgmtReference.OrderMgmtClient omc = new OrderMgmtReference.OrderMgmtClient();

            int order_id = omc.InsertOrder(user_id);
            omc.InsertOrdMed(meds, order_id);
            
            return 1;
        }

        public List<OrderMgmtReference.Order> ViewOrdersCond(int user_id)
        {
            List<OrderMgmtReference.Order> orderList = new List<OrderMgmtReference.Order>();
            OrderMgmtReference.OrderMgmtClient omc = new OrderMgmtReference.OrderMgmtClient();
            UserMgmtReference.UserMgmtClient umc = new UserMgmtReference.UserMgmtClient();
            if (umc.GetUserInfo(user_id).title.Equals("admin"))
            {
                orderList = omc.GetOrderList().ToList();
            }
            else
            {
                orderList = omc.GetUserOrderList(user_id).ToList();
            }

            return orderList;
        }

        public Dictionary<int, string> DeleteOrderCond(int user_id, int order_id)
        {
            OrderMgmtReference.OrderMgmtClient omc = new OrderMgmtReference.OrderMgmtClient();
            Dictionary<int, string> response = new Dictionary<int, string>();
            UserMgmtReference.UserMgmtClient umc = new UserMgmtReference.UserMgmtClient();
            if(umc.GetUserInfo(user_id).title.Equals("admin"))
            {
                response.Add(omc.DeleteOrder(order_id), "Orders Deleted");
            }
            else
            {
                response.Add(omc.DeleteOrderUser(user_id, order_id), "Orders Deleted");
            }

            return response;
        }

        public int SingleOrderPlausibility(int order_id)
        {
            OrderMgmtReference.OrderMgmtClient omc = new OrderMgmtReference.OrderMgmtClient();
            MedMgmtReference.MedMgmtClient mmc = new MedMgmtReference.MedMgmtClient();
            int plausible = 1;
            List<OrderMgmtReference.meds> medList = omc.GetOrdMed(order_id).ToList();
            foreach (var med in medList)
            {
                if (med.amount > mmc.GetMedicineQuantity(med.med_id))
                {
                    plausible = 0;
                }
            }

            return plausible;
        }

        public Dictionary<OrderMgmtReference.Order, int> MultiOrderPlausibility()
        {
            Dictionary<OrderMgmtReference.Order, int> orderInventory = new Dictionary<OrderMgmtReference.Order, int>();
            OrderMgmtReference.OrderMgmtClient omc = new OrderMgmtReference.OrderMgmtClient();
            MedMgmtReference.MedMgmtClient mmc = new MedMgmtReference.MedMgmtClient();

            List<OrderMgmtReference.Order> orderList = omc.GetOrderList().ToList();
            foreach (var order in orderList)
            {
                if (order.status.Equals("Pending"))
                {
                    orderInventory.Add(order, SingleOrderPlausibility(order.order_id));                
                }
                
            }

            return orderInventory;
        }

        public Dictionary<int, string> OrderConfirmation(int order_id)
        {
            Dictionary<int, string> confirm = new Dictionary<int, string>();
            if (SingleOrderPlausibility(order_id).Equals(1))
            {
                OrderMgmtReference.OrderMgmtClient omc = new OrderMgmtReference.OrderMgmtClient();
                MedMgmtReference.MedMgmtClient mmc = new MedMgmtReference.MedMgmtClient();
                
                List<OrderMgmtReference.meds> medList = omc.GetOrdMed(order_id).ToList();
                foreach (var med in medList)
                {
                    mmc.SetMedicineQuantity(med.med_id, mmc.GetMedicineQuantity(med.med_id) - med.amount);
                }

                confirm.Add(omc.CompleteOrder(order_id),"Order confirmed");

                return confirm;
            }
            else
            {
                confirm.Add(0, "Order confirmed");

                return confirm;
            }
        }
    }
}
