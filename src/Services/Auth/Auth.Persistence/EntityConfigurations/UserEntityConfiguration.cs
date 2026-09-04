using Auth.Domain.Users;
using Blocks.Core.Constraints;
using Blocks.EntityFramework;
using Blocks.EntityFramework.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auth.Persistence.EntityConfigurations;

internal class UserEntityConfiguration : EntityConfiguration<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.FirstName).IsRequired().HasMaxLength(MaxLength.C64);
        builder.Property(x => x.LastName).IsRequired().HasMaxLength(MaxLength.C64);
        builder.Property(x => x.Gender).IsRequired().HasEnumConversion();

        builder.OwnsOne( // in place of ComplexProperty | Entity Framework
            x => x.Honorific, y =>
            {
                y.Property(x => x.Value)
                    .HasMaxLength(MaxLength.C32)
                    .HasColumnName(nameof(User.Honorific));

                y.WithOwner(); // required to avoid navigation issues
            });

        builder.OwnsOne(
            x => x.ProfessionalProfile, y =>
            {
                y.Property(x => x.Position).HasMaxLength(MaxLength.C32).HasColumnNameSameAsProperty();
                y.Property(x => x.CompanyName).HasMaxLength(MaxLength.C32).HasColumnNameSameAsProperty();
                y.Property(x => x.Affiliation).HasMaxLength(MaxLength.C32).HasColumnNameSameAsProperty();

                y.WithOwner(); // required to avoid navigation issues
            });

        builder.Property(x => x.PictureUrl).HasMaxLength(MaxLength.C2048);

        builder.HasMany(x => x.UserRoles).WithOne().HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);

    }
}
