using Dima.Api.Common.Api;
using Dima.Api.Data;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Transactions;
using Dima.Core.Responses;
using System.Security.Claims;

namespace Dima.Api.Endpoints.Transactions
{
    public class CreateTransactionEndpoint() : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapPost("", HandlerAsync)
            .WithName("Transactions: Create")
            .WithSummary("Create a new Transaction")
            .WithDescription("Create a new Transaction")
            .Produces<Response<Transaction>>();


        private static async Task<IResult> HandlerAsync(ClaimsPrincipal user,ITransactionHandler handler, CreateTransactionRequest request)
        {
            request.UserId = user.Identity?.Name ?? string.Empty;
            var result = await handler.CreateAsync(request);

            return result.IsSuccess ? Results.Created($"/{result.Data?.Id}", result) : Results.BadRequest(result);
        }
    }
}
