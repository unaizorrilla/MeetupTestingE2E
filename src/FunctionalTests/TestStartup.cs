using Acheve.AspNetCore.TestHost.Security;
using Acheve.TestHost;
using Api;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace FunctionalTests
{
    public class TestStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            ApiConfiguration.ConfigureServices(services)
                .AddAuthentication(TestServerAuthenticationDefaults.AuthenticationScheme)
                .AddTestServerAuthentication();
        }

        public void Configure(IApplicationBuilder app)
        {
            ApiConfiguration.Configure(app);

            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
