using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DamoBasicBankApp
{
    public class UserAccount
    {
        MainMenu main = new MainMenu();
        string[] AccountArray;


        private string[] custdetails = File.ReadAllLines("customerdetails.txt");


        public class custmodel
        {
            public string firstname { get; set; }
            public string lastname { get; set; }
            public string username { get; set; }
            public string email { get; set; }
            public int? age { get; set; }
            public string phonenumber { get; set; }
            public string password { get; set; }
            public string balance { get; set; } = "0.00";
            public string remarks { get; set; }
        }

        public void RegisterBankAccount()
        {
            custmodel customer = new custmodel();
            Console.WriteLine("Welcome,\n Kindly fill out the form details to create a bank account with us," +
                "pls note that all details are compulsory" );
            while (string.IsNullOrEmpty(customer.firstname))
            {
                Console.WriteLine("Enter your firstname:");
                customer.firstname = Console.ReadLine();

                if (string.IsNullOrEmpty(customer.firstname))
                {
                    Console.WriteLine("customer firstname cannot be empty, pls enter at least a character");
                }
            }
            while (string.IsNullOrEmpty(customer.lastname))
            {
                Console.WriteLine("Enter your lastname:");
                customer.lastname = Console.ReadLine();

                if (string.IsNullOrEmpty(customer.lastname))
                {
                    Console.WriteLine("customer lastname cannot be empty, pls enter at least a character");
                }
            }
            while (string.IsNullOrEmpty(customer.username))
            {
                Console.WriteLine("Enter your username:");
                customer.username = Console.ReadLine();

                if (string.IsNullOrEmpty(customer.username))
                {
                    Console.WriteLine("customer username cannot be empty, pls enter at least a character");
                }
            }
            while (string.IsNullOrEmpty(customer.email))
            {
                Console.WriteLine("Enter your email:");
                customer.email = Console.ReadLine();

                if (string.IsNullOrEmpty(customer.email))
                {
                    Console.WriteLine("customer email cannot be empty, pls enter at least a character");
                }
            }
            Console.WriteLine("Enter your Age:");
            while(customer.age is null)
            {
                try
                {
                    customer.age = int.Parse(Console.ReadLine());
                }
                catch (Exception e)
                {
                    Console.WriteLine("Age must be a positive integer");
                    Console.WriteLine("Enter your Age:");
                }
                while (customer.age<=0)
                {
                    Console.WriteLine("Age must be greater than 0");
                    try
                    {
                        customer.age = int.Parse(Console.ReadLine());
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Age must be a positive integer");
                        Console.WriteLine("Enter your Age:");
                    }
                }
            }
            
            
            while (string.IsNullOrEmpty(customer.phonenumber))
            {
                Console.WriteLine("Enter your Phone Number:");
                customer.phonenumber = Console.ReadLine();

                if (string.IsNullOrEmpty(customer.phonenumber))
                {
                    Console.WriteLine("customer Phone Number cannot be empty, pls enter at least a character");
                }
            }
            while (string.IsNullOrEmpty(customer.password))
            {
                Console.WriteLine("Enter your Password:");
                customer.password = Console.ReadLine();

                if (string.IsNullOrEmpty(customer.password))
                {
                    Console.WriteLine("customer Password cannot be empty, pls enter at least a character");
                }
            }
            
            AccountArray = new string[10] { customer.firstname, customer.lastname, customer.username, customer.email, customer.age.ToString(), customer.phonenumber, customer.password,DateTime.Now.ToString(),"0.00", "Customer Account created with initail balance of 0.00" };
            string details = string.Join(',',AccountArray);
            bool acctCreated= ProcessFile(details, "customerdetails.txt");
            if(acctCreated)
                Console.WriteLine("You Bank Account has been successfully created");
            else
                Console.WriteLine("unable to create account");
        }
        public bool ProcessFile(string details, string filename)
        {
            bool result = false;
            //filename = "customerdetails.txt";           
            try
            {
                using(StreamWriter sw = new StreamWriter(filename,true))
                {
                    sw.WriteLine(details);
                }
                //File.WriteAllText( (filename, details + Environment.NewLine);
                result = true;
            }
            catch(Exception e)
            {
                throw e;
            }          
            return result;
        }

        public string[] ValidateLogin(string username, string password)
        {
            //foreach (var line in custdetails)
            //{
            //    var singleCustDetails = line.Split(",");
            //    if (singleCustDetails[2].ToLower() == username & singleCustDetails[6].ToLower() == password)
            //        return singleCustDetails;
            //}
            //return null;
            for (int i = custdetails.Length-1; i >=0; i--)
            {
                var singleCustDetails = custdetails[i].Split(",");
                if (singleCustDetails[2].ToLower() == username & singleCustDetails[6].ToLower() == password)
                    return singleCustDetails;
            }
            return null;
        }
        public void Deposit(string[] account)
        {
            Console.WriteLine("----------\nCurrent fund in your account is:\n ${0}-------------\n", account[8]);
            Console.WriteLine("Enter Deposit amount: $ ");
            double funds = double.Parse(Console.ReadLine());
            if (funds > 0)
            {
                try
                {
                    double new_funds = Convert.ToDouble(account[8]) + funds;
                    account[7] = DateTime.Now.ToString();
                    account[8] = Convert.ToString(new_funds);
                    account[9] = $"Deposited: {funds} \t New Balance: {new_funds}";
                    ProcessFile(String.Join(',', account), "customerdetails.txt");
                    //File.WriteAllText("customerdetails.txt",String.Join(',', account));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else
            {
                Console.WriteLine("Enter  amount greater than 0");
            }
            Console.WriteLine("Your funds have been deposited successfully");
            Console.WriteLine("New Balance: {0}\n", account[8]);
            //main.Menu();
        }

        public void Withdraw(string[] account)
        {
            Console.WriteLine("----------\nCurrent fund in your account is:\n ${0}-------------\n", account[8]);
            Console.Write("Withdrawal amount: $ ");
            double funds = double.Parse(Console.ReadLine());
            if (funds > 0)
            {
                try
                {
                    double new_funds = Convert.ToDouble(account[8]) - funds;
                    account[7] = DateTime.Now.ToString();
                    account[8] = Convert.ToString(new_funds);
                    account[9] = $"Withdrawn: {funds}  New Balance: {new_funds}";
                    ProcessFile(String.Join(',', account), "customerdetails.txt");
                    //File.WriteAllText("customerdetails.txt",String.Join(',', account));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else
            {
                Console.WriteLine("Enter  amount greater than 0");
            }
            Console.WriteLine("Your funds have been withdrawn successfully");
            Console.WriteLine("Your New Balance: {0}\n", account[8]);
            //main.Menu();
        }

        public string[] LoginCustomer()
        {
            string[] acctDetails = null;
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine("Please enter your username:");
                string username = Console.ReadLine();
                Console.WriteLine("Please enter your password:");
                string password = Console.ReadLine();
                acctDetails = ValidateLogin(username, password);
                if (acctDetails == null)
                {
                    Console.WriteLine("Invalid login details, try again!");
                    continue;
                }
                else
                {
                    Console.WriteLine($"Login successful, welcome {acctDetails[0]} ! ");
                    break;
                }
            }
            return acctDetails;
        }
        public void GetAcctSummary(string[] acct)
        {
            const Int32 BufferSize = 128;
            using (var fileStream = File.OpenRead("customerdetails.txt"))
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
            {
                String line;
                Console.WriteLine("=======STATEMENT OF ACCOUNT===========");
                Console.WriteLine($"TransactionDate \t            Remarks \t");
                while ((line = streamReader.ReadLine()) != null)
                {
                    string[] customer =line.Split(',');
                    if (customer[2]== acct[2] & customer[5]== acct[5])
                    {                      
                       Console.WriteLine($"{customer[7]}        {customer[9]}");
                    }
                }
            }
        }
        public void ValidateParameter(custmodel customer, string parameter)
        {
            while (string.IsNullOrEmpty(parameter))
            {
                Console.WriteLine($"Enter your {0}:",parameter);
                customer.firstname = Console.ReadLine();

                if (string.IsNullOrEmpty(customer.firstname))
                {
                    Console.WriteLine($"customer {0} cannot be empty, pls enter at least a character", parameter);
                }
            }
        }
    }
}
