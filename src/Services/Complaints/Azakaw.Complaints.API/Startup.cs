using System.IO;
using System.Reflection;
using Azakaw.Complaints.API.Application;
using Azakaw.Complaints.API.Swagger;
using Azakaw.Complaints.Infrastructure;
using Code.Library.AspNetCore;
using Code.Library.AspNetCore.Extensions;
using Code.Library.AspNetCore.Middleware;
using Code.Library.AspNetCore.Middleware.RequestResponseLogging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Azakaw.Complaints.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseRequestResponseLogging()
                .UseApiExceptionHandler();

            app.UseAuthorization();

            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(
                options =>
                {
                    // build a swagger endpoint for each discovered API version
                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                    }
                });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapDefaultHealthChecks();
            });
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services
                .AddApiExceptionHandler()
                .AddAppInsight(Configuration, "Complaints API");

            services.AddApiVersioning(
                options =>
                {
                    // reporting api versions will return the headers "api-supported-versions" and "api-deprecated-versions"
                    options.ReportApiVersions = true;
                });

            services.AddVersionedApiExplorer(
                options =>
                {
                    // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
                    // note: the specified format code will format the version as "'v'major[.minor][-status]"
                    options.GroupNameFormat = "'v'VVV";

                    // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
                    // can also be used to control the format of the API version in route templates
                    options.SubstituteApiVersionInUrl = true;
                });

            services
                .AddCustomConfiguration(Configuration)
                .AddCustomHealthCheck(Configuration)
                .AddCustomSwaggerConfiguration()
                .AddCustomAuthentication(Configuration);

            services
                .AddApplication()
                .AddInfrastructure();
        }
    }

    internal static class CustomExtensionMethods
    {
        private static string XmlCommentsFilePath
        {
            get
            {
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var fileName = typeof(Startup).GetTypeInfo().Assembly.GetName().Name + ".xml";
                return Path.Combine(basePath, fileName);
            }
        }

        public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            // We can make use of identityserver, ASP.NET Core Identity or any other customer authentication handler to manage user authentication and authorization.
            // I have an article on the same here https://www.abhith.net/blog/aspnet-core-using-multiple-authentication-schemes/

            // A sample identityserver setup given below

            //var identityUrl = configuration.GetValue<string>("Authentication:Sts:BaseUrl");

            //services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //}).AddJwtBearer(options =>
            //{
            //    options.Authority = identityUrl;
            //    options.RequireHttpsMetadata = false;
            //    options.Audience = "complaints_api";
            //});

            //// Authorization
            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy(Application.Authorization.Policies.ComplaintsRead, policy =>
            //    {
            //        policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
            //        policy.RequireClaim("scope", Application.Authorization.Policies.ComplaintsRead);
            //    });

            //    options.AddPolicy(Application.Authorization.Policies.ComplaintsWrite, policy =>
            //    {
            //        policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
            //        policy.RequireClaim("scope", Application.Authorization.Policies.ComplaintsWrite);
            //    });

            //});

            return services;
        }

        public static IServiceCollection AddCustomConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions();
            services.Configure<ComplaintsAppSettings>(configuration);

            return services;
        }

        public static IServiceCollection AddCustomHealthCheck(this IServiceCollection services, IConfiguration configuration)
        {
            var hcBuilder = services.AddHealthChecks();

            hcBuilder
                .AddCheck("self", () => HealthCheckResult.Healthy())
                //.AddSqlServer(
                //        configuration["ConnectionStrings:Default"],
                //        name: "ComplaintsDB-check",
                //        tags: new string[] { "complaintsdb" })
                ;

            return services;
        }

        public static IServiceCollection AddCustomSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(
                options =>
                {
                    // add a custom operation filter which sets default values
                    options.OperationFilter<SwaggerDefaultValues>();

                    // integrate xml comments
                    options.IncludeXmlComments(XmlCommentsFilePath);
                });

            return services;
        }
    }
}