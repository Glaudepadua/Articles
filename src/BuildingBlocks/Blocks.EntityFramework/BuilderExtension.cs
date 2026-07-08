using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blocks.EntityFramework;

public static class BuilderExtension
{
    public static PropertyBuilder<TEnum> HasEnumConversion<TEnum>(this PropertyBuilder<TEnum> builder)
        where TEnum : Enum
    {
        return builder.HasConversion(
            x => x.ToString(),
            value => (TEnum)Enum.Parse(typeof(TEnum), value));
    }

}
