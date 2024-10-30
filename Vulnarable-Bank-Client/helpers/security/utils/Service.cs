using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vulnarable_Bank_Client.helpers.security.utils
{
    public class Service
    {
        public static List<string> list()
        {
            return ["http://127.0.0.1:6001/", "http://127.0.0.1:6002/", "http://127.0.0.1:6003/", "http://127.0.0.1:6004/"];
        }
        public class Access
        {
            public class Perrmission
            {
                public static bool IsAllowed(string Username)
                {
                    if (UserTypes.Privlidges.Get(Username).Contains(UserTypes.Privlidges.Types._network_access))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                public static List<string> AllowedOnes()
                {
                    List<string> AuthED_Users = [];

                    foreach (var UserN in Program.ServiceUsers)
                    {
                        if (IsAllowed(UserN) == true)
                        {
                            AuthED_Users.Add(UserN);
                        }
                    }

                    return AuthED_Users;
                }
            }

            public static void Login(string Username, string Service_Name)
            {
                if (Perrmission.IsAllowed(Username) == true)
                {
                    helpers.Display.p_info("YOU ARE LOGINED!!");
                }
                else
                {
                    helpers.Display.p_error($"User: `{Username}`: Is not allowed to access the network!");

                    Perrmission.AllowedOnes();

                }
            }
        }
    }
}
