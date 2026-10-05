namespace AirlineOperations.Api.DTOs.DelaySeverity
{
    public class DelaySeverityHeatmapDto
    {
        public string Month { get; set; } = string.Empty;
        public string TimeBand { get; set; } = string.Empty;
        public decimal SevereDelayRate { get; set; }
    }
}
