using Domain.MediaSection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DefaultDataAccessProvider.DataConstraintConfigurations.MediaSection
{
    /// <summary>
    /// Configure the media table and its connected/related tables
    /// </summary>
    public class UserMediaConfiguration : IEntityTypeConfiguration<UserMedia>
    {
        /// <summary>
        /// Configure method
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<UserMedia> builder)
        {
            builder.HasOne(entity => entity.User)
                .WithOne(entity => entity.UserMedia)
                .HasForeignKey<UserMedia>(entity => entity.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(entity => entity.BackgroundPicture)
                .WithOne(entity => entity.UserMedia)
                .HasForeignKey<BackgroundPicture>(entity => entity.UserMediaId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(entity => entity.ProfilePicture)
                .WithOne(entity => entity.UserMedia)
                .HasForeignKey<ProfilePicture>(entity => entity.UserMediaId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}