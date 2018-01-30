using Api.Controllers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.EntityFrameworkCore;

namespace Api
{
    public static class ApiConfiguration
    {

        public static IServiceCollection ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddApiVersioning(setup =>
            {
                setup.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
                setup.AssumeDefaultVersionWhenUnspecified = true;
            }).AddDbContext<FooContext>(options =>
            {
                options.UseSqlServer("Server=.;Initial Catalog=FooDatabase;Integrated Security=true", setup =>
                 {
                     setup.MaxBatchSize(10);
                     setup.MigrationsAssembly("HostApi");
                 });
            }).AddAuthorization(setup =>
            {
                setup.AddPolicy("mypolicy", builder =>
                 {
                     builder.RequireClaim("CustomClaim");
                 });
            });

            return services;
        }

        public static void Configure(IApplicationBuilder app)
        {
        }
    }
}
