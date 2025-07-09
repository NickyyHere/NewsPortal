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
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> AddArticle([FromBody]CreateAtricleDTO dto)
        {
            await _articleService.CreateArticleAsync(dto);
            return Created();
        }
        /// <summary>
        /// Get all articles
        /// </summary>
        /// <param name="status">Status filter, will only get articles of specified status. Will fetch all if unspecified</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> GetArticles([FromQuery] ArticleStatus? status)
        {
            var articles = await _articleService.GetArticlesAsync(status);
            return Ok(articles);
        }
        /// <summary>
        /// Get details of specified article
        /// </summary>
        /// <param name="articleId"></param>
        /// <returns></returns>
        [HttpGet("{articleId}")]
        public async Task<ActionResult> GetArticleDetails(Guid articleId)
        {
            var article = await _articleService.GetArticleAsync(articleId);
            return Ok(article);
        }
        /// <summary>
        /// Edit article
        /// </summary>
        /// <param name="articleId"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut("{articleId}")]
        public async Task<ActionResult> EditArticle(Guid articleId, [FromBody] UpdateArticleDTO dto)
        {
            await _articleService.UpdateArticleAsync(articleId, dto);
            return Ok();
        }
        /// <summary>
        /// Publish article
        /// </summary>
        /// <param name="articleId"></param>
        /// <returns></returns>
        [HttpPost("{articleId}/publish")]
        public async Task<ActionResult> PublishArticle(Guid articleId)
        {
            await _articleService.PublishArticleAsync(articleId);
            return Ok();
        }
        /// <summary>
        /// Get stats of articles
        /// </summary>
        /// <returns></returns>
        [HttpGet("stats")]
        public async Task<ActionResult> GetStats()
        {
            var stats = await _articleService.GetArticleStatsAsync();
            return Ok(stats);
        }
    }
}
