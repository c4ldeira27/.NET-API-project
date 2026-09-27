using Dima.Api.Common.Api;
using Dima.Api.Models;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Transactions;
using Dima.Core.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Dima.Api.Endpoints.Transactions
{
    public class UpdateTransactionEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapPut("/{id}", HandlerAsync)
                .WithName("Transaction: Update")
                .WithSummary("Update a Transaction")
                .WithDescription("Update a Transaction")
                .Produces<Response<Transaction?>>();
        }

        private static async Task<IResult> HandlerAsync(ClaimsPrincipal user,ITransactionHandler handler, UpdateTransactionRequest request, long id)
        {
            request.UserId = user.Identity?.Name ?? string.Empty;
            request.Id = id;

            var result = await handler.UpdateAsync(request);

            return result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result);
        }
    }
}
