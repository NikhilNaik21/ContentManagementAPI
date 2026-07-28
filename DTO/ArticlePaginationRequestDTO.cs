namespace ArticleManagementAPI.DTO
{
    public class ArticlePaginationRequestDTO
    {

        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 10;

        // Sorting
        // title / createdAt
        public string? SortBy { get; set; }

        // Filter
        public int? Status { get; set; }
    }
}
