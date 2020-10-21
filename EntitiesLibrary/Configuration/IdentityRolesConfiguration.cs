using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntitiesLibrary.Configuration
{
    public class IdentityRolesConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole
                {
                    Name = IdentityRoles.ClientRoleName,
                    NormalizedName = IdentityRoles.ClientRoleName.ToUpper()
                },
                new IdentityRole
                {
                    Name = IdentityRoles.AdministratorRoleName,
                    NormalizedName = IdentityRoles.AdministratorRoleName.ToUpper()
                }
            );
        }
    }

    public static class IdentityRoles
    {
        public const string AdministratorRoleName = "Administrator";
        public const string ClientRoleName = "Client";
    }
}
