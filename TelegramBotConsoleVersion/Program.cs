using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TelegramBotConsoleVersion.Services;



using IHost host = CreateHostBuilder(args).Build();

_ = host.Services.GetService<MyCustomTelegramBot>();
await StartBot(host.Services);



await host.RunAsync();

static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration(config =>
            config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true))
    .ConfigureServices((_, services) =>
            services.AddSingleton<ICosmosDBSetter, CosmosDBSetter>()
                    .AddSingleton<MyCustomTelegramBot>());



static async Task StartBot(IServiceProvider services)
{
    using IServiceScope serviceScope = services.CreateScope();
    IServiceProvider provider = serviceScope.ServiceProvider;
    MyCustomTelegramBot bot = provider.GetRequiredService<MyCustomTelegramBot>();
    await bot.Run();
}



