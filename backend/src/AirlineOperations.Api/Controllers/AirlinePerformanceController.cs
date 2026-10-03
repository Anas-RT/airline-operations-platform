using AirlineOperations.Api.DTOs.AirlinePerformance;
using AirlineOperations.Api.DTOs.Common;
using AirlineOperations.Api.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;

namespace AirlineOperations.Api.Controllers
{
    public class AirlinePerformanceController : BaseApiController
    {
        private readonly IAirlinePerformanceService _service;
        public AirlinePerformanceController(IAirlinePerformanceService service)
        {
            _service = service;
        }

        [HttpGet("GetAirlinePerformanceSevereDelayRate")]

        public async Task<ActionResult<IEnumerable<AirlinePerformanceSevereDelayRateDto>>> GetAirlinePerformanceSevereDelayRateAsync()
        {
            var airlinePerformanceSevereDelayRate = await _service.GetAirlinePerformanceSevereDelayRateAsync();
            return Ok(airlinePerformanceSevereDelayRate);
        }

        [HttpGet("GetAirlineKpis")]
        public async Task<ActionResult<AirlineKpisDto>> GetAirlineKpisAsync([FromQuery] string targetAirline)
        {
            var airlineKpis = await _service.GetAirlineKpisAsync(targetAirline);
            return Ok(airlineKpis);
        }
        [HttpGet("GetAirlineBenchmark")]
        public async Task<ActionResult<AirlineBenchmarkDto>> GetAirlineBenchmarkAsync([FromQuery] string targetAirline)
        {
            var airlineBenchmark = await _service.GetAirlineBenchmarkAsync(targetAirline);
            return Ok(airlineBenchmark);
        }

        [HttpGet("GetAirlineMonthlyOtp15Comparison")]
        public async Task<ActionResult<IEnumerable<AirlineMonthlyOtp15ComparisonDto>>> GetAirlineMonthlyOtp15ComparisonAsync([FromQuery] string targetAirline)
        {
            var airlineMonthlyOtp15Comparison = await _service.GetAirlineMonthlyOtp15ComparisonAsync(targetAirline);
            return Ok(airlineMonthlyOtp15Comparison);
        }

        [HttpGet("GetAirlineScorecard")]
        public async Task<ActionResult<PagedResult<AirlineScorecardDto>>> GetAirlineScorecardAsync([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            var airlineScorecard = await _service.GetAirlineScorecardAsync(pageNumber, pageSize);
            return Ok(airlineScorecard);
        }
    }
}
