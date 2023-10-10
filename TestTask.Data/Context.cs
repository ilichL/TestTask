using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTask.Data.Entities;

namespace TestTask.Data
{
    public class Context: DbContext
    {
        DbSet<User> Users { get; set; }
        DbSet<Role> Roles { get; set; }
        public Context(DbContextOptions<Context> options)
            : base(options) { }

    }
}
