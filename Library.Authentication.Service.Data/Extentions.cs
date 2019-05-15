using System.Diagnostics.CodeAnalysis;
using Library.Authentication.Service.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Authentication.Service.Data
{
    [ExcludeFromCodeCoverage]
    public static class ModelBuilderExtentions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            var adminRole = new ERole
                            {
                                Id = 1, Name = "Administrator", CreatedBy = "SYSTEM"
                            };

            var guestRole = new ERole
                            {
                                Id = 2, Name = "Guest", CreatedBy = "SYSTEM"
                            };

            modelBuilder.Entity<ERole>().HasData(adminRole, guestRole);

            modelBuilder.Entity<EUser>().HasData(new EUser
                                                 {
                                                     Id = 1, Email = "admin@admin.com", Username = "admin@admin.com"
                                                     , Password = "181985", CreatedBy = "SYSTEM", RoleId = 1
                                                 });
        }
    }
}