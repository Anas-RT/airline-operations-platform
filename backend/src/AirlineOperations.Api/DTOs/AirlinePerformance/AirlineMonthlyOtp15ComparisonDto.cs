namespace AirlineOperations.Api.DTOs.AirlinePerformance
{
    public class AirlineMonthlyOtp15ComparisonDto
    {
        public string Month { get; set; } = string.Empty;
        public decimal NetworkOtp15Pct { get; set; }
        public decimal TargetAirlineOtp15Pct { get; set; }
    }
}
