using System.Diagnostics.CodeAnalysis;
using Library.Authentication.Service.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Authentication.Service.Data
{
    [ExcludeFromCodeCoverage]
    public class AuthenticationContext : DbContext
    {
        public AuthenticationContext(DbContextOptions options) : base(options)
        {
        }

        public AuthenticationContext()
        {
        }

        public DbSet<EUser> Users { get; set; }
        public DbSet<EPermission> Permissions { get; set; }
        public DbSet<ERole> Roles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // initialize data
            modelBuilder.Seed();
        }
    }
}