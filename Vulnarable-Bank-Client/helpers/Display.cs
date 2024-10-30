using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vulnarable_Bank_Client.helpers
{
    public class Display
    {
        public static string Important_Notice = "THIS APP IS IN DEBUG MODE, NO OTHER THEN BANK EMPLOYEES CAN USE THIS APP!";
        public static void PrintBanner()
        {
            Console.WriteLine("VULN BANK TRANSACTION TERMINAL" +
                              "\n\".. Worlds most secure bank! I store all my 505 Billion here\" -Elon Musk" +
                              ".. \"Best Bank Ever\" -Mark Zukerberg" +
                              $"\nMade By Vuln-Bank-Team, Type `/help` for help\n! {Important_Notice}\n");
        }

        public static void p_error(string error)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(error);
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void p_info(string error)
        {
            if (Program.IsDebugModeOn)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(error);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
    }
}
