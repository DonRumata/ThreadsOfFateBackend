using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NLog;
using ThreadsofFate.Common.Const;
using ThreadsOfFate.Config;
using ThreadsOfFate.Filters;
using ThreadsOfFate.Options;

namespace ThreadsOfFate
{
    public class Startup
    {
        private IHostingEnvironment HostingEnvironment { get; }
        private IConfigurationRoot Configuration { get; }
        private StartupOptions StartupOptions { get; }
        private string AspNetCoreEnvironment { get; }
        private string AspNetCoreDomain { get; }

        /// <summary>
        /// Startup
        /// </summary>
        public Startup(IHostingEnvironment env)
        {
            HostingEnvironment = env;
            AspNetCoreEnvironment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "prod";
            AspNetCoreDomain = Environment.GetEnvironmentVariable("ASPNETCORE_DOMAIN");
            Configuration = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddEnvironmentVariables()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{AspNetCoreEnvironment}.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{AspNetCoreDomain}.{AspNetCoreEnvironment}.json", optional: true, reloadOnChange: true)
                .Build();

            LogManager.LoadConfiguration($"nlog.{AspNetCoreEnvironment}.config");
            StartupOptions = Configuration.GetSection(nameof(Options.StartupOptions)).Get<StartupOptions>();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        /// </summary>
        /// <param name="services"></param>
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            using (var loggerFactory = new LoggerFactory())
            {
                var logger = loggerFactory.CreateLogger<Startup>();

                try
                {
                    services.AddSingleton<IConfiguration>(Configuration);
                    services.AddMemoryCache();

                    ConfigureUsers(services);
                    AddCors(services);
                    ConfigureMvc(services);
                    ConfigureIISOptions(services);
                    services.AddSwaggerGen(SetUp.Swagger);
                    services.AddApiVersioning(SetUp.ApiVersioning);
                    services.AddOptions();
                    services.AddHttpContextAccessor();

                    var containerBuilder = new ContainerBuilder();
                    containerBuilder.Populate(services);
                    ModuleBootstrapper.Configure(Configuration, containerBuilder);

                    return new AutofacServiceProvider(containerBuilder.Build());
                }
                catch (Exception e)
                {
                    logger.LogError(e.ToString());
                    throw;
                }
            }
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IApplicationLifetime applicationLifetime)
        {
            UseCors(app);

            if (IsDevOrTestEnvironment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(SetUp.SwaggerUi);
            }

            app.UseMvc();
        }

        private void ConfigureUsers(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        private void ConfigureMvc(IServiceCollection services)
        {
            services
                .AddMvc(o => { o.Filters.Add(typeof(GlobalExceptionFilterAttribute)); })
                .AddJsonOptions(o =>
                {
                    o.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    o.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                });
        }

        // ReSharper disable once InconsistentNaming
        private void ConfigureIISOptions(IServiceCollection services)
        {
            if (!IsDevOrTestEnvironment())
                return;

            services.Configure<IISOptions>(options =>
            {
                options.AutomaticAuthentication = true;
            });
        }

        private void AddCors(IServiceCollection services)
        {
            if (StartupOptions.UseCors)
            {
                services.AddCors();
            }
        }

        private void UseCors(IApplicationBuilder app)
        {
            if (StartupOptions.UseCors)
            {
                app.UseCors(builder => builder
                    .WithOrigins(StartupOptions.Origins)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            }
        }

        private bool IsDevEnvironment()
        {
            return HostingEnvironment.IsEnvironment(AppSettingsConst.AspNetAppDevEnvirontment);
        }

        private bool IsTestEnvironment()
        {
            return HostingEnvironment.IsEnvironment(AppSettingsConst.AspNetAppTestEnvirontment);
        }

        private bool IsStableEnvironment()
        {
            return HostingEnvironment.IsEnvironment(AppSettingsConst.AspNetAppStableEnvirontment);
        }

        private bool IsDevOrTestEnvironment()
        {
            return IsDevEnvironment() || IsTestEnvironment() || IsStableEnvironment();
        }
    }
}
