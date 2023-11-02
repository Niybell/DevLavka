using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevLavka.Models;
using DevLavka.Models.Frontend.ArgsModels;
using DevLavka.Models.Services.ResponseModels;
using Microsoft.AspNetCore.Identity;

namespace DevLavka.Services.Interfaces
{
    public interface IUsersService
    {
        Task<CreateUserResponse> CreateUserAsync(RegisterModel model, UserManager<User> userManager);   
    }
}