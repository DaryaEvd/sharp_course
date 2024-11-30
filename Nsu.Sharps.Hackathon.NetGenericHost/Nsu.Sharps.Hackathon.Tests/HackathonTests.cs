using Microsoft.Extensions.Options;
using Nsu.Sharps.Hackathon.NetGenericHost.Models;
using Nsu.Sharps.Hackathon.NetGenericHost.Options;
using Nsu.Sharps.Hackathon.NetGenericHost.Services;

namespace Nsu.Sharps.Hackathon.Tests;

public class HackathonTests
{
    [Fact]
    public void PredefinedParticipantsAndWishlistsShouldReturnExpectedHarmonyLevel()
    {
        var calculationOptions = Options.Create(new CalculationDataOptions
        {
            AmountOfParticipantsInNumerator = 2,
            NumeratorInCountingHarmony = 1
        });
        var amountOptions = Options.Create(new AmountValuesOptions
        {
            AmountOfParticipantsFromOneSide = 3
        });

        var harmonyCalculator = new HarmonyCalculator(calculationOptions, amountOptions);
        var hrDirector = new HRDirector(harmonyCalculator);

        var juniors = new List<Junior>
        {
            new(1, "Junior 1") { Wishlist = new List<int> { 1 } },
            new(2, "Junior 2") { Wishlist = new List<int> { 2 } },
            new(3, "Junior 3") { Wishlist = new List<int> { 3 } }
        };

        var teamLeads = new List<TeamLead>
        {
            new(1, "TeamLead 1") { Wishlist = new List<int> { 1 } },
            new(2, "TeamLead 2") { Wishlist = new List<int> { 2 } },
            new(3, "TeamLead 3") { Wishlist = new List<int> { 3 } }
        };

        var wishlistGenerator = new WishlistGenerator(false);

        var matchingStrategy = new DefaultMatchingStrategy();

        var hackathon =
            new NetGenericHost.Services.Hackathon(juniors, teamLeads, wishlistGenerator, hrDirector, matchingStrategy);

        var expectedHarmonyLevel = 3.0;

        var result = hackathon.RunOneHackathon();

        Assert.Equal(expectedHarmonyLevel, result);
    }
}