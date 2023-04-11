using Datacom.TestAutomation.Web.Selenium;
using OpenQA.Selenium;
using ServiceStack;
using Tempo.TestAutomation.Model.Web.Components.Common;
using Tempo.TestAutomation.Model.Web.Components.Elements;
using Tempo.TestAutomation.Model.Web.Components.Object;
using Tempo.TestAutomation.Model.Web.Locators.Pages;

namespace Tempo.TestAutomation.Model.Web.Components.Pages
{
    public class EventsPage : TempoBasePage<EventsPage>, ILoadable<EventsPage>
    {
        private readonly IWebDriver driver;
        private readonly LoadingWheel loadingWheel;
        private readonly TableFilter tableFilter;
        private readonly NavigationBar navigationBar;
        private readonly ToastMessage toastMessage;

        public EventsPage(IWebDriver driver, LoadingWheel loadingWheel, TableFilter tableFilter, NavigationBar navigationBar, ToastMessage toastMessage)
            : base(driver)
        {
            this.driver = driver;
            this.loadingWheel = loadingWheel;
            this.tableFilter = tableFilter;
            this.navigationBar = navigationBar;
            this.toastMessage = toastMessage;
        }

        public void ClickEventsTable()
        {
            var eventsRow = driver.GetElements(EventsPageLocators.EventsFrame.Table.EventsRow).ToList();
            eventsRow.ElementAt(new Random().Next(0, eventsRow.Count - 42)).Click();
        }

        public void ClickEventsRow(int rowToClick = 0)
        {
            Table eventsTable = new Table(driver.GetElement(EventsPageLocators.EventsFrame.Table.EventsTable), driver);
            eventsTable.ClickRow(rowToClick);
            loadingWheel.WaitToDisappear();
        }

        public void ClickViewMore()
        {
            if (navigationBar.IsNavButtonStatus("View More") == true)
            {
                loadingWheel.WaitToDisappear();
                navigationBar.NavButton("View More");
            }
            else
            {
                throw new ElementClickInterceptedException("View More Button is disabled");
            }
        }

        protected override bool EvaluateLoadedStatus()
        {
            loadingWheel.WaitToDisappear();
            bool IsEventsListPresent = driver.IsElementPresent(EventsPageLocators.EventsFrame.NavBar.Header.EventsList);
            bool IsEventDetailsPresent = driver.IsElementPresent(EventsPageLocators.EventsFrame.Table.EventDetails);
            bool IsEventItemsPresent = driver.IsElementPresent(EventsPageLocators.EventsFrame.Table.EventItems);

            return IsEventsListPresent && IsEventDetailsPresent && IsEventItemsPresent;
        }

        public void ClickProcessFilterSettings(string TableColumnHeadingSettings)
        {
            tableFilter.ClickonColumnSettings(TableColumnHeadingSettings);
        }

        public void HoverOnColumnSubmenu()
        {
            tableFilter.HoverOverColumn();
        }

        public void AddEventIDColumn()
        {
            tableFilter.AddColumn("eventId");
        }

        public bool CheckColumnStatus()
        {
            return tableFilter.CheckColumnStatus();
        }

        public bool IsColumnExist()
        {
            Table eventsTable = new Table(driver.GetElement(EventsPageLocators.EventsFrame.Table.EventsTable), driver);
            return eventsTable.VerifyTableColumn("Event Id");
        }

        public void ClickOnSaveLayout()
        {
            navigationBar.NavButton("Save Layout");
        }

        public void ClickOnClearLayout()
        {
            navigationBar.NavButton("Clear Layout");
        }

        public void ClickOnEventFirstRecord(int row)
        {
            loadingWheel.WaitToDisappear();
            Table EventTable = new Table(driver.GetElement(EventsPageLocators.EventsFrame.Table.EventsTable), driver);
            EventTable.ClickRow(row);
        }

        public void ClickOnMapButton()
        {
            navigationBar.NavButton("Map");
        }

        public bool IsMapButtonDisplayed()
        {
            return driver.IsElementPresent(EventsPageLocators.EventsFrame.NavBar.Button.Map);
        }

        public void ClickOnEventHeader()
        {
            driver.GetElement(EventsPageLocators.EventsFrame.NavBar.Header.EventsHeading).Click();
        }

        public bool IsToastMessageDisplayed(string toastmessage)
        {
            if (toastMessage.GetToastMessageValue().Contains(toastmessage))
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