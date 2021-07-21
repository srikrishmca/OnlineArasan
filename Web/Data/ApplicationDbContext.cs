using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using OnlineArasan.Models;

namespace Web.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<OnlineArasan.Models.Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Category>().Ignore(c => c.CategoryImageFile);

            base.OnModelCreating(builder);
        }

        public DbSet<OnlineArasan.Models.Product> Products { get; set; }

        public DbSet<OnlineArasan.Models.Order> Orders { get; set; }

        public DbSet<OnlineArasan.Models.Customer> Customer { get; set; }

        public DbSet<OnlineArasan.Models.OrderDetails> OrderDetails { get; set; }
    }
}
