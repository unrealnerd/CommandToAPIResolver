using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using bitsmonkey.common.Services;
using bitsmonkey.common.Services.CorporateBuzzWords;
using bitsmonkey.common;
using bitsmonkey.common.Services.CopyCat;
using bitsmonkey.telegram;
using bitsmonkey.whatsapp;
using bitsmonkey.slack;
using featureprovider.core.Registry;
using bitsmonkey.common.Search;
using Microsoft.Extensions.Hosting;

namespace bitsmonkey.api
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
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
            services.AddControllers();
            services.AddScoped<ICustomService, BuzzWordGenerator>();
            services.AddScoped<ICustomService, RandomDogGenerator>();
            services.AddScoped<ICustomService, CopyCatRepeater>();
            services.AddScoped<ICustomService, OpenDotaRetriever>();
            services.AddScoped<RestExecutioner>();
            services.AddScoped<IMessageProcessor, IncomingMessageProcessor>();
            services.AddHttpClient<TelegramService>();
            services.AddHttpClient<SlackService>();
            services.AddScoped<WhatsAppService>();

            // Feature Provider Library initial setup            
            services.AddFeatureProvider(Configuration);

            //
            // services.AddOptions();
            // services.Configure<ServicesSettings>(options => Configuration.GetSection("ServicesSettings").Bind(options));

            var servicesSettings = Configuration.GetSection("ServicesSettings").Get<ServicesSettings>();//Configuration.GetSection("Services").Get<Service[]>();
            ServiceMapper.Initialize(servicesSettings);
            // services.AddSingleton<ServicesSettings>(servicesSettings);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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

            app.UseCors("CorsPolicy");
            app.UseRouting();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
