using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TelegramBotConsoleVersion.Services;

//using IHost host = Host.CreateDefaultBuilder(args)
//    .ConfigureAppConfiguration(config =>
//    config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true))
//    .ConfigureServices(services =>
//    services.AddSingleton<ICosmosDBSetter, CosmosDBSetter>()
//            .AddSingleton<IMyCustomTelegramBot, MyCustomTelegramBot>()
//).Build();
// await host.RunAsync();


using IHost host = CreateHostBuilder(args).Build();

_ = host.Services.GetService<MyCustomTelegramBot>();

await host.RunAsync();

static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration(config =>
            config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true))
    .ConfigureServices((_, services) =>
            services.AddSingleton<ICosmosDBSetter, CosmosDBSetter>()
                    .AddSingleton<MyCustomTelegramBot>());
