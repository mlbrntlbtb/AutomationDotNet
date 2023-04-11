using OpenQA.Selenium;

namespace Tempo.TestAutomation.Model.Interface
{
    public interface IPageElementContainer
    {
        public IWebElement GetElementByText(string referenceText);
    }
}