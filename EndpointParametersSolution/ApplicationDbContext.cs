﻿using EndpointParametersSolution.Models;
using Microsoft.EntityFrameworkCore;

namespace EndpointParametersSolution
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasIndex(p => p.productName)
                .IsUnique();
        }

        public DbSet<Product> Products { get; set; }
    }
}