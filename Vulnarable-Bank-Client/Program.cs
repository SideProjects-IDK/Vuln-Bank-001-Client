using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Security;
using System.Security.Cryptography;
using System.Security.Principal;

namespace Vulnarable_Bank_Client
{
   

    internal class Program
    {
        public static string DispData = "@vuln/transaction/terminal";
        public static string Username = helpers.security.utils.UserTypes.Debug_Developer;

        public static List<string> ServiceAddresses = helpers.security.utils.Service.list();
        public static List<string> ServiceUsers = [
            helpers.security.utils.UserTypes.Admin,
            helpers.security.utils.UserTypes.Admin_Dev,
            helpers.security.utils.UserTypes.Debug_Developer,
            helpers.security.utils.UserTypes.API_User
            ];

        public static bool IsDebugModeOn = true;

        static void Main(string[] args)
        {
            helpers.Display.PrintBanner();
            helpers.Setup.DB_Add_Data();

            if (IsDebugModeOn)
            {
                helpers.Display.p_info("Welcome `Debug` user, you are welcome to make fake transactions");
                Username = helpers.security.utils.UserTypes.Debug_Developer;
            }
            else
            {
                helpers.Display.p_info("Welcome `Admin` user, you are welcome to make transactions");
                Username = "Admin";
            }

            while (true)
            {
                Console.Write($"{DispData}>");

                string Input = Console.ReadLine();

                List<string> Commands = Input.Split(" ").ToList();

                if (Commands.Count > 0)
                {
                    if (Commands[0] == "/help")
                    {
                        Actions.ShowHelp();
                    }
                    else if (Commands[0] == "/whoami")
                    {
                        Actions.WhoAmI();
                    }
                    else if (Commands[0] == "/whereami")
                    {
                        Actions.WhereAmI();
                    }
                    else if (Commands[0] == "/nc")
                    {
                        if (IsDebugModeOn || Username == helpers.security.utils.UserTypes.Admin)
                        {
                            if (Commands[1] == "/login")
                            {
                                string Node_Name = Commands[2];
                            }
                            else
                            {
                                helpers.Display.p_error($"This `{Commands[1]}` is not a valid subcommand for `{Commands[0]}`!");
                            }
                        }
                        else
                        {
                            helpers.Display.p_error("[]");
                        }
                    }
                    else if (Commands[0] == "/account")
                    {
                        if (Username == helpers.security.utils.UserTypes.Admin || helpers.security.utils.UserTypes.ApiCalls.IsAutherized() == true || Username != "fd")
                        {
                            if (Commands[1] == "/transaction")
                            {
                                if (Commands[1] == "/send")
                                {
                                    string Node_Name = Commands[2];
                                }
                                else if (Commands[1] == "/info")
                                {
                                    string key = Commands[2];
                                    string value = Commands[3];

                                    Console.WriteLine($"{"-------------------------",-29} | {"-------------------",-19} | {"------------------------------",-30}");
                                    Console.WriteLine($"{"From Id",-29} | {"To Id",-19} | {"Amount",-30}");
                                    Console.WriteLine($"{"",-29} | {"",-19} | {"",-30}");
                                    foreach (var Account in helpers.banking.db.users_db.Tbl_Usrs.Transactions.FindAll(x => x.FromID.Equals(value)))
                                    {
                                        Console.WriteLine($"{Account.FromID,-29} | {Account.ToID,-19} | {helpers.banking.db.users_db.Tbl_Usrs.Operations.Calculate.Balance(helpers.banking.db.users_db.Tbl_Usrs.Records.Find(x => x.ID.Equals(Account.FromID))),-30}");
                                    }

                                    foreach (var Account in helpers.banking.db.users_db.Tbl_Usrs.Transactions.FindAll(x => x.ToID.Equals(value)))
                                    {
                                        Console.WriteLine($"{Account.FromID,-29} | {Account.ToID,-19} | {helpers.banking.db.users_db.Tbl_Usrs.Operations.Calculate.Balance(helpers.banking.db.users_db.Tbl_Usrs.Records.Find(x => x.ID.Equals(Account.ToID))),-30}");
                                    }

                                    helpers.Display.p_info($"Total: `{helpers.banking.db.users_db.Tbl_Usrs.Operations.Select.All_where.like(key, value).Count}` Accounts");

                                }
                                else if (Commands[1] == "/list")
                                {
                                    Console.WriteLine($"{"-------------------------",-9} | {"-------------------",-19} | {"------------------------------",-30}");
                                    Console.WriteLine($"{"From Id",-29} | {"To Id",-19} | {"Amount",-30}");
                                    Console.WriteLine($"{"",-29} | {"",-19} | {"",-30}");
                                    foreach (var Account in helpers.banking.db.users_db.Tbl_Usrs.Transactions)
                                    {
                                        Console.WriteLine($"{Account.FromID,-29} | {Account.ToID,-19} | {helpers.banking.db.users_db.Tbl_Usrs.Operations.Calculate.Balance(helpers.banking.db.users_db.Tbl_Usrs.Records.Find(x => x.ID.Equals(Account.FromID))),-30}");
                                    }

                                    helpers.Display.p_info($"Total: `{helpers.banking.db.users_db.Tbl_Usrs.Operations.Select.All().Count}` Transactions");
                                }
                            }
                            else if (Commands[1] == "/info")
                            {
                                string key = Commands[2];
                                string value = Commands[3];

                                Console.WriteLine($"{"-------------------------",-29} | {"-------------------",-19} | {"------------------------------",-30}");
                                Console.WriteLine($"{"Id",-29} | {"Username",-19} | {"Balance",-30}");
                                Console.WriteLine($"{"",-29} | {"",-19} | {"",-30}");
                                foreach (var Account in helpers.banking.db.users_db.Tbl_Usrs.Operations.Select.All_where.like(key, value))
                                {
                                    Console.WriteLine($"{Account.ID,-29} | {Account.UserName,-19} | {helpers.banking.db.users_db.Tbl_Usrs.Operations.Calculate.Balance(Account),-30}");
                                }
                                
                                helpers.Display.p_info($"Total: `{helpers.banking.db.users_db.Tbl_Usrs.Operations.Select.All_where.like(key, value).Count}` Accounts");

                            }
                            else if (Commands[1] == "/list")
                            {
                                Console.WriteLine($"{"-------------------------",-9} | {"-------------------",-19} | {"------------------------------",-30}");
                                Console.WriteLine($"{"Id",-29} | {"Username",-19} | {"Balance",-30}");
                                Console.WriteLine($"{"",-29} | {"",-19} | {"",-30}");
                                foreach (var Account in helpers.banking.db.users_db.Tbl_Usrs.Operations.Select.All())
                                {
                                    Console.WriteLine($"{Account.ID,-29} | {Account.UserName,-19} | {helpers.banking.db.users_db.Tbl_Usrs.Operations.Calculate.Balance(Account),-30}");
                                }
                                
                                helpers.Display.p_info($"Total: `{helpers.banking.db.users_db.Tbl_Usrs.Operations.Select.All().Count}` Accounts");

                            }
                            else
                            {
                                helpers.Display.p_error($"This `{Commands[1]}` is not a valid subcommand for `{Commands[0]}`!");
                            }
                        }
                        else
                        {
                            helpers.Display.p_error("[]");
                        }
                    }
                    else if (Commands[0] == "/stats")
                    {
                        if (IsDebugModeOn)
                        {
                            Actions.CheckStats();
                        }
                        else if (!IsDebugModeOn 
                            || Username == helpers.security.utils.UserTypes.Admin_Dev 
                            || Username == helpers.security.utils.UserTypes.Admin 
                            || Username == helpers.security.utils.UserTypes.Debug_Developer)
                        {
                            Actions.CheckStats();
                        }
                        else
                        {
                            helpers.Display.p_error("[]");
                        }
                    }
                    else if (Commands[0] == "/scan")
                    {
                        if (IsDebugModeOn)
                        {
                            if (Commands[1] == "/network")
                            {
                                Actions.ScanNetwork();
                            }
                            else if (Commands[1] == "/chain")
                            {
                                Actions.ScanChain();
                            }
                            else
                            {
                                helpers.Display.p_error($"This `{Commands[1]}` is not a valid subcommand for `{Commands[0]}`!");
                            }
                        }
                        else
                        {
                            helpers.Display.p_error("[]");
                        }
                    }
                    else
                    {
                        if (Commands[0] != string.Empty)
                        {
                            helpers.Display.p_error($"This `{Commands[0]}` is not a command!");
                        }
                    }
                }
            }
        }
    }

