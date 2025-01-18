using System;
using System.Linq;
using System.Reflection;

using Microsoft.Extensions.DependencyInjection;

namespace ATech.Repository;

public static class RepositoryRegistration
{
    public static IServiceCollection AddRepositories(this IServiceCollection services, Assembly assembly)
    {
        ArgumentNullException.ThrowIfNull(assembly, nameof(assembly));

        // Find all classes that implement IRepository<,> (directly or through descendant interfaces)
        var repositoryTypes = assembly.GetTypes()
            .Where(t => !t.IsInterface && !t.IsAbstract) // Non-interface and non-abstract
            .SelectMany(t => t.GetInterfaces(), (type, i) => new { Type = type, Interface = i })
            .Where(t => IsAssignableToGenericType(t.Interface, typeof(IRepository<,>)));

        // Register each found implementation as a scoped service
        foreach (var repository in repositoryTypes)
        {
            services.AddScoped(repository.Interface, repository.Type);
        }
        return services;
    }

    // Helper method to check if a given interface is assignable to a generic type
    private static bool IsAssignableToGenericType(Type givenType, Type genericType)
    {
        if (!genericType.IsGenericType)
        {
            return false;
        }

        // Check direct assignment
        if (givenType.IsGenericType && givenType.GetGenericTypeDefinition() == genericType)
        {
            return true;
        }

        // Check all implemented interfaces
        return givenType.GetInterfaces()
            .Any(it => it.IsGenericType && it.GetGenericTypeDefinition() == genericType);
    }
}