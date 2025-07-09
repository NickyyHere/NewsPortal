using NewsPortal.Domain.Enums;
using NewsPortal.Domain.Models;
using NewsPortal.Domain.Services;

namespace NewsPortal.Tests.Domain.Services
{
    public class ArticleStatServiceTest
    {
        [Fact]
        public void GetStats_ShouldReturnTupleOfStats()
        {
            // Arrange
            var categoryId1 = Guid.NewGuid();
            var categoryId2 = Guid.NewGuid();
            var articles = new List<Article>
            {
                new Article { Status = ArticleStatus.DRAFT, CategoryId = categoryId1 },
                new Article { Status = ArticleStatus.PUBLISHED, CategoryId = categoryId1 },
                new Article { Status = ArticleStatus.DRAFT, CategoryId = categoryId1 },
                new Article { Status = ArticleStatus.PUBLISHED, CategoryId = categoryId2 },
                new Article { Status = ArticleStatus.DRAFT, CategoryId = categoryId2 },
                new Article { Status = ArticleStatus.PUBLISHED, CategoryId = categoryId1 },
                new Article { Status = ArticleStatus.DRAFT, CategoryId = categoryId1 },
            };
            // Act
            var(published, drafts, mostUsedCategoryId) = ArticleStatsService.GetStats(articles);
            // Assert
            Assert.Equal(3, published);
            Assert.Equal(4, drafts);
            Assert.Equal(categoryId1, mostUsedCategoryId);
        }
    }
}
