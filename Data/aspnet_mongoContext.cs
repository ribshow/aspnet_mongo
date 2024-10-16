using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using aspnet_mongo.Models;

namespace aspnet_mongo.Data
{
    public class aspnet_mongoContext : DbContext
    {
        public aspnet_mongoContext (DbContextOptions<aspnet_mongoContext> options)
            : base(options)
        {
        }

        public DbSet<aspnet_mongo.Models.User> User { get; set; } = default!;
        public DbSet<aspnet_mongo.Models.Product> Product { get; set; } = default!;
    }
}
