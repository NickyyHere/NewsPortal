using NewsPortal.Application.DTO.Create;
using NewsPortal.Application.DTO.Read;

namespace NewsPortal.Application.Interfaces
{
    public interface ICategoryService
    {
        public Task CreateCategoryAsync(CreateCategoryDTO dto);
        public Task<List<CategoryDTO>> GetAllCategoriesAsync();
    }
}
