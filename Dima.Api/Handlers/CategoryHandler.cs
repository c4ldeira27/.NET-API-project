using Dima.Api.Data;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Dima.Api.Handlers
{
    public class CategoryHandler(AppDbContext context) : ICategoryHandler
    {
        public async Task<Response<Category?>> CreateAsync(CreateCategoryRequest request)
        {
            try
            {
                var category = new Category
                {
                    UserId = request.UserId,
                    Title = request.Title,
                    Description = request.Description
                };

                await context.AddAsync(category);
                await context.SaveChangesAsync();

                return new Response<Category?>(category, 201, "Nova categoria criada com sucesso!");
            }
            catch (Exception ex)
            {
                return new Response<Category?>(null, 500, "Não foi possível criar a categoria.");
            }
        }
        public async Task<Response<Category?>> UpdateAsync(UpdateCategoryRequest request)
        {
            try
            {
                var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

                if (category is null)
                    return new Response<Category?>(null, 404, "Categoria não encontrada");

                category.Title = request.Title;
                category.Description = request.Description;

                context.Categories.Update(category);
                await context.SaveChangesAsync();

                return new Response<Category?>(category, message: "Categoria atualizada com sucesso!");
            }
            catch
            {
                return new Response<Category?>(null, 500, "Não foi possível alterar a categoria.");
            }
        }

        public async Task<PagedResponse<List<Category>>> GetAllAsync(GetAllCategoriesRequest request)
        {
            try
            {
                var categories = await context.Categories
                .AsNoTracking()
                .Where(x => request.UserId == x.UserId)
                .OrderBy(x => x.Title)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

                var count = await context.Categories
                    .AsNoTracking()
                    .Where(x => x.UserId == request.UserId)
                    .CountAsync();

                return new PagedResponse<List<Category>>(categories, count, request.PageNumber, request.PageSize);
            }
            catch
            {
                return new PagedResponse<List<Category>>(null, 500, "Serviço Indiponivel no momento.");
            }
        }

        public async Task<Response<Category?>> GetByIdAsync(GetCategoryByIdRequest request)
        {
            try
            {
                var category = await context.Categories.AsNoTracking().FirstOrDefaultAsync(x => request.Id == x.Id);

                return 
                    category is null 
                    ? new Response<Category?>(null, 404, "Categoria não encontrada") 
                    : new Response<Category?>(category);

            }catch
            {
                return new Response<Category?>(null, 500, "Serviço Indisponivel.");
            }
        }

        public async Task<Response<Category?>> DeleteAsync(DeleteCategoryRequest request)
        {
            try
            {
                var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

                if (category is null)
                    return new Response<Category?>(null, 404, "Categoria não encontrada.");

                context.Categories.Remove(category);
                await context.SaveChangesAsync();

                return new Response<Category?>(null, message: "Categoria excluída com sucesso!");
            }
            catch
            {
                return new Response<Category?>(null, 500, message: "Não foi possível excluir a categoria.");
            }
        }
    }
}
