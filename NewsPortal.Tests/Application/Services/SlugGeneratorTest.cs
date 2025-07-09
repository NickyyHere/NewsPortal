using NewsPortal.Application.Interfaces;
using NewsPortal.Application.Services;
using NewsPortal.Domain.Interfaces;
using NSubstitute;

namespace NewsPortal.Tests.Application.Services
{
    public class SlugGeneratorTest
    {
        private readonly ISlugGenerator _slugGenerator;
        private readonly IArticleRepository _articleRepository;
        public SlugGeneratorTest()
        {
            _articleRepository = Substitute.For<IArticleRepository>();
            _slugGenerator = new SlugGenerator(_articleRepository);
        }

        [Fact]
        public async Task GenerateUniqueSlugAsync_ShouldAddNumberAtTheEndOfNewSlug_WhenSlugAlreadyExists()
        {
            // Arrange
            var title = "Some title!";
            var similarSlugs = new List<string>
            {
                "some-title",
                "some-title-1"
            };
            _articleRepository.GetSlugsStartingWith("some-title").Returns(similarSlugs);
            // Act
            var slug = await _slugGenerator.GenerateUniqueSlugAsync(title);
            // Assert
            Assert.Equal("some-title-2", slug);
        }
        [Fact]
        public async Task GenerateUniqueSlugAsync_ShouldReturnNewSlug_WhenNoSimilarSlugsExist()
        {
            // Arrange
            var title = "Some title!";
            var similarSlugs = new List<string>();
            _articleRepository.GetSlugsStartingWith("some-title").Returns(similarSlugs);
            // Act
            var slug = await _slugGenerator.GenerateUniqueSlugAsync(title);
            // Assert
            Assert.Equal("some-title", slug);
        }
    }
}
