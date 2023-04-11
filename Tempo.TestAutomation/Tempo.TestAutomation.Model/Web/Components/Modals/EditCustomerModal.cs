using Datacom.TestAutomation.Web.Selenium;
using OpenQA.Selenium;
using Tempo.TestAutomation.Model.Web.Components.Object;
using Tempo.TestAutomation.Model.Web.Components.PageContainers;
using Tempo.TestAutomation.Model.Web.Locators.Modals;

namespace Tempo.TestAutomation.Model.Web.Components.Modals
{
    public class EditCustomerModal : TempoBaseModal<EditCustomerModal>, ILoadable<EditCustomerModal>
    {
        private readonly IWebDriver driver;

        public EditCustomerModal(IWebDriver driver)
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
            bool IsEditCustomerTabPresent = driver.IsElementPresent(EditCustomerModalLocators.EditCustomerModalFrame.Tab.EditCustomerTab);
            bool IsCloseButtonPresent = driver.IsElementPresent(EditCustomerModalLocators.EditCustomerModalFrame.Button.Close);
            bool IsSaveButtonPresent = driver.IsElementPresent(EditCustomerModalLocators.EditCustomerModalFrame.Button.Save);
            bool IsShortNamePresent = driver.IsElementPresent(EditCustomerModalLocators.EditCustomerModalFrame.TextBox.ShortName);

            return IsEditCustomerTabPresent && IsCloseButtonPresent && IsSaveButtonPresent && IsShortNamePresent;
        }
    }
}