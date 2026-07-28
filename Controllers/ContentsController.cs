using ArticleManagementAPI.DTO;
using ArticleManagementAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ArticleManagementAPI.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class ContentsController : ControllerBase
    {
        private readonly IContentService _contentService;

        public ContentsController(IContentService contentService)
        {
            _contentService = contentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var contents = await _contentService.GetAllAsync();
            return Ok(contents);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            try
            {
                var content = await _contentService.GetByIdAsync(id);

                if (content == null)
                {
                    return NotFound();
                }

                return Ok(content);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(ContentDTO content)
        {
            try
            {
                var createdContent = await _contentService.CreateAsync(content);
                if (createdContent == null)
                    return BadRequest();
                return Ok(createdContent);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, ContentDTO content)
        {
            try
            {
                var updated = await _contentService.UpdateAsync(id, content);
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
                var deleted = await _contentService.DeleteAsync(id);
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
    }
}
