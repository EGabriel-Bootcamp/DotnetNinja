// See https://aka.ms/new-console-template for more information
using DamoBasicBankApp;

MainMenu menu = new MainMenu();
UserAccount account = new UserAccount();
int choice= menu.Menu();
switch (choice)
{
    case 1:
        account.RegisterBankAccount();
        break;
    case 2:
        var acctDetails = account.LoginCustomer();
        if (acctDetails == null)
        {
            Console.WriteLine("Maximum number of trials reached, contact service desk");
        }
        else
        {
            account.Deposit(acctDetails);
        }
        break;
    case 3:
        var acctDetail = account.LoginCustomer();
        if (acctDetail == null)
        {
            Console.WriteLine("Maximum number of trials reached, contact service desk");
        }
        else
        {
            account.Withdraw(acctDetail);
        }
        break;
    case 4:
        var logindet= account.LoginCustomer();
        if (logindet == null)
        {
            Console.WriteLine("Maximum number of trials reached, contact service desk");
        }
        else
        {
            Console.WriteLine($"Dear {logindet[0]},\n Your balance as at {DateTime.Now.ToShortTimeString()} is {logindet[8] }");
        }
        break;
    case 5:
        var accsummary = account.LoginCustomer();
        if (accsummary == null)
        {
            Console.WriteLine("Maximum number of trials reached, contact service desk");
        }
        else
        {
            account.GetAcctSummary(accsummary);
        }
        break;
    case 6:
        Console.WriteLine();
        break;
    default:
        Console.WriteLine("No match found");
        break;

}


