using ArticleManagementAPI.Data;
using ArticleManagementAPI.DTO;
using ArticleManagementAPI.Models;
using ArticleManagementAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ArticleManagementAPI.Services.Implementations
{
    public class ContentService : IContentService
    {
        private readonly ArticleManagementDbContext _dbContext;

        public ContentService(ArticleManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Creates new content and saves it to database.
        /// </summary>
        public async Task<ContentDTO> CreateAsync(ContentDTO contentDto)
        {
            try
            {
                var content = new Contents
                {
                    Title = contentDto.Title,
                    Content = contentDto.Content,
                    AuthorId = contentDto.AuthorId,
                    Status = contentDto.Status,
                    Language = contentDto.Language,
                    ArticleId = contentDto.ArticleId,
                    CreatedAt = DateTime.UtcNow
                };

                await _dbContext.Contents.AddAsync(content);

                await _dbContext.SaveChangesAsync();

                contentDto.Id = content.Id;

                return contentDto;
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "An error occurred while creating content.",
                    ex);
            }
        }

        /// <summary>
        /// Retrieves all contents.
        /// </summary>
        public async Task<List<ContentDTO>> GetAllAsync()
        {
            try
            {
                return await _dbContext.Contents
                    .Select(content => new ContentDTO
                    {
                        Id = content.Id,
                        Title = content.Title,
                        Content = content.Content,
                        AuthorId = content.AuthorId,
                        Status = content.Status,
                        Language = content.Language,
                        CreatedAt = content.CreatedAt,
                        ArticleId = content.ArticleId
                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "An error occurred while retrieving contents.",
                    ex);
            }
        }

        /// <summary>
        /// Retrieves content by ID.
        /// </summary>
        public async Task<ContentDTO?> GetByIdAsync(int id)
        {
            try
            {
                var content = await _dbContext.Contents
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (content == null)
                {
                    return null;
                }

                return new ContentDTO
                {
                    Id = content.Id,
                    Title = content.Title,
                    Content = content.Content,
                    AuthorId = content.AuthorId,
                    Status = content.Status,
                    Language = content.Language,
                    CreatedAt = content.CreatedAt,
                    ArticleId = content.ArticleId
                };
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "An error occurred while retrieving content.",
                    ex);
            }
        }

        /// <summary>
        /// Updates content by ID.
        /// </summary>
        public async Task<bool> UpdateAsync(int id, ContentDTO contentDto)
        {
            try
            {
                var content = await _dbContext.Contents
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (content == null)
                {
                    return false;
                }

                content.Title = contentDto.Title;
                content.Content = contentDto.Content;
                content.AuthorId = contentDto.AuthorId;
                content.Status = contentDto.Status;
                content.Language = contentDto.Language;
                content.ArticleId = contentDto.ArticleId;

                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "An error occurred while updating content.",
                    ex);
            }
        }

        /// <summary>
        /// Deletes content by ID.
        /// </summary>
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var content = await _dbContext.Contents
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (content == null)
                {
                    return false;
                }

                _dbContext.Contents.Remove(content);

                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "An error occurred while deleting content.",
                    ex);
            }
        }
    }
}