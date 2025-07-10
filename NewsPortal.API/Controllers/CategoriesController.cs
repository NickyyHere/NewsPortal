using Microsoft.AspNetCore.Mvc;
using NewsPortal.Application.DTO.Create;
using NewsPortal.Application.Interfaces;
using System.Threading.Tasks;

namespace NewsPortal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        /// <summary>
        /// Get all categories
        /// </summary>
        /// <returns>
        /// List of categories
        /// </returns>
        /// <response code="200">Ok</response>
        /// <response code="500">Internal server error</response>
        [HttpGet]
        public async Task<ActionResult> GetCategories()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(categories);
        }
        /// <summary>
        /// Create category
        /// </summary>
        /// <param name="dto">DTO</param>
        /// <response code="200">Ok</response>
        /// <response code="400">BadRequest</response>
        /// <response code="500">Internal server error</response>
        /// <exception cref="UniqueConstraintViolationException">Thrown when category with the same name already exists</exception>
        [HttpPost]
        public async Task<ActionResult> CreateCategory([FromBody] CreateCategoryDTO dto)
        {
            await _categoryService.CreateCategoryAsync(dto);
            return Created();
        }
    }
}
