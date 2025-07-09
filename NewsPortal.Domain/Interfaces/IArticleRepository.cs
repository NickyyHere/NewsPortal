using NewsPortal.Domain.Enums;
using NewsPortal.Domain.Models;

namespace NewsPortal.Domain.Interfaces
{
    public interface IArticleRepository
    {
        public Task<List<Article>> GetAllArticlesAsync();
        public Task<List<Article>> GetArticlesByStatusAsync(ArticleStatus status);
        public Task<Article?> GetArticleByIdAsync(Guid id);
        public Task AddArticleAsync(Article article);
        public Task SaveAsync();
        public Task<List<string>> GetSlugsStartingWith(string prefix);
        public Task<Guid?> GetMostUsedCategoryIdAsync();
    }
}
