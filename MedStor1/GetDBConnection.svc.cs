using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MedStor1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "GetDBConnection" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select GetDBConnection.svc or GetDBConnection.svc.cs at the Solution Explorer and start debugging.
    public class GetDBConnection : IGetDBConnection
    {

        public string GetDBConnectionString()
        {
            string conString = @"Server = 83.212.122.8; Port = 3306; Database = med_stor; Uid = medstor; Pwd = 1234;";

            return conString;
        }
    }
}
