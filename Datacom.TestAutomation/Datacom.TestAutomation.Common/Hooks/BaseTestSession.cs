using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Datacom.TestAutomation.Common
{
    public abstract class TestSession<T> where T : TestSession<T>
    {
        protected ILogger<T>? Logger;
        protected IServiceProvider? Services = null!;
        protected IServiceScope? Scope = null!;

        public abstract string GetTestName();
        public abstract object GetContext();
        public abstract object GetReporter();

        public TService Resolve<TService>() where TService : notnull
        {
            if (Services == null)
            {
                throw new InvalidOperationException("The session is not started");
            }

            return Services.GetRequiredService<TService>();
        }

        public TSettings GetSettings<TSettings>(string section = "AppSettings") where TSettings : class, new()
        {
            return TestSettingsService.Instance.Load<TSettings>(section);
        }

        public virtual void Start()
        {
            IServiceProvider service = ServiceRegistry.Register();
            Scope = service.CreateScope();
            Services = Scope.ServiceProvider;
            Logger = Services.GetRequiredService<ILogger<T>>();
            Logger!.LogDebug("Start test session {name}", GetTestName());
        }

        public virtual void Stop()
        {
            Scope!.Dispose();
            Scope = null;
            Services = null;
            Logger!.LogDebug("Stop test session {name}", GetTestName());
        }

        public virtual void Reset()
        {
            Logger!.LogInformation("Reset test session {name}", GetTestName());
            Stop();
            Start();
        }
    }
}
