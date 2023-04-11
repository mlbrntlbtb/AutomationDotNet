using Datacom.TestAutomation.Web.Selenium;
using OpenQA.Selenium;
using Tempo.TestAutomation.Model.Web.Locators.Modals;

namespace Tempo.TestAutomation.Model.Web.Components.Modals
{
    public class EditLocationModal : TempoBaseModal<EditLocationModal>, ILoadable<EditLocationModal>
    {
        private readonly IWebDriver driver;

        public EditLocationModal(IWebDriver driver)
            : base(driver)
        {
            this.driver = driver;
        }

        public void ClickCloseButton()
        {
            driver.GetElement(EditCustomerModalLocators.EditCustomerModalFrame.Button.Close).Click();
        }

        protected bool IsPresent()
        {
            bool IsOverlayPresent = driver.FindElements(By.CssSelector(".k-overlay")).Any();
            bool IsModalWindowPresent = driver.FindElements(By.CssSelector("*[class*='k-window']")).Any();

            return IsOverlayPresent && IsModalWindowPresent;
        }

        protected override bool EvaluateLoadedStatus()
        {
            bool IsKeyPresent = driver.IsElementPresent(EditLocationModalLocators.EditLocationModalFrame.TextBox.Key);
            bool IsAddressPresent = driver.IsElementPresent(EditLocationModalLocators.EditLocationModalFrame.TextBox.Address);
            bool IsCloseButtonPresent = driver.IsElementPresent(EditLocationModalLocators.EditLocationModalFrame.Button.Close);

            return IsKeyPresent && IsAddressPresent && IsCloseButtonPresent;
        }
    }
}