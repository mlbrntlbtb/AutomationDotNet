namespace Datacom.TestAutomation.Web.Selenium
{
    public interface ILoadable<out T>
    {
        public T Load();
    }
}
