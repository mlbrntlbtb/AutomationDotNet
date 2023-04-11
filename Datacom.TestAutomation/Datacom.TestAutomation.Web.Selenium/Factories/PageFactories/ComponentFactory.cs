using Datacom.TestAutomation.Common;
using Microsoft.Extensions.DependencyInjection;

namespace Datacom.TestAutomation.Web.Selenium
{
    public class ComponentFactory : IComponentFactory
    {
        private readonly IServiceProvider serviceProvider;

        public ComponentFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public T GetComponent<T>()
        {
            var page = serviceProvider.GetService<ILoadable<T>>();

            if (page is null)
            {
                throw new ServiceNotRegisteredException($"No page registered for {typeof(T)}");
            }

            return (T)page;
        }
    }
}
