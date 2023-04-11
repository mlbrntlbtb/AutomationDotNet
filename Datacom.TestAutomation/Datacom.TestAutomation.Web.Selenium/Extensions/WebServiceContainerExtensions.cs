using Datacom.TestAutomation.Common;
using Microsoft.Extensions.DependencyInjection;

namespace Datacom.TestAutomation.Web.Selenium
{
    public static class WebServiceContainerExtensions
    {
        public static IServiceCollection AddLoadableComponents(this IServiceCollection collection)
        {

            var types = from assembly in AssemblyUtilities.GetSolutionAssemblies()
                        from type in assembly.GetTypes()
                        select type;

            var registrations = from type in types
                                where !type.IsAbstract
                                where !type.IsGenericTypeDefinition
                                let services =
                                    from iface in type.GetInterfaces()
                                    where iface.IsGenericType
                                    where iface.GetGenericTypeDefinition() == typeof(ILoadable<>)
                                    select iface
                                from service in services
                                select new { service, type };

            foreach (var registration in registrations)
            {
                collection.AddScoped(registration.service, registration.type);
            }

            return collection;
        }

        public static IServiceCollection AddWebDriverFactories(this IServiceCollection collection)
        {
   
            var factories = from assembly in AssemblyUtilities.GetSolutionAssemblies()
                            from type in assembly.GetTypes()
                            where type.IsClass
                            where typeof(IWebDriverFactory).IsAssignableFrom(type)
                            select type;

            foreach (var factory in factories)
            {
                collection.AddScoped(typeof(IWebDriverFactory), factory);
            }

            return collection;
        }
    }
}