using ArticleManagementAPI.Enums;

namespace ArticleManagementAPI.DTO
{
    public class ArticlePaginatedResponseDTO
    {
        public string Title { get; set; }

        public string Author { get; set; }

        public Status Status { get; set; }
    }
}
