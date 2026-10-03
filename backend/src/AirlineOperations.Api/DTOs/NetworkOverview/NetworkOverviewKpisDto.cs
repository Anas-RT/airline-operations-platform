namespace AirlineOperations.Api.DTOs.NetworkOverview
{
    public class NetworkOverviewKpisDto
    {
        public decimal Otp15Pct { get; set; }
        public decimal SevereDelaySd60Pct { get; set; }
        public decimal CancellationPct { get; set; }
        public decimal DiversionPct { get; set; }
    }
}
