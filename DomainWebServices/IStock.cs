using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DomainWebServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IStock" in both code and config file together.
    [DataContract]
    public class MedStat
    {
        [DataMember]
        public int med_id { get; set; }
        [DataMember]
        public String description { get; set; }
        [DataMember]
        public decimal price { get; set; }
        [DataMember]
        public int quantity { get; set; }
        [DataMember]
        public int quantity_needed { get; set; }
    }

    [ServiceContract]
    public interface IStock
    {
        [OperationContract]
        List<MedStat> ViewMed();
        [OperationContract]
        void UpdateMed(int med_id, string description, int quantity, decimal price);
        [OperationContract]
        void AddMed(string description, int quantity, decimal price);
    }
}
