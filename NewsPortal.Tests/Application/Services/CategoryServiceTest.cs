using AutoMapper;
using NewsPortal.Application.DTO.Create;
using NewsPortal.Application.Interfaces;
using NewsPortal.Application.Services;
using NewsPortal.Domain.Interfaces;
using NewsPortal.Domain.Models;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace NewsPortal.Tests.Application.Services
{
    public class CategoryServiceTest
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;
        public CategoryServiceTest()
        {
            _mapper = Substitute.For<IMapper>();
            _categoryRepository = Substitute.For<ICategoryRepository>();
            _categoryService = new CategoryService(_mapper, _categoryRepository);
        }
        [Fact]
        public async Task CreateCategoryAsync_ShouldMapAndCallRepository_WhenCategoryWithTheSameNameDoesntExist()
        {
            // Arrange
            var createCategoryDto = new CreateCategoryDTO("Category");
            _categoryRepository.GetCategoryByNameAsync(createCategoryDto.name).ReturnsNull();
            var category = new Category { Name = createCategoryDto.name };
            _mapper.Map<Category>(createCategoryDto).Returns(category);
            _categoryRepository.AddCategoryAsync(category).Returns(Task.CompletedTask);
            // Act
            await _categoryService.CreateCategoryAsync(createCategoryDto);
            // Assert
            await _categoryRepository.Received(1).GetCategoryByNameAsync(Arg.Any<string>());
            _mapper.Received(1).Map<Category>(Arg.Any<CreateCategoryDTO>());
            await _categoryRepository.Received(1).AddCategoryAsync(Arg.Any<Category>());
        }
        [Fact]
        public async Task CreateCategoryAsync_ShouldThrowException_WhenCategoryWithTheSameNameExists()
        {
            // Arrange
            var createCategoryDto = new CreateCategoryDTO("Category");
            var category = new Category { Name = createCategoryDto.name };
            _categoryRepository.GetCategoryByNameAsync(createCategoryDto.name).Returns(category);
            // Act & Assert
            await Assert.ThrowsAsync<UniqueConstraintViolationException>(() => _categoryService.CreateCategoryAsync(createCategoryDto));
        }
    }
}
