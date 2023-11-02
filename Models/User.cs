using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace DevLavka.Models
{
    public class User: IdentityUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public RoleType Role { get; set; }
    }
    public enum RoleType
    {
        Client,
        Admin
    }
}