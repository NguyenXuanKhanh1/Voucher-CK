using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.OpenApi.Models;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using VoucherCK.Api.IoC.NativeInjector;
using VoucherCK.Api.Middlewares;
using VoucherCK.Application;
using VoucherCK.SharedKernel.Helpers;
using Microsoft.EntityFrameworkCore;


namespace VoucherCK.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        private IConfiguration Configuration { get; }

        private IHostEnvironment Environment { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var corsOrigins = Configuration.GetSection("CorsOrigins").Get<string[]>();
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.SetIsOriginAllowedToAllowWildcardSubdomains()
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .WithOrigins(corsOrigins);
                    });
            });

            var abc = Configuration.GetSection("ConnectionStrings").Get<string>();
            services.AddDbContext<CKContext>(options => options.UseNpgsql(Configuration.GetSection("ConnectionStrings").Get<string>()));

            services.AddControllers(options =>
                options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new DateTimeConverter());
            });


            NativeInjector.Register(services, Configuration, Environment);

            bool enableSwagger =
                "true".Equals(Configuration["EnableSwagger"], StringComparison.InvariantCultureIgnoreCase);
            if (enableSwagger)
            {
                services.AddSwaggerGen(options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Title = "Voucher Promotion API(s)",
                        Version = "v1",
                        Description = "Voucher Promotion Services"
                    });
                    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        Description = "JWT Authorization header: Bearer",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = "Bearer"
                    });
                    options.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                },
                                Scheme = "oauth2",
                                Name = "Bearer",
                                In = ParameterLocation.Header
                            },
                            new List<string>()
                        }
                    });
                });
            }

            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders =
                    ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });

            services.AddHealthChecks();

        }

        public void Configure(IApplicationBuilder app, CKContext db)
        {
            var pathBase = Configuration["PathBase"];
            if (!string.IsNullOrEmpty(pathBase))
            {
                app.UsePathBase(pathBase);
            }

            db.Database.EnsureCreated();

            app.UseForwardedHeaders();
            app.UseCors();

            app.UseStaticFiles();
            app.UseRouting();

            bool enableSwagger =
                "true".Equals(Configuration["EnableSwagger"], StringComparison.InvariantCultureIgnoreCase);
            if (enableSwagger)
            {
                app.UseSwagger(c =>
                {
                    if (!string.IsNullOrEmpty(pathBase))
                    {
                        c.PreSerializeFilters.Add((swagger, httpReq) =>
                        {
                            if (httpReq.Headers.ContainsKey("X-Forwarded-Host"))
                            {
                                var serverUrl = $"https://{httpReq.Headers["X-Forwarded-Host"]}{pathBase}";
                                swagger.Servers = new List<OpenApiServer> { new OpenApiServer { Url = serverUrl } };
                            }
                        });
                    }
                });
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint($"v1/swagger.json", "ChargeHub Integration Services");
                });
            }

            app.UseMiddleware<ExceptionHandlerMiddleware>();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
            });
        }
    }

    public class DateTimeConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DateTimeHelper.ConvertToTimeZoneInfo(reader.GetDateTime().ToUniversalTime());
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(DateTimeHelper.ConvertFromTimeZoneInfo(value)
                .ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss.fffZ"));
        }
    }
}
