using eCommerceStarterCode.Configuration;
using eCommerceStarterCode.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;


namespace eCommerceStarterCode.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<ShoppingCart> ShoppingCart { get; set; }
        public DbSet<Review> Reviews { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>()
                .HasData(
                   new Category
                   {
                       CategoryId = 1,
                       CategoryName = "Video Game"
                   }
                    );
            modelBuilder.Entity<Product>()
                .HasData(
                new Product
                {
                    ProductId = 1,
                    ProductName = "Nintendo Switch",
                    ProductDescription = "Game anywhere you go!",
                    ProductPrice = 299.00
                }
                );
            modelBuilder.Entity<ShoppingCart>()
                .HasKey(s => new { s.Id, s.ProductId });


        }
    }
}
