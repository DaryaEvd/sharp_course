using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Nsu.Sharps.Hackathon.NetGenericHost.Options;

namespace Nsu.Sharps.Hackathon.NetGenericHost.Services;

public class HackathonWorker : IHostedService
{
    private readonly AmountValuesOptions _amountValuesOptions;
    private readonly HackathonFactory _hackathonFactory;

    public HackathonWorker(HackathonFactory hackathonFactory, IOptions<AmountValuesOptions> amountValuesOptions)
    {
        _hackathonFactory = hackathonFactory;
        _amountValuesOptions = amountValuesOptions.Value;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        var hackathon = _hackathonFactory.CreateHackathon();
        var averageHarmonyLevel = hackathon.CalculateAverageHarmonyLevel(_amountValuesOptions.AmountOfHackathons);
        
        Console.WriteLine($"Average harmony level: {averageHarmonyLevel}");
        
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}