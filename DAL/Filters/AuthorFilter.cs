namespace DAL.Filters
{
    public class AuthorFilter : IFilter
    {
        public int? ById { get; set; }
        public string? AuthorName { get; set; }
        public DateTime? BornAfter { get; set; }
        public DateTime? BornBefore { get; set; }
    }
}
