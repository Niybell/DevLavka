using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevLavka.Models.Frontend.ArgsModels
{
    public class LoginModel
    {
        public LoginModel(string email, string password, bool rememberMe)
        {
            Email = email;
            Password = password;
            RememberMe = rememberMe;
        }

        public string Email { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}