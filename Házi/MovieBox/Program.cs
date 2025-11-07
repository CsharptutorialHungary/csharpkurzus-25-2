using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MovieBox;
using MovieBox.Core.Repository;
using MovieBox.Core.Service;
using MovieBox.Infrastucture;
using MovieBox.Ui;
using System.Globalization;

// --- Setup ---
CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
Console.OutputEncoding = System.Text.Encoding.UTF8;

// Set up Dependency Injection
using var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
    {
        services.AddSingleton<IMovieService, MovieService>();
        services.AddTransient<IMovieFilterService, MovieFilterService>();

        services.AddSingleton<IMovieRepository, JsonMovieRepository>();

        services.AddTransient<IConsoleUIService, ConsoleUIService>();
        services.AddSingleton<MovieAppController>();
    })
    .Build();

var appController = host.Services.GetRequiredService<MovieAppController>();
await appController.Run();

Console.WriteLine("\nGoodbye!");