

using Application.UnitOfWork;
using AspNetCoreRateLimit;
using Domain.Interfaces;

namespace ApiFarmacia.Extencions;
public static  class ApplicationServiceExtension
{
    public static void ConfigureCors(this IServiceCollection services) =>
   services.AddCors(options =>
   {
       options.AddPolicy("CorsPolicy", builder =>
           builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader());
   });

    public static void AddApplicationServices(this IServiceCollection services)
    {
        //services.AddScoped<IPais, PaisRepository>();
        //servies.AddScoped<ITipoPersona, TipoPersonaRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }

    public static void ConfigureRateLimit(this IServiceCollection services)
    {
        services.AddMemoryCache();
        services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
        services.AddInMemoryRateLimiting();
        services.Configure<IpRateLimitOptions>(options =>
        {
            options.EnableEndpointRateLimiting = true;
            options.StackBlockedRequests = false;
            options.HttpStatusCode = 429;
            options.RealIpHeader = "X-Real-IP";
            options.GeneralRules = new List<RateLimitRule>
            {
                new RateLimitRule
                {
                    Endpoint = "*",
                    Period = "10s",
                    Limit = 5
                }
            };
        });
    }
}
