using ArticleManagementAPI.Enums;

namespace ArticleManagementAPI.DTO
{
    public class ArticleDTO
    {
        public int Id { get; set; }

        public Status Status { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
