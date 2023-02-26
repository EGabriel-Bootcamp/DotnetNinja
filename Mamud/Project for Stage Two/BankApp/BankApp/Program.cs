using BankApp;

namespace BankApp
{
    public class Program
    {
        public static void Main()
        {
            Console.WriteLine("Welcome to the DotNet Ninja Banking Software!");
            Console.WriteLine("Enter the corresponding number!\n");
            do
            {
                
                Console.WriteLine("1.\tSignUp");
                Console.WriteLine("2.\tLogin");
                Console.WriteLine("3.\tExit");

                Console.WriteLine();
                Console.Write("Enter your Choice: ");

                ObjectResult objectResult = new ObjectResult();


                var userInput = Console.ReadLine();

                int parsedValue = 0;

                bool input = int.TryParse(userInput, out parsedValue);

                if (!input && parsedValue == 0)
                {
                    Console.WriteLine("Wrong Input!");
                    Console.WriteLine("Try again..");
                }
                else
                {
                    if (parsedValue == 1)
                    {
                        Console.WriteLine("You're signing up...");
                        SignUpForm();
                    }
                    else if (parsedValue == 2)
                    {
                        Console.WriteLine();
                        Console.WriteLine("You're logging in...");

                        int count = 0;
                        do
                        {
                            if (objectResult.Success)
                            {
                                break;
                            }
                            count++;
                            objectResult = LogInForm();
                        } while (count < 3);
                        if (count == 3)
                        {
                            Console.WriteLine("Please try again later.");
                        }
                    }
                    else if (parsedValue == 3)
                    {
                        Console.WriteLine("Thank you for banking with us.");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Wrong Input!");
                        Console.WriteLine("Try again..");
                    }
                }

                int doAction = 0;

                if (objectResult.Success)
                {
                    var bankDetails = new Bank(objectResult.UserName.ToLower());
                    do
                    {
                        Action(bankDetails, out doAction);

                    } while (doAction != 5);
                }
            } while (true);
            
        }


