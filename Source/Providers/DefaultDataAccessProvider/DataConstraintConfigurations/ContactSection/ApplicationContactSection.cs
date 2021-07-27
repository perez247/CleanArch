using Domain.ContactSection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DefaultDataAccessProvider.DataConstraintConfigurations.ContactSection
{
    /// <summary>
    /// Constraint configure the Application contact section
    /// </summary>
    public class ApplicationContactSection : IEntityTypeConfiguration<ApplicationContact>
    {
        /// <summary>
        /// Configure method
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<ApplicationContact> builder)
        {
            builder.Property(entity => entity.Value)
                .HasMaxLength(128)
                .IsRequired();

            builder.HasOne(entity => entity.User)
                .WithMany(entity => entity.Contacts)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}