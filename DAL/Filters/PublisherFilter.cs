namespace DAL.Filters
{
    public class PublisherFilter : IFilter
    {
        public int? ById { get; set; }
        public string? Name { get; set; }
        public DateTime? FoundedAfter { get; set; }
        public DateTime? FoundedBefore { get; set; }
    }
}
