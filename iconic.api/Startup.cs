using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using iconic.common.Services;
using iconic.common.Services.CorporateBuzzWords;
using iconic.common;
using iconic.common.Services.CopyCat;
using iconic.telegram;
using iconic.whatsapp;
using iconic.slack;
using featureprovider.core.Registry;

namespace iconic.api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddScoped<ICustomService, BuzzWordGenerator>();
            services.AddScoped<ICustomService, CopyCatRepeater>();
            services.AddScoped<IMessageProcessor, IncomingMessageProcessor>();
            services.AddHttpClient<TelegramService>();
            services.AddHttpClient<SlackService>();
            services.AddScoped<WhatsAppService>();
            
            // Feature Provider Library initial setup            
            services.AddFeatureProvider(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
                app.UseHttpsRedirection();
            }


            app.UseMvc();
        }
    }
}
