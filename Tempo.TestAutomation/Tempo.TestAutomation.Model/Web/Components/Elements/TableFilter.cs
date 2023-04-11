using Datacom.TestAutomation.Web.Selenium;
using OpenQA.Selenium;
using Tempo.TestAutomation.Model.Web.Components.PageContainers;

namespace Tempo.TestAutomation.Model.Web.Components.Elements
{
    public class TableFilter
    {
        protected IWebDriver driver;
        private readonly DropDownListContainer dropDownList;

        public TableFilter(IWebDriver driver, DropDownListContainer dropDownList)
        {
            this.driver = driver;
            this.dropDownList = dropDownList;
        }

        private IWebElement FilterBtn => driver.FindElement(By.XPath("//button[contains(text(),'Filter')]"));

        private By ColumnSettings(string datatitle) => By.CssSelector($"[data-title='{datatitle}'] .k-header-column-menu");

        private By FilterValue(string title) => By.CssSelector($"[title='{title}']");

        private By ColumnName(string column) => By.CssSelector($"li[role = 'menuitemcheckbox'] .k-link input[data-field = '{column}']");

        private By ColumnStatus => By.CssSelector("li[role = 'menuitemcheckbox']");

        public void ClickonColumnSettings(string datatitle)
        {
            driver.GetElement(ColumnSettings(datatitle)).Click();
        }

        public bool IsColumnSettingsDisplayed(string datatitle)
        {
            if (driver.GetElement(ColumnSettings(datatitle)).Displayed)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void HoverOverFilter()
        {
            dropDownList.MouseHoverByText("Filter");
        }

        public void HoverOverColumn()
        {
            dropDownList.MouseHoverByText("Column");
        }

        public void AddColumn(string columnName)
        {
            driver.GetElement(ColumnName(columnName)).Click();
        }

        public bool CheckColumnStatus()
        {
            string columnstatus = driver.GetElement(ColumnStatus).GetAttribute("aria-checked");
            if (columnstatus.Contains("true"))
            {
                return true;
            }
            else

            {
                return false;
            }
        }

        public void SendTextToFilter(string title, string text)
        {
            driver.GetElement(FilterValue(title)).Click();
            driver.GetElement(FilterValue(title)).SendKeys(text);
        }

        public void FilterButton()
        {
            FilterBtn.Click();
        }

        public bool IsFilterButtonDisplayed()
        {
            if (FilterBtn.Displayed)
            {
                return true;
            }
            else

            {
                return false;
            }
        }
    }
}