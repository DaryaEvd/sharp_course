using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nsu.Sharps.Hackathon.NetGenericHost;
using Nsu.Sharps.Hackathon.NetGenericHost.Interfaces;
using Nsu.Sharps.Hackathon.NetGenericHost.Models;
using Nsu.Sharps.Hackathon.NetGenericHost.Options;
using Nsu.Sharps.Hackathon.NetGenericHost.Services;

public class Program
{
    public static async Task Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            })
            .ConfigureServices((hostContext, services) =>
            {
                services.Configure<DataPathOptions>(hostContext.Configuration.GetSection("DataPath"));
                services.Configure<AmountValuesOptions>(hostContext.Configuration.GetSection("AmountValues"));
                services.Configure<CalculationDataOptions>(hostContext.Configuration.GetSection("CalculationData"));
                
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