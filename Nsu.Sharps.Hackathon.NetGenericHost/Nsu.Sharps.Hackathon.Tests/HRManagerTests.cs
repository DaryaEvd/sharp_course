using Microsoft.Extensions.Options;
using Moq;
using Nsu.Sharps.Hackathon.NetGenericHost.Models;
using Nsu.Sharps.Hackathon.NetGenericHost.Options;
using Nsu.Sharps.Hackathon.NetGenericHost.Services;

namespace Nsu.Sharps.Hackathon.Tests;

public class HRManagerTests
{
    [Fact]
    public void ShouldReturnExpectedDistribution()
    {
        var juniors = new List<Junior> { new(1, "Junior1", 1) { Wishlist = new List<int> { 1 } } };
        var teamLeads = new List<TeamLead> { new(1, "TeamLead1", 1) { Wishlist = new List<int> { 1 } } };
        var wishlistGenerator = new WishlistGenerator();

        var calculationDataOptions = Options.Create(new CalculationDataOptions {});
        var hrDirectorMock = new Mock<HRDirector>(calculationDataOptions) { CallBase = true };
        
        var manager = new HRManager(juniors, teamLeads, wishlistGenerator, hrDirectorMock.Object);

    var result = manager.ExecuteMatchingAlgorithm();

        Assert.True(result.ContainsKey(1) && result[1] == 1);
    }
    
    [Fact]
    public void StrategyShouldBeCalledOnce() // todo: нитево не робит чота, подумац
    {
        
        var juniors = new List<Junior> { new(1, "Junior1", 1) };
        var teamLeads = new List<TeamLead> { new(1, "TeamLead1", 1) };
        var wishlistGenerator = new WishlistGenerator();
        var calculationDataOptions = Options.Create(new CalculationDataOptions {});
        var hrDirectorMock = new Mock<HRDirector>(calculationDataOptions) { CallBase = true };
        
        var managerMock = new Mock<HRManager>(juniors, teamLeads, wishlistGenerator, hrDirectorMock.Object);
        managerMock.Setup(m => m.ExecuteMatchingAlgorithm()).CallBase();
        
        managerMock.Verify(m => m.ExecuteMatchingAlgorithm(), Times.Once);
    }
    
    [Fact]
    public void AmountOfTeamsShouldBeSameWithPrerequiredAmountOfTeams()
    {
        var juniors = new List<Junior> { new(1, "Junior1", 2), new(2, "Junior2", 2) };
        var teamLeads = new List<TeamLead> { new(1, "TeamLead1", 2), new(2, "TeamLead2", 2) };
        var wishlistGenerator = new WishlistGenerator();
        
        var calculationDataOptions = Options.Create(new CalculationDataOptions {});
        var hrDirectorMock = new Mock<HRDirector>(calculationDataOptions) { CallBase = true };
        
        var manager = new HRManager(juniors, teamLeads, wishlistGenerator, hrDirectorMock.Object);

        var result = manager.ExecuteMatchingAlgorithm();

        Assert.Equal(juniors.Count, result.Count); // todo: think Assert.Equal() Failure: Values differ plack plack
    }
}