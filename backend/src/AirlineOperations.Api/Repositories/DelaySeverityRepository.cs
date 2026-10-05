using AirlineOperations.Api.DTOs.DelaySeverity;
using AirlineOperations.Api.Interfaces.IRepositories;
using Dapper;
using Npgsql;

namespace AirlineOperations.Api.Repositories
{
    public class DelaySeverityRepository : IDelaySeverityRepository
    {
        private readonly NpgsqlDataSource _dataSource;

        public DelaySeverityRepository(NpgsqlDataSource dataSource)
        {
            _dataSource = dataSource;
        }


        private async Task<IEnumerable<T>> QueryAsync<T>(
            string sql,
            object? parameters = null)
        {
            await using var connection =
                await _dataSource.OpenConnectionAsync();

            return await connection.QueryAsync<T>(
                sql,
                parameters
            );
        }


        private async Task<T> QuerySingleAsync<T>(
            string sql,
            object? parameters = null)
        {
            await using var connection =
                await _dataSource.OpenConnectionAsync();

            return await connection.QuerySingleAsync<T>(
                sql,
                parameters
            );
        }


        public Task<DelaySeverityKpisDto> GetDelaySeverityKpisAsync()
        {
            const string sql = """
                            WITH totals AS (
                                SELECT
                                    COALESCE(SUM(otp15_eligible_flights), 0) AS otp15_eligible_flights,
                                    COALESCE(SUM(delayed_flights), 0) AS delayed_flights,
                                    COALESCE(SUM(severe_delay_flights), 0) AS severe_delay_flights,
                                    COALESCE(SUM(severe_delay_minutes), 0) AS severe_delay_minutes

                                FROM mv_delayseverity_summary
                            )

                            SELECT
                                delayed_flights AS DelayedFlights,

                                ROUND(
                                    delayed_flights::numeric * 100
                                    / NULLIF(otp15_eligible_flights, 0),
                                    2
                                ) AS DelayedFlightsPct,

                                ROUND(
                                    severe_delay_flights::numeric * 100
                                    / NULLIF(delayed_flights, 0),
                                    2
                                ) AS DelayedFlightsBecomingSeverePct,

                                severe_delay_flights AS SevereDelayFlights,

                                severe_delay_minutes AS TotalSevereDelayMinutes

                            FROM totals;
                            """;

            return QuerySingleAsync<DelaySeverityKpisDto>(sql);
        }


        public Task<IEnumerable<DelaySeverityDistributionDto>>
            GetDelaySeverityDistributionAsync()
        {
            const string sql = """
                WITH totals AS (
                    SELECT
                        COALESCE(SUM(delayed_flights), 0) AS delayed_flights,
                        COALESCE(SUM(delay_16_29_flights), 0) AS delay_16_29_flights,
                        COALESCE(SUM(delay_30_59_flights), 0) AS delay_30_59_flights,
                        COALESCE(SUM(delay_60_119_flights), 0) AS delay_60_119_flights,
                        COALESCE(SUM(delay_120_plus_flights), 0) AS delay_120_plus_flights

                    FROM mv_delayseverity_summary
                ),

                severity_bands AS (
                    SELECT
                        1 AS band_order,
                        '16-29 minutes' AS severity_band,
                        delay_16_29_flights AS delayed_flights
                    FROM totals

                    UNION ALL

                    SELECT
                        2,
                        '30-59 minutes',
                        delay_30_59_flights
                    FROM totals

                    UNION ALL

                    SELECT
                        3,
                        '60-119 minutes',
                        delay_60_119_flights
                    FROM totals

                    UNION ALL

                    SELECT
                        4,
                        '120+ minutes',
                        delay_120_plus_flights
                    FROM totals
                )

                SELECT
                    severity_band AS SeverityBand,
                    delayed_flights AS DelayedFlights,

                    ROUND(
                        delayed_flights::numeric * 100
                        / NULLIF(
                            (SELECT delayed_flights FROM totals),
                            0
                        ),
                        2
                    ) AS Percentage

                FROM severity_bands

                ORDER BY band_order;
                """;

            return QueryAsync<DelaySeverityDistributionDto>(sql);
        }


        public Task<IEnumerable<DelaySeverityHeatmapDto>>
            GetDelaySeverityHeatmapAsync()
        {
            const string sql = """
                SELECT
                    EXTRACT(MONTH FROM TO_DATE(month, 'Month'))::int
                        AS MonthNumber,
                    month AS Month,
                    time_band AS TimeBand,

                    ROUND(
                        COALESCE(SUM(severe_delay_flights), 0)::numeric * 100
                        /
                        NULLIF(
                            COALESCE(SUM(otp15_eligible_flights), 0),
                            0
                        ),
                        2
                    ) AS SevereDelayRate

                FROM mv_delayseverity_summary

                GROUP BY
                    month,
                    time_band

                ORDER BY
                    TO_DATE(month, 'Month'),
                    CASE time_band
                        WHEN 'AMPeak' THEN 1
                        WHEN 'Midday' THEN 2
                        WHEN 'PMPeak' THEN 3
                        WHEN 'Evening' THEN 4
                        WHEN 'Night' THEN 5
                        ELSE 99
                    END;
                """;

            return QueryAsync<DelaySeverityHeatmapDto>(sql);
        }


        public Task<IEnumerable<DelaySeverityMonthlyTrendDto>>
            GetDelaySeverityMonthlyTrendAsync()
        {
            const string sql = """
                SELECT
                    EXTRACT(MONTH FROM TO_DATE(month, 'Month'))::int
                        AS MonthNumber,
                    month AS Month,

                    ROUND(
                        COALESCE(SUM(severe_delay_flights), 0)::numeric * 100
                        /
                        NULLIF(
                            COALESCE(SUM(otp15_eligible_flights), 0),
                            0
                        ),
                        2
                    ) AS SevereDelayRate

                FROM mv_delayseverity_summary

                GROUP BY month

                ORDER BY TO_DATE(month, 'Month');
                """;

            return QueryAsync<DelaySeverityMonthlyTrendDto>(sql);
        }


        public Task<IEnumerable<DelaySeverityDriverImpactDto>>
            GetDelaySeverityDriverImpactAsync()
        {
            const string sql = """
                WITH driver_totals AS (
                    SELECT
                        primary_delay_driver,

                        COALESCE(
                            SUM(severe_delay_flights),
                            0
                        ) AS severe_delay_flights,

                        COALESCE(
                            SUM(primary_driver_severe_minutes),
                            0
                        ) AS total_severe_delay_minutes

                    FROM mv_delayseverity_summary

                    WHERE primary_delay_driver IS NOT NULL

                    GROUP BY primary_delay_driver
                )

                SELECT
                    primary_delay_driver AS DelayDriver,

                    severe_delay_flights AS SevereDelayFlights,

                    ROUND(
                        severe_delay_flights::numeric * 100
                        /
                        NULLIF(
                            SUM(severe_delay_flights) OVER (),
                            0
                        ),
                        2
                    ) AS SevereDelayCaseShare,

                    total_severe_delay_minutes AS TotalSevereDelayMinutes

                FROM driver_totals

                ORDER BY
                    severe_delay_flights DESC,
                    primary_delay_driver ASC;
                """;

            return QueryAsync<DelaySeverityDriverImpactDto>(sql);
        }
    }
}
