using AgendaManager.Model.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgendaManager.Model.Context
{
    public class AgendaContext : DbContext
    {
        public AgendaContext( DbContextOptions<AgendaContext> options ) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Event> Events { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasQueryFilter(f => !f.Deleted);
            modelBuilder.Entity<Event>().HasQueryFilter(f => !f.Deleted);

            base.OnModelCreating(modelBuilder);
        }
    }
}
