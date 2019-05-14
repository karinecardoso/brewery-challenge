using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PragmaBrewery.API.Contracts.Containers;
using PragmaBrewery.API.Domain.Repositories;
using PragmaBrewery.API.Infrastructure.Data;
using PragmaBrewery.API.Infrastructure.Repositories;
using PragmaBrewery.API.Services;

namespace PragmaBrewery.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<PragmaBreweryDbContext>(options =>
            {
                options.UseInMemoryDatabase("pragma-brewery-api-in-memory");
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddScoped<IBeerRepository, BeerRepository>();
            services.AddScoped<IContainerRepository, ContainerRepository>();

            services.AddScoped<IContainerService, ContainerService>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
