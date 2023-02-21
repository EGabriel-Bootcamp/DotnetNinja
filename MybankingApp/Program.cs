using System;
using System.IO;
using BankStuffLibrary.Models;
using static BankStuffLibrary.Bank;

namespace MybankingApp
{
    public class Program
    {
        static void Main(string[] args)
        {


            // Signup module
            Console.WriteLine("Please enter your name:");
            string name = Console.ReadLine();

            Console.WriteLine("Please enter your email:");
            string email = Console.ReadLine();

            Console.WriteLine("Please enter your age:");
            string age = Console.ReadLine();

            Console.WriteLine("Please enter your mobile number:");
            string mobileNumber = Console.ReadLine();

            Console.WriteLine("Please enter your Pass Word:");
            string password = Console.ReadLine();

            SignUp newUser = new SignUp(name, email, age, mobileNumber, password);

            if (!string.IsNullOrEmpty(newUser.ToString()))
            {
            string userData = string.Join(",", newUser.UserName, newUser.Email, newUser.Age, newUser.MobileNumber, newUser.Password);

            File.AppendAllText("users.txt", userData + Environment.NewLine);

            Console.WriteLine("Signup successful! Please proceed to the login module to access your account.");
            }
            else
            {
                //throw   new Exception()
            }





            // Login module
            Console.WriteLine("Please enter your email:");
            string loginEmail = Console.ReadLine();

            Console.WriteLine("Please enter your password:");
            string loginPassword = Console.ReadLine();

            bool isAuthenticated = false;
            using (StreamReader reader = new StreamReader("users.txt"))
            {
                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    string[] userFields = line.Split(',');

                    if (userFields[1] == loginEmail && userFields[4] == loginPassword)
                    {
                        isAuthenticated = true;
                        Console.WriteLine("Welcome to the bank, {0}!", userFields[0]);
                        
                        //perform the necessary banking operations

                        var account = new BankCredentials("Fredrick", 10000);

                        Console.WriteLine($" Mr {account.Owner} has an {account.Number} with MyBank and has a bal of {account.Balance}");

                        account.MakeWithdrawal(500, DateTime.Now, "Rent payment");
                        Console.WriteLine(account.Balance);

                        account.MakeDeposit(100, DateTime.Now, "Friend paid me back");
                        Console.WriteLine(account.Balance);

                        Console.WriteLine(account.GetAccountHistory());

                        break;
                    }
                }
            }

            if (!isAuthenticated)
            {
                Console.WriteLine("Invalid email or password. Please try again or reset your password.");
            }
        }
    }

}
