using ChartsServer.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ChartsServer.Subscription;
using ChartsServer.Subscription.Middleware;
using ChartsServer.Hubs;

namespace ChartsServer
{
    public class Startup
    {       

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options => options.AddDefaultPolicy(policy => policy
            .AllowCredentials()
            .AllowAnyHeader()
            .AllowAnyMethod()
            .SetIsOriginAllowed(x => true)));
            services.AddSignalR();
            services.AddSingleton<DatabaseSubscription<Satislar>>();
            services.AddSingleton<DatabaseSubscription<Personeller>>();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseDatabaseSubscription<DatabaseSubscription<Satislar>>("Satislar");
            app.UseDatabaseSubscription<DatabaseSubscription<Personeller>>("Personeller");

            app.UseCors();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<SatisHub>("/satishub");
            });
        }
    }
}
