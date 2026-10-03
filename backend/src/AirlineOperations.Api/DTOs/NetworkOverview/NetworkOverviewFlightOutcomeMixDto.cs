namespace AirlineOperations.Api.DTOs.NetworkOverview
{
    public class NetworkOverviewFlightOutcomeMixDto
    {
        public int TotalFlights { get; set; }
        public int Otp15Flights { get; set; }
        public int ModerateDelayFlights { get; set; }
        public int SevereDelayFlights { get; set; }
        public int DivertedOrCancelledFlights { get; set; }
    }
}
