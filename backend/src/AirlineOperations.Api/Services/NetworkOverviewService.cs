using AirlineOperations.Api.Interfaces.IServices;
using AirlineOperations.Api.Interfaces.IRepositories;
using AirlineOperations.Api.DTOs.NetworkOverview;

namespace AirlineOperations.Api.Services
{
    public class NetworkOverviewService : INetworkOverviewService
    {   private readonly INetworkOverviewRepository _repository;
        public NetworkOverviewService(INetworkOverviewRepository repository)
        {
            _repository = repository;
        }
        public async Task<NetworkOverviewFilterOptionsDto> GetOverviewFilterOptionsAsync()
        {
            return await _repository.GetOverviewFilterOptionsAsync();
        }
        public Task<NetworkOverviewKpisDto> GetOverviewKpisAsync(NetworkOverviewFilterDto filters)
        {
            return _repository.GetOverviewKpisAsync(filters);
        }
        
        public Task<IEnumerable<NetworkOverviewOtp15MonthlyDto>> GetOverviewOtp15MonthlyAsync(NetworkOverviewFilterDto filters)
        {
            return _repository.GetOverviewOtp15MonthlyAsync(filters);
        }
        public Task<NetworkOverviewFlightOutcomeMixDto> GetOverviewFlightOutcomeMixAsync(NetworkOverviewFilterDto filters)
        {
            return _repository.GetOverviewFlightOutcomeMixAsync(filters);
        }
        public Task<IEnumerable<NetworkOverviewAirlineOtp15Dto>>
            GetAirlineOtp15PerformanceRateAsync(NetworkOverviewFilterDto filters)
        {
            return _repository.GetAirlineOtp15PerformanceRateAsync(filters);
        }
    }
}
