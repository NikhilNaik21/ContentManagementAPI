using ArticleManagementAPI.Enums;

namespace ArticleManagementAPI.DTO
{
    public class ContentDTO
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        // Foreign Key
        public int AuthorId { get; set; }

        public Status Status { get; set; }

        public Language Language { get; set; }

        public DateTime CreatedAt { get; set; }

        // Foreign Key
        public int ArticleId { get; set; }
    }
}
