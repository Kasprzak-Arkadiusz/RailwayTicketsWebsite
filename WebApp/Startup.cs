using Application;
using Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using Microsoft.Net.Http.Headers;
using MimeKit;

namespace WebApp
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
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApp", Version = "v1" });
            });

            services.AddHttpClient("api", c =>
            {
                c.BaseAddress = new Uri(Configuration.GetValue<string>("ApiUrl"));
                c.DefaultRequestHeaders.Add(
                    HeaderNames.Accept, "*/*");
                c.DefaultRequestHeaders.Add("Connection", "Keep-Alive");
                c.DefaultRequestHeaders.Add("Keep-Alive", "3600");
            });

            services.AddRazorPages().AddRazorRuntimeCompilation();;
            services.AddMvc().AddRazorPagesOptions(opt =>
            {
                opt.RootDirectory = "/Frontend/Pages";
            });

            services.AddAuthentication()
                .AddGoogle(options =>
                {
                    var googleAuthNSection = Configuration.GetSection("GoogleAuthentication");
                    options.ClientId = googleAuthNSection["ClientId"];
                    options.ClientSecret = googleAuthNSection["ClientSecret"];
                })
                .AddMicrosoftAccount(options =>
                {
                    var microsoftSection = Configuration.GetSection("MicrosoftAuthentication");
                    options.ClientId = microsoftSection["ClientId"];
                    options.ClientSecret = microsoftSection["ClientSecret"];
                });

            services.AddAutoMapper(typeof(Startup));

            services.AddApplication();
            services.AddInfrastructure(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApp v1"));
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapRazorPages();
                endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Frontend/Pages}/{action=Index}/{id?}");
            });
        }
    }
}