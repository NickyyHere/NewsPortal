using NewsPortal.Domain.Models;

namespace NewsPortal.Domain.Services
{
    public static class ArticleStatsService
    {
        public static (int published, int drafts, Guid mostUsedCategoryId) GetStats(List<Article> articles)
        {
            Dictionary<Guid, uint> categoryCount = new Dictionary<Guid, uint>();
            int published = 0;
            int drafts = 0;
            foreach(var article in articles)
            {
                if(article.Status == Enums.ArticleStatus.PUBLISHED)
                {
                    published++;
                }
                else
                {
                    drafts++;
                }
                var categoryId = article.CategoryId;
                if (categoryCount.ContainsKey(categoryId))
                {
                    categoryCount[categoryId]++;
                }
                else
                {
                    categoryCount[categoryId] = 1;
                }
            }
            var mostUsedCategory = categoryCount.Aggregate((x, y) => x.Value > y.Value ? x : y).Key;
            return (published, drafts, mostUsedCategory);
        }
    }
}
