namespace AirlineOperations.Api.DTOs.AirlinePerformance
{
    public class AirlineKpisDto
    {
        public int CompletedFlights{ get; set; }
        public decimal Otp15Rate{ get; set; }
        public decimal SevereDelayRate{ get; set; }
        public decimal CancellationRate{ get; set; }
    }
}
