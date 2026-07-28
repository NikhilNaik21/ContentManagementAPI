namespace ArticleManagementAPI.Models
{
    public class Users
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation property --> one user can have many contents
        public List<Contents> Contents { get; set; } = new List<Contents>();
    }
}
