using Microsoft.EntityFrameworkCore;
using NewsPortal.Domain.Interfaces;
using NewsPortal.Domain.Models;

namespace NewsPortal.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;
        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddCategoryAsync(Category category)
        {
            await _context.Categories
                .AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            return await _context.Categories
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Category?> GetCategoryByIdAsync(Guid id)
        {
            return await _context.Categories
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Category?> GetCategoryByNameAsync(string name)
        {
            return await _context.Categories
                .FirstOrDefaultAsync(c => c.Name.ToLower() == name.ToLower());
        }
    }
}
