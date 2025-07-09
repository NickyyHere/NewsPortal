using AutoMapper;
using NewsPortal.Application.DTO.Create;
using NewsPortal.Application.DTO.Read;
using NewsPortal.Application.Interfaces;
using NewsPortal.Domain.Interfaces;
using NewsPortal.Domain.Models;

namespace NewsPortal.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(IMapper mapper, ICategoryRepository categoryRepository)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }
        public async Task CreateCategoryAsync(CreateCategoryDTO dto)
        {
            var category = await _categoryRepository.GetCategoryByNameAsync(dto.name);
            if (category != null)
            {
                throw new UniqueConstraintViolationException(typeof(Category), nameof(dto.name), dto.name);
            }
            var newCategory = _mapper.Map<Category>(dto);
            newCategory.Id = Guid.NewGuid();
            await _categoryRepository.AddCategoryAsync(newCategory);
        }

        public async Task<List<CategoryDTO>> GetAllCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllCategoriesAsync();
            return _mapper.Map<List<CategoryDTO>>(categories);
        }
    }
}
