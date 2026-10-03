using AirlineOperations.Api.DTOs.NetworkOverview;

namespace AirlineOperations.Api.Interfaces.IServices
{
    public interface INetworkOverviewService
    {
        Task<NetworkOverviewFilterOptionsDto> GetOverviewFilterOptionsAsync();
        Task<NetworkOverviewKpisDto> GetOverviewKpisAsync(NetworkOverviewFilterDto filters);
        Task<IEnumerable<NetworkOverviewOtp15MonthlyDto>> GetOverviewOtp15MonthlyAsync(NetworkOverviewFilterDto filters);
        Task<NetworkOverviewFlightOutcomeMixDto> GetOverviewFlightOutcomeMixAsync(NetworkOverviewFilterDto filters);
        Task<IEnumerable<NetworkOverviewAirlineOtp15Dto>> GetAirlineOtp15PerformanceRateAsync(NetworkOverviewFilterDto filters);
    }
}
