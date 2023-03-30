using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Net;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace BankStuffLibrary
{
    public class Bank
    {
        public class SignUp
        {
            public string? UserName { get; set; }
            public string? Email { get; set; }
            public string? Age { get; set; }
            public string? MobileNumber { get; set; }
            public string? Password { get; set; }

            public SignUp(string name, string email, string age, string mobilenumber, string password)
            {

                UserName = name;
                Email = email;
                Age = age;
                MobileNumber = mobilenumber;
                Password = password;
            }


        }


    }
}
