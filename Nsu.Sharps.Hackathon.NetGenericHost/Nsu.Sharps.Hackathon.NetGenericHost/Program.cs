using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nsu.Sharps.Hackathon.NetGenericHost.Interfaces;
using Nsu.Sharps.Hackathon.NetGenericHost.Models;
using Nsu.Sharps.Hackathon.NetGenericHost.Services;

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureServices((context, services) =>
            {
                services.AddHostedService<HackathonWorker>();
                services.AddTransient<IReader, ReadCsvFile>();
                services.AddTransient<Hackathon>();
                services.AddTransient<WishlistGenerator>();
                services.AddTransient<HRDirector>();
                services.AddTransient<HRManager>();

                services.AddSingleton<List<Junior>>(provider => new List<Junior>());
                services.AddSingleton<List<TeamLead>>(provider => new List<TeamLead>());
            });
    }
}