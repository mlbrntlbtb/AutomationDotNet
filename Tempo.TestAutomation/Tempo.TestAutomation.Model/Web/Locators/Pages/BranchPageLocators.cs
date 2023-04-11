using OpenQA.Selenium;

namespace Tempo.TestAutomation.Model.Web.Locators.Pages
{
    public class BranchPageLocators
    {
        public static class BranchesFrame
        {
            public static class NavBar
            {
                public static class Button
                {
                    public static By AddNewBranch => By.CssSelector("button[data-tooltip=Add]");
                    public static By UpdateExistingBranch => By.CssSelector("button[data-tooltip=Edit]");
                }
            }

            public static class Table
            {
                public static By Branches => By.CssSelector("div#branchGrid");
            }
        }
    }
}