using ArticleManagementAPI.Enums;

namespace ArticleManagementAPI.DTO
{
    public class ArticleListDTO
    {
            public int Id { get; set; }

            public Status Status { get; set; }

            public DateTime CreatedAt { get; set; }

            public List<ContentDTO> Contents { get; set; }      
    }
}
