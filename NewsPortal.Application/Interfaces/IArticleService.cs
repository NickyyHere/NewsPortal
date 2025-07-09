using NewsPortal.Application.DTO.Create;
using NewsPortal.Application.DTO.Read;
using NewsPortal.Application.DTO.Update;
using NewsPortal.Domain.Enums;

namespace NewsPortal.Application.Interfaces
{
    public interface IArticleService
    {
        public Task CreateArticleAsync(CreateAtricleDTO dto);
        public Task<List<ArticleDTO>> GetArticlesAsync(ArticleStatus? statusFilter);
        public Task<ArticleDTO> GetArticleAsync(Guid id);
        public Task UpdateArticleAsync(Guid id, UpdateArticleDTO dto);
        public Task PublishArticleAsync(Guid id);
        public Task<StatsDTO> GetArticleStatsAsync();
    }
}
