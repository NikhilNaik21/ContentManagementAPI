using ArticleManagementAPI.Enums;

namespace ArticleManagementAPI.Models
{
    public class Contents
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        // Foreign Key
        public int AuthorId { get; set; }

        // Navigation property - user object
        public Users Author { get; set; }

        public Status Status { get; set; }

        public Language Language { get; set; }

        public DateTime CreatedAt { get; set; }

        // Foreign Key
        public int ArticleId { get; set; }

        // Navigation property - article object 
        public Articles Article { get; set; }
    }
}
