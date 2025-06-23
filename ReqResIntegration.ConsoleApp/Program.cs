using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using ReqResIntegration.Core.Configuration;
using ReqResIntegration.Core.Services;
using ReqResIntegration.Core.Interfaces;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration(config =>
    {
        config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
    })
    .ConfigureServices((context, services) =>
    {
        // Bind options
        services.Configure<ReqResApiOptions>(
            context.Configuration.GetSection("ReqResApi"));

        // Register HTTP Client
        services.AddHttpClient<IExternalUserService, ExternalUserService>((sp, client) =>
        {
            var options = sp.GetRequiredService<Microsoft.Extensions.Options.IOptions<ReqResApiOptions>>().Value;
            client.BaseAddress = new Uri(options.BaseUrl);
        });
    })
    .Build();

// Resolve and use service
var userService = host.Services.GetRequiredService<IExternalUserService>();
var users = await userService.GetAllUsersAsync();

foreach (var user in users)
{
    Console.WriteLine($"{user.Id} - {user.First_Name} {user.Last_Name}");
}
