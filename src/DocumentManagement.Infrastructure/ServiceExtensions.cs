using DocumentManagement.Application.Interfaces;
using DocumentManagement.Infrastructure.DataAccess.Repository;
using DocumentManagement.Infrastructure.DataAccess.SignaturesContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DocumentManagement.Infrastructure;

public static class ServiceExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration, string connection)
    {
        ArgumentNullException.ThrowIfNull(connection);

        services.AddDbContext<SignatureDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString(connection));
        });
        services.AddScoped<IDocumentRepository, DocumentRepository>();
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<ISignatureRepository, SignatureRepository>();
    }
}
