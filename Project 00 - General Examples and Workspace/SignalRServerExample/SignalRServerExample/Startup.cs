using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SignalRServerExample.Business;
using SignalRServerExample.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRServerExample
{
    public class Startup
    {
       
        public void ConfigureServices(IServiceCollection services)
        {
            //artık bu uygulamaya gelecek olan bütün isteklerin aşağıdaki şartlara uygun olmasını istemiş olduk.
            //Default CORS'umuzu belirttik.
            services.AddCors(options => options.AddDefaultPolicy(policy =>
                             policy.AllowAnyMethod()
                                   .AllowAnyHeader()
                                   .AllowCredentials()
                                   .SetIsOriginAllowed(origin => true)
            ));

            //artık mimari tarafından MyBusiness, ctor'unda enjekte edilen IHubContext .NET mimarisindeki
            //SignalR kütüphanesinden alınacak ve nesnesi üretilip gönderilecektir. Yani bu sınıfı talep ettiğimiz
            //noktalarda ilgili nesne gelmiş ve direkt WebSocket çalışmalarında dahil edilmiş olacaktır.
            services.AddTransient<MyBusiness>();

            services.AddSignalR();
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors(); //route'dan önce bir kontrol etsin.
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                //https://localhost:5001/myhub
                endpoints.MapHub<MyHub>("/myhub");
                endpoints.MapHub<MessageHub>("/messagehub");

                endpoints.MapControllers();
            });
        }
    }
}