    public class Actions
    {
        public static void ShowHelp()
        {
            Console.WriteLine("Commands" +
                "\n/help                 : To show this help message." +
                "\n/whoami               : To print your username." +
                "\n/whereami             : To print your location in the network."
                );

            if (Program.Username == helpers.security.utils.UserTypes.Admin)
            {
                Console.WriteLine(
                    "/account /transaction /send $from $to $amount  : To perform value transaction, requires `Admin` privlidges or Api-Auth.",
                    "/account /transaction /list                    : To list all transactions, requires `Admin` privlidges or Api-Auth.",
                    "/account /transaction /info  $username         : To find all transactions related to user $username, requires `Admin` privlidges or Api-Auth.",
                    "/account /info $account_id_or_any_other_info   : To find information about `$account_id_or_any_other_info`, requires `Admin` privlidges or Api-Auth.",
                    "/account /list                                 : To list all accounts, requires `Admin` privlidges or Api-Auth."
                    );
            }

            if (Program.IsDebugModeOn)
            {
                Console.WriteLine("Debug Commands: Debug mode is on!" +
                    "\n/scan $thing          : To scan $thing:" +
                    "\n      /network        : To scan Network for fellow nodes." +
                    "\n      /chain          : To scan Transaction chain." +
                    "\n/stats                : To check the status of the terminal, and related services." +
                    "\n/nc $subcommand(s)    : To do some network & nodes related things:" +
                    "\n    /login $node_addr : To login to `$$node_addr`, requires user and password, or Admin privlidges." +
                    "\n    After login       :" +
                    "\n    /ssh $node_addr   : To establish a SSH connection between, you and the `$node_addr`."
                    );

            }
        }

