using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using WebApi.DependencyInjections;

namespace IntegratedTests.Shared
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseContentRoot(".");

            builder.UseEnvironment("Development");

            builder.ConfigureAppConfiguration((context, configBuilder) =>
            {
                configBuilder.Sources.Clear();

                configBuilder.AddJsonFile("appsettings.Integrated.json", optional: false)
                             .AddEnvironmentVariables();
            });

            builder.ConfigureServices((webbuilder, services) =>
            {
                services.AddInfra(webbuilder.Configuration).AddApplication();
            });

            base.ConfigureWebHost(builder);
        }
    }
}
