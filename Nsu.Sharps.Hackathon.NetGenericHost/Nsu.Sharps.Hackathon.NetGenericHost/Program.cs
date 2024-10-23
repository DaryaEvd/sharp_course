using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nsu.Sharps.Hackathon.NetGenericHost;
using Nsu.Sharps.Hackathon.NetGenericHost.Interfaces;
using Nsu.Sharps.Hackathon.NetGenericHost.Models;
using Nsu.Sharps.Hackathon.NetGenericHost.Services;

public class Program
{
    public static async Task Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                services.AddHostedService<HackathonWorker>();
                services.AddTransient<IReader, ReadCsvFile>();
                services.AddTransient<WishlistGenerator>();
                services.AddTransient<Hackathon>();
                services.AddTransient<HRDirector>();

                services.AddTransient<HRManager>(serviceProvider =>
                {
                    var reader = serviceProvider.GetRequiredService<IReader>();

                    var juniors = reader.ReadFile(Constants.PathForJuniors)
                        .OfType<Junior>().ToList();
                    var teamLeads = reader.ReadFile(Constants.PathForTeamLeads)
                        .OfType<TeamLead>().ToList();

                    var wishlistGenerator = serviceProvider.GetRequiredService<WishlistGenerator>();
                    var hrDirector = serviceProvider.GetRequiredService<HRDirector>();

                    return new HRManager(juniors, teamLeads, wishlistGenerator, hrDirector);
                });
            })
            .Build();

        await host.RunAsync();
    }
}