using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vulnarable_Bank_Client.helpers.security.utils
{
    public class UserTypes
    {
        public static string Admin = "Admin";
        public static string Admin_Dev = "Admin/Dev";
        public static string Debug_Developer = "Debug/Develper";

        public static string API_User = "Client/Api";
        public class ApiCalls
        {
            public static bool IsAuth_OK = false;
            public static bool IsAutherized()
            {
                return IsAuth_OK;
            }
        }


        public class Privlidges
        {
            public class Types
            {
                public static string _transactions = "Transactions";
                public static string _account_management = "Account-Management";
                public static string _network_access = "Network-Access";
                public static string _chain_access = "Chain-Access";
                public static string _developement = "Developement";
                public static string _debug_mode = "Debug-Mode";

                public static string _client_usage = "Client-Usage";
            }
            public static List<string> Get(string Username)
            {
                if (Username == UserTypes.Admin)
                {
                    return [
                        Types._transactions,
                        Types._account_management,
                        Types._network_access,
                        Types._chain_access,
                        Types._developement,
                        Types._debug_mode,

                        Types._client_usage
                        ];
                }
                else if (Username == UserTypes.Admin_Dev)
                {
                    return [
                        Types._network_access,
                        Types._chain_access,
                        Types._developement,
                        Types._debug_mode
                       ];
                }
                else if (Username == UserTypes.Debug_Developer)
                {
                    return [
                        Types._developement,
                        Types._debug_mode
                        ];
                }
                else if (Username == UserTypes.API_User)
                {
                    return [
                        Types._client_usage
                        ];
                }
                else
                {
                    return [];
                }
            }
        }
    }
}
