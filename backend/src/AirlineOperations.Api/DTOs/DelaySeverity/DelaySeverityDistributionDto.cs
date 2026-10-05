namespace AirlineOperations.Api.DTOs.DelaySeverity
{
    public class DelaySeverityDistributionDto
    {
        public string SeverityBand { get; set; } = string.Empty;
        public long DelayedFlights { get; set; }
        public decimal Percentage { get; set; }
    }
}
