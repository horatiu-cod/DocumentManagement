using DocumentManagement.Application.Interfaces;
using DocumentManagement.Infrastructure.DataAccess.DocumentManagementContext;
using DocumentManagement.Infrastructure.DataAccess.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DocumentManagement.Infrastructure
{
    public static class ServiceExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration, string connection)
        {
            ArgumentNullException.ThrowIfNull(connection);

            services.AddDbContext<DocumentManagementDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString(connection));
            });
            services.AddScoped<IDocumentRepository, DocumentRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<ISignatureRepository, SignatureRepository>();
        }
    }
}
