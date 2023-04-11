using Datacom.TestAutomation.Web.Selenium;
using OpenQA.Selenium;
using Tempo.TestAutomation.Model.DTOs;
using Tempo.TestAutomation.Model.Web.Components.Common;
using Tempo.TestAutomation.Model.Web.Components.Object;
using Tempo.TestAutomation.Model.Web.Components.PageContainers;
using Tempo.TestAutomation.Model.Web.Locators.Pages;

namespace Tempo.TestAutomation.Model.Web.Components.Pages
{
    public class OrdersPage : TempoBasePage<OrdersPage>, ILoadable<OrdersPage>
    {
        private readonly IWebDriver driver;
        private readonly DropDownListContainer dropDownList;
        private readonly LoadingWheel loadingWheel;
        private bool isOrderExisting;

        public OrdersPage(IWebDriver driver, DropDownListContainer dropDownList, LoadingWheel loadingWheel)
            : base(driver)
        {
            this.driver = driver;
            this.dropDownList = dropDownList;
            this.loadingWheel = loadingWheel;
            isOrderExisting = false;
        }

        public void ClickEditOrderButton()
        {
            driver.GetElement(OrdersPageLocators.Body.Button.Edit).Click();
        }

        public void ClickItemsPerPage()
        {
            driver.GetElement(OrdersPageLocators.OrdersFrame.Footer.Button.ItemsPerPageMenu).Click();
        }

        public void ClickOrderRow(OrderData orderData, int rowToCheck = 0)
        {
            Table OrdersTable = new Table(driver.GetElement(OrdersPageLocators.OrdersFrame.Table.Orders), driver);
            int searchOrderRowIndex = (int)OrdersTable.GetRowIndex(orderData!.ColumnName!, orderData!.Service!, rowToCheck)!;
            if (searchOrderRowIndex == -1)
                isOrderExisting = false;
            else
            {
                OrdersTable.ClickCell(orderData!.ColumnName!, searchOrderRowIndex);
                OrdersTable.ClickCell(orderData!.ColumnName!, searchOrderRowIndex);
                isOrderExisting = true;
            }
        }

        public void ClickRowByIndex(int? rowIndex)
        {
            Table OrdersTable = new Table(driver.GetElement(OrdersPageLocators.OrdersFrame.Table.Orders), driver);
            OrdersTable.ClickRow((int)rowIndex!);
        }

        public void ClickSearchButton()
        {
            driver.GetElement(OrdersPageLocators.Body.Button.Search).Click();
            loadingWheel.WaitToDisappear();
        }

        public void FillOrdersSearchTextBox(OrderData orderData)
        {
            driver.GetElement(OrdersPageLocators.Body.TextBox.Search).Click();
            driver.GetElement(OrdersPageLocators.Body.TextBox.Search).SendKeys(orderData.OrderNumber!);
        }

        public bool IsDropDownDisplayed()
        {
            return dropDownList.IsDropDownPresent();
        }

        public bool IsOrderExisting()
        {
            return isOrderExisting;
        }

        public bool IsOrdersRowCountConsistent(int rowCount)
        {
            loadingWheel.WaitToDisappear();
            Table OrdersTable = new Table(driver.GetElement(OrdersPageLocators.OrdersFrame.Table.Orders), driver);
            return OrdersTable.GetRowCount() == rowCount;
        }

        public bool IsRowSelected(int? rowIndex)
        {
            Table OrdersTable = new Table(driver.GetElement(OrdersPageLocators.OrdersFrame.Table.Orders), driver);
            bool isRowSelected = OrdersTable.IsRowHighlighted((int)rowIndex!);
            return isRowSelected;
        }

        public void SelectItemsPerPageDropdownItem(string referenceText)
        {
            dropDownList.SelectByText(referenceText);
        }

        protected override bool EvaluateLoadedStatus()
        {
            loadingWheel.WaitToDisappear();
            bool IsItemsPerPagePresent = driver.IsElementPresent(OrdersPageLocators.OrdersFrame.Footer.Button.ItemsPerPageMenu);
            bool IsOrdersTablePresent = driver.IsElementPresent(OrdersPageLocators.OrdersFrame.Table.Orders);

            return IsOrdersTablePresent && IsItemsPerPagePresent;
        }
    }
}