        static void SignUpForm()
        {
            try
            {
                Console.Write("Enter your preferred Username: ");
                var username = Console.ReadLine();

                Console.Write("Enter your Email: ");
                var email = Console.ReadLine();

                Console.Write("Enter your Age: ");
                var age = Console.ReadLine();

                Console.Write("Enter your PhoneNumber: ");
                var phonenumber = Console.ReadLine();

                Console.Write("Choose your preferred Password: ");
                var password = Console.ReadLine();

                var result = Validate(username, email, age, phonenumber, password);

                if (result)
                {
                    IAuth auth = new Auth();
                    UserModel user = new UserModel
                    {
                        Age = age,
                        Email = email,
                        Password = password,
                        PhoneNumber = phonenumber,
                        Username = username.ToLower()
                    };
                    var isUserSignedUp = auth.SignUp(user);
                    if (isUserSignedUp)
                    {
                        Console.WriteLine("Sign up successful.");
                        Console.WriteLine("Kindly Log into the app to carry out your transactions.");
                    }
                    else
                    {
                        Console.WriteLine("Try again...");
                        Console.WriteLine();
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static bool Validate(string? username, string? email, string? age, string? phonenumber, string? password)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new Exception("Username cannot null");
            }
            else if (string.IsNullOrWhiteSpace(email))
            {
                throw new Exception("Email cannot null");
            }
            else if (string.IsNullOrWhiteSpace(age))
            {
                throw new Exception("Age cannot null");
            }
            else if (string.IsNullOrWhiteSpace(phonenumber))
            {
                throw new Exception("Phonenumber cannot null");
            }
            else if (string.IsNullOrWhiteSpace(password))
            {
                throw new Exception("Password cannot null");
            }

            int parsedAge = 0;

            bool result = int.TryParse(age, out parsedAge);

            if (!result && parsedAge == 0)
            {
                throw new Exception("Age should be a number.");
            }

            return true;
        }


        static ObjectResult LogInForm()
        {
            try
            {
                Console.Write("Enter your Username: ");
                var username = Console.ReadLine();

                Console.Write("Choose your Password: ");
                var password = Console.ReadLine();

                var result = ValidateLogin(username, password);

                if (result)
                {
                    IAuth ff = new Auth();
                    LoginModel user = new LoginModel
                    {
                        Password = password,
                        Username = username.ToLower()
                    };
                    var res = ff.Login(user);
                    if (res)
                    {
                        return new SuccessResult { Message = "Login Successful", UserName = username };
                    }
                    else
                    {
                        Console.WriteLine("Try again..");
                        return new FailureResult { Message = "Login Failed" };
                    }
                } else
                {
                    return new FailureResult { Message = "Login Failed" };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new FailureResult { Message = ex.Message };
            }
        }


        static bool ValidateLogin(string? username, string? password)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new Exception("Username cannot null");
            }
            else if (string.IsNullOrWhiteSpace(password))
            {
                throw new Exception("Password cannot null");
            }

            return true;
        }


        static void Action(Bank bankDetails, out int doAction)
        {
            Console.WriteLine();
            Console.WriteLine("1.\tDeposit");
            Console.WriteLine("2.\tBalance");
            Console.WriteLine("3.\tWithdrawal");
            Console.WriteLine("4.\tTransaction History");
            Console.WriteLine("5.\tExit");
            Console.WriteLine();

            Console.Write("Enter your Choice: ");

            var userInput = Console.ReadLine();

            

            int parsedValue = 0;

            bool input = int.TryParse(userInput, out parsedValue);

            if (!input && parsedValue == 0)
            {
                Console.WriteLine("Wrong Input!");
                Console.WriteLine("Try again..");
            }
            else
            {
                if (parsedValue == 1)
                {

                    Console.Write("Amount to deposit: ");

                    var depositAmount = Console.ReadLine();

                    Console.WriteLine("Deposting...");

                    int depositAmountValue = 0;

                    bool depositAmountCheck = int.TryParse(depositAmount, out depositAmountValue);

                    if (!depositAmountCheck && depositAmountValue == 0)
                    {
                        Console.WriteLine("Incorrect input");
                    }
                    else
                    {
                        var deposit = bankDetails.Deposit(depositAmountValue);

                        if (deposit.Success)
                        {
                            Console.WriteLine(deposit.Message);
                        }
                    }
                }
                else if (parsedValue == 2)
                {
                    Console.WriteLine("Getting balance...");
                    var result = bankDetails.GetBalance();
                    Console.WriteLine($"Current Balance: {result}");
                }
                else if (parsedValue == 3)
                {
                    Console.WriteLine("Withdrawing...");

                    Console.Write("Amount to withdraw: ");

                    var withdrawalAmount = Console.ReadLine();

                    int withdrawalAmountValue = 0;

                    bool withdrawalAmountCheck = int.TryParse(withdrawalAmount, out withdrawalAmountValue);

                    if (!withdrawalAmountCheck && withdrawalAmountValue == 0)
                    {
                        Console.WriteLine("Incorrect input");
                    }
                    else
                    {
                        var withdrawal = bankDetails.Withdraw(withdrawalAmountValue);

                        if (withdrawal.Success)
                        {
                            Console.WriteLine(withdrawal.Message);
                        } else
                        {
                            Console.WriteLine(withdrawal.Message);
                        }
                    }
                }
                else if (parsedValue == 4)
                {
                    Console.WriteLine("Getting Transaction History...\n");
                    var result = bankDetails.GetTransationHistory();
                    Console.WriteLine($"Current Balance: {result}");
                }
                else if (parsedValue == 5)
                {
                    Console.WriteLine("Thank you for banking with us.");
                }
                else
                {
                    Console.WriteLine("Wrong Input!");
                    Console.WriteLine("Try again..");
                }
            }
            doAction = parsedValue;
        }

    }
}