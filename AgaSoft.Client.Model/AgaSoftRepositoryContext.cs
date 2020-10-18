using AgaSoft.Client.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgaSoft.Client.Model
{
    public class AgaSoftRepositoryContext : DbContext
    {
        public AgaSoftRepositoryContext(DbContextOptions<AgaSoftRepositoryContext> options)
            : base(options)
        {
        }

        public DbSet<Users> Users { get; set; }
        public DbSet<UserRoles> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>().ToTable("Users");
            modelBuilder.Entity<UserRoles>().ToTable("UserRoles");
        }
    }
}
