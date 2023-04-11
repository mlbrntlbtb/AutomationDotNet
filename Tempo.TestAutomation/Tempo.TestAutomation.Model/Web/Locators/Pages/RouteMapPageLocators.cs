using OpenQA.Selenium;

namespace Tempo.TestAutomation.Model.Web.Locators.Pages
{
    public class RouteMapPageLocators
    {
        public static class Containers
        {
            public static By SideMenuContainer => By.CssSelector("[id='hereMapSideMenu']");
            public static By MapContainer => By.CssSelector("[id='hereMapContainer']");
        }

        public static class SideBar
        {
            public static class Button
            {
                public static By SideMenuToggle => By.CssSelector("[class='flex_container flex-justify-end no-spacing']");

                public static By SideMenuToggleOpen => By.CssSelector("[class='flex_container flex-justify-center no-spacing']");
            }

            public static class CheckBox
            {
                public static By ShowRoutes => By.CssSelector("[id='showRoutesCheckbox'][class='ng-pristine ng-untouched ng-valid']");
                public static By ShowRoutesTicked => By.CssSelector("[id='showRoutesCheckbox'][class='ng-valid ng-dirty ng-valid-parse ng-touched']");
                public static By LoggedOnCheckBox => By.CssSelector("[id='loggedon-checkbox']");
                public static By VehiclesWithRoutesCheckBox => By.CssSelector("[id='vehicles-checkbox']");
            }

            public static class Accordion
            {
                public static class Button
                {
                    public static By RouteFiltersAccordionButton => By.CssSelector("[class='fa fa-chevron-circle-down fa-fw fa-2x'][ng-hide='routeFiltersMenuVisible']");
                    public static By ViewAreaAccordionButton => By.CssSelector("[class='fa fa-chevron-circle-down fa-fw fa-2x'][ng-hide='viewAreaMenuVisible']");
                    public static By RouteDisplayAccordionButton => By.CssSelector("[class='fa fa-chevron-circle-down fa-fw fa-2x'][ng-hide='routeMenuVisible']");
                }

                public static class Dropdown
                {
                    public static By ViewAreaDropdownButton => By.CssSelector("[aria-owns='viewAreaList_listbox'] [class='k-dropdown-wrap k-state-default']");
                }

                public static class ListBox
                {
                    public static By ViewAreaList => By.CssSelector("");
                }

                public static class MenuHeader
                {
                    public static By RouteDisplayHeader => By.CssSelector("[class='menu-items-indent'] [for='iconHeader']");
                    public static By RouteDisplaySubHeader => By.CssSelector("[class='menu-items-indent'] [for='iconHeader']");
                    public static By RouteDisplayIconType => By.XPath("//div[@class='menu-items-indent']//label[contains(text(),'Route icon style')]");
                }
            }
        }
    }
}