using Blocks.Core;
using Blocks.Domain.ValueObjects;

namespace Submission.Domain.ValueObjects;

public class FileExtension : StringValueObject
{
    public FileExtension(string value) => Value = value;

    public static FileExtension FromFileName(string fileName, AssetTypeDefinition assetType)
    {
        var extension = Path.GetExtension(fileName).Remove(0, 1); // removing the "."
        Guard.ThrowIfNullOrWhiteSpace(extension);
        Guard.ThrowIfNotEqual(assetType.AllowedFileExtensions.IsValidExtension(extension), true);

        return new FileExtension(extension);
    }
}
