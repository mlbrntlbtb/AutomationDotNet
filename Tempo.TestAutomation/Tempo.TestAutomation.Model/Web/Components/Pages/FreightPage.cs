using Datacom.TestAutomation.Web.Selenium;
using OpenQA.Selenium;
using Tempo.TestAutomation.Model.DTOs;
using Tempo.TestAutomation.Model.Web.Components.Elements;
using Tempo.TestAutomation.Model.Web.Components.Object;
using Tempo.TestAutomation.Model.Web.Components.PageContainers;
using Tempo.TestAutomation.Model.Web.Locators.Pages;

namespace Tempo.TestAutomation.Model.Web.Components.Pages
{
    public class FreightPage : TempoBasePage<FreightPage>, ILoadable<FreightPage>
    {
        private readonly IWebDriver driver;
        private readonly DropDownListContainer dropDownList;
        private readonly LoadingWheel loadingWheel;
        private readonly TableFilter tableFilter;

        public FreightPage(IWebDriver driver, DropDownListContainer dropDownList, LoadingWheel loadingWheel, TableFilter tableFilter)
            : base(driver)
        {
            this.driver = driver;
            this.dropDownList = dropDownList;
            this.loadingWheel = loadingWheel;
            this.tableFilter = tableFilter;
        }

        public void AddConsignment(FreightData freightData)
        {
            driver.GetElement(FreightPageLocators.Table.Button.MenuKebab).Click();
            if (driver.IsElementPresent(FreightPageLocators.Table.Header.MenuList, TimeSpan.FromMilliseconds(3000)))
                SelectMenuText(freightData.MenuList!.AddConsignment!);
        }

        public void ClearLayout(FreightData freightData)
        {
            driver.GetElement(FreightPageLocators.Table.Button.MenuKebab).Click();
            if (driver.IsElementPresent(FreightPageLocators.Table.Header.MenuList, TimeSpan.FromMilliseconds(3000)))
                SelectMenuText(freightData.MenuList!.ClearLayout!);
        }

        public void ClickSearchButton()
        {
            driver.GetElement(FreightPageLocators.Table.Button.Search).Click();
            loadingWheel.WaitToDisappear();
        }

        public bool IsAddConsignmentDisplayed()
        {
            loadingWheel.WaitToDisappear();
            driver.SwitchToLastWindow();
            return driver.IsElementPresent(FreightPageLocators.Table.AddConsignment) && driver.IsPageTitle("New Consignment | Tempo");
        }

        public bool IsFreightDetailsCleared()
        {
            loadingWheel.WaitToDisappear();
            return driver.GetElement(FreightPageLocators.Message.NotificationMessage).IsElementTextContains("Your configuration has been cleared.");
        }

        public bool IsFreightDetailsSaved()
        {
            loadingWheel.WaitToDisappear();
            return driver.GetElement(FreightPageLocators.Message.NotificationMessage).IsElementTextContains("Your configuration has been saved.");
        }

        public bool IsSearchedResultDisplayed()
        {
            loadingWheel.WaitToDisappear();
            return driver.IsElementPresent(FreightPageLocators.Table.Results);
        }

        public void SaveLayout(FreightData freightData)
        {
            driver.GetElement(FreightPageLocators.Table.Button.MenuKebab).Click();
            if (driver.IsElementPresent(FreightPageLocators.Table.Header.MenuList, TimeSpan.FromMilliseconds(3000)))
                SelectMenuText(freightData.MenuList!.SaveLayout!);
        }

        public void SearchFreightDetails(FreightData freightDetails)
        {
            if (driver.IsElementPresent(FreightPageLocators.Table.TextBox.SearchBox, TimeSpan.FromMilliseconds(3000)))
            {
                driver.GetElement(FreightPageLocators.Table.TextBox.SearchBox).SendKeys(freightDetails.ConsignmentNumber);
            }
            else if (!driver.IsElementPresent(FreightPageLocators.Table.TextBox.SearchBox, TimeSpan.FromMilliseconds(3000)))
            {
                driver.WaitUntilElementIsPresent(FreightPageLocators.Table.TextBox.SearchBox);
                driver.GetElement(FreightPageLocators.Table.TextBox.SearchBox).SendKeys(freightDetails.ConsignmentNumber);
            }
            else
                throw new ElementNotVisibleException("Searchbox load timeout");
        }

        public void SelectBarcodeSubMenu(string subMenu)
        {
            subMenu = subMenu.ToLower().Trim();
            IWebElement BardocdeSubMenu = driver.GetElement(FreightPageLocators.Table.Header.BarcodeSubMenu);

            BardocdeSubMenu!.GetElements(FreightPageLocators.Table.Header.BarcodeListItem)
                .Where(e => e.Text.ToLower().Trim().Contains(subMenu))
                .First()
                .Hover(driver);
        }

        public void SelectBardcodeItem(FreightData freightData)
        {
            loadingWheel.WaitToDisappear();
            tableFilter.ClickonColumnSettings(freightData.ColumnName!);

            if (driver.IsElementPresent(FreightPageLocators.Table.Header.BarcodeSideMenu, TimeSpan.FromMilliseconds(3000)))
            {
                dropDownList.MouseHoverByText(freightData.ParentMenu!.Columns!);
                SelectBarcodeSubMenu(freightData.ChildMenu!.ItemID!);

                if (driver.GetElement(FreightPageLocators.Table.Button.ItemIDRadio).Selected == false)
                    driver.GetElement(FreightPageLocators.Table.Button.ItemIDRadio).Click();
            }
        }

        public void SelectMenuText(string menuText)
        {
            menuText = menuText.ToLower().Trim();
            IWebElement BardocdeSubMenu = driver.GetElement(FreightPageLocators.Table.Header.MenuList);

            BardocdeSubMenu!.GetElements(FreightPageLocators.Table.Header.BarcodeListItem)
                .Where(e => e.Text.ToLower().Trim().Contains(menuText))
                .First()
                .Click();
        }

        protected override bool EvaluateLoadedStatus()
        {
            loadingWheel.WaitToDisappear();
            bool IsFreightTablePresent = driver.IsElementPresent(FreightPageLocators.Table.Freight);

            return IsFreightTablePresent;
        }
    }
}