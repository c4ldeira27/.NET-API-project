using Dima.Api.Common.Extensions;
using Dima.Api.Data;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Transactions;
using Dima.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace Dima.Api.Handlers
{
    public class TransactionHandler(AppDbContext context) : ITransactionHandler
    {
        public async Task<Response<Transaction?>> CreateAsync(CreateTransactionRequest request)
        {
            try
            {
                var transaction = new Transaction
                {
                    UserId = request.UserId,
                    Title = request.Title,
                    Type = request.Type,
                    Amount = request.Amount,
                    CategoryId = request.CategoryId,
                };

                await context.AddAsync(transaction);
                await context.SaveChangesAsync();

                return new Response<Transaction?>(transaction, 200, "Successful created");

            }
            catch
            {
                return new Response<Transaction?>(null, 500, "Internal server error");
            }
        }

        public async Task<Response<Transaction?>> DeleteAsync(DeleteTransactionRequest request)
        {
            try
            {
                var transaction = await context.Transactions.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

                if (transaction == null)
                    return new Response<Transaction?>(null, 400, "Transaction not found");

                context.Transactions.Remove(transaction);
                await context.SaveChangesAsync();

                return new Response<Transaction?>(null, message: "Sucessful deleted");
            }
            catch
            {
                return new Response<Transaction?>(null, 500, "Internal server error");
            }
        }

        public async Task<Response<Transaction?>> GetByIdAsync(GetTransactionByIdRequest request)
        {
            try
            {
                var transaction = await context.Transactions.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

                if (transaction == null)
                    return new Response<Transaction?>(null, 400, "Transaction not found");

                return new Response<Transaction?>(transaction);
            }
            catch
            {
                return new Response<Transaction?>(null, 500, "Internal server error");
            }
        }

        public async Task<PagedResponse<List<Transaction>>> GetByPeriodAsync(GetTransactionsByPeriodRequest request)
        {
            try
            {
                request.StartDate ??= DateTime.Now.GetFirstDay();
                request.EndDate ??= DateTime.Now.GetLastDay();
            }
            catch
            {
                return new PagedResponse<List<Transaction>>(null, 500, "Internal server error");
            }

            try
            {
                var query = context.Transactions
                .AsNoTracking()
                .Where(x => x.UserId == request.UserId
                && x.CreatedAt >= request.StartDate
                && x.CreatedAt <= request.EndDate)
                .OrderBy(x => x.CreatedAt);

                var transactions = await query
                    .Skip((request.PageNumber - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToListAsync();

                var count = await query.CountAsync();

                return new PagedResponse<List<Transaction>>(transactions, count, request.PageNumber, request.PageSize);
            }
            catch
            {
                return new PagedResponse<List<Transaction>>(null, 500, "Internal server error to get transactions");
            }
        }
            
        

        public async Task<Response<Transaction?>> UpdateAsync(UpdateTransactionRequest request)
        {
            try
            {
                var transaction = await context.Transactions.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

                if (transaction == null)
                    return new Response<Transaction?>(null, 400, "Transaction not found");

                transaction.CategoryId = request.CategoryId;
                transaction.Title = request.Title;
                transaction.Type = request.Type;
                transaction.PaidOrReceivedAt = request.PaidOrReceivedAt;
                transaction.Amount = request.Amount;

                context.Transactions.Update(transaction);
                await context.SaveChangesAsync();

                return new Response<Transaction?>(transaction, 200);
            }
            catch
            {
                return new Response<Transaction?>(null, 500, "Internal server error");
            }


        }
    }
}
