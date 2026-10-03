namespace AirlineOperations.Api.DTOs.NetworkOverview
{
    public class NetworkOverviewFilterOptionsDto
    {
        public IEnumerable<int> Year { get; set; } = [];
        public IEnumerable<string> Airline { get; set; } = [];
        public IEnumerable<string> Month { get; set; } = [];
        public IEnumerable<string> TimeBand { get; set; } = [];
    }
}
