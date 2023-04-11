namespace Datacom.TestAutomation.Common
{
    public interface IFactory<out T>
    {
        T Create();
    }
}
