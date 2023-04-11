using Datacom.TestAutomation.Web.Selenium;
using OpenQA.Selenium;
using Tempo.TestAutomation.Model.DTOs.Dispatch;
using Tempo.TestAutomation.Model.Web.Components.Object;
using Tempo.TestAutomation.Model.Web.Components.PageContainers;
using Tempo.TestAutomation.Model.Web.Locators.Pages;

namespace Tempo.TestAutomation.Model.Web.Components.Pages
{
    public class RouteMapPage : TempoBasePage<RouteMapPage>, ILoadable<RouteMapPage>
    {
        private readonly IWebDriver driver;
        private readonly DropDownListContainer dropDownList;
        private readonly LoadingWheel loadingWheel;

        public RouteMapPage(IWebDriver driver, DropDownListContainer dropDownList, LoadingWheel loadingWheel)
            : base(driver)
        {
            this.driver = driver;
            this.dropDownList = dropDownList;
            this.loadingWheel = loadingWheel;
        }

        public void ClickLoggedOnCheckBox()
        {
            if (driver.GetElement(RouteMapPageLocators.SideBar.CheckBox.LoggedOnCheckBox).Selected == false)
                driver.GetElement(RouteMapPageLocators.SideBar.CheckBox.LoggedOnCheckBox).Click();
        }

        public void ClickShowRoutesCheckBox()
        {
            if (driver.GetElement(RouteMapPageLocators.SideBar.CheckBox.ShowRoutes).Selected == false)
                driver.GetElement(RouteMapPageLocators.SideBar.CheckBox.ShowRoutes).Click();
        }

        public void ClickVehiclesWithRoutesCheckBox()
        {
            if (driver.GetElement(RouteMapPageLocators.SideBar.CheckBox.VehiclesWithRoutesCheckBox).Selected == false)
                driver.GetElement(RouteMapPageLocators.SideBar.CheckBox.VehiclesWithRoutesCheckBox).Click();
        }

        public void CloseRoutesTab()
        {
            driver.Close();
            driver.SwitchToLastWindow();
        }

        public void EnableRouteSideMenu()
        {
            if (driver.GetElement(RouteMapPageLocators.SideBar.Button.SideMenuToggleOpen).Displayed == true)
                driver.GetElement(RouteMapPageLocators.SideBar.Button.SideMenuToggleOpen).Click();
        }

        public void ExpandRouteDisplayAccordion()
        {
            if (driver.IsElementPresent(RouteMapPageLocators.SideBar.Accordion.Button.RouteDisplayAccordionButton) == true)
                driver.GetElement(RouteMapPageLocators.SideBar.Accordion.Button.RouteDisplayAccordionButton).Click();
        }

        public void ExpandRouteFilterAccordion()
        {
            if (driver.IsElementPresent(RouteMapPageLocators.SideBar.Accordion.Button.RouteFiltersAccordionButton) == true)
                driver.GetElement(RouteMapPageLocators.SideBar.Accordion.Button.RouteFiltersAccordionButton).Click();
        }

        public void ExpandViewAreaAccordion()
        {
            if (driver.IsElementPresent(RouteMapPageLocators.SideBar.Accordion.Button.ViewAreaAccordionButton) == true)
                driver.GetElement(RouteMapPageLocators.SideBar.Accordion.Button.ViewAreaAccordionButton).Click();
        }

        public void SelectViewArea(RouteMapData routeMapdata)
        {
            driver.GetElement(RouteMapPageLocators.SideBar.Accordion.Dropdown.ViewAreaDropdownButton).Click();
            dropDownList.SelectByText(routeMapdata.ViewArea!);
        }

        public void SwitchToRouteMapWindow()
        {
            driver.SwitchTo().Window(driver.WindowHandles[1]);
            loadingWheel.WaitToDisappear();
        }

        public void UntickShowRoutesCheckBox()
        {
            if (driver.IsElementPresent(RouteMapPageLocators.SideBar.CheckBox.ShowRoutesTicked) == false)
            {
                if (driver.IsElementPresent(RouteMapPageLocators.SideBar.CheckBox.ShowRoutes) == true)
                    driver.GetElement(RouteMapPageLocators.SideBar.CheckBox.ShowRoutes).Click();
            }
            else
                driver.GetElement(RouteMapPageLocators.SideBar.CheckBox.ShowRoutesTicked).Click();
        }

        public void VerifyRouteDisplaySideMenu()
        {
            driver.IsElementPresent(RouteMapPageLocators.SideBar.Accordion.MenuHeader.RouteDisplayHeader);
            driver.IsElementPresent(RouteMapPageLocators.SideBar.Accordion.MenuHeader.RouteDisplaySubHeader);
            driver.IsElementPresent(RouteMapPageLocators.SideBar.Accordion.MenuHeader.RouteDisplayIconType);
        }

        protected override bool EvaluateLoadedStatus()
        {
            loadingWheel.WaitToDisappear();
            bool IsSideMenuContainerPresent = driver.IsElementPresent(RouteMapPageLocators.Containers.SideMenuContainer);
            bool IsMapContainerPresent = driver.IsElementPresent(RouteMapPageLocators.Containers.MapContainer);

            return IsSideMenuContainerPresent && IsMapContainerPresent;
        }
    }
}