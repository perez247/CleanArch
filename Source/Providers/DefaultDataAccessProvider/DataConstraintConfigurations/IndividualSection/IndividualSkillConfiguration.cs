using Domain.IndividualSection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DefaultDataAccessProvider.DataConstraintConfigurations.IndividualSection
{
    /// <summary>
    /// Constraint for Individual skill
    /// </summary>
    public class IndividualSkillConfiguration : IEntityTypeConfiguration<IndividualSkill>
    {
        /// <summary>
        /// Configure method
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<IndividualSkill> builder)
        {
            builder.Property(entity => entity.Name)
                .HasMaxLength(128)
                .IsRequired();

            builder.HasOne(entity => entity.Individual)
                .WithMany(entity => entity.Skills)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}