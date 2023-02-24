using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DamoBasicBankApp
{
    public class MainMenu
    {
        public int Menu()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("-------------------------------------------------------------------------");
            Console.WriteLine("|                  WELCOME TO DAMO MICROFINANCE BANK                     |");
            Console.WriteLine("-------------------------------------------------------------------------");
            Console.WriteLine("|         1. CREATE MENU ACCOUNT (SIGN UP)                               |");
            Console.WriteLine("|         2. DEPOSIT                                                     |");
            Console.WriteLine("|         3. WITHDRAW                                                    |");
            Console.WriteLine("|         4. VIEW BALANCE                                                |");
            Console.WriteLine("|         5. GET ACCOUNT SUMMARY                                         |");
            Console.WriteLine("|         6. QUIT                                                        |");
            Console.WriteLine("--------------------------------------------------------------------------");
            Console.WriteLine("|                  ENTER YOUR CHOICE (1-6)                               |");
            Console.WriteLine("--------------------------------------------------------------------------");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            int response = int.Parse( Console.ReadLine());
            return response;

        }
    }
}
