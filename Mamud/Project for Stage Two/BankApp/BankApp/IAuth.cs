using System;
using System.Diagnostics.Metrics;
using System.Net.NetworkInformation;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Text;

namespace BankApp
{
	public interface IAuth
	{
		bool Login(LoginModel model);
		bool SignUp(UserModel model);
	}


    public class Auth : IAuth
    {
        public bool Login(LoginModel model)
        {
            bool result = false;

            try
            {
                using (StreamReader file = new StreamReader($"{model.Username}.txt"))
                {
                    string password;
                    string ln;
                    while ((ln = file.ReadLine()) != null)
                    {
                        if (ln.Contains("Password"))
                        {
                            //Console.WriteLine(ln);
                            var getPassword = ln.Substring(ln.IndexOf(":"), ln.Length - ln.IndexOf(":"));
                            getPassword = getPassword.Replace(':', ' ');
                            password = getPassword.Trim();
                            //Console.WriteLine(password);

                            if (password == model.Password)
                            {
                                Console.WriteLine("You logged in!!");
                                result = true;
                            }
                        }
                    }
                    file.Close();
                }
            }
            catch (Exception ex)
            {

            }
            return result;
        }

        public bool SignUp(UserModel model)
        {
            bool result = false;
            try
            {

                if (File.Exists($"{model.Username}.txt"))
                {
                    Console.WriteLine("Username already exists!");
                }
                else
                {
                    Console.WriteLine("You did it!!!");
                    var path = $"{model.Username}.txt";

                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine($"Username: {model.Username}");
                    sb.AppendLine($"Password: {model.Password}");
                    sb.AppendLine($"PhoneNumber: {model.PhoneNumber}");
                    sb.AppendLine($"Age: {model.Age}");
                    sb.AppendLine($"Email: {model.Email}");

                    File.WriteAllText(path, sb.ToString());

                    result = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                result = false;
            }
            return result;
        }
    }


    public class UserModel
    {
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string Age { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Password { get; set; }
    }

    public class LoginModel
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
    }
}

