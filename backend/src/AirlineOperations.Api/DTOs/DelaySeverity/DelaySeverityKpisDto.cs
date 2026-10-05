namespace AirlineOperations.Api.DTOs.DelaySeverity
{
    public class DelaySeverityKpisDto
    {
        public long DelayedFlights { get; set; }

        public decimal DelayedFlightsPct { get; set; }

        public decimal DelayedFlightsBecomingSeverePct { get; set; }

        public long SevereDelayFlights { get; set; }

        public long TotalSevereDelayMinutes { get; set; }
    }
}
