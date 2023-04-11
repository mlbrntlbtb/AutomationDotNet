namespace Datacom.TestAutomation.Web.Selenium
{
    public interface IFactory<out T>
    {
        T Create();
    }
}
