namespace AirlineOperations.Api.DTOs.DelaySeverity
{
    public class DelaySeverityDriverImpactDto
    {
        public string DelayDriver { get; set; } = string.Empty;
        public long SevereDelayFlights { get; set; }
        public decimal SevereDelayCaseShare { get; set; }
        public long TotalSevereDelayMinutes { get; set; }
    }
}
