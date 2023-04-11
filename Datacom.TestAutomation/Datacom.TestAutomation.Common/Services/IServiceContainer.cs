using Microsoft.Extensions.DependencyInjection;

namespace Datacom.TestAutomation.Common
{
    public interface IServiceContainer
    {
        void Register(IServiceCollection collection);
    }
}