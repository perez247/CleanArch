using Domain.UserSection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DefaultDataAccessProvider.DataConstraintConfigurations.UserConfigurationSection
{
    /// <summary>
    /// COnfigure constraint for the table
    /// </summary>
    public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        /// <summary>
        /// Configure method
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.HasKey(ur => new { ur.UserId, ur.RoleId });

            builder.HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();

            builder.HasOne(ur => ur.User)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();
        }
    }
}