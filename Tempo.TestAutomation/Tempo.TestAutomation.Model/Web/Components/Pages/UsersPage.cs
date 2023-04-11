using Datacom.TestAutomation.Web.Selenium;
using OpenQA.Selenium;
using Tempo.TestAutomation.Model.Web.Components.Common;
using Tempo.TestAutomation.Model.Web.Components.Object;
using Tempo.TestAutomation.Model.Web.Components.PageContainers;
using Tempo.TestAutomation.Model.Web.Locators.Pages;

namespace Tempo.TestAutomation.Model.Web.Components.Pages
{
    public class UsersPage : TempoBasePage<UsersPage>, ILoadable<UsersPage>
    {
        private readonly IWebDriver driver;
        private readonly DropDownListContainer dropDownList;
        private readonly LoadingWheel loadingWheel;
        protected IList<IWebElement> Rows;

        public UsersPage(IWebDriver driver, DropDownListContainer dropDownList, LoadingWheel loadingWheel)
            : base(driver)
        {
            this.driver = driver;
            this.dropDownList = dropDownList;
            this.loadingWheel = loadingWheel;
        }

        public void SearchUser(string username)
        {
            driver.GetElement(UsersPageLocators.UsersFrame.NavBar.TextBox.Search).SendKeys(username);
            driver.GetElement(UsersPageLocators.UsersFrame.NavBar.Button.Search).Click();
            loadingWheel.WaitToDisappear();
        }

        public void ClickUserRow(string username)
        {
            Table UsersTable = new Table(driver.GetElement(UsersPageLocators.UsersFrame.Table.Users), driver);
            var rowIndex = UsersTable.GetRowIndexContainingText("Username", username);
            UsersTable.ClickRow((int)rowIndex!);
        }

        public void ClickSetPassword()
        {
            driver.GetElement(UsersPageLocators.UsersFrame.NavBar.Button.SetPassword).Click();

        }

        protected override bool EvaluateLoadedStatus()
        {
            loadingWheel.WaitToDisappear();
            bool IsUserTablePresent = driver.IsElementPresent(UsersPageLocators.UsersFrame.Table.Users);
            bool IsUserTableEnabled = driver.GetElement(UsersPageLocators.UsersFrame.Table.Users).Enabled;

            return IsUserTablePresent && IsUserTableEnabled;
        }
    }
}