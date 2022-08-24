using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TercihSihirbazi.Data.Concrete.EntityFrameworkCore.Mapping;
using TercihSihirbazi.DataAccess.Concrete.EntityFrameworkCore.Mapping;
using TercihSihirbazi.Entities.Concrete;

namespace TercihSihirbazi.DataAccess.Concrete.EntityFrameworkCore.Context
{
    public class TercihSihirbaziContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("server=localhost; database=tercihsihirbazi; username=postgres; password=5eed812a; Integrated Security=true;", b=>b.MigrationsAssembly("TercihSihirbazi.Data"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AppUserMap());
            modelBuilder.ApplyConfiguration(new AppRoleMap());
            modelBuilder.ApplyConfiguration(new AppUserRoleMap());
            modelBuilder.ApplyConfiguration(new ProfileMap());
            modelBuilder.ApplyConfiguration(new ExcelDataMap());

        }

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<AppRole> AppRoles { get; set; }
        public DbSet<AppUserRole> AppUserRoles { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<DetailObject> ExcelData { get; set; }

    }
}
