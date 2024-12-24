using System.Linq;
using System.Reflection;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace ATech.Repository;

public static class RepositoryRegistration
{
    public static IServiceCollection AddRepositories(this IServiceCollection services, Assembly assembly)
    {
        // Add your repository registration logic here
        ServiceDescriptor[] serviceDescriptors = assembly
            .DefinedTypes
            .Where(type => type is { IsAbstract: false, IsInterface: false } && type.IsAssignableTo(typeof(IRepository<,>)))
            .Select(type => 
            {
                var entityType = type.GetInterfaces()
                    .First(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRepository<,>))
                    .GetGenericArguments()[0];
                var idType = type.GetInterfaces()
                    .First(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRepository<,>))
                    .GetGenericArguments()[1];
                return ServiceDescriptor.Transient(typeof(IRepository<,>).MakeGenericType(entityType, idType), type);
            })
            .ToArray();

        services.TryAddEnumerable(serviceDescriptors);

        return services;
    }
}
