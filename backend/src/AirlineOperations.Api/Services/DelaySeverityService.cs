using AirlineOperations.Api.DTOs.DelaySeverity;
using AirlineOperations.Api.Interfaces.IRepositories;
using AirlineOperations.Api.Interfaces.IServices;

namespace AirlineOperations.Api.Services
{
    public class DelaySeverityService : IDelaySeverityService
    {
        private readonly IDelaySeverityRepository _repository;

        public DelaySeverityService(IDelaySeverityRepository repository)
        {
            _repository = repository;
        }

        public Task<DelaySeverityKpisDto> GetDelaySeverityKpisAsync()
        {
            return _repository.GetDelaySeverityKpisAsync();
        }

        public Task<IEnumerable<DelaySeverityDistributionDto>>
            GetDelaySeverityDistributionAsync()
        {
            return _repository.GetDelaySeverityDistributionAsync();
        }

        public Task<IEnumerable<DelaySeverityHeatmapDto>>
            GetDelaySeverityHeatmapAsync()
        {
            return _repository.GetDelaySeverityHeatmapAsync();
        }

        public Task<IEnumerable<DelaySeverityMonthlyTrendDto>>
            GetDelaySeverityMonthlyTrendAsync()
        {
            return _repository.GetDelaySeverityMonthlyTrendAsync();
        }

        public Task<IEnumerable<DelaySeverityDriverImpactDto>>
            GetDelaySeverityDriverImpactAsync()
        {
            return _repository.GetDelaySeverityDriverImpactAsync();
        }
    }
}

