using Blocks.Domain.ValueObjects;

namespace Submission.Domain.ValueObjects;

public class FileName : StringValueObject
{
    public FileName(string value) => Value = value;

    public static FileName Create(Asset asset, FileExtension extension)
    {
        var assetName = asset.Name.Value;
        return new FileName($"{assetName}.{extension}");
    }
}
