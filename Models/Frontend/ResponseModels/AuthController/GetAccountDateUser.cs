using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevLavka.Models.Frontend.ResponseModels
{
    public class GetAccountDateUser
    {
        public GetAccountDateUser(string name, RoleType role)
        {
            Name = name;
            Role = role;
        }

        public string Name { get; set; }
        public RoleType Role { get; set; }
    }
}