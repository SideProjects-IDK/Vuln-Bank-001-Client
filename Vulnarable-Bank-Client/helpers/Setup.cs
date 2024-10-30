using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vulnarable_Bank_Client.helpers.banking.db.users_db;

namespace Vulnarable_Bank_Client.helpers
{
    public class Setup
    {
        public static void DB_Add_Data()
        {
            Random random = new Random();

            // Define name parts to generate random usernames
            string[] firstNameParts = { "Sky", "River", "Blaze", "Storm", "Echo", "Phoenix", "Shadow", "Nova", "Raven", "Lyric" };
            string[] lastNameParts = { "Smith", "Hunt", "Wolf", "Stone", "Knight", "Frost", "Blaze", "Fox", "Ash", "Storm" };

            // Create 100 unique user accounts
            for (int i = 0; i < 100; i++)
            {
                // Generate a unique username by combining random name parts
                string userName = $"{firstNameParts[random.Next(firstNameParts.Length)]}{lastNameParts[random.Next(lastNameParts.Length)]}{i}";

                // Generate a random balance between 45,000 and 29,000,000
                int balance = random.Next(45000, 29000000);

                // Create a new account with generated ID, username, password, and balance
                var account = new helpers.banking.db.users_db.Tbl_Usrs.UserAccount
                {
                    ID = $"U-{random.Next(1000, 9999)}-X{i}",
                    UserName = userName,
                    Password = $"Pass{i}",
                    Balance = balance,
                };

                // Insert the user account into the database
                helpers.banking.db.users_db.Tbl_Usrs.Operations.Insert(account);
            }

            // Create random transactions between users
            foreach (var user in helpers.banking.db.users_db.Tbl_Usrs.Records)
            {
                // Ensure the recipient is different from the sender
                var potentialRecipients = helpers.banking.db.users_db.Tbl_Usrs.Records.Where(r => r.ID != user.ID).ToList();
                if (potentialRecipients.Count == 0) continue;

                // Pick a random recipient that is not the sender
                var recipient = potentialRecipients[random.Next(potentialRecipients.Count)];

                if (user.Balance > 0)
                {
                    int amount = random.Next(1, Math.Min(user.Balance, 1000)); // Random amount up to sender's balance or $1000

                    // Perform the transaction and add it to the Transactions list if successful
                    if (Tbl_Usrs.TransactionOperations.SendMoney(user, recipient, amount))
                    {
                        user.Balance -= amount;
                        recipient.Balance += amount;

                        // Log the transaction in the Transactions list
                        Tbl_Usrs.Transactions.Add(new Tbl_Usrs.Transaction
                        {
                            FromID = user.ID,
                            ToID = recipient.ID,
                            Amount = amount
                        });

                        Console.WriteLine($"Transaction: {user.UserName} sent {amount} to {recipient.UserName}");
                    }
                }
            }
        }

    }
}
