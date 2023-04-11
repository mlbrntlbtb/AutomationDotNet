using Datacom.TestAutomation.Web.Selenium;
using OpenQA.Selenium;
using Tempo.TestAutomation.Model.Web.Components.PageContainers;

namespace Tempo.TestAutomation.Model.Web.Components.Modals
{
    public class AddNewRecurringJobModal : TempoBasePage<AddNewRecurringJobModal>, ILoadable<AddNewRecurringJobModal>
    {
        private readonly IWebDriver driver;
        private readonly DropDownListContainer dropDownList;

        public AddNewRecurringJobModal(IWebDriver driver, DropDownListContainer dropDownList)
            : base(driver)
        {
            this.driver = driver;
            this.dropDownList = dropDownList;
        }

        protected override bool EvaluateLoadedStatus()
        {
            return false;
        }
    }
}