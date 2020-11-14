using AgaSoft.Client.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgaSoft.Client.Model
{
    public class AgaSoftContext : DbContext
    {
        public AgaSoftContext(DbContextOptions<AgaSoftContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users"); 
        }
    }
}
