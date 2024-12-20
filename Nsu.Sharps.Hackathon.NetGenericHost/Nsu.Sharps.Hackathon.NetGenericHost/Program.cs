﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Nsu.Sharps.Hackathon.NetGenericHost.Interfaces;
using Nsu.Sharps.Hackathon.NetGenericHost.Options;
using Nsu.Sharps.Hackathon.NetGenericHost.OptionsValidation;
using Nsu.Sharps.Hackathon.NetGenericHost.Services;

public class Program
{
    public static async Task Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.AddJsonFile("appsettings.json", false, true);
            })
            .ConfigureServices((hostContext, services) =>
            {
                services.Configure<DataPathOptions>(hostContext.Configuration.GetSection("DataPath"));
                services.Configure<AmountValuesOptions>(hostContext.Configuration.GetSection("AmountValues"));
                services.Configure<CalculationDataOptions>(hostContext.Configuration.GetSection("CalculationData"));

                services.AddSingleton<IValidateOptions<DataPathOptions>, DataPathOptionsValidation>();
                services.AddSingleton<IValidateOptions<AmountValuesOptions>, AmountValuesOptionsValidation>();
                services.AddSingleton<IValidateOptions<CalculationDataOptions>, CalculationDataOptionsValidation>();

                services.AddHostedService<HackathonWorker>();
                services.AddTransient<IDataLoader, FileDataLoader>();
                services.AddTransient<IReader, ReadCsvFile>();
                services.AddTransient<WishlistGenerator>();
                services.AddTransient<HackathonFactory>();
                services.AddTransient<Hackathon>();
                services.AddTransient<HarmonyCalculator>();
                services.AddTransient<IMatchingStrategy, DefaultMatchingStrategy>();
                services.AddTransient<HRDirector>();
                services.AddTransient<HRManager>();
            })
            .Build();

        await host.RunAsync();
    }
}