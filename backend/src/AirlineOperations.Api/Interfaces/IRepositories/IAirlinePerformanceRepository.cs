using AirlineOperations.Api.DTOs.AirlinePerformance;
using AirlineOperations.Api.DTOs.Common;

namespace AirlineOperations.Api.Interfaces.IRepositories
{
    public interface IAirlinePerformanceRepository
    {
        Task<IEnumerable<AirlinePerformanceSevereDelayRateDto>> GetAirlinePerformanceSevereDelayRateAsync();
        Task<AirlineKpisDto> GetAirlineKpisAsync(string targetAirline);
        Task<AirlineBenchmarkDto> GetAirlineBenchmarkAsync(string targetAirline);
        Task<IEnumerable<AirlineMonthlyOtp15ComparisonDto>> GetAirlineMonthlyOtp15ComparisonAsync(string targetAirline);
        Task<PagedResult<AirlineScorecardDto>> GetAirlineScorecardAsync(int pageNumber, int pageSize);
    }
}
