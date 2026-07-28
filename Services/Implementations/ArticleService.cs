using ArticleManagementAPI.Data;
using ArticleManagementAPI.DTO;
using ArticleManagementAPI.Enums;
using ArticleManagementAPI.Models;
using ArticleManagementAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ArticleManagementAPI.Services.Implementations
{
    public class ArticleService : IArticleService
    {
        private readonly ArticleManagementDbContext _dbContext;

        public ArticleService(ArticleManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Creates a new article with its contents and saves it to the database.
        /// </summary>
        /// <param name="articleDto">The article data transfer object containing the article details.</param>
        /// <returns>The created article data transfer object.</returns>
        public async Task<ArticleDTO> CreateAsync(ArticleDTO articleDto)
        {
            try
            {
                var article = new Articles
                {
                    Status = articleDto.Status,
                    CreatedAt = DateTime.UtcNow,
                };

                await _dbContext.Articles.AddAsync(article);

                await _dbContext.SaveChangesAsync();

                articleDto.Id = article.Id;

                return articleDto;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while creating the article.", ex);
            }
        }

        /// <summary>
        /// Deletes an article by its ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var article = await _dbContext.Articles
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (article == null)
                {
                    return false;
                }

                _dbContext.Articles.Remove(article);

                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the article.", ex);
            }
        }

        /// <summary>
        /// Retrieves all articles.
        /// </summary>
        /// <returns></returns>
        public async Task<List<ArticleDTO>> GetAllAsync()
        {
            try
            {
                var articles = await _dbContext.Articles
                    .Include(a => a.Contents)
                    .ToListAsync();
                var articleDtos = articles.Select(article => new ArticleDTO
                {
                    Id = article.Id,
                    Status = article.Status,
                    CreatedAt = article.CreatedAt
                   
                }).ToList();
                return articleDtos;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving articles.", ex);
            }
        }

        /// <summary>
        /// Retrieves an article by its ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ArticleDTO?> GetByIdAsync(int id)
        {
            try
            {
                var article = await _dbContext.Articles
                    .Include(a => a.Contents)
                    .FirstOrDefaultAsync(a => a.Id == id);
                if (article == null)
                {
                    return null;
                }
                var articleDto = new ArticleDTO
                {
                    Id = article.Id,
                    Status = article.Status,
                    CreatedAt = article.CreatedAt
                    
                };
                return articleDto;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the article.", ex);
            }
        }

        /// <summary>
        /// Updates an article by its ID.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="article"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(int id, ArticleDTO article)
        {
            try
            {
                var existingArticle = await _dbContext.Articles
                .Include(a => a.Contents)
                .FirstOrDefaultAsync(a => a.Id == id);

                if (existingArticle == null)
                {
                    return false;
                }

                existingArticle.Status = article.Status;

                await _dbContext.SaveChangesAsync();

                return true;
            }

            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the article.", ex);
            }
        }

        /// <summary>
        /// Retrieves all articles with content.
        /// </summary>
        /// <returns></returns>
        public async Task<List<ArticleListDTO>> GetArticleWithContentAsync()
        {
            try
            {
                var articles = await _dbContext.Articles
                    .Include(a => a.Contents)
                    .ToListAsync();
                var articleDtos = articles.Select(article => new ArticleListDTO
                {
                    Id = article.Id,
                    Status = article.Status,
                    CreatedAt = article.CreatedAt,
                    Contents = article.Contents.Select(c => new ContentDTO
                    {
                        Id = c.Id,
                        Title = c.Title,
                        Content = c.Content,
                        AuthorId = c.AuthorId,
                        Status = c.Status,
                        Language = c.Language,
                        CreatedAt = c.CreatedAt,
                        ArticleId = c.ArticleId
                    }).ToList()
                }).ToList();
                return articleDtos;

            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving articles.", ex);
            }
        }

        /// <summary>
        /// Retrieves Paginated request for articles.
        /// </summary>
        /// <returns></returns>
        public async Task<List<ArticlePaginatedResponseDTO>> GetPagedArticlesAsync(ArticlePaginationRequestDTO request)
        {
            try
            {
                var query = _dbContext.Articles
                    .Include(a => a.Contents)
                        .ThenInclude(c => c.Author)
                    .AsQueryable();

                // Filter by Status
                if (request.Status.HasValue)
                {
                    query = query.Where(a => (int)a.Status == request.Status.Value);
                }

                // Sort
                if (!string.IsNullOrEmpty(request.SortBy) &&
                    request.SortBy.Equals("title", StringComparison.OrdinalIgnoreCase))
                {
                    query = query.OrderBy(a => a.Contents
                        .Where(c => c.Language == Language.English)
                        .Select(c => c.Title)
                        .FirstOrDefault());
                }
                else
                {
                    // Default sorting by CreatedAt
                    query = query.OrderByDescending(a => a.CreatedAt);
                }

                // Pagination
                var articles = await query
                    .Skip((request.PageNumber - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToListAsync();

                // Response
                var result = articles.Select(a =>
                {
                    var englishContent = a.Contents
                        .FirstOrDefault(c => c.Language == Language.English);

                    return new ArticlePaginatedResponseDTO
                    {
                        Title = englishContent?.Title ?? string.Empty,
                        Author = englishContent?.Author?.Username ?? string.Empty,
                        Status = a.Status
                    };
                }).ToList();

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving paginated articles.", ex);
            }
        }
    }
}
