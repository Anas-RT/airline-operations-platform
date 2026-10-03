using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace AirlineOperations.Api.Tests.Fixtures
{
   
    public class TestDatabaseFixture:IAsyncLifetime
    {
        public NpgsqlDataSource DataSource { get; }
        public TestDatabaseFixture()
        {
            var connectionString =
                Environment.GetEnvironmentVariable(
                "AIRLINE_TEST_DB_CONNECTION_STRING")
            ?? throw new InvalidOperationException(
                "Test database connection string is not configured.");            ;

            DataSource = NpgsqlDataSource.Create(connectionString);
        }

        public async Task InitializeAsync()
        {
            await using var connection =
            await DataSource.OpenConnectionAsync();

            const string sql = """
            DROP TABLE IF EXISTS mv_airlineperformance_summary;
            DROP TABLE IF EXISTS vw_airlineperformance_airline_severe_delay_rate;
            DROP TABLE IF EXISTS vw_airlineperformance_scorecard;

            CREATE TABLE mv_airlineperformance_summary
            (
                airline_code varchar(20),
                airline_name varchar(200),
                month varchar(20),

                total_flights bigint,
                otp15_eligible_flights bigint,
                otp15_flights bigint,
                delayed_flights bigint,
                severe_delayed_flights bigint,
                cancelled_flights bigint,
                non_cancelled_flights bigint,
                diverted_flights bigint
            );

            CREATE TABLE vw_airlineperformance_airline_severe_delay_rate
            (
                airline_name varchar(200),
                otp15_eligible_flights bigint,
                severe_delay_flights bigint,
                severe_delay_pct numeric
            );
            CREATE TABLE vw_airlineperformance_scorecard
            (
                airline_code varchar(20),
                airline_name varchar(200),
                completed_flights bigint,
                otp15_rate numeric,
                severe_delay_rate numeric,
                severe_cases bigint,
                cancellation_rate numeric,
                priority_interpretation varchar(200)
            );
            """;

            await using var command = new NpgsqlCommand(sql, connection);
            await command.ExecuteNonQueryAsync();
        }

        public async Task DisposeAsync()
        {
            await DataSource.DisposeAsync();
        }
    }
}
