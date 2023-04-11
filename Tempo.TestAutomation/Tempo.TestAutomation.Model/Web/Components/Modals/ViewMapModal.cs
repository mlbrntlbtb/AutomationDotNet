using Datacom.TestAutomation.Web.Selenium;
using OpenQA.Selenium;
using Tempo.TestAutomation.Model.Web.Components.Object;
using Tempo.TestAutomation.Model.Web.Components.PageContainers;
using Tempo.TestAutomation.Model.Web.Locators.Modals;

namespace Tempo.TestAutomation.Model.Web.Components.Modals
{
    public class ViewMapModal : TempoBasePage<ViewMapModal>, ILoadable<ViewMapModal>
    {
        private readonly IWebDriver driver;
        private readonly DropDownListContainer dropDownList;
        private readonly LoadingWheel loadingWheel;

        public ViewMapModal(IWebDriver driver, DropDownListContainer dropDownList, LoadingWheel loadingWheel)
            : base(driver)
        {
            this.driver = driver;
            this.dropDownList = dropDownList;
            this.loadingWheel = loadingWheel;

        }

        protected override bool EvaluateLoadedStatus()
        {
            loadingWheel.WaitToDisappear();
            driver.WaitAvailability(ViewMapModalLocators.Header.Map);
            bool IsMapHeaderPresent = driver.IsElementPresent(ViewMapModalLocators.Header.Map);
            return IsMapHeaderPresent;
            
        }

        public void ClickCloseButton()
        {
            loadingWheel.WaitToDisappear();
            driver.WaitAvailability(ViewMapModalLocators.Button.Close, 30);
            driver.GetElement(ViewMapModalLocators.Button.Close).Click();
        }
    }
}