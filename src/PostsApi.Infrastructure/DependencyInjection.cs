using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PostsApi.Application.Common.Interfaces;
using PostsApi.Infrastructure.Identity;
using PostsApi.Infrastructure.Interceptors;
using PostsApi.Infrastructure.Repositories;
using PostsApi.Persistence.Services;


namespace PostsApi.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddHangfire(x =>
            x.UsePostgreSqlStorage(options =>
                options.UseNpgsqlConnection(configuration.GetConnectionString("DefaultConnection"))));

        services.AddHangfireServer();
        
        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();

        services.AddTransient<IIdentityService, IdentityService>();
        services.AddTransient<IAdminService, AdminService>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddTransient<ISlugService, SlugService>();
        
        services.AddTransient<IPostService, PostService>();
        services.AddTransient<IBackgroundJobProcessor, BackgroundJobProcessor>();


        return services;
    }
}
