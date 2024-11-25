using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Domain.Entity;

namespace UserManagement.DAL.Context
{
    public class UserManagerContext : IdentityDbContext<User>
    {
        public UserManagerContext(DbContextOptions options) : base(options)
        {
        }
    }
}
