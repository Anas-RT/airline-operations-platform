using AirlineOperations.Api.DTOs.NetworkOverview;
using AirlineOperations.Api.Interfaces.IRepositories;
using Dapper;
using Npgsql;

namespace AirlineOperations.Api.Repositories
{
    public class NetworkOverviewRepository : INetworkOverviewRepository
    {
        private readonly NpgsqlDataSource _dataSource;

        public NetworkOverviewRepository(NpgsqlDataSource dataSource)
        {
            _dataSource = dataSource;
        }
        public async Task<NetworkOverviewFilterOptionsDto> GetOverviewFilterOptionsAsync()
        {
            const string sql = """
                SELECT

                ARRAY(
                    SELECT DISTINCT year
                    FROM mv_networkoverview_summary
                    ORDER BY year
                ) AS Year,

                ARRAY(
                    SELECT DISTINCT airline_name
                    FROM mv_networkoverview_summary
                    ORDER BY airline_name
                ) AS Airline,

                ARRAY(
                    SELECT month
                    FROM (
                        SELECT DISTINCT month
                        FROM mv_networkoverview_summary
                    ) m
                    ORDER BY TO_DATE(month, 'Month')
                ) AS Month,

                ARRAY(
                    SELECT time_band
                    FROM (
                        SELECT DISTINCT time_band
                        FROM mv_networkoverview_summary
                    ) t
                    ORDER BY
                        CASE time_band
                            WHEN 'AMPeak' THEN 1
                            WHEN 'Midday' THEN 2
                            WHEN 'PMPeak' THEN 3
                            WHEN 'Evening' THEN 4
                            WHEN 'Night' THEN 5
                        END
                ) AS TimeBand;
                """;
            await using var connection =
            await _dataSource.OpenConnectionAsync();

            return await connection.QuerySingleAsync<NetworkOverviewFilterOptionsDto>(sql); 
        }
        public async Task<NetworkOverviewKpisDto> GetOverviewKpisAsync(NetworkOverviewFilterDto filters)
        {
            const string sql = """
            SELECT
                ROUND(
                    COALESCE(SUM(otp15_flights), 0)::numeric * 100
                    / NULLIF(COALESCE(SUM(otp15_eligible_flights), 0), 0),
                    2
                ) AS Otp15Pct,

                ROUND(
                    COALESCE(SUM(severe_delay_flights), 0)::numeric * 100
                    / NULLIF(COALESCE(SUM(otp15_eligible_flights), 0), 0),
                    2
                ) AS SevereDelaySd60Pct,

                ROUND(
                    COALESCE(SUM(cancelled_flights), 0)::numeric * 100
                    / NULLIF(COALESCE(SUM(total_flights), 0), 0),
                    2
                ) AS CancellationPct,

                ROUND(
                    COALESCE(SUM(diverted_flights), 0)::numeric * 100
                    / NULLIF(COALESCE(SUM(non_cancelled_flights), 0), 0),
                    2
                ) AS DiversionPct

            FROM mv_networkoverview_summary

            WHERE (@Year IS NULL OR year = @Year)
              AND (@Airline IS NULL OR airline_name = @Airline)
              AND (@Month IS NULL OR month = @Month)
              AND (@TimeBand IS NULL OR time_band = @TimeBand);
            """;

            await using var connection =
                await _dataSource.OpenConnectionAsync();

            return await connection.QuerySingleAsync<NetworkOverviewKpisDto>(sql, filters);
        }

        public async Task<IEnumerable<NetworkOverviewOtp15MonthlyDto>> GetOverviewOtp15MonthlyAsync(NetworkOverviewFilterDto filters)
        {
            const string sql = """
            SELECT
                month AS Month,

                ROUND(
                    COALESCE(SUM(otp15_flights), 0)::numeric * 100
                    / NULLIF(COALESCE(SUM(otp15_eligible_flights), 0), 0),
                    2
                ) AS Otp15Pct
            FROM mv_networkoverview_summary

            WHERE (@Year IS NULL OR year = @Year)
              AND (@Airline IS NULL OR airline_name = @Airline)
              AND (@TimeBand IS NULL OR time_band = @TimeBand)

            GROUP BY month

            ORDER BY TO_DATE(month, 'Month');
            """;

            await using var connection =
                await _dataSource.OpenConnectionAsync();

            return await connection.QueryAsync<NetworkOverviewOtp15MonthlyDto>(sql, filters);
        }

        public async Task<NetworkOverviewFlightOutcomeMixDto> GetOverviewFlightOutcomeMixAsync(NetworkOverviewFilterDto filters) {
            const string sql = """
                                SELECT 
                                    COALESCE(SUM(total_flights), 0) AS TotalFlights,
                                    COALESCE(SUM(otp15_flights), 0) AS Otp15Flights,
                                    COALESCE(SUM(moderate_delay_flights), 0) AS ModerateDelayFlights,
                                    COALESCE(SUM(severe_delay_flights), 0) AS SevereDelayFlights,
                                    COALESCE(SUM(diverted_or_cancelled_flights), 0) AS DivertedOrCancelledFlights

                                    FROM mv_networkoverview_summary

                                    WHERE (@Year IS NULL OR year = @Year)
                                      AND (@Airline IS NULL OR airline_name = @Airline)
                                      AND (@Month IS NULL OR month = @Month)
                                      AND (@TimeBand IS NULL OR time_band = @TimeBand);
            
            """;

            await using var connection =
                await _dataSource.OpenConnectionAsync();

            return await connection.QuerySingleAsync<NetworkOverviewFlightOutcomeMixDto>(
                sql,
                filters);
        }

        public async Task<IEnumerable<NetworkOverviewAirlineOtp15Dto>> GetAirlineOtp15PerformanceRateAsync(NetworkOverviewFilterDto filters)
        {
            const string sql = """
                                SELECT
                                airline_name AS AirlineName,

                                ROUND(
                                        SUM(otp15_flights)::numeric * 100
                                        / NULLIF(SUM(otp15_eligible_flights), 0),
                                        2
                                ) AS Otp15Rate

                                FROM mv_networkoverview_summary

                                WHERE (@Year IS NULL OR year = @Year)
                                AND (@Month IS NULL OR month = @Month)
                                AND (@TimeBand IS NULL OR time_band = @TimeBand)

                                GROUP BY airline_name

                                ORDER BY Otp15Rate DESC;
                                """;

            await using var connection =
                await _dataSource.OpenConnectionAsync();

            return await connection.QueryAsync<NetworkOverviewAirlineOtp15Dto>(
                sql,
                filters
            );
        }
    } 
}
