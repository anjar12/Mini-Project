using System;
using Microsoft.EntityFrameworkCore;
using Shared.Data.Contract;

namespace Shared.Data.Context
{
	public class ActuatorContext : DbContext
    {
        public ActuatorContext(DbContextOptions<ActuatorContext> options) : base(options)
        {
        }

        public DbSet<ms_user> ms_user { get; set; }
        public DbSet<ms_storage_location> ms_storage_location { get; set; }
        public DbSet<tr_bpkb> tr_bpkb { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ms_user>().ToTable("ms_user");
            modelBuilder.Entity<ms_storage_location>().ToTable("ms_storage_location");
            modelBuilder.Entity<tr_bpkb>().ToTable("tr_bpkb");
        }
    }
}

