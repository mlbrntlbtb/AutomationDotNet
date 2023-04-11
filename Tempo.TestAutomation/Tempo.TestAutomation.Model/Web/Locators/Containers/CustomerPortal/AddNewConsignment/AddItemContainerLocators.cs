using OpenQA.Selenium;

namespace Tempo.TestAutomation.Model.Web.Locators.Containers.CustomerPortal.AddNewConsignment
{
    public static class AddItemContainerLocators
    {
        public static By Container => By.CssSelector("div[formarrayname='formItems']");
        public static By FormItems => By.CssSelector("div[class*='accent panel']");
    }
}