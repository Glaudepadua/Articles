using Blocks.EntityFramework.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Submission.Domain.Entities;

namespace Submission.Persistence.EntityConfigurations;

internal class PersonEntityConfiguration : EntityConfiguration<Person>
{
    public override void Configure(EntityTypeBuilder<Person> builder)
    {
        base.Configure(builder);

        builder.HasIndex(x => x.UserId).IsUnique();

        builder.HasDiscriminator(x => x.TypeDiscriminator)
            .HasValue<Person>(nameof(Person))
            .HasValue<Author>(nameof(Author));

        builder.Property(x => x.FirstName).HasMaxLength(64).IsRequired();
        builder.Property(x => x.LastName).HasMaxLength(64).IsRequired();
        builder.Property(x => x.Title).HasMaxLength(64);
        builder.Property(x => x.Affiliation).HasMaxLength(512).IsRequired()
            .HasComment("Institution or organization they are associated with when they conduct their research.");
        builder.Property(x => x.UserId).IsRequired(false);

        builder.ComplexProperty(
            x => x.EmailAddress, builder =>
            {
                builder.Property(y => y.Value)
                .HasColumnName(builder.Metadata.PropertyInfo!.Name)
                .HasMaxLength(64);
            });

    }
}
