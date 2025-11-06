using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Services;
using TestMicrosoftPing;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration(config =>
    {
        config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
    })
    .ConfigureServices((context, services) =>
    {
        services.Configure<PingSettings>(context.Configuration.GetSection("PingSettings"));
        
        var webHookUrl = context.Configuration["WebHookUrl"];
        if (string.IsNullOrEmpty(webHookUrl))
        {
            throw new ArgumentException("WebHookUrl is not configured in appsettings.json");
        }

        services.AddSingleton(new MicrosoftPingService(webHookUrl));
    })
    .Build();

var pingService = host.Services.GetRequiredService<MicrosoftPingService>();

var ret = await pingService.Ping("ping ivan");
