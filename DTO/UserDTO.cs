
namespace ArticleManagementAPI.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation property --> one user can have many contents
        public List<ContentDTO> Contents { get; set; } = new List<ContentDTO>();
    }
}
