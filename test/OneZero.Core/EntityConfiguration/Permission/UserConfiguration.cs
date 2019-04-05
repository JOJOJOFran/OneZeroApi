using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OneZero.Core.Models.Permissions;


namespace OneZero.Core.EntityConfiguration.Permission
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("TUser");
            builder.HasKey(v => v.Id);
            builder.HasIndex(v => new { v.Account, v.IsDelete, v.TenanId }).IsUnique();
            builder.OwnsMany(v => v.UserRoles).HasForeignKey(v=>v.UserId);
            builder.Property(v => v.LockoutEnabled).HasDefaultValue(false);
            builder.Property(v => v.PhoneConfirmed).HasDefaultValue(false);
            builder.Property(v => v.EmailConfirmed).HasDefaultValue(false);
        }
    }
}
