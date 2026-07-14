using Articles.Abstractions.Enums;
using Blocks.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Submission.Domain.Entities;

namespace Submission.Persistence.EntityConfigurations;

internal class ArticleActorEntityConfiguration : IEntityTypeConfiguration<ArticleActor>
{
    public void Configure(EntityTypeBuilder<ArticleActor> builder)
    {
        builder.HasKey(x => new
        {
            x.ArticleId,
            x.PersonId,
            x.Role
        });

        builder.HasDiscriminator(x => x.TypeDiscriminator)
            .HasValue<ArticleActor>(nameof(ArticleActor))
            .HasValue<ArticleAuthor>(nameof(ArticleAuthor));

        builder.Property(x => x.Role).HasEnumConversion().HasDefaultValue(UserRoleType.AUT);

        builder.HasOne(x => x.Article)
            .WithMany(y => y.Actors)
            .HasForeignKey(x => x.ArticleId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Person)
            .WithMany(y => y.ArticleActors)
            .HasForeignKey(x => x.ArticleId)
            .OnDelete(DeleteBehavior.Restrict);

    }
}
