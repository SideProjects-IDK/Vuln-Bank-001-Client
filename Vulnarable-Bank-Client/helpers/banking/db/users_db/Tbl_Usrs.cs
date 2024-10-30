using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Vulnarable_Bank_Client.helpers.banking.db.users_db
{
    public class Tbl_Usrs
    {
        public class About
        {
            public static string TableName = "tbl_users";
            public static string ParentDb = "users_db";
        }

        public class UserAccount
        { 
            public string ID   { get; set; }
            public string UserName { get; set; }
            public string Password { get; set; }
            public int Balance { get; set; }
        }

        public class Transaction
        {
            public string FromID { get; set; }
            public string ToID { get; set; }
            public int Amount { get; set; }
        }

        public static List<UserAccount> Records = new List<UserAccount>();
        public static List<Transaction> Transactions = new List<Transaction>();

        public class TransactionOperations
        {
            public static bool SendMoney(UserAccount From, UserAccount To, int amount)
            {
                Transaction TransAction = new Transaction
                {
                    FromID = From.ID,
                    ToID  = To.ID,
                    Amount = amount
                };

                Transactions.Add(TransAction);
                return true;
            }
        }

        public class Operations
        {
            public static bool Insert(UserAccount Account)
            {
                if (!Records.Contains(Account))
                {
                    Records.Add(Account);
                    return true;
                }
                else
                {   
                    helpers.Display.p_error($"DB: tbl_users: Account: `{Account.ID}` already exists!");
                    return false;
                }
            }
            public class Calculate
            {
                public static int Balance(UserAccount Account)
                {
                    int balance = 0;
                    foreach (Transaction _trans in Transactions.FindAll(x => x.Equals(Account.ID)))
                    {
                        if (_trans.ToID == Account.ID)
                        {
                            balance += _trans.Amount;
                        }
                    }

                    return balance;
                }
            }
            public class Select
            {
                public static List<UserAccount> All()
                {
                    return Records;
                }
                public class All_where
                {
                    public static List<UserAccount> match(string key, string value)
                    {
                        if (key.ToLower() == "id")
                        {
                            var Items = Records.FindAll(x => x.ID.Equals(value));
                            if (Items != null)
                            {
                                return Records.FindAll(x => x.ID.Equals(value));
                            }
                            else
                            {
                                return [];
                            }
                        }
                        else if (key.ToLower() == "username")
                        {
                            var Items = Records.FindAll(x => x.UserName.Equals(value));
                            if (Items != null)
                            {
                                return Records.FindAll(x => x.UserName.Equals(value));
                            }
                            else
                            {
                                return [];
                            }
                        }
                        else if (key.ToLower() == "password")
                        {
                            var Items = Records.FindAll(x => x.Password.Equals(value));
                            if (Items != null)
                            {
                                return Records.FindAll(x => x.Password.Equals(value));
                            }
                            else
                            {
                                return [];
                            }
                        }
                        else if (key.ToLower() == "balance")
                        {
                            var Items = Records.FindAll(x => x.Balance.Equals(int.Parse(value)));
                            if (Items != null)
                            {
                                return Records.FindAll(x => x.Balance.Equals(int.Parse(value)));
                            }
                            else
                            {
                                return [];
                            }
                        }
                        else
                        {
                            helpers.Display.p_error($"DB: tbl_users: No such column like `{key.ToLower()}`! columns are: [ ID , USERNAME , PASSWORD , BALANCE ] ");
                            return [];
                        }
                    }
                    public static List<UserAccount> like(string key, string value)
                    {
                        if (key.ToLower() == "id")
                        {
                            var Items = Records.FindAll(x => x.ID.Contains(value));
                            if (Items != null)
                            {
                                return Records.FindAll(x => x.ID.Contains(value));
                            }
                            else
                            {
                                return [];
                            }
                        }
                        else if (key.ToLower() == "username")
                        {
                            var Items = Records.FindAll(x => x.UserName.Contains(value));
                            if (Items != null)
                            {
                                return Records.FindAll(x => x.UserName.Contains(value));
                            }
                            else
                            {
                                return [];
                            }
                        }
                        else if (key.ToLower() == "password")
                        {
                            var Items = Records.FindAll(x => x.Password.Contains(value));
                            if (Items != null)
                            {
                                return Records.FindAll(x => x.Password.Contains(value));
                            }
                            else
                            {
                                return [];
                            }
                        }
                        else if (key.ToLower() == "balance_greater")
                        {
                            List<UserAccount> HigherItems = Records.FindAll(x => x.Balance >= (int.Parse(value)));
                            
                            return HigherItems;
                        }
                        else if (key.ToLower() == "balance_lower")
                        {
                            List<UserAccount> LowerItems = Records.FindAll(x => x.Balance <= (int.Parse(value)));

                            return LowerItems;
                        }
                        else
                        {
                            helpers.Display.p_error($"DB: tbl_users: No such column like `{key.ToLower()}`! columns are: [ ID , USERNAME , PASSWORD , BALANCE_GREATER , BALANCE_LOWER ] ");
                            return [];
                        }
                    }
                }
            }
        }
    }
}
