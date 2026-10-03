namespace AirlineOperations.Api.DTOs.Common
{
    public class PagedResult<T>
    {
        public IEnumerable<T> Items { get; set; } = [];
        public long TotalCount { get; set; }
    }
}
