using ArticleManagementAPI.DTO;

namespace ArticleManagementAPI.Services.Interfaces
{
    public interface IArticleService
    {
        Task<List<ArticleDTO>> GetAllAsync();

        Task<List<ArticleListDTO>> GetArticleWithContentAsync();

        Task<ArticleDTO?> GetByIdAsync(int id);

        Task<ArticleDTO> CreateAsync(ArticleDTO article);

        Task<bool> UpdateAsync(int id, ArticleDTO article);

        Task<bool> DeleteAsync(int id);

        Task<List<ArticlePaginatedResponseDTO>> GetPagedArticlesAsync(ArticlePaginationRequestDTO request);
    }
}
