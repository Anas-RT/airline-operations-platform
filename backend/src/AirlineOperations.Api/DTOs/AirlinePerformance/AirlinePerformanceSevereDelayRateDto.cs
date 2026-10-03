namespace AirlineOperations.Api.DTOs.AirlinePerformance
{
    public class AirlinePerformanceSevereDelayRateDto
    {
        public required string AirlineName { get; set; }
        public int Otp15EligibleFlights { get; set; }
        public int SevereDelayFlights { get; set; }
        public decimal SevereDelayPct { get; set; }
    }
}
