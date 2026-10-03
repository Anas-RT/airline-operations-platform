namespace AirlineOperations.Api.DTOs.AirlinePerformance
{
    public class AirlineBenchmarkDto
    {
        public decimal TargetAirlineOtp15Rate { get; set; }
        public decimal NetworkOtp15Rate { get; set; }
        public decimal TargetAirlineSevereDelayRate { get; set; }
        public decimal NetworkSevereDelayRate { get; set; }
    }
}
