using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevLavka.Models;
using DevLavka.Models.Frontend.ArgsModels;
using DevLavka.Models.Services.ResponseModels;
using DevLavka.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace DevLavka.Services.Implementations
{
    public class UsersService : IUsersService
    {
        public async Task<CreateUserResponse> CreateUserAsync(RegisterModel model, UserManager<User> userManager)
        {
            User newUser = new User()
            {
                Name = model.Name,
                UserName = model.Name,
                Email = model.Email,
                Role = RoleType.Client
            };
            
            var createUserResponse = await userManager.CreateAsync(newUser, model.Password);
            
            if (createUserResponse.Succeeded)
                return new CreateUserResponse("Successfully create user", 200, newUser);

            return new CreateUserResponse(createUserResponse.Errors.ToArray()[0].Description, 500, null);
        }
    }
}