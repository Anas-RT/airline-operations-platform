using AirlineOperations.Api.Repositories;
using AirlineOperations.Api.Tests.Fixtures;
using Npgsql;
using Xunit;

namespace AirlineOperations.Api.Tests.Repositories;

[Collection("Database Integration Tests")]
public class AirlinePerformanceRepositoryTests
    : IClassFixture<TestDatabaseFixture>
{
    private readonly TestDatabaseFixture _fixture;

    public AirlinePerformanceRepositoryTests(
        TestDatabaseFixture fixture)
    {
        _fixture = fixture;
    }


    // =========================================================
    // SEVERE DELAY RATE
    // =========================================================

    [Fact]
    public async Task GetAirlinePerformanceSevereDelayRateAsync_ReturnsSevereDelayRates()
    {
        // Arrange
        await ResetSevereDelayRateDataAsync();

        await ExecuteSqlAsync("""
            INSERT INTO vw_airlineperformance_airline_severe_delay_rate
            (
                airline_name,
                otp15_eligible_flights,
                severe_delay_flights,
                severe_delay_pct
            )
            VALUES
            (
                'Test Air',
                100,
                10,
                10.00
            ),
            (
                'Other Air',
                200,
                5,
                2.50
            );
            """);

        var repository =
            new AirlinePerformanceRepository(_fixture.DataSource);

        // Act
        var result =
            await repository.GetAirlinePerformanceSevereDelayRateAsync();

        var airlines = result.ToList();

        // Assert
        Assert.Equal(2, airlines.Count);

        Assert.Equal("Test Air", airlines[0].AirlineName);
        Assert.Equal(100, airlines[0].Otp15EligibleFlights);
        Assert.Equal(10, airlines[0].SevereDelayFlights);
        Assert.Equal(10.00m, airlines[0].SevereDelayPct);

        Assert.Equal("Other Air", airlines[1].AirlineName);
        Assert.Equal(200, airlines[1].Otp15EligibleFlights);
        Assert.Equal(5, airlines[1].SevereDelayFlights);
        Assert.Equal(2.50m, airlines[1].SevereDelayPct);
    }


    // =========================================================
    // AIRLINE KPIs
    // =========================================================

    [Fact]
    public async Task GetAirlineKpisAsync_WithTargetAirline_ReturnsCorrectKpis()
    {
        // Arrange
        await ResetAirlinePerformanceSummaryAsync();

        await ExecuteSqlAsync("""
            INSERT INTO mv_airlineperformance_summary
            (
                airline_name,
                month,
                total_flights,
                otp15_eligible_flights,
                otp15_flights,
                severe_delayed_flights,
                cancelled_flights,
                diverted_flights
            )
            VALUES
            (
                'Test Air',
                'January',
                100,
                90,
                72,
                9,
                5,
                2
            ),
            (
                'Test Air',
                'February',
                50,
                40,
                32,
                4,
                2,
                1
            ),
            (
                'Other Air',
                'January',
                1000,
                1000,
                1000,
                0,
                0,
                0
            );
            """);

        var repository =
            new AirlinePerformanceRepository(_fixture.DataSource);

        // Act
        var result =
            await repository.GetAirlineKpisAsync("Test Air");

        // Assert

        // 150 total - 7 cancelled - 3 diverted
        Assert.Equal(140, result.CompletedFlights);

        // 104 / 130
        Assert.Equal(80.00m, result.Otp15Rate);

        // 13 / 130
        Assert.Equal(10.00m, result.SevereDelayRate);

        // 7 / 150
        Assert.Equal(4.67m, result.CancellationRate);
    }


    // =========================================================
    // AIRLINE VS NETWORK BENCHMARK
    // =========================================================

    [Fact]
    public async Task GetAirlineBenchmarkAsync_WithTargetAirline_ReturnsTargetAndNetworkRates()
    {
        // Arrange
        await ResetAirlinePerformanceSummaryAsync();

        await ExecuteSqlAsync("""
            INSERT INTO mv_airlineperformance_summary
            (
                airline_name,
                month,
                otp15_eligible_flights,
                otp15_flights,
                severe_delayed_flights
            )
            VALUES
            (
                'Test Air',
                'January',
                100,
                80,
                10
            ),
            (
                'Other Air',
                'January',
                100,
                90,
                5
            );
            """);

        var repository =
            new AirlinePerformanceRepository(_fixture.DataSource);

        // Act
        var result =
            await repository.GetAirlineBenchmarkAsync("Test Air");

        // Assert

        // Target: 80 / 100
        Assert.Equal(80.00m, result.TargetAirlineOtp15Rate);

        // Network: 170 / 200
        Assert.Equal(85.00m, result.NetworkOtp15Rate);

        // Target: 10 / 100
        Assert.Equal(10.00m, result.TargetAirlineSevereDelayRate);

        // Network: 15 / 200
        Assert.Equal(7.50m, result.NetworkSevereDelayRate);
    }


    // =========================================================
    // MONTHLY OTP15 COMPARISON
    // =========================================================

    [Fact]
    public async Task GetAirlineMonthlyOtp15ComparisonAsync_WithTargetAirline_ReturnsMonthlyComparison()
    {
        // Arrange
        await ResetAirlinePerformanceSummaryAsync();

        await ExecuteSqlAsync("""
            INSERT INTO mv_airlineperformance_summary
            (
                airline_name,
                month,
                otp15_eligible_flights,
                otp15_flights
            )
            VALUES
            (
                'Test Air',
                'January',
                3,
                2
            ),
            (
                'Other Air',
                'January',
                1,
                1
            ),
            (
                'Test Air',
                'February',
                4,
                2
            ),
            (
                'Other Air',
                'February',
                2,
                2
            ),
            (
                'Test Air',
                'March',
                5,
                5
            ),
            (
                'Other Air',
                'March',
                5,
                4
            );
            """);

        var repository =
            new AirlinePerformanceRepository(_fixture.DataSource);

        // Act
        var result =
            await repository
                .GetAirlineMonthlyOtp15ComparisonAsync("Test Air");

        var monthlyResults = result.ToList();

        // Assert
        Assert.Equal(3, monthlyResults.Count);

        Assert.Equal("January", monthlyResults[0].Month);
        Assert.Equal(75.00m, monthlyResults[0].NetworkOtp15Pct);
        Assert.Equal(66.67m, monthlyResults[0].TargetAirlineOtp15Pct);

        Assert.Equal("February", monthlyResults[1].Month);
        Assert.Equal(66.67m, monthlyResults[1].NetworkOtp15Pct);
        Assert.Equal(50.00m, monthlyResults[1].TargetAirlineOtp15Pct);

        Assert.Equal("March", monthlyResults[2].Month);
        Assert.Equal(90.00m, monthlyResults[2].NetworkOtp15Pct);
        Assert.Equal(100.00m, monthlyResults[2].TargetAirlineOtp15Pct);
    }
    [Fact]
    public async Task GetAirlineScorecardAsync_WithSecondPage_ReturnsCorrectPageAndTotalCount()
    {
        // Arrange
        await ExecuteSqlAsync("""
            TRUNCATE TABLE vw_airlineperformance_scorecard;
            """);

        await ExecuteSqlAsync("""
                INSERT INTO vw_airlineperformance_scorecard
                (
                    airline_code,
                    airline_name,
                    completed_flights,
                    otp15_rate,
                    severe_delay_rate,
                    severe_cases,
                    cancellation_rate,
                    priority_interpretation
                )
                VALUES
                    ('A', 'Airline A', 100, 90, 10, 10, 1, 'Low'),
                    ('B', 'Airline B', 100, 80, 30, 30, 2, 'Medium'),
                    ('C', 'Airline C', 100, 85, 20, 20, 3, 'Medium'),
                    ('D', 'Airline D', 100, 70, 50, 50, 4, 'High'),
                    ('E', 'Airline E', 100, 75, 40, 40, 5, 'High');
                """);
        var repository = new AirlinePerformanceRepository(_fixture.DataSource);
        var pageNumber = 2;
        var pageSize = 2;

        // Act
        var result =
            await repository.GetAirlineScorecardAsync(
                pageNumber,
                pageSize);
        var items = result.Items.ToList();
        // Assert

        Assert.Equal(2, items.Count);
        Assert.Equal(5, result.TotalCount);

        Assert.Equal("B", items[0].AirlineCode);
        Assert.Equal(30m, items[0].SevereDelayRate);

        Assert.Equal("C", items[1].AirlineCode);
        Assert.Equal(20m, items[1].SevereDelayRate);
    }

    // =========================================================
    // HELPERS
    // =========================================================

    private async Task ResetAirlinePerformanceSummaryAsync()
    {
        await ExecuteSqlAsync("""
            TRUNCATE TABLE mv_airlineperformance_summary;
            """);
    }


    private async Task ResetSevereDelayRateDataAsync()
    {
        await ExecuteSqlAsync("""
            TRUNCATE TABLE vw_airlineperformance_airline_severe_delay_rate;
            """);
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