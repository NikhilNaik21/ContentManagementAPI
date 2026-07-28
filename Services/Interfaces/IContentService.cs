using ArticleManagementAPI.DTO;

namespace ArticleManagementAPI.Services.Interfaces
{
    public interface IContentService
    {
        Task<List<ContentDTO>> GetAllAsync();

        Task<ContentDTO?> GetByIdAsync(int id);

        Task<ContentDTO> CreateAsync(ContentDTO content);

        Task<bool> UpdateAsync(int id, ContentDTO content);

        Task<bool> DeleteAsync(int id);
    }
}
