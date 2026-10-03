using AirlineOperations.Api.DTOs.AirlinePerformance;
using AirlineOperations.Api.DTOs.Common;
using AirlineOperations.Api.Interfaces.IRepositories;
using AirlineOperations.Api.Interfaces.IServices;

namespace AirlineOperations.Api.Services
{
    public class AirlinePerformanceService : IAirlinePerformanceService
    {
        private readonly IAirlinePerformanceRepository _repository;
        public AirlinePerformanceService(IAirlinePerformanceRepository repository)
        {
            _repository = repository;
        }
        public Task<IEnumerable<AirlinePerformanceSevereDelayRateDto>> GetAirlinePerformanceSevereDelayRateAsync()
        {
            return _repository.GetAirlinePerformanceSevereDelayRateAsync();
        }
        public Task<AirlineKpisDto> GetAirlineKpisAsync(string targetAirline)
        {
            return _repository.GetAirlineKpisAsync(targetAirline);
        }

        public Task<AirlineBenchmarkDto> GetAirlineBenchmarkAsync(string targetAirline)
        {
            return _repository.GetAirlineBenchmarkAsync(targetAirline);
        }
        public Task<IEnumerable<AirlineMonthlyOtp15ComparisonDto>> GetAirlineMonthlyOtp15ComparisonAsync(string targetAirline)
        {
            return _repository.GetAirlineMonthlyOtp15ComparisonAsync(targetAirline);
        }
        public Task<PagedResult<AirlineScorecardDto>> GetAirlineScorecardAsync(int pageNumber, int pageSize)
        {
            return _repository.GetAirlineScorecardAsync(pageNumber, pageSize);
        }
    }
}