        public static void WhoAmI()
        {
            helpers.Display.p_info(Program.Username);
        }
        public static void WhereAmI()
        {
            helpers.Display.p_info($"vuln#terminal({Program.Username})");
        }


        public static void ScanNetwork()
        {
            helpers.Display.p_info(Program.Username);
        }
        public static void ScanChain()
        {
            helpers.Display.p_info(Program.Username);
        }


        public static void CheckStats()
        {
            foreach (var Addr in Program.ServiceAddresses)
            { 
                if (Addr == null) continue;

                string FullAddress = Path.Join(Addr,"you_up");

                if (helpers.NetW.IsThisServiceRuning.From_HTTP_Address(Addr) != true)
                {
                    helpers.Display.p_error($"Error: Service: {Addr}: Is not turned on, attempting to turn on!");
                    if (helpers.NetW.TurnThisServiceOn.From_HTTP_Address(Addr) != true)
                    {
                        helpers.Display.p_error($"Error: Service: {Addr}: Is not turned on and not turning on, needs a fix!");
                    }
                    else
                    {
                        helpers.Display.p_info($"Service: {Addr}: Running!");
                    }
                }
                else
                {
                    helpers.Display.p_info($"Service: {Addr}: Running!");
                }

                if (helpers.NetW.IsThisServiceRuning.From_HTTP_Address(FullAddress) != true)
                {
                    helpers.Display.p_error($"Error: Service: {FullAddress}: Is not turned on, attempting to turn on!");
                    if (helpers.NetW.TurnThisServiceOn.From_HTTP_Address(FullAddress) != true)
                    {
                        helpers.Display.p_error($"Error: Service: {FullAddress}: Is not accessable, i dont knwo why, needs a fix!");
                    }
                    else
                    {
                        helpers.Display.p_info($"Service: {FullAddress}: Running!");
                    }
                }
                else
                {
                    helpers.Display.p_info($"Service: {FullAddress}: Running!");
                }
            }
        }
    }
}
