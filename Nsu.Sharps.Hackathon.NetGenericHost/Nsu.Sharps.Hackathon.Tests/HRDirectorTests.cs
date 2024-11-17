using Microsoft.Extensions.Options;
using Nsu.Sharps.Hackathon.NetGenericHost.Models;
using Nsu.Sharps.Hackathon.NetGenericHost.Options;
using Nsu.Sharps.Hackathon.NetGenericHost.Services;

namespace Nsu.Sharps.Hackathon.Tests;

public class HRDirectorTests
{
    [Fact]
    public void HarmonicMeanOfEqualNumbersShouldEqualToNumber()
    {
        var calculationOptions = Options.Create(new CalculationDataOptions
        {
            AmountOfParticipantsInNumerator = 2.0,
            NumeratorInCountingHarmony = 1.0
        });
        var amountOptions = Options.Create(new AmountValuesOptions());
        var harmonyCalculator = new HarmonyCalculator(calculationOptions, amountOptions);

        var value = 50;

        var result = harmonyCalculator.CalculateHarmonicMean(new List<int> { value, value });

        Assert.Equal(value, result);
    }

    [Fact]
    public void CheckCorrectnessOfHarmonicMean()
    {
        var calculationOptions = Options.Create(new CalculationDataOptions
        {
            AmountOfParticipantsInNumerator = 2,
            NumeratorInCountingHarmony = 1.0
        });
        var amountOptions = Options.Create(new AmountValuesOptions());
        var harmonyCalculator = new HarmonyCalculator(calculationOptions, amountOptions);

        var values = new List<int> { 2, 6 };
        var result = harmonyCalculator.CalculateHarmonicMean(values);
        Assert.Equal(3, result);

        values = new List<int> { 1, 50 };
        result = harmonyCalculator.CalculateHarmonicMean(values);
        Assert.Equal(1.9608, result, 4);
    }

    [Fact]
    public void HarmonicMeanCalculationForMultipleValues()
    {
        var calculationOptions = Options.Create(new CalculationDataOptions
        {
            AmountOfParticipantsInNumerator = 3.0,
            NumeratorInCountingHarmony = 1.0
        });

        var amountOptions = Options.Create(new AmountValuesOptions
        {
            AmountOfParticipantsFromOneSide = 3
        });

        var harmonyCalculator = new HarmonyCalculator(calculationOptions, amountOptions);

        var values = new List<int> { 2, 4, 6 };

        var expectedHarmonicMean = 3 / (1.0 / 2 + 1.0 / 4 + 1.0 / 6);

        var harmonicMean = harmonyCalculator.CalculateHarmonicMean(values);

        Assert.Equal(expectedHarmonicMean, harmonicMean, 5);
    }


    [Fact]
    public void PredefinedTeamsAndWishlistsShouldReturnExpectedHarmonyLevel()
    {
        var calculationOptions = Options.Create(new CalculationDataOptions
        {
            AmountOfParticipantsInNumerator = 2.0,
            NumeratorInCountingHarmony = 1.0
        });
        var amountOptions = Options.Create(new AmountValuesOptions
        {
            AmountOfParticipantsFromOneSide = 2
        });

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