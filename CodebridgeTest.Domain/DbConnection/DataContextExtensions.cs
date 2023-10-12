using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CodebridgeTest.Domain.DbConnection;

public static class DataContextExtensions
{
    public static IServiceCollection AddDbContextsCustom(this IServiceCollection services, IConfiguration builder)
    {
        services.AddDbContext<DataContext>(
            o => o.UseSqlServer(builder.GetConnectionString("DefaultConnection")));
        return services;
    }
}
