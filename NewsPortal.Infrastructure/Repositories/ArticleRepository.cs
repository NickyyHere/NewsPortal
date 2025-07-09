using Microsoft.EntityFrameworkCore;
using NewsPortal.Domain.Enums;
using NewsPortal.Domain.Interfaces;
using NewsPortal.Domain.Models;

namespace NewsPortal.Infrastructure.Repositories
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly AppDbContext _context;
        public ArticleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddArticleAsync(Article article)
        {
            await _context.Articles
                .AddAsync(article);
        }

        public async Task<List<Article>> GetAllArticlesAsync()
        {
            return await _context.Articles
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Article?> GetArticleByIdAsync(Guid id)
        {
            return await _context.Articles
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Article>> GetArticlesByStatusAsync(ArticleStatus status)
        {
            return await _context.Articles
                .Where(a => a.Status == status)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Guid?> GetMostUsedCategoryIdAsync()
        {
            return await _context.Articles
                .GroupBy(a => a.CategoryId)
                .OrderByDescending(g => g.Count())
                .Select(g => (Guid?)g.Key)
                .FirstOrDefaultAsync();
        }

        public async Task<List<string>> GetSlugsStartingWith(string prefix)
        {
            return await _context.Articles
                .Where(a => a.Slug.StartsWith(prefix))
                .Select(a => a.Slug)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
