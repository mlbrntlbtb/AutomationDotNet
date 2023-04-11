using Datacom.TestAutomation.Web.Selenium;
using OpenQA.Selenium;
using Tempo.TestAutomation.Model.Web.Components.Common;
using Tempo.TestAutomation.Model.Web.Components.Object;
using Tempo.TestAutomation.Model.Web.Components.PageContainers;
using Tempo.TestAutomation.Model.Web.Locators.Pages;

namespace Tempo.TestAutomation.Model.Web.Components.Pages
{
    public class BranchPage : TempoBasePage<BranchPage>, ILoadable<BranchPage>
    {
        private readonly IWebDriver driver;
        private readonly DropDownListContainer dropDownList;
        private readonly LoadingWheel loadingWheel;

        public BranchPage(IWebDriver driver, DropDownListContainer dropDownList, LoadingWheel loadingWheel)
            : base(driver)
        {
            this.driver = driver;
            this.dropDownList = dropDownList;
            this.loadingWheel = loadingWheel;
        }

        public void ClickAddNewBranchIcon()
        {
            driver.GetElement(BranchPageLocators.BranchesFrame.NavBar.Button.AddNewBranch).Click();
        }

        public void ClickBranchRowLink(int rowIndex)
        {
            Table BranchTable = new Table(driver.GetElement(BranchPageLocators.BranchesFrame.Table.Branches), driver);
            BranchTable.ClickRow(rowIndex);
        }

        public void ClickPencilIcon()
        {
            driver.GetElement(BranchPageLocators.BranchesFrame.NavBar.Button.UpdateExistingBranch).Click();
        }

        protected override bool EvaluateLoadedStatus()
        {
            bool isBranchesTablePresent = driver.IsElementPresent(BranchPageLocators.BranchesFrame.Table.Branches);
            return isBranchesTablePresent;
        }
    }
}