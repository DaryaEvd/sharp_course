using Nsu.Sharps.Hackathon.NetGenericHost.Models;
using Nsu.Sharps.Hackathon.NetGenericHost.Services;

namespace Nsu.Sharps.Hackathon.Tests;

public class HRDirectorTests
{
    private readonly Fixture _fixture;

    public HRDirectorTests()
    {
        _fixture = new Fixture();
    }

    [Fact]
    public void HarmonicMeanOfEqualNumbersShouldEqualToNumber()
    {
        var calculationOptions = _fixture.CreateCalculationDataOptions(2, 1);
        var amountOptions = _fixture.CreateAmountValuesOptions(0);
        var harmonyCalculator = new HarmonyCalculator(calculationOptions, amountOptions);

        var value = 50;

        var result = harmonyCalculator.CalculateHarmonicMean(new List<int> { value, value });

        Assert.Equal(value, result);
    }

    [Theory]
    [InlineData(new[] { 2, 6 }, 3)]
    [InlineData(new[] { 1, 50 }, 1.9608)]
    public void CheckCorrectnessOfHarmonicMean(int[] valuesArray, double expected)
    {
        var calculationOptions = _fixture.CreateCalculationDataOptions(2, 1);
        var amountOptions = _fixture.CreateAmountValuesOptions(0);

        var harmonyCalculator = new HarmonyCalculator(calculationOptions, amountOptions);

        var values = new List<int>(valuesArray);

        var result = harmonyCalculator.CalculateHarmonicMean(values);

        Assert.Equal(expected, result, 4);
    }

    [Fact]
    public void HarmonicMeanCalculationForMultipleValues()
    {
        var calculationOptions = _fixture.CreateCalculationDataOptions(3, 1);
        var amountOptions = _fixture.CreateAmountValuesOptions(3);

        var harmonyCalculator = new HarmonyCalculator(calculationOptions, amountOptions);

        var values = new List<int> { 2, 4, 6 };

        var expectedHarmonicMean = 3 / (1.0 / 2 + 1.0 / 4 + 1.0 / 6);

        var harmonicMean = harmonyCalculator.CalculateHarmonicMean(values);

        Assert.Equal(expectedHarmonicMean, harmonicMean, 5);
    }

    [Fact]
    public void PredefinedTeamsAndWishlistsShouldReturnExpectedHarmonyLevel()
    {
        var calculationOptions = _fixture.CreateCalculationDataOptions(2, 1);
        var amountOptions = _fixture.CreateAmountValuesOptions(2);

        var harmonyCalculator = new HarmonyCalculator(calculationOptions, amountOptions);
        var hrDirector = new HRDirector(harmonyCalculator);

        var teams = new List<Team>
        {
            new(
                new TeamLead(1, "TeamLead 1") { Wishlist = new List<int> { 1 } },
                new Junior(1, "Junior 1") { Wishlist = new List<int> { 1 } }
            ),
            new(
                new TeamLead(2, "TeamLead 2") { Wishlist = new List<int> { 2 } },
                new Junior(2, "Junior 2") { Wishlist = new List<int> { 2 } }
            )
        };

        var expectedHarmonyLevel = 2;

        var result = hrDirector.CalculateHarmonyLevel(teams);

        Assert.Equal(expectedHarmonyLevel, result);
    }
}