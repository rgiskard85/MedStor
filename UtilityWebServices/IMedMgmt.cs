using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace UtilityWebServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IMedMgmt" in both code and config file together.
    [DataContract]
    public class Medicine
    {
        [DataMember]
        public int med_id { get; set; }
        [DataMember]
        public String description { get; set; }
        [DataMember]
        public int quantity { get; set; }
        [DataMember]
        public decimal price { get; set; }
    }

    [ServiceContract]
    public interface IMedMgmt
    {
        [OperationContract]
        int InsertMedicine(string description, int quantity, decimal price);

        [OperationContract]
        int UpdateMedicine(int med_id, string description, int quantity, decimal price);

        [OperationContract]
        int SetMedicineQuantity(int med_id, int quantity);

        [OperationContract]
        int GetMedicineQuantity(int med_id);

        [OperationContract]
        List<Medicine> GetMedicineList();

        [OperationContract]
        int MedicineExists(string description);
    }
}
