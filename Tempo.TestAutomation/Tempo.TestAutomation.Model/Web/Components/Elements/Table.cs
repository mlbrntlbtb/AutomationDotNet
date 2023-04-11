using Datacom.TestAutomation.Web.Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using Tempo.TestAutomation.Model.Interface;

namespace Tempo.TestAutomation.Model.Web.Components.Common
{
    public class Table : IGenericPageObj
    {
        protected IList<IWebElement> Columns;
        protected IWebDriver driver;
        protected IWebElement ParentElement;
        protected IList<IWebElement> Rows;

        public Table(IWebElement parentElement, IWebDriver driver)
        {
            ParentElement = parentElement;
            Columns = (IList<IWebElement>?)ParentElement.GetElements(By.TagName("th"))!;
            Rows = (IList<IWebElement>?)ParentElement.GetElements(By.CssSelector("tbody tr"))!;
            this.driver = driver;
        }

        public void ClickCell(string columnName, int rowIndex)
        {
            int colIndex = (int)GetColumnIndex(columnName)!;
            int rowCount = Rows.Count;
            IList<IWebElement> cells;

            cells = Rows[rowIndex].GetElements(By.TagName("td"));
            cells[colIndex].Click();
        }

        public void ClickCellLink(string columnName, int rowIndex)
        {
            int colIndex = (int)GetColumnIndex(columnName)!;
            int rowCount = Rows.Count;
            IList<IWebElement> cells;

            cells = Rows[rowIndex].GetElements(By.TagName("td"));
            cells[colIndex].FindElement(By.CssSelector("a")).Click();
        }

        public void ClickCellLinkByName(string columnName, int rowIndex, string referenceText)
        {
            int colIndex = (int)GetColumnIndex(columnName)!;
            int rowCount = Rows.Count;
            IList<IWebElement> cells;

            cells = Rows[rowIndex].GetElements(By.TagName("td"));
            cells[colIndex].GetElement(By.XPath(".//a[text()='" + referenceText + "']")).Click();
        }

        public void ClickRow(int rowIndex)
        {
            Rows[rowIndex].Click();
        }
        public string GetCellValue(string columnName, int rowIndex)
        {
            int colIndex = (int)GetColumnIndex(columnName)!;
            string valueHolder = "";
            int rowCount = Rows.Count;
            IList<IWebElement> cells;

            cells = Rows[rowIndex].GetElements(By.TagName("td"));
            valueHolder = cells[colIndex].Text;
            return valueHolder;
        }

        public int? GetColumnIndex(string columnName)
        {
            int colCount = Columns.Count;
            string valueHolder = "";
            for (int i = 0; i < colCount; i++)
            {
                valueHolder = Columns[i].Text;
                if (columnName.Equals(valueHolder))
                {
                    return i;
                }
            }
            return colCount;
        }

        public IWebElement GetElementByText(string referenceText)
        {
            return null!;
        }

        public int GetRowCount()
        {
            return Rows.Count;
        }

        public int? GetRowIndex(string columnName, string textReference, int maxRowToCheck = 0)
        {
            int colIndex = (int)GetColumnIndex(columnName)!;
            string valueHolder = "";

            int rowCount = Rows.Count();
            rowCount = maxRowToCheck == 0 ? rowCount : maxRowToCheck;
            IEnumerable<IWebElement> cells;
            IWebElement row;

            for (int i = 0; i < rowCount; i++)
            {
                row = Rows[i];
                cells = row.FindElements(By.TagName("td")).Where(e => e.Displayed);
                ScrollInToCell(row);
                valueHolder = cells.ElementAt(colIndex).Text;

                if (textReference.Equals(valueHolder))
                {
                    return i;
                }
            }
            return -1;
        }

        public int? GetRowIndexContainingText(string columnName, string textReference)
        {
            int colIndex = (int)GetColumnIndex(columnName)!;
            string valueHolder = "";
            int rowCount = Rows.Count;
            IList<IWebElement> cells;
            IWebElement row;

            for (int i = 0; i < rowCount; i++)
            {
                row = Rows[i];
                cells = row.GetElements(By.TagName("td"));
                ScrollInToCell(row);
                valueHolder = cells[colIndex].Text;

                if (textReference.Contains(valueHolder))
                {
                    return i;
                }
            }
            return null;
        }

        public bool IsCellLinkExisting(string columnName, int rowIndex, string referenceText)
        {
            int colIndex = (int)GetColumnIndex(columnName)!;
            int rowCount = Rows.Count;
            IList<IWebElement> cells;

            cells = Rows[rowIndex].GetElements(By.TagName("td"));
            return cells[colIndex].GetElements(By.XPath(".//a[text()='" + referenceText + "']")).Any();
        }

        public bool IsCellWithText(string columnName, int rowIndex)
        {
            int colIndex = (int)GetColumnIndex(columnName)!;
            string valueHolder = "";
            int rowCount = Rows.Count;
            IList<IWebElement> cells;

            cells = Rows[rowIndex].GetElements(By.TagName("td"));
            valueHolder = cells[colIndex].Text;
            return valueHolder != string.Empty;
        }

        public bool IsColumnValueConsistent(string referenceText, string columnName)
        {
            referenceText = referenceText.ToLower().Trim();

            return !Rows.Any(x => x.FindElement(By.CssSelector("td:nth-of-type(" + (GetColumnIndex(columnName) + 1) + ") a")).Text.ToLower().Trim() != referenceText);
        }

        public bool IsFilteredValueConsistent(string referenceText, string columnName)
        {
            referenceText = referenceText.ToLower().Trim();

            return !Rows.Any(x => x.FindElement(By.CssSelector("td:nth-of-type(" + (GetColumnIndex(columnName) + 2) + ") span")).Text.ToLower().Trim() != referenceText);
        }

        public bool IsRowHighlighted(int rowIndex = 0)
        {
            return Rows[rowIndex].GetAttribute("aria-selected") == "true";
        }

        public void SelectByText(string referenceText)
        {
        }

        public void SetCellValue(string columnName, int rowIndex, string text)
        {
            int colIndex = (int)GetColumnIndex(columnName)!;
            int rowCount = Rows.Count;
            IList<IWebElement> cells;

            cells = Rows[rowIndex].GetElements(By.TagName("td"));
            cells[colIndex].SendKeys(text);
        }

        private void ScrollInToCell(IWebElement tableCellElement)
        {
            Actions actions = new Actions(driver);
            actions.MoveToElement(tableCellElement);
            actions.Perform();
        }

        public bool VerifyTableColumn(string columnName)
        {
            string actualColumnName = "";
            bool isColumnVisible = false;
            for (int i = 0; i < Columns.Count; i++)
            {
                actualColumnName = Columns[i].Text;
                if (actualColumnName.Equals(columnName))
                {
                    isColumnVisible = true;
                    break;
                }
            }
            return isColumnVisible;
        }
    }
}