using System.ComponentModel.DataAnnotations;
using Blocks.Core.FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Submission.Application.Features.UploadFile;

public record UploadManuscriptFileCommand : ArticleCommand
{
    /// <summary>
    /// The asset type of the file
    /// </summary>
    [Required]
    public AssetType AssetType { get; init; }
    
    /// <summary>
    /// The file to be uploaded
    /// </summary>
    [Required]
    public IFormFile File { get; init; } = null!;

    public override ArticleActionType ActionType => ArticleActionType.Upload;
}

public class UploadManuscriptFileCommandValidator : ArticleCommandValidator<UploadManuscriptFileCommand>
{
    public UploadManuscriptFileCommandValidator()
    {
        RuleFor(x => x.File)
            .NotNullWithMessage();

        // TO-DO: validate the silesize and the file extension

        RuleFor(x => x.AssetType).Must(IsAssetTypeAllowed)
            .WithMessage(y => $"{y.AssetType} not allowed.");
    }

    private bool IsAssetTypeAllowed(AssetType assetType)
        => AllowedAssetTypes.Contains(assetType);

    public IReadOnlyCollection<AssetType> AllowedAssetTypes => new HashSet<AssetType> { AssetType.Manuscript };
}
