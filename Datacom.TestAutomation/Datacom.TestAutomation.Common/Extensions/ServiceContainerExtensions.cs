using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Datacom.TestAutomation.Common
{
    public static class ServiceContainerExtensions
    {
        public static IServiceCollection RegisterContainers(this IServiceCollection collection)
        {
            Assembly[] assemblies = AssemblyUtilities.GetSolutionAssemblies();
            IEnumerable<Type> containers = from assembly in AssemblyUtilities.GetSolutionAssemblies()
                                           from type in assembly.GetTypes()
                                           where type.IsClass
                                           where typeof(IServiceContainer).IsAssignableFrom(type)
                                           select type;

            foreach (var container in containers)
            {
                (Activator.CreateInstance(container) as IServiceContainer)!.Register(collection);
            }

            return collection;
        }
        public static IServiceCollection UseTestSettings<T>(this IServiceCollection container, string section = "TestSettings") where T : class, new()
        {
            IConfigurationRoot config = TestSettingsService.Instance.Root;
            T settings = TestSettingsService.Instance.Load<T>(section);

            return container
                .AddSingleton(config)
                .AddSingleton(settings);
        }
    }
}