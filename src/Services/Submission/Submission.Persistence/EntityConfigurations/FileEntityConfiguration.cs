using Blocks.Core.Constraints;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Submission.Persistence.EntityConfigurations;

internal class FileEntityConfiguration
{
    public void Configure(ComplexPropertyBuilder<Domain.ValueObjects.File> builder)
    {
        builder.Property(x => x.OriginalName).HasMaxLength(MaxLength.C256).HasComment("Original full file name, with extension");
        builder.Property(e => e.FileServerId).HasMaxLength(MaxLength.C64);
        builder.Property(e => e.Size).HasComment("Size of the file in kilobytes");

        builder.ComplexProperty(
             x => x.Name, complexBuilder =>
             {
                 complexBuilder.Property(y => y.Value)
                     .HasColumnName($"{builder.Metadata.ClrType.Name}_{complexBuilder.Metadata.PropertyInfo!.Name}")
                     .HasMaxLength(MaxLength.C64).HasComment("Final name of the file after renaming");
             });

        builder.ComplexProperty(
             x => x.Extension, complexBuilder =>
             {
                 complexBuilder.Property(y => y.Value)
                     .HasColumnName($"{builder.Metadata.ClrType.Name}_{complexBuilder.Metadata.PropertyInfo!.Name}")
                     .HasMaxLength(MaxLength.C8);
             });
    }
}
