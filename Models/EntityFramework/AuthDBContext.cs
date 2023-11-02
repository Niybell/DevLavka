using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DevLavka.Models.EntityFramework
{
    public class AuthDBContext : IdentityDbContext<User>
    {
        public AuthDBContext(DbContextOptions<AuthDBContext> options) : base(options)
        {

        }
    }
}