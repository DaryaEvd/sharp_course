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

        var result = harmonyCalculator.CalculateHarmonicMean(value, value);

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

        var firstValue = 2;
        var secondValue = 6;

        var result = harmonyCalculator.CalculateHarmonicMean(firstValue, secondValue);
        Assert.Equal(3, result);

        firstValue = 1;
        secondValue = 50;
        result = harmonyCalculator.CalculateHarmonicMean(firstValue, secondValue);
        Assert.Equal(1.96, Math.Round(result, 2));
    }

    [Fact]
    public void PredefinedTeamsAndWishlistsShouldReturnExpectedHarmonyLevel() //todo: to think, doesn't work
    {
        var calculationOptions = Options.Create(new CalculationDataOptions
        {
            AmountOfParticipantsInNumerator = 2.0,
            NumeratorInCountingHarmony = 1.0
        });
        var amountOptions = Options.Create(new AmountValuesOptions
        {
            AmountOfParticipantsInOneTeam = 2
        });

        var harmonyCalculator = new HarmonyCalculator(calculationOptions, amountOptions);
        var hrDirector = new HRDirector(harmonyCalculator);

        var teams = new List<Team>
        {
            new(new TeamLead(1, "TeamLead 1") { Wishlist = new List<int> { 1 } },
                new Junior(1, "Junior 1") { Wishlist = new List<int> { 1 } }),
            new(new TeamLead(2, "TeamLead 2") { Wishlist = new List<int> { 2 } },
                new Junior(2, "Junior 2") { Wishlist = new List<int> { 2 } })
        };

        var expectedHarmonyLevel = 1.0;

        var result = hrDirector.CalculateHarmonyLevel(teams);

        Assert.Equal(expectedHarmonyLevel, result);
    }
}