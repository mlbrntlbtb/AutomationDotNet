using Datacom.TestAutomation.Common;
using OpenQA.Selenium.Support.UI;

namespace Datacom.TestAutomation.Web.Selenium
{
    public abstract class Loadable<T> : LoadableComponent<T>, ILoadable<T>, ILoadableComponent
        where T : LoadableComponent<T>
    {
        public override T Load() => TryLoad();
        public new bool IsLoaded => base.IsLoaded;
        public virtual bool Wait(TimeSpan timeout) => WaitUtilities.Until(() => base.IsLoaded, timeout);
    }
}
