using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DomainWebServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IUser" in both code and config file together.
    [ServiceContract]
    public interface IUser
    {
        [OperationContract]
        Dictionary<int, UserMgmtReference.User> LoginUser(string username, string password);

        [OperationContract]
        Dictionary<int, string> RegisterUser(string username, string name, string surname, string email, string phone, string city, string password);
    }
}
