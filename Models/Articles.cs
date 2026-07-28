using ArticleManagementAPI.Enums;
namespace ArticleManagementAPI.Models
{
    public class Articles
    {
        public int Id { get; set; }

        public Status Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public List<Contents> Contents { get; set; }
            = new List<Contents>();
    }
}
