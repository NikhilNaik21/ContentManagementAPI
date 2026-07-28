using ArticleManagementAPI.DTO;
using ArticleManagementAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ArticleManagementAPI.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly IArticleService _articleService;

        public ArticlesController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var articles = await _articleService.GetAllAsync();
            return Ok(articles);
        }

        [HttpGet("details")]
        public async Task<IActionResult> GetArticleWithContentAsync()
        {
            var articles = await _articleService.GetArticleWithContentAsync();
            return Ok(articles);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            try
            {
                var article = await _articleService.GetByIdAsync(id);

                if (article == null)
                {
                    return NotFound();
                }

                return Ok(article);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(ArticleDTO article)
        {

            try
            {
                var createdArticle = await _articleService.CreateAsync(article);
                if (createdArticle == null)
                    return BadRequest();
                return Ok(createdArticle);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, ArticleDTO article)
        {
            try
            {
                var updated = await _articleService.UpdateAsync(id, article);
                if (!updated)
                {
                    return NotFound();
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                var deleted = await _articleService.DeleteAsync(id);
                if (!deleted)
                {
                    return NotFound();
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("paged")]
        public async Task<IActionResult> GetPagedArticlesAsync([FromQuery] ArticlePaginationRequestDTO request)
        {
            try
            {
                var result = await _articleService.GetPagedArticlesAsync(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
