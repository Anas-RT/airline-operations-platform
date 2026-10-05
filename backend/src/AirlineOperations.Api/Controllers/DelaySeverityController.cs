using AirlineOperations.Api.DTOs.DelaySeverity;
using AirlineOperations.Api.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;

namespace AirlineOperations.Api.Controllers
{
    public class DelaySeverityController : BaseApiController
    {
        private readonly IDelaySeverityService _service;

        public DelaySeverityController(IDelaySeverityService service)
        {
            _service = service;
        }

        [HttpGet("GetDelaySeverityKpis")]
        public async Task<ActionResult<DelaySeverityKpisDto>> GetDelaySeverityKpisAsync()
        {
            var delaySeverityKpis = await _service.GetDelaySeverityKpisAsync();
            return Ok(delaySeverityKpis);
        }

        [HttpGet("GetDelaySeverityDistribution")]
        public async Task<ActionResult<IEnumerable<DelaySeverityDistributionDto>>> GetDelaySeverityDistributionAsync()
        {
            var delaySeverityDistribution = await _service.GetDelaySeverityDistributionAsync();
            return Ok(delaySeverityDistribution);
        }

        [HttpGet("GetDelaySeverityHeatmap")]
        public async Task<ActionResult<IEnumerable<DelaySeverityHeatmapDto>>> GetDelaySeverityHeatmapAsync()
        {
            var delaySeverityHeatmap = await _service.GetDelaySeverityHeatmapAsync();
            return Ok(delaySeverityHeatmap);
        }

        [HttpGet("GetDelaySeverityMonthlyTrend")]
        public async Task<ActionResult<IEnumerable<DelaySeverityMonthlyTrendDto>>> GetDelaySeverityMonthlyTrendAsync()
        {
            var delaySeverityMonthlyTrend = await _service.GetDelaySeverityMonthlyTrendAsync();
            return Ok(delaySeverityMonthlyTrend);
        }

        [HttpGet("GetDelaySeverityDriverImpact")]
        public async Task<ActionResult<IEnumerable<DelaySeverityDriverImpactDto>>> GetDelaySeverityDriverImpactAsync()
        {
            var delaySeverityDriverImpact = await _service.GetDelaySeverityDriverImpactAsync();
            return Ok(delaySeverityDriverImpact);
        }
    }
}