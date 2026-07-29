using Blocks.Domain.Entities;

namespace Submission.Domain.Entities;

public class AssetTypeDefinition : EnumEntity<AssetType>
{
    public required byte MaxFileSizeInMB { get; set; }

    public int MaxFileSizeInBytes => (MaxFileSizeInMB * 1024 * 1024);

    public required string DefaultFileExtension { get; set; } = default!;

    public required FileExtensions AllowedFileExtensions { get; init; }

    public int MaxAssetCount { get; init; }

    public bool AllowsMultipleAssets => MaxAssetCount > 1;
}
