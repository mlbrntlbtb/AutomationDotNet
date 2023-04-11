using Microsoft.Extensions.DependencyInjection;

namespace Datacom.TestAutomation.Common
{
    public static class ServiceRegistry
    {
        public static IServiceProvider Register()
        {
            var collection = new ServiceCollection();
            collection.RegisterContainers();

            return collection.BuildServiceProvider(validateScopes: true);
        }
    }
}