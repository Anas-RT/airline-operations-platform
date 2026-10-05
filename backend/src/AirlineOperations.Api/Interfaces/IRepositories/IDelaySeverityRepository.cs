using AirlineOperations.Api.DTOs.DelaySeverity;
namespace AirlineOperations.Api.Interfaces.IRepositories
{
    public interface IDelaySeverityRepository
    {
        Task<DelaySeverityKpisDto> GetDelaySeverityKpisAsync();

        Task<IEnumerable<DelaySeverityDistributionDto>>
            GetDelaySeverityDistributionAsync();

        Task<IEnumerable<DelaySeverityHeatmapDto>>
            GetDelaySeverityHeatmapAsync();

        Task<IEnumerable<DelaySeverityMonthlyTrendDto>>
            GetDelaySeverityMonthlyTrendAsync();

        Task<IEnumerable<DelaySeverityDriverImpactDto>>
            GetDelaySeverityDriverImpactAsync();
    }
}
