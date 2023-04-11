using Datacom.TestAutomation.Web.Selenium;
using OpenQA.Selenium;
using Tempo.TestAutomation.Model.Web.Components.Object;
using Tempo.TestAutomation.Model.Web.Components.PageContainers;
using Tempo.TestAutomation.Model.Web.Locators.Modals;

namespace Tempo.TestAutomation.Model.Web.Components.Modals
{
    public class AddNewCustomerModal : TempoBaseModal<AddNewCustomerModal>, ILoadable<AddNewCustomerModal>
    {
        private readonly IWebDriver driver;

        public AddNewCustomerModal(IWebDriver driver)
            : base(driver)
        {
            this.driver = driver;
        }

        public void ClickCloseButton()
        {
            driver.GetElement(AddNewCustomerModalLocators.AddNewCustomerModalFrame.Button.Close).Click();
        }

        protected override bool EvaluateLoadedStatus()
        {
            bool IsAddCustomerTabPresent = driver.IsElementPresent(AddNewCustomerModalLocators.AddNewCustomerModalFrame.Tab.AddCustomerTab);
            bool IsCloseButtonPresent = driver.IsElementPresent(AddNewCustomerModalLocators.AddNewCustomerModalFrame.Button.Close);
            bool IsSaveButtonPresent = driver.IsElementPresent(AddNewCustomerModalLocators.AddNewCustomerModalFrame.Button.Save);
            bool IsShortNamePresent = driver.IsElementPresent(AddNewCustomerModalLocators.AddNewCustomerModalFrame.TextBox.ShortName);

            return IsAddCustomerTabPresent && IsCloseButtonPresent && IsSaveButtonPresent && IsShortNamePresent;
        }
    }
}