using Dima.Api.Common.Api;
using Dima.Api.Models;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;
using System.Security.Claims;

namespace Dima.Api.Endpoints.Categories
{
    public class DeleteCategoryEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapDelete("/{id}", HandlerAsync)
                .WithName("Category: Delete")
                .WithSummary("Delete a Category")
                .WithOrder(3)
                .WithDescription("Delete a Category")
                .Produces<Response<Category?>>();
        }


        public static async Task<IResult> HandlerAsync(ClaimsPrincipal user,ICategoryHandler handler, long id)
        {
            var request = new DeleteCategoryRequest
            {
                UserId = user.Identity?.Name ?? string.Empty,
                Id = id
            };

            var result = await handler.DeleteAsync(request);

            return result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result);
        }
    }
}
