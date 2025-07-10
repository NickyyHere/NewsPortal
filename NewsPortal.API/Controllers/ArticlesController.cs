using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using NewsPortal.Application.DTO.Create;
using NewsPortal.Application.DTO.Update;
using NewsPortal.Application.Interfaces;
using NewsPortal.Domain.Enums;

namespace NewsPortal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly IArticleService _articleService;
        public ArticlesController(IArticleService articleService)
        {
            _articleService = articleService;
        }
        /// <summary>
        /// Add new article
        /// </summary>
        /// <param name="dto">DTO</param>
        /// <response code="201">Article created</response>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal server error</response>
        /// <exception cref="EntityDoesNotExistException">Thrown when category assigned to article does not exist</exception>
        /// <exception cref="ValidationException">Thrown when data failed validation</exception>
        [HttpPost]
        public async Task<ActionResult> AddArticle([FromBody]CreateAtricleDTO dto)
        {
            await _articleService.CreateArticleAsync(dto);
            return Created();
        }
        /// <summary>
        /// Get articles - allows filtering by status
        /// </summary>
        /// <returns>
        /// List of articles
        /// </returns>
        /// <param name="status">Status to be filtered by</param>
        /// <response code="200">Ok</response>
        /// <response code="500">Internal server error</response>
        /// <exception cref="EntityDoesNotExistException">Thrown when category assigned to article does not exist</exception>
        [HttpGet]
        public async Task<ActionResult> GetArticles([FromQuery] ArticleStatus? status)
        {
            var articles = await _articleService.GetArticlesAsync(status);
            return Ok(articles);
        }
        /// <summary>
        /// Get article by id
        /// </summary>
        /// <returns>
        /// article
        /// </returns>
        /// <param name="articleId">Guid of article</param>
        /// <response code="200">Ok</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal server error</response>
        /// <exception cref="EntityDoesNotExistException">Thrown when article does not exist</exception>
        [HttpGet("{articleId}")]
        public async Task<ActionResult> GetArticleDetails(Guid articleId)
        {
            var article = await _articleService.GetArticleAsync(articleId);
            return Ok(article);
        }
        /// <summary>
        /// Edit article
        /// </summary>
        /// <param name="articleId">Guid of article</param>
        /// <param name="dto">DTO</param>
        /// <response code="200">Article edited</response>
        /// <response code="400">Bad request</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal server error</response>
        /// <exception cref="EntityDoesNotExistException">Thrown when article does not exist</exception>
        /// <exception cref="ValidationException">Thrown when data failed validation</exception>
        [HttpPut("{articleId}")]
        public async Task<ActionResult> EditArticle(Guid articleId, [FromBody] UpdateArticleDTO dto)
        {
            await _articleService.UpdateArticleAsync(articleId, dto);
            return Ok();
        }
        /// <summary>
        /// Publish article
        /// </summary>
        /// <param name="articleId">Guid of article</param>
        /// <response code="200">Article published</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal server error</response>
        /// <exception cref="EntityDoesNotExistException">Thrown when article does not exist</exception>
        /// <exception cref="ArticleAlreadyPublishedException">Thrown when article is already published</exception>
        [HttpPost("{articleId}/publish")]
        public async Task<ActionResult> PublishArticle(Guid articleId)
        {
            await _articleService.PublishArticleAsync(articleId);
            return Ok();
        }
        /// <summary>
        /// Get article stats
        /// </summary>
        /// <returns>
        /// StatDTO
        /// </returns>
        /// <response code="200">Ok</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal server error</response>
        /// <exception cref="EntityDoesNotExistException">Thrown when category does not exist</exception>
        [HttpGet("stats")]
        public async Task<ActionResult> GetStats()
        {
            var stats = await _articleService.GetArticleStatsAsync();
            return Ok(stats);
        }
    }
}
