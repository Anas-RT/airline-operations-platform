namespace AirlineOperations.Api.DTOs.AirlinePerformance
{
    public class AirlineScorecardDto
    {
        public string AirlineCode { get; set; } = string.Empty;
        public string AirlineName { get; set; } = string.Empty;
        public long CompletedFlights { get; set; }
        public decimal Otp15Rate { get; set; }
        public decimal SevereDelayRate { get; set; }
        public long SevereCases { get; set; }
        public decimal CancellationRate { get; set; }
        public string PriorityInterpretation { get; set; } = string.Empty;
    }
}
