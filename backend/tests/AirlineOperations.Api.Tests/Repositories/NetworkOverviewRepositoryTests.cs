using AirlineOperations.Api.DTOs.NetworkOverview;
using AirlineOperations.Api.Repositories;
using AirlineOperations.Api.Tests.Fixtures;
using Npgsql;
using Xunit;

namespace AirlineOperations.Api.Tests.Repositories;

[Collection("Database Integration Tests")]
public class NetworkOverviewRepositoryTests
    : IClassFixture<TestDatabaseFixture>
{
    private readonly TestDatabaseFixture _fixture;

    public NetworkOverviewRepositoryTests(
        TestDatabaseFixture fixture)
    {
        _fixture = fixture;
    }


    // =========================================================
    // FILTER OPTIONS
    // =========================================================

    [Fact]
    public async Task GetOverviewFilterOptionsAsync_ReturnsDistinctOptionsInExpectedOrder()
    {
        // Arrange
        await ResetDatabaseAsync();

        await ExecuteSqlAsync("""
            INSERT INTO mv_networkoverview_summary
            (
                year,
                airline_name,
                month,
                time_band
            )
            VALUES
                (2016, 'Test Air',  'March',    'Night'),
                (2015, 'Other Air', 'January',  'AMPeak'),
                (2015, 'Test Air',  'February', 'PMPeak'),
                (2015, 'Test Air',  'January',  'AMPeak');
            """);

        var repository =
            new NetworkOverviewRepository(_fixture.DataSource);

        // Act
        var result =
            await repository.GetOverviewFilterOptionsAsync();

        // Assert
        Assert.Equal(
            new[] { 2015, 2016 },
            result.Year);

        Assert.Equal(
            new[] { "Other Air", "Test Air" },
            result.Airline);

        Assert.Equal(
            new[] { "January", "February", "March" },
            result.Month);

        Assert.Equal(
            new[] { "AMPeak", "PMPeak", "Night" },
            result.TimeBand);
    }


    // =========================================================
    // KPI TESTS
    // =========================================================

    [Fact]
    public async Task GetOverviewKpisAsync_WithNoFilters_ReturnsNetworkKpis()
    {
        // Arrange
        await ResetDatabaseAsync();

        await ExecuteSqlAsync("""
            INSERT INTO mv_networkoverview_summary
            (
                year,
                airline_name,
                month,
                time_band,
                total_flights,
                otp15_eligible_flights,
                otp15_flights,
                severe_delay_flights,
                cancelled_flights,
                non_cancelled_flights,
                diverted_flights
            )
            VALUES
            (
                2015,
                'Test Air',
                'January',
                'AMPeak',
                4,
                3,
                2,
                1,
                1,
                3,
                1
            ),
            (
                2015,
                'Other Air',
                'January',
                'AMPeak',
                100,
                100,
                100,
                0,
                0,
                100,
                0
            );
            """);

        var repository =
            new NetworkOverviewRepository(_fixture.DataSource);

        // Act
        var result =
            await repository.GetOverviewKpisAsync(
                new NetworkOverviewFilterDto());

        // Assert
        Assert.Equal(99.03m, result.Otp15Pct);
        Assert.Equal(0.97m, result.SevereDelaySd60Pct);
        Assert.Equal(0.96m, result.CancellationPct);
        Assert.Equal(0.97m, result.DiversionPct);
    }


    [Fact]
    public async Task GetOverviewKpisAsync_WithAirlineFilter_ReturnsCorrectKpis()
    {
        // Arrange
        await ResetDatabaseAsync();

        await ExecuteSqlAsync("""
            INSERT INTO mv_networkoverview_summary
            (
                year,
                airline_name,
                month,
                time_band,
                total_flights,
                otp15_eligible_flights,
                otp15_flights,
                severe_delay_flights,
                cancelled_flights,
                non_cancelled_flights,
                diverted_flights
            )
            VALUES
            (
                2015,
                'Test Air',
                'January',
                'AMPeak',
                4,
                3,
                2,
                1,
                1,
                3,
                1
            ),
            (
                2015,
                'Other Air',
                'January',
                'AMPeak',
                100,
                100,
                100,
                0,
                0,
                100,
                0
            );
            """);

        var repository =
            new NetworkOverviewRepository(_fixture.DataSource);

        var filters = new NetworkOverviewFilterDto
        {
            Airline = "Test Air"
        };

        // Act
        var result =
            await repository.GetOverviewKpisAsync(filters);

        // Assert
        Assert.Equal(66.67m, result.Otp15Pct);
        Assert.Equal(33.33m, result.SevereDelaySd60Pct);
        Assert.Equal(25.00m, result.CancellationPct);
        Assert.Equal(33.33m, result.DiversionPct);
    }


    // =========================================================
    // MONTHLY OTP15
    // =========================================================

    [Fact]
    public async Task GetOverviewOtp15MonthlyAsync_WithNoFilters_ReturnsMonthlyOtp15Rates()
    {
        // Arrange
        await ResetDatabaseAsync();

        await ExecuteSqlAsync("""
            INSERT INTO mv_networkoverview_summary
            (
                year,
                airline_name,
                month,
                time_band,
                otp15_eligible_flights,
                otp15_flights
            )
            VALUES
                (2015, 'Test Air',  'January',  'AMPeak', 3, 2),
                (2015, 'Other Air', 'January',  'AMPeak', 1, 1),

                (2015, 'Test Air',  'February', 'AMPeak', 4, 2),
                (2015, 'Other Air', 'February', 'AMPeak', 2, 2),

                (2015, 'Test Air',  'March',    'AMPeak', 5, 5),
                (2015, 'Other Air', 'March',    'AMPeak', 5, 4);
            """);

        var repository =
            new NetworkOverviewRepository(_fixture.DataSource);

        // Act
        var result =
            await repository.GetOverviewOtp15MonthlyAsync(
                new NetworkOverviewFilterDto());

        var monthlyResults = result.ToList();

        // Assert
        Assert.Equal(3, monthlyResults.Count);

        Assert.Equal("January", monthlyResults[0].Month);
        Assert.Equal(75.00m, monthlyResults[0].Otp15Pct);

        Assert.Equal("February", monthlyResults[1].Month);
        Assert.Equal(66.67m, monthlyResults[1].Otp15Pct);

        Assert.Equal("March", monthlyResults[2].Month);
        Assert.Equal(90.00m, monthlyResults[2].Otp15Pct);
    }


    [Fact]
    public async Task GetOverviewOtp15MonthlyAsync_WithAirlineAndMonthFilter_ReturnsAllMonthsForSelectedAirline()
    {
        // Arrange
        await ResetDatabaseAsync();

        await ExecuteSqlAsync("""
            INSERT INTO mv_networkoverview_summary
            (
                year,
                airline_name,
                month,
                time_band,
                otp15_eligible_flights,
                otp15_flights
            )
            VALUES
                (2015, 'Test Air',  'January',  'AMPeak', 3, 2),
                (2015, 'Other Air', 'January',  'AMPeak', 10, 10),

                (2015, 'Test Air',  'February', 'AMPeak', 4, 2),
                (2015, 'Other Air', 'February', 'AMPeak', 10, 9),

                (2015, 'Test Air',  'March',    'AMPeak', 5, 5),
                (2015, 'Other Air', 'March',    'AMPeak', 10, 6);
            """);

        var repository =
            new NetworkOverviewRepository(_fixture.DataSource);

        var filters = new NetworkOverviewFilterDto
        {
            Airline = "Test Air",

            // Intentionally supplied.
            // Monthly trend query should ignore Month
            // and still return the full trend.
            Month = "January"
        };

        // Act
        var result =
            await repository.GetOverviewOtp15MonthlyAsync(filters);

        var monthlyResults = result.ToList();

        // Assert
        Assert.Equal(3, monthlyResults.Count);

        Assert.Equal("January", monthlyResults[0].Month);
        Assert.Equal(66.67m, monthlyResults[0].Otp15Pct);

        Assert.Equal("February", monthlyResults[1].Month);
        Assert.Equal(50.00m, monthlyResults[1].Otp15Pct);

        Assert.Equal("March", monthlyResults[2].Month);
        Assert.Equal(100.00m, monthlyResults[2].Otp15Pct);
    }


    // =========================================================
    // FLIGHT OUTCOME MIX
    // =========================================================

    [Fact]
    public async Task GetOverviewFlightOutcomeMixAsync_WithAirlineFilter_ReturnsCorrectOutcomeMix()
    {
        // Arrange
        await ResetDatabaseAsync();

        await ExecuteSqlAsync("""
            INSERT INTO mv_networkoverview_summary
            (
                year,
                airline_name,
                month,
                time_band,
                total_flights,
                otp15_flights,
                moderate_delay_flights,
                severe_delay_flights,
                diverted_or_cancelled_flights
            )
            VALUES
            (
                2015,
                'Test Air',
                'January',
                'AMPeak',
                10,
                6,
                2,
                1,
                1
            ),
            (
                2015,
                'Other Air',
                'January',
                'AMPeak',
                100,
                90,
                5,
                3,
                2
            );
            """);

        var repository =
            new NetworkOverviewRepository(_fixture.DataSource);

        var filters = new NetworkOverviewFilterDto
        {
            Airline = "Test Air"
        };

        // Act
        var result =
            await repository.GetOverviewFlightOutcomeMixAsync(filters);

        // Assert
        Assert.Equal(10, result.TotalFlights);
        Assert.Equal(6, result.Otp15Flights);
        Assert.Equal(2, result.ModerateDelayFlights);
        Assert.Equal(1, result.SevereDelayFlights);
        Assert.Equal(1, result.DivertedOrCancelledFlights);
    }


    // =========================================================
    // AIRLINE OTP15 COMPARISON
    // =========================================================

    [Fact]
    public async Task GetAirlineOtp15PerformanceRateAsync_WithMonthFilter_ReturnsAirlinesOrderedByOtp15Rate()
    {
        // Arrange
        await ResetDatabaseAsync();

        await ExecuteSqlAsync("""
            INSERT INTO mv_networkoverview_summary
            (
                year,
                airline_name,
                month,
                time_band,
                otp15_eligible_flights,
                otp15_flights
            )
            VALUES
                (2015, 'Test Air',  'January',  'AMPeak', 10, 8),
                (2015, 'Other Air', 'January',  'AMPeak', 10, 9),
                (2015, 'Third Air', 'January',  'AMPeak', 10, 5),

                -- Distractor row from another month
                (2015, 'Other Air', 'February', 'AMPeak', 10, 0);
            """);

        var repository =
            new NetworkOverviewRepository(_fixture.DataSource);

        var filters = new NetworkOverviewFilterDto
        {
            Month = "January"
        };

        // Act
        var result =
            await repository.GetAirlineOtp15PerformanceRateAsync(filters);

        var airlines = result.ToList();

        // Assert
        Assert.Equal(3, airlines.Count);

        Assert.Equal("Other Air", airlines[0].AirlineName);
        Assert.Equal(90.00m, airlines[0].Otp15Rate);

        Assert.Equal("Test Air", airlines[1].AirlineName);
        Assert.Equal(80.00m, airlines[1].Otp15Rate);

        Assert.Equal("Third Air", airlines[2].AirlineName);
        Assert.Equal(50.00m, airlines[2].Otp15Rate);
    }


    // =========================================================
    // TEST HELPERS
    // =========================================================

    private async Task ResetDatabaseAsync()
    {
        await using var connection =
            await _fixture.DataSource.OpenConnectionAsync();

        const string sql = """
            TRUNCATE TABLE mv_networkoverview_summary;
            """;

        await using var command =
            new NpgsqlCommand(sql, connection);

        await command.ExecuteNonQueryAsync();
    }


    private async Task ExecuteSqlAsync(string sql)
    {
        await using var connection =
            await _fixture.DataSource.OpenConnectionAsync();

        await using var command =
            new NpgsqlCommand(sql, connection);

        await command.ExecuteNonQueryAsync();
    }
}