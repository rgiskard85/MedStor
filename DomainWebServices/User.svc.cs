using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DomainWebServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "User" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select User.svc or User.svc.cs at the Solution Explorer and start debugging.
    public class User : IUser
    {
        public Dictionary<int, UserMgmtReference.User> LoginUser(string username, string password)
        {

            Dictionary<int, UserMgmtReference.User> response = new Dictionary<int, UserMgmtReference.User>();
            UserMgmtReference.UserMgmtClient umc = new UserMgmtReference.UserMgmtClient();

            int user_id = umc.GetExistingUserId(username, password);
            if (user_id == 0)
            {
                // username-password combination is wrong
                response.Add(0, new UserMgmtReference.User());
            }
            else
            {
                //username/password combo exists
                response.Add(1, umc.GetUserInfo(user_id));
            }

            return response;
        }

        public Dictionary<int, string> RegisterUser(string username, string name, string surname, string email, string phone, string city, string password)
        {
            Dictionary<int, string> response = new Dictionary<int, string>();
            UserMgmtReference.UserMgmtClient umc = new UserMgmtReference.UserMgmtClient();

            if (umc.UserExists(username))
            {
                response.Add(0, "Username exists. Please user another username.");
            }
            else
            {
                umc.InsertUser(username, name, surname, email, phone, city, password);
                response.Add(1, "You have registered succesfully.");
            }

            return response;
        }
    }
}
