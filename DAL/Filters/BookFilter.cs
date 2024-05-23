namespace DAL.Filters
{
    public class BookFilter : IFilter
    {
        public int? ById { get; set; }
        public string? TextSearch { get; set; }
        public string? AuthorName { get; set; }
        public string? PublisherName { get; set; }
        public DateTime? ReleaseAfter { get; set; }
        public DateTime? ReleaseBefore { get; set; }
    }
}
