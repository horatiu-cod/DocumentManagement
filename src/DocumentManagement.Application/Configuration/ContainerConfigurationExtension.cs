using DocumentManagement.Domain.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DocumentManagement.Application.Configuration;

public static class ContainerConfigurationExtension
{
    public static void AddCore(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.Configure<FilesOptions>(configuration.GetSection(FilesOptions.Files));
        //serviceCollection
            //.Bind(configuration.GetSection(FilesOptions.Files))
            //.ValidateDataAnnotations()
            //.ValidateOnStart();

    }
}
