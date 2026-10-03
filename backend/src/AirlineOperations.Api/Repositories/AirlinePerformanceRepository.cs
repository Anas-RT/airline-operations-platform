using AirlineOperations.Api.DTOs.AirlinePerformance;
using AirlineOperations.Api.DTOs.Common;
using AirlineOperations.Api.Interfaces.IRepositories;
using Dapper;
using Npgsql;

namespace AirlineOperations.Api.Repositories
{
    public class AirlinePerformanceRepository : IAirlinePerformanceRepository
    {
        private readonly NpgsqlDataSource _dataSource;

        public AirlinePerformanceRepository(NpgsqlDataSource dataSource)
        {
            _dataSource = dataSource;
        }


        public async Task<IEnumerable<AirlinePerformanceSevereDelayRateDto>> GetAirlinePerformanceSevereDelayRateAsync()
        {
            const string sql = """
                                select 	airline_name as AirlineName,
                		                otp15_eligible_flights as Otp15EligibleFlights,
                		                severe_delay_flights as SevereDelayFlights,
                		                severe_delay_pct as SevereDelayPct
                                from vw_airlineperformance_airline_severe_delay_rate ;
                """;
            await using var connection =
                await _dataSource.OpenConnectionAsync();
            return await connection.QueryAsync<AirlinePerformanceSevereDelayRateDto>(sql);
        }
        public Task<AirlineKpisDto> GetAirlineKpisAsync(string targetAirline)
        {
            const string sql = """
                                SELECT
                    COALESCE(SUM(total_flights), 0)
                    - (
                        COALESCE(SUM(cancelled_flights), 0)
                        + COALESCE(SUM(diverted_flights), 0)
                    ) AS CompletedFlights,

                    ROUND(
                        COALESCE(SUM(otp15_flights), 0)::numeric * 100
                        / NULLIF(COALESCE(SUM(otp15_eligible_flights), 0), 0),
                        2
                    ) AS Otp15Rate,

                    ROUND(
                        COALESCE(SUM(severe_delayed_flights), 0)::numeric * 100
                        / NULLIF(COALESCE(SUM(otp15_eligible_flights), 0), 0),
                        2
                    ) AS SevereDelayRate,

                    ROUND(
                        COALESCE(SUM(cancelled_flights), 0)::numeric * 100
                        / NULLIF(COALESCE(SUM(total_flights), 0), 0),
                        2
                    ) AS CancellationRate

                FROM mv_airlineperformance_summary

                WHERE airline_name = @targetAirline;
                """;
            var connection = _dataSource.OpenConnection();
            return connection.QuerySingleAsync<AirlineKpisDto>(sql, new { targetAirline });
        }

        public Task<AirlineBenchmarkDto> GetAirlineBenchmarkAsync(string targetAirline)
        {
            const string sql = """
                                SELECT
                    ROUND(
                        COALESCE(
                            SUM(otp15_flights) FILTER (
                                WHERE airline_name = @targetAirline
                            ),
                            0
                        )::numeric * 100
                        /
                        NULLIF(
                            COALESCE(
                                SUM(otp15_eligible_flights) FILTER (
                                    WHERE airline_name = @targetAirline
                                ),
                                0
                            ),
                            0
                        ),
                        2
                    ) AS TargetAirlineOtp15Rate,

                    ROUND(
                        COALESCE(SUM(otp15_flights), 0)::numeric * 100
                        /
                        NULLIF(
                            COALESCE(SUM(otp15_eligible_flights), 0),
                            0
                        ),
                        2
                    ) AS NetworkOtp15Rate,

                    ROUND(
                        COALESCE(
                            SUM(severe_delayed_flights) FILTER (
                                WHERE airline_name = @targetAirline
                            ),
                            0
                        )::numeric * 100
                        /
                        NULLIF(
                            COALESCE(
                                SUM(otp15_eligible_flights) FILTER (
                                    WHERE airline_name = @targetAirline
                                ),
                                0
                            ),
                            0
                        ),
                        2
                    ) AS TargetAirlineSevereDelayRate,

                    ROUND(
                        COALESCE(SUM(severe_delayed_flights), 0)::numeric * 100
                        /
                        NULLIF(
                            COALESCE(SUM(otp15_eligible_flights), 0),
                            0
                        ),
                        2
                    ) AS NetworkSevereDelayRate

                FROM mv_airlineperformance_summary;
                """;
            var connection = _dataSource.OpenConnection();
            return connection.QuerySingleAsync<AirlineBenchmarkDto>(sql, new { targetAirline });
        }

        public Task<IEnumerable<AirlineMonthlyOtp15ComparisonDto>> GetAirlineMonthlyOtp15ComparisonAsync(string targetAirline)
        {
            const string sql = """
        WITH counts AS (
            SELECT
                month,

                COALESCE(
                    SUM(otp15_eligible_flights),
                    0
                ) AS network_otp15_eligible_flights,

                COALESCE(
                    SUM(otp15_flights),
                    0
                ) AS network_otp15_flights,

                COALESCE(
                    SUM(otp15_eligible_flights) FILTER (
                        WHERE airline_name = @targetAirline
                    ),
                    0
                ) AS target_airline_otp15_eligible_flights,

                COALESCE(
                    SUM(otp15_flights) FILTER (
                        WHERE airline_name = @targetAirline
                    ),
                    0
                ) AS target_airline_otp15_flights

            FROM mv_airlineperformance_summary

            GROUP BY month
        )

        SELECT
            month AS Month,

            ROUND(
                network_otp15_flights::numeric * 100
                / NULLIF(network_otp15_eligible_flights, 0),
                2
            ) AS NetworkOtp15Pct,

            ROUND(
                target_airline_otp15_flights::numeric * 100
                / NULLIF(target_airline_otp15_eligible_flights, 0),
                2
            ) AS TargetAirlineOtp15Pct

        FROM counts

        ORDER BY TO_DATE(month, 'Month');
        """;

            var connection = _dataSource.OpenConnection();

            return connection.QueryAsync<AirlineMonthlyOtp15ComparisonDto>(
                sql,
                new { targetAirline }
            );
        }
        public async Task<PagedResult<AirlineScorecardDto>> GetAirlineScorecardAsync(int pageNumber,int pageSize)
        {
            const string sql = """
        SELECT COUNT(*)
        FROM vw_airlineperformance_scorecard;

        SELECT
            airline_code AS AirlineCode,
            airline_name AS AirlineName,
            completed_flights AS CompletedFlights,
            otp15_rate AS Otp15Rate,
            severe_delay_rate AS SevereDelayRate,
            severe_cases AS SevereCases,
            cancellation_rate AS CancellationRate,
            priority_interpretation AS PriorityInterpretation
        FROM vw_airlineperformance_scorecard
        ORDER BY
            severe_delay_rate DESC,
            airline_name ASC
        LIMIT @pageSize
        OFFSET (@pageNumber - 1) * @pageSize;
        """;

            var connection = _dataSource.OpenConnection();

            using var result = await connection.QueryMultipleAsync(
                sql,
                new { pageNumber, pageSize }
            );

            var totalCount = await result.ReadSingleAsync<long>();
            var items = await result.ReadAsync<AirlineScorecardDto>();

            return new PagedResult<AirlineScorecardDto>
            {
                Items = items,
                TotalCount = totalCount
            };
        }
    }
}
