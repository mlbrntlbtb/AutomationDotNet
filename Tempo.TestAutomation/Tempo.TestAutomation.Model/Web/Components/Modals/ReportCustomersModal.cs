using Datacom.TestAutomation.Web.Selenium;
using OpenQA.Selenium;
using Tempo.TestAutomation.Model.Web.Components.PageContainers;

namespace Tempo.TestAutomation.Model.Web.Components.Modals
{
    public class ReportCustomersModal : TempoBasePage<ReportCustomersModal>, ILoadable<ReportCustomersModal>
    {
        private readonly IWebDriver driver;
        private readonly DropDownListContainer dropDownList;

        public ReportCustomersModal(IWebDriver driver, DropDownListContainer dropDownList)
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