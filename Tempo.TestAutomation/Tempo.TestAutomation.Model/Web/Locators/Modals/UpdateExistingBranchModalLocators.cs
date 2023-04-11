using OpenQA.Selenium;

namespace Tempo.TestAutomation.Model.Web.Locators.Modals
{
    public class UpdateExistingBranchModalLocators
    {
        public static class Footer
        {
            public static By Cancel => By.CssSelector("button.k-button.k-primary[ng-click='cancelEntity();$event.stopPropagation();']");
        }

        public static class Header
        {
            public static By UpdateExistingBranch => By.XPath("//div[text()=' Update Existing Branch ']");
        }

        public static class TabMenu
        {
            public static class DetailsForm
            {
                public static By Code => By.Id("code");
             
            }
        }
    }
}