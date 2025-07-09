using NewsPortal.Application.Interfaces;
using NewsPortal.Domain.Interfaces;
using System.Text.RegularExpressions;

namespace NewsPortal.Application.Services
{
    public class SlugGenerator : ISlugGenerator
    {
        private readonly IArticleRepository _articleRepository;
        public SlugGenerator(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }

        public async Task<string> GenerateUniqueSlugAsync(string title)
        {
            var slug = title.ToLower();
            slug = Regex.Replace(slug, @"[^a-z0-9\s]", ""); // Remove special characters
            slug = Regex.Replace(slug, @"\s+", "-"); // Replace spaces with "-"

            var similarSlugs = await _articleRepository.GetSlugsStartingWith(slug);

            if (similarSlugs.Count == 0 || !similarSlugs.Contains(slug))
            {
                return slug;
            }

            int maxSuffix = 0;

            foreach (var existing in similarSlugs)
            {
                if (existing == slug)
                {
                    continue;
                }

                var match = Regex.Match(existing, $"^{Regex.Escape(slug)}-(\\d+)$"); // Check if existing slug == slug-N where N is any number
                if(match.Success)
                {
                    var number = int.Parse(match.Groups[1].Value);
                    if (number > maxSuffix)
                    {
                        maxSuffix = number;
                    }
                }
            }
            return $"{slug}-{maxSuffix + 1}";
        }
    }
}
