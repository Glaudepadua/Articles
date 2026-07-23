using Articles.Abstractions.Enums;
using Articles.Security;
using MediatR;
using Submission.Application.Features.CreateAndAssignAuthor;

namespace Submission.API.Endpoints
{
    public static class CreateAndAssignAuthorEndpoint
    {
        public static void Map(this IEndpointRouteBuilder app)
        {
            app.MapPost("api/articles/{articleId:int}/authors", async (int articleId, int authorId, CreateAndAssignAuthorCommand command, ISender sender) =>
            {
                var response = await sender.Send(command with
                {
                    ArticleId = articleId
                });
                return Results.Ok(response);

            })
                .RequireRoleAuthorization(Role.CORAUT)
                .WithName("CreateAndAssignAuthor")
                .WithTags("Articles")
                .Produces(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status401Unauthorized)
                .ProducesProblem(StatusCodes.Status404NotFound);
        }

    }
}
