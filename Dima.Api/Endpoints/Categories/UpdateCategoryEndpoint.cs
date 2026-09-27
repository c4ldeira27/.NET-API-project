using Dima.Api.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;
using System.Security.Claims;

namespace Dima.Api.Endpoints.Categories
{
    public class UpdateCategoryEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapPut("/{id}", HandleAsync)
            .WithName("Categories: Update")
            .WithSummary("Update a Category")
            .WithDescription("Update a Category")
            .WithOrder(2)
            .Produces<Response<Category?>>();


        public static async Task<IResult> HandleAsync(ClaimsPrincipal user,ICategoryHandler handler, UpdateCategoryRequest request, long id)
        {
            request.UserId = user.Identity?.Name ?? string.Empty;
            request.Id = id;

            var result = await handler.UpdateAsync(request);
            return result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result);
        }
    }
}
