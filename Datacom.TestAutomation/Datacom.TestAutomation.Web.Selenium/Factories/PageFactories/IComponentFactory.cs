namespace Datacom.TestAutomation.Web.Selenium
{
    public interface IComponentFactory
    {
        public T GetComponent<T>();
    }
}
