using NewsPortal.Domain.Models;

namespace NewsPortal.Domain.Interfaces
{
    public interface ICategoryRepository
    {
        public Task<List<Category>> GetAllCategoriesAsync();
        public Task AddCategoryAsync(Category category);
        public Task<Category?> GetCategoryByIdAsync(Guid id);
        public Task<Category?> GetCategoryByNameAsync(string name);
    }
}
