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
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> GetCategories()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(categories);
        }
        /// <summary>
        /// Create new category
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> CreateCategory([FromBody] CreateCategoryDTO dto)
        {
            await _categoryService.CreateCategoryAsync(dto);
            return Created();
        }
    }
}
