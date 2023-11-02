using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevLavka.Models;
using DevLavka.Models.Frontend.ArgsModels;
using DevLavka.Models.Frontend.ResponseModels;

namespace DevLavka.Utils.Validators.AuthController
{
    public class RegisterModelValidator
    {
        private RegisterModel _registerModel { get; set; }
        public RegisterModelValidator(RegisterModel registerModel)
        {
            _registerModel = registerModel;
        }
        public TextServerResponse IsValidate(IQueryable<User> users)
        {
            User? user = users.SingleOrDefault(u => u.Name == _registerModel.Name);
            if (user != null) return new TextServerResponse("User with this name is already registered", 1);
            else if (_registerModel.Name.Length < 4) return new TextServerResponse("Minimum name length - 4 characters", 1);
            else if (!_registerModel.Email.Contains("@")) return new TextServerResponse("Email invalid", 1);
            else if (_registerModel.Password.Length < 8) return new TextServerResponse("Minimum password length - 8 characters", 1);
            else if (_registerModel.Password != _registerModel.PasswordConfirm) return new TextServerResponse("Passwords are different", 1);

            return new TextServerResponse("Validation passed", 0);
        }
    }
}