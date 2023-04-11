using OpenQA.Selenium;

namespace Tempo.TestAutomation.Model.Web
{
    public static class DispatchRouteContainerLocators
    {
        public static class Text
        {
            public static By ActiveLabel => By.CssSelector(".route-job-count .active_label");
            public static By RouteJobCount => By.CssSelector(".route-job-count .active_count");
            public static By RouteName => By.CssSelector(".route-name");
        }

        public static class RouteDetail
        {
            public static By Container => By.CssSelector("ul[kendo-list-view='dispatchRouteDetail']");
            public static By Card => By.CssSelector(".route-job-container");
            public static By Status => By.CssSelector(".route-job-status");
        }
    }
}