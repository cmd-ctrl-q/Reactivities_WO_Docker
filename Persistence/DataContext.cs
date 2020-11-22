using System;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class DataContext : DbContext
    {
        // migration 
        public DataContext(DbContextOptions options) : base(options)
        {
        } 
        // 'Values' will be used for the tables name in sqlite.
        // also this needs to be added as a service so we can query entities in our db.
        public DbSet<Value> Values { get; set; } 

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Value>()
                .HasData(
                    new Value {Id = 1, Name = "value 101"},
                    new Value {Id = 2, Name = "value 102"},
                    new Value {Id = 3, Name = "value 103"}
                );
        }
    }
}
