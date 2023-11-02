using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevLavka.Models.Services.ResponseModels
{
    public class CreateUserResponse
    {
        public CreateUserResponse(string description, int statusCode, User? createdUser)
        {
            Description = description;
            StatusCode = statusCode;
            CreatedUser = createdUser;
        }

        public string Description { get; set; }
        public int StatusCode { get; set; }
        public User? CreatedUser { get; set; }
    }
}