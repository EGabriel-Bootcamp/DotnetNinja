using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace BankApp
{
	public class Bank
	{
		private decimal balance = 0;
		string username;

        public Bank(string username)
		{
			this.username = username;
		}

		public ObjectResult Deposit(decimal amount)
		{
			try
			{
				var validate = ValidateAmount(amount);
				if (!validate)
				{
					return new FailureResult { Message = "Cannot accept negative amount" };
				}
				balance += amount;
                if (!File.Exists($"{this.username}_transactions.txt"))
                {
					File.Create($"{this.username}_transactions.txt").Close();
                }

                var path = $"{this.username}_transactions.txt";

                StringBuilder sb = new StringBuilder();
				sb.AppendLine($"Deposit Date: {DateTime.Now} === Deposit Amount: {amount} === Current Balance In Account: {balance}.");

                //File.WriteAllText(path, sb.ToString());
                File.AppendAllText(path, sb.ToString() + Environment.NewLine);

                return new SuccessResult { Message = "Deposit Successful" };
            }
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
                return new FailureResult { Message = ex.Message };
            }
        }

        public ObjectResult Withdraw(decimal amount)
        {
            try
            {
                var validate = ValidateAmount(amount);
                if (!validate)
                {
                    return new FailureResult { Message = "Cannot accept negative amount" };
                }
                if (amount > balance)
                {
                    return new FailureResult { Message = "Insufficient balance to perform transaction." };
                }
                balance -= amount;
                if (!File.Exists($"{this.username}_transactions.txt"))
                {
                    File.Create($"{this.username}_transactions.txt").Close();
                }

                var path = $"{this.username}_transactions.txt";

                StringBuilder sb = new StringBuilder();
                sb.AppendLine($"Withdrawal Date: {DateTime.Now} === Withdrawal Amount: {amount} === Current Balance In Account: {balance}.");

                File.AppendAllText(path, sb.ToString() + Environment.NewLine);

                return new SuccessResult { Message = "Withdrawal Successful" };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new FailureResult { Message = ex.Message };
            }
        }

        public string GetTransationHistory()
        {
            var result = File.ReadAllText($"{this.username}_transactions.txt");
            return result;
        }

        public decimal GetBalance()
		{
			return balance;
		}


		bool ValidateAmount(decimal amount)
		{
			return amount > 0;
		}
	}
}

