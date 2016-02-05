using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace UtilityWebServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IUserMgmt" in both code and config file together.
    [DataContract]
    public class User
    {
        [DataMember]
        public int user_id { get; set; }
        [DataMember]
        public string username { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string surname { get; set; }
        [DataMember]
        public string email { get; set; }
        [DataMember]
        public string phone { get; set; }
        [DataMember]
        public string city { get; set; }
        [DataMember]
        public string title { get; set; }
    }

    [ServiceContract]
    public interface IUserMgmt
    {
        [OperationContract]
        bool UserExists(string username);

        [OperationContract]
        int GetExistingUserId(string username, string password);

        [OperationContract]
        int InsertUser(string username, string name, string surname, string email, string phone, string city, string password);

        [OperationContract]
        int UpdateUser(int user_id, string name, string surname, string email, string phone, string city, string password);

        [OperationContract]
        User GetUserInfo(int user_id);

        [OperationContract]
        List<User> GetUserList();
    }
}
