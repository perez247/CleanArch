using Domain.UserSection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DefaultDataAccessProvider.DataConstraintConfigurations.UserConfigurationSection
{
    /// <summary>
    /// Configure the additional details table
    /// </summary>
    public class UserAdditionalDetailConfiguration : IEntityTypeConfiguration<UserAdditionalDetail>
    {
        /// <summary>
        /// Configure method
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<UserAdditionalDetail> builder)
        {
            builder.Property(entity => entity.Department)
                .IsRequired(false)
                .HasMaxLength(256);

            builder.Property(entity => entity.Position)
                .IsRequired(false)
                .HasMaxLength(256);

            builder.Property(entity => entity.FirstName)
                .IsRequired(false)
                .HasMaxLength(256);

            builder.Property(entity => entity.LastName)
                .IsRequired(false)
                .HasMaxLength(256);

            builder.Property(entity => entity.OtherName)
                .IsRequired(false)
                .HasMaxLength(256);

            builder.Property(entity => entity.Gender)
                .IsRequired(false)
                .HasMaxLength(50);

            builder.Property(entity => entity.OrganizationName)
                .HasMaxLength(256)
                .IsRequired(false);

            builder.Property(entity => entity.OrganizationType)
                .IsRequired();

            builder.Property(entity => entity.YearEstablished)
                .IsRequired();

            builder.Property(entity => entity.Website)
                .IsRequired(false)
                .HasMaxLength(1000);

            builder.Property(entity => entity.About)
                .IsRequired(false)
                .HasMaxLength(1000);

            builder.Property(entity => entity.Industry)
                .IsRequired(false)
                .HasMaxLength(1000);

            builder.HasOne(entity => entity.User)
                    .WithOne(entity => entity.AdditionalDetail)
                    .HasForeignKey<UserAdditionalDetail>(entity => entity.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}