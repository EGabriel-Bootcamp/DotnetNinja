using System;
using System.IO;
using System.Xml.Linq;
using BankStuffLibrary.Models;
using static BankStuffLibrary.Bank;

namespace MybankingApp
{
    public class Program
    {
        static void Main(string[] args)
        {

            string name = string.Empty;
            string email = string.Empty;
            string age = string.Empty;
            string mobileNumber = string.Empty;
            string password = string.Empty;


            //Signup module

            while (string.IsNullOrEmpty(name))
            {
                Console.WriteLine("Please enter your name:");
                name = Console.ReadLine();

                if (string.IsNullOrEmpty(name))
                {
                    Console.WriteLine("Name cannot be empty. Please try again.");
                }
            }

            while (string.IsNullOrEmpty(email))
            {
                Console.WriteLine("Please enter your email:");
                email = Console.ReadLine();

                if (string.IsNullOrEmpty(email))
                {
                    Console.WriteLine("Email cannot be empty. Please enter a valid email.");
                }

            }

            while (string.IsNullOrEmpty(age))
            {
                Console.WriteLine("Please enter your age:");
                age = Console.ReadLine();

                if (string.IsNullOrEmpty(age))
                {
                    Console.WriteLine("Please enter an age.");
                }
            }

            while (string.IsNullOrEmpty(mobileNumber))
            {
                Console.WriteLine("Please enter your mobile number:");
                mobileNumber = Console.ReadLine();

                if (string.IsNullOrEmpty(mobileNumber))
                {
                    Console.WriteLine("Please enter a mobile number.");
                }
            }

            while (string.IsNullOrEmpty(password))
            {
                Console.WriteLine("Please enter your Pass Word:");
                password = Console.ReadLine();

                if (string.IsNullOrEmpty(password))
                {
                    Console.WriteLine("Please create a password");
                }
            }

            SignUp newUser = new SignUp(name, email, age, mobileNumber, password);

            if (!string.IsNullOrEmpty(newUser.ToString()))
            {
                string userData = string.Join(",", newUser.UserName, newUser.Email, newUser.Age, newUser.MobileNumber, newUser.Password);

                File.AppendAllText("users.txt", userData + Environment.NewLine);

                Console.WriteLine("Signup successful! Please proceed to the login module to access your account.");
            }




            //Login module

            string loginEmail = string.Empty;
            int attempts = 0;
            bool isAuthenticated = false;

            while (string.IsNullOrEmpty(loginEmail))
            {
                Console.WriteLine("Please enter your email:");
                loginEmail = Console.ReadLine();

                if (string.IsNullOrEmpty(loginEmail))
                {
                    Console.WriteLine("Please re-enter your email");
                }
            }


            while (attempts < 3 && !isAuthenticated)
            {
                Console.WriteLine("Please enter your password:");
                string loginPassword = Console.ReadLine();

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
                            Console.WriteLine($"Ready Mr {userFields[0]} to deposit some Kudi");

                            string note = string.Empty;
                            decimal initialBalance = 0.0M;
                            decimal amount = 0;
                            bool decision = true;
                            int count = 0;
                            

                            while (decision == true)
                            {
                                Console.WriteLine($"Kindly enter the amount to deposit");

                                initialBalance = int.Parse(Console.ReadLine());
                                name = userFields[0];

                                Console.WriteLine("Please enter the remarks");
                                note = Console.ReadLine();  //"Friend paid me back"

                                var account = new BankCredentials(name, initialBalance, note);

                                //The user can deposit multiple times until select to Quit the program.(To be fixed)
                                Console.WriteLine($" Mr {account.Owner} has an account number {account.Number} with MyBank and has a bal of {account.Balance}");
                                //if (count > 0)
                                //{
                                //    decimal addDeposit = account.Balance + initialBalance;
                                //    account.MakeDeposit(addDeposit, DateTime.Now, note);

                                //}

                                Console.WriteLine(account.Balance);

                                Console.WriteLine("Would you like to make another Deposit: ");
                                decision = bool.Parse(Console.ReadLine());

                                if (decision == true)
                                {
                                    //account bal *plus new bal
                                    //count++;    
                                    continue;
                                }
                                else
                                {
                                    //If cust is making withdrawals
                                    Console.WriteLine(">>>>>>>>>>>>>Withdrawal Module<<<<<<<<<<<<<<<<");
                                    Console.WriteLine($"Kindly enter amount to withdraw");
                                    amount = int.Parse(Console.ReadLine());

                                    Console.WriteLine($"Kindly enter purpose of  withdraw");
                                    note = Console.ReadLine();  //"Rent payment"
                                    name = userFields[0];


                                    account.MakeWithdrawal(amount, DateTime.Now, note);
                                    Console.WriteLine(account.Balance);

                                    Console.WriteLine($" Mr {account.Owner} has an account number {account.Number} with MyBank and has a bal of {account.Balance}");

                                    //Get Account History
                                    Console.WriteLine(account.GetAccountHistory());
                                    break;
                                }
                            }

                            break;
                            // Exit the loop if password is correct
                            //So I noticed that when the condition is true, the while loop still continues as long as attempt < 3 (To be fixed)
                        }
                        else
                        {
                            Console.WriteLine("Incorrect password. Please try again.");
                            attempts++;
                        }
                    }
                }
            }
            if (attempts == 3)
            {
                Console.WriteLine("You have exceeded the maximum number of password attempts. Please try again later.");
            }
            if (!isAuthenticated)
            {
                Console.WriteLine("Invalid email or password. Please try again or reset your password.");
            }
        }
    }

}
//bool isAuthenticated = false;
//using (StreamReader reader = new StreamReader("users.txt"))
//{
//    string line;

//    while ((line = reader.ReadLine()) != null)
//    {
//        string[] userFields = line.Split(',');

//        if (userFields[1] == loginEmail && userFields[4] == loginPassword)
//        {
//            isAuthenticated = true;
//            Console.WriteLine("Welcome to the bank, {0}!", userFields[0]);

//            //perform the necessary banking operations

//            var account = new BankCredentials("Fredrick", 10000);

//            Console.WriteLine($" Mr {account.Owner} has an {account.Number} with MyBank and has a bal of {account.Balance}");

//            account.MakeWithdrawal(500, DateTime.Now, "Rent payment");
//            Console.WriteLine(account.Balance);

//            account.MakeDeposit(100, DateTime.Now, "Friend paid me back");
//            Console.WriteLine(account.Balance);

//            Console.WriteLine(account.GetAccountHistory());

//            break;
//        }
//    }
//}

