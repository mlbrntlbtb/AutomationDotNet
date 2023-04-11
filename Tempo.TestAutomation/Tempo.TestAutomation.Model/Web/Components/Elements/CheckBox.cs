using OpenQA.Selenium;

namespace Tempo.TestAutomation.Model.Web.Components.Elements
{
    public class CheckBox
    {
        protected IWebDriver driver;
        protected IWebElement parentElement;

        public CheckBox(IWebElement parentElement, IWebDriver driver)
        {
            this.driver = driver;
            this.parentElement = parentElement;
        }

        public void SetCheckBoxState(bool? state)
        {
            if (GetCheckBoxState() != state)
                parentElement.Click();
        }

        private bool GetCheckBoxState()
        {
            return parentElement.Selected;
        }
    }
}