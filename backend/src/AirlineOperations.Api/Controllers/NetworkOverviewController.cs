using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AirlineOperations.Api.Interfaces.IServices;
using AirlineOperations.Api.DTOs.NetworkOverview; // or whatever the actual namespace is

namespace AirlineOperations.Api.Controllers
{
    public class NetworkOverviewController : BaseApiController
    {
        private readonly INetworkOverviewService _service;

        public NetworkOverviewController(INetworkOverviewService service)
        {
            _service = service;
        }

        [HttpGet("GetNetworkOverviewFiltersOptions")]
        public async Task<ActionResult<NetworkOverviewFilterOptionsDto>>
            GetFilterOptions()
        {
            var filterOptions = await _service.GetOverviewFilterOptionsAsync();
            return Ok(filterOptions);
        }
        [HttpGet("GetKpis")]
        public async Task<ActionResult<NetworkOverviewKpisDto>> 
            GetKpis([FromQuery] NetworkOverviewFilterDto filters)
        {
            var kpis = await _service.GetOverviewKpisAsync(filters);

            return Ok(kpis);
        }

        [HttpGet("GetOtp15Monthly")]
        public async Task<ActionResult<IEnumerable<NetworkOverviewOtp15MonthlyDto>>> 
            GetOtp15Monthly([FromQuery] NetworkOverviewFilterDto filters)
        {
            var otp15Monthly = await _service.GetOverviewOtp15MonthlyAsync(filters);

            return Ok(otp15Monthly);
        }
        [HttpGet("GetFlightOutcomeMix")]
        public async Task<ActionResult<NetworkOverviewFlightOutcomeMixDto>> 
            GetFlightOutcomeMix([FromQuery] NetworkOverviewFilterDto filters)
        {
            var flightOutcomeMix = await _service.GetOverviewFlightOutcomeMixAsync(filters);
            return Ok(flightOutcomeMix);
        }
        [HttpGet("GetAirlineOtp15PerformanceRate")]
        public async Task<ActionResult<IEnumerable<NetworkOverviewAirlineOtp15Dto>>>
        GetAirlineOtp15PerformanceRate([FromQuery] NetworkOverviewFilterDto filters)
        {
            var airlineOtp15PerformanceRate =
                await _service.GetAirlineOtp15PerformanceRateAsync(filters);

            return Ok(airlineOtp15PerformanceRate);
        }
    }
}
