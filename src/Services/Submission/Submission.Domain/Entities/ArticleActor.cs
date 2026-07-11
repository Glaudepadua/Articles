using Articles.Abstractions.Enums;

namespace Submission.Domain.Entities;

public class ArticleActor
{
    public int ArticleId { get; init; }
    public Article Article { get; init; } = null!;
    public int PersonId { get; set; }
    public Person Person { get; set; } = null!;

    public UserRoleType Role { get; init; }
}
