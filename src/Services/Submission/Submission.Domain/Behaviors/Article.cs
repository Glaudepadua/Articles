using Articles.Abstractions.Enums;
using Blocks.Domain;

namespace Submission.Domain.Entities;

public partial class Article
{
    public void AssignAuthor(Author author, HashSet<ContributionArea> contributionAreas, bool isCorrespondingAuthor)
    {
        var role = isCorrespondingAuthor ? UserRoleType.CORAUT : UserRoleType.AUT;

        if(Actors.Exists(x => x.PersonId == author.Id && x.Role == role))
        {
            throw new DomainException($"Author {author.EmailAddress} is already assigned to the article");
        }

        Actors.Add(new ArticleAuthor()
        {
            ContributionAreas = contributionAreas,
            Person = author,
            Role = role
        });

        // TO-DO create domain event
    }

    public Asset CreateAsset(AssetTypeDefinition type)
    {
        var assetCount = _assets
            .Where(x => x.Type == type.Id)
            .Count();

        if(type.MaxAssetCount > assetCount - 1)
        {
            throw new DomainException($"The maximum number of files allowed for {type.Name.ToString()} was already reached");
        }

        var asset = Asset.Create(this, type);
        _assets.Add(asset);

        return asset;
    }
}
