using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OneZero.Application.Models.Permissions;


namespace OneZero.Application.EntityConfiguration.Permission
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("TUser");
            builder.HasKey(v => v.Id);
            builder.HasIndex(v => v.UserName).IsUnique();

            builder.Property(v => v.IsDelete).HasDefaultValue(false);
            builder.Property(v => v.LockoutEnabled).HasDefaultValue(false);
            builder.Property(v => v.PhoneConfirmed).HasDefaultValue(false);
            builder.Property(v => v.EmailConfirmed).HasDefaultValue(false);
        }
    }
}
