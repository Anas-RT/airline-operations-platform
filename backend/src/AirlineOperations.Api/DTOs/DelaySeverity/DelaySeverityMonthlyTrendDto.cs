namespace AirlineOperations.Api.DTOs.DelaySeverity
{
    public class DelaySeverityMonthlyTrendDto
    {
        public string Month { get; set; } = string.Empty;
        public decimal SevereDelayRate { get; set; }
    }
}
