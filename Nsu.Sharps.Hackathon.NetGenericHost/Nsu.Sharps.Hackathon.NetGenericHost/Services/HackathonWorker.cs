using Microsoft.Extensions.Hosting;

namespace Nsu.Sharps.Hackathon.NetGenericHost.Services;

public class HackathonWorker : IHostedService
{
    private readonly HRManager _hrManager;

    public HackathonWorker(HRManager hrManager)
    {
        _hrManager = hrManager;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        var averageHarmonyLevel = _hrManager.RunHackathons(Constants.AmountOfHackathons);

        Console.WriteLine($"Average harmony level: {averageHarmonyLevel}");

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}