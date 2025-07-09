using AutoMapper;
using NewsPortal.Application.DTO.Create;
using NewsPortal.Application.DTO.Read;
using NewsPortal.Application.DTO.Update;
using NewsPortal.Application.Interfaces;
using NewsPortal.Domain.Enums;
using NewsPortal.Domain.Interfaces;
using NewsPortal.Domain.Models;
using NewsPortal.Domain.Services;

namespace NewsPortal.Application.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IMapper _mapper;
        private readonly IArticleRepository _articleRepository;
        private readonly ISlugGenerator _slugGenerator;
        private readonly ICategoryRepository _categoryRepository;
        public ArticleService(IMapper mapper, IArticleRepository articleRepository, ISlugGenerator slugGenerator, ICategoryRepository categoryRepository)
        {
            _mapper = mapper;
            _articleRepository = articleRepository;
            _slugGenerator = slugGenerator;
            _categoryRepository = categoryRepository;
        }
        public async Task CreateArticleAsync(CreateAtricleDTO dto)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(dto.categoryId)
                ?? throw new EntityDoesNotExistException(typeof(Category), dto.categoryId);
            var article = _mapper.Map<Article>(dto);
            article.Id = Guid.NewGuid();
            article.CreatedAt = DateTime.UtcNow;
            article.Slug = await _slugGenerator.GenerateUniqueSlugAsync(article.Title);
            article.Status = Domain.Enums.ArticleStatus.DRAFT;
            await _articleRepository.AddArticleAsync(article);
            await _articleRepository.SaveAsync();
        }

        public async Task<ArticleDTO> GetArticleAsync(Guid id)
        {
            var article = await _articleRepository.GetArticleByIdAsync(id)
                ?? throw new EntityDoesNotExistException(typeof(Article), id);
            return _mapper.Map<ArticleDTO>(article);
        }

        public async Task<List<ArticleDTO>> GetArticlesAsync(ArticleStatus? statusFilter)
        {
            var articles = statusFilter == null 
                ? await _articleRepository.GetAllArticlesAsync()
                : await _articleRepository.GetArticlesByStatusAsync((ArticleStatus)statusFilter);
            return _mapper.Map<List<ArticleDTO>>(articles);
        }

        public async Task PublishArticleAsync(Guid id)
        {
            var article = await _articleRepository.GetArticleByIdAsync(id)
                ?? throw new EntityDoesNotExistException(typeof(Article), id);
            if(article.Status == ArticleStatus.PUBLISHED)
            {
                throw new ArticleAlreadyPublishedException(id);
            }
            article.Status = ArticleStatus.PUBLISHED;
            await _articleRepository.SaveAsync();
        }

        public async Task UpdateArticleAsync(Guid id, UpdateArticleDTO dto)
        {
            var article = await _articleRepository.GetArticleByIdAsync(id)
                ?? throw new EntityDoesNotExistException(typeof(Article), id);
            var category = await _categoryRepository.GetCategoryByIdAsync(dto.categoryId)
                ?? throw new EntityDoesNotExistException(typeof(Category), dto.categoryId);
            article.Title = dto.title;
            article.Slug = await _slugGenerator.GenerateUniqueSlugAsync(article.Title);
            article.Author = dto.author;
            article.Content = dto.content;
            article.Category = category;
            await _articleRepository.SaveAsync();
        }
        public async Task<StatsDTO> GetArticleStatsAsync()
        {
            var articles = await _articleRepository.GetAllArticlesAsync();
            var (published, drafts, categoryId) = ArticleStatsService.GetStats(articles);
            var category = await _categoryRepository.GetCategoryByIdAsync(categoryId)
                ?? throw new EntityDoesNotExistException(typeof(Category), categoryId);
            return new StatsDTO(published, drafts, category.Name);
        }
    }
}
