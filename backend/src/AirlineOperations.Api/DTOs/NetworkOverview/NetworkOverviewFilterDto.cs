namespace AirlineOperations.Api.DTOs.NetworkOverview
{
    public class NetworkOverviewFilterDto
    {
        public int? Year { get; set; }
        public string? Airline { get; set; }
        public string? Month { get; set; }
        public string? TimeBand { get; set; }
    }
}
