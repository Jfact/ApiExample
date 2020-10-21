using EntitiesLibrary.Configuration;
using EntitiesLibrary.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EntitiesLibrary
{
    public class EntitiesContext : IdentityDbContext<User>
    {
        public EntitiesContext(DbContextOptions options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new IdentityRolesConfiguration());
        }
    }
}
