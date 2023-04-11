using OpenQA.Selenium;

namespace Tempo.TestAutomation.Model.Web.Locators.Pages
{
    public static class DispatchPageLocators
    {
        public static class Dialog
        {
            public static By Progress => By.CssSelector("#progressWindow_wnd_title");
        }

        public static class JobsFrame
        {
            public static class NavBar
            {
                public static class Button
                {
                    public static By AddJob => By.CssSelector("button[data-tooltip='Add Job']");
                    public static By DispatchMenu => By.CssSelector("form[name='dispatchJobSearch'] .menu_button.no_arrow");
                    public static By Edit => By.CssSelector("button[data-tooltip='Edit Job']");
                    public static By PrintJob => By.XPath("//span[text()=' Print Job ']");
                }

                public static class List
                {
                    public static By DispatchMenu => By.CssSelector("form[name='dispatchJobSearch'] .menu_button.no_arrow ul[role='menu']");
                    public static By MenuButton => By.CssSelector("form[name='dispatchJobSearch'] .menu_button.no_arrow");
                }

                public static class Message
                {
                    public static By Cancel => By.CssSelector("span[ng-bind-html='message.content']");
                }

                public static class DropdownField
                {
                    public static By Branches => By.CssSelector("ng-dropdown-multiselect[options='controller.branchFilterList'] .multiselect-parent.btn-group.dropdown-multiselect button[ng-click='toggleDropdown()']");

                    public static class Text
                    {
                        public static By Branches => By.CssSelector("span.ng-binding.ng-scope");
                    }
                }
            }

            public static class Table
            {
                public static By DispatchJob => By.CssSelector("#dispatchJobGrid");
                public static By FirstRow => By.CssSelector("#dispatchJobGrid tr:nth-child(1) td:nth-child(9)");
                public static By TableRow => By.CssSelector("tbody tr");
            }
        }

        public static class RouteFrame
        {
            public static class ListBox
            {
                public static By dispatchRoute => By.CssSelector("#dispatchRouteListView");
                public static By EditPopUp => By.CssSelector("span[class='column_25 fixed_height ng-binding']");
                public static By ProcessJob => By.CssSelector("div[class='customSelectBoxTitleBar customSelectBoxBody']");
                public static By AutoAdhoc => By.XPath("//span[text()='AutoAdhoc']");
                public static By Confirmation => By.CssSelector("#manualProcessConfirmationwindow_wnd_title");
                public static By ReasonForChange => By.XPath("//div[@id='manualProcessConfirmationwindow']//span[@class='k-select']");
                public static By OnBehalfOfDriver => By.XPath("//li[text()='On behalf of driver']");
                public static By Comment => By.CssSelector("input[name='manualProcessChangeReasonComment']");
                public static By JobProcess => By.CssSelector("button[ng-click='manualProcessActioned()']");
            }

            public static class NavBar
            {
                public static class TextBox
                {
                    public static By FindARoute => By.CssSelector("input[placeholder='Find a Route']");
                }

                public static class Button
                {
                    public static By JobStatusFilterDropDown => By.XPath("//ng-dropdown-multiselect[@options='jobStatusFilterList']");
                }
            }

            public static class Button
            {
                public static By ApproveAllJobs = By.CssSelector("li[ng-click='approveAllJobs(dataItem)']");
                public static By CancelAllocations = By.CssSelector("li[ng-click='cancelAllAllocations(dataItem)']");
                public static By EditJob = By.CssSelector("button[ng-click='controller.onEditJob(dataItem.jobId)']");
                public static By RouteOption = By.CssSelector("ul[ng-show='isRouteBurgerMenuVisible(dataItem)']");
                public static By Yes = By.CssSelector("button[ng-click='onConfirmCancelAllAllocations()']");
                public static By routeOptions => By.CssSelector("form[name = 'dispatchRouteSearch'] .menu_button.no_arrow.k-widget");

                public static By viewRouteMap => By.CssSelector("[class='fa fa-map-marker fa-fw']");
            }
        }

        public static class Toast
        {
            public static By Close => By.CssSelector("button[class='close ng-binding ng-scope'][ng-if='message.dismissButton'");
            public static By Text => By.CssSelector("span[ng-bind-html='message.content'][ class='ng-binding ng-scope']");
        }
    }
}