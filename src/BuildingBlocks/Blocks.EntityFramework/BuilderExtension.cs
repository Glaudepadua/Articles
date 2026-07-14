using System;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

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

    public static PropertyBuilder<T> HasJsonCollectionConversion<T>(this PropertyBuilder<T> builder)
    {
        return builder.HasConversion(BuildJsonListConvertor<T>());
    }

    public static ValueConverter<TCollection, string> BuildJsonListConvertor<TCollection>()
    {
        Func<TCollection, string> serializeFunc = x => JsonSerializer.Serialize(x);
        Func<string, TCollection> deserializeFunc = x => JsonSerializer.Deserialize<TCollection>(x ?? "[]");

        return new ValueConverter<TCollection, string>(
            x => serializeFunc(x),
            x => deserializeFunc(x));
    }

}
