using Datacom.TestAutomation.Common;
using Datacom.TestAutomation.Web.Selenium;
using OpenQA.Selenium;
using Tempo.TestAutomation.Model.Web.Components.Common;
using Tempo.TestAutomation.Model.Web.Components.Object;
using Tempo.TestAutomation.Model.Web.Locators.Modals;

namespace Tempo.TestAutomation.Model.Web.Components.Modals
{
    public class EventDetailsModal : TempoBasePage<EventDetailsModal>
    {
        private readonly IWebDriver driver;

        public EventDetailsModal(IWebDriver driver)
            : base(driver)
        {
            this.driver = driver;
        }

        public bool ValidateEventsTabContents(String TabName)
        {
            try
            {
                switch (TabName.ToLowerString())
                {
                    case ("event details"):

                        if (driver.IsElementPresent(EventDetailsModalLocators.Tabs.EventDetailsList, TimeSpan.FromMilliseconds(3000)))
                        {
                            driver.GetElement(EventDetailsModalLocators.Tabs.EventDetailsList).Click();
                            return IsTabSelected(EventDetailsModalLocators.Tabs.ActiveTab) && driver.IsElementPresent(EventDetailsModalLocators.Body.EventBody);
                        }
                        break;

                    case ("items"):

                        if (driver.IsElementPresent(EventDetailsModalLocators.Tabs.ItemsList, TimeSpan.FromMilliseconds(3000)))
                        {
                            driver.GetElement(EventDetailsModalLocators.Tabs.ItemsList).Click();
                            return IsTabSelected(EventDetailsModalLocators.Tabs.ActiveTab) && driver.IsElementPresent(EventDetailsModalLocators.Body.ItemsBody);
                        }
                        break;

                    case ("signatures"):

                        if (driver.IsElementPresent(EventDetailsModalLocators.Tabs.SignaturesList, TimeSpan.FromMilliseconds(3000)))
                        {
                            driver.GetElement(EventDetailsModalLocators.Button.ArrowRight).Click();
                            driver.GetElement(EventDetailsModalLocators.Tabs.SignaturesList).Click();
                            return IsTabSelected(EventDetailsModalLocators.Tabs.ActiveTab) && driver.IsElementPresent(EventDetailsModalLocators.Body.SignaturesBody);
                        }

                        break;

                    case ("jobs"):

                        if (driver.IsElementPresent(EventDetailsModalLocators.Tabs.JobsList, TimeSpan.FromMilliseconds(3000)))
                        {
                            driver.GetElement(EventDetailsModalLocators.Button.ArrowRight).Click();
                            driver.GetElement(EventDetailsModalLocators.Tabs.JobsList).Click();
                            return IsTabSelected(EventDetailsModalLocators.Tabs.ActiveTab) && driver.IsElementPresent(EventDetailsModalLocators.Body.JobsBody);
                        }

                        break;

                    case ("endorsements"):

                        if (driver.IsElementPresent(EventDetailsModalLocators.Tabs.EndorsementsList, TimeSpan.FromMilliseconds(3000)))
                        {
                            driver.GetElement(EventDetailsModalLocators.Button.ArrowRight).Click();
                            driver.GetElement(EventDetailsModalLocators.Tabs.EndorsementsList).Click();
                            return IsTabSelected(EventDetailsModalLocators.Tabs.ActiveTab) && driver.IsElementPresent(EventDetailsModalLocators.Body.EndorsementsBody);
                        }

                        break;

                    case ("attachments"):

                        if (driver.IsElementPresent(EventDetailsModalLocators.Tabs.AttachmentsList, TimeSpan.FromMilliseconds(3000)))
                        {
                            driver.GetElement(EventDetailsModalLocators.Button.ArrowRight).Click();
                            driver.GetElement(EventDetailsModalLocators.Tabs.AttachmentsList).Click();
                            return IsTabSelected(EventDetailsModalLocators.Tabs.ActiveTab) && driver.IsElementPresent(EventDetailsModalLocators.Body.AttachmentsBody);
                        }

                        break;

                    case ("location"):

                        if (driver.IsElementPresent(EventDetailsModalLocators.Tabs.LocationList, TimeSpan.FromMilliseconds(3000)))
                        {
                            driver.GetElement(EventDetailsModalLocators.Button.ArrowRight).Click();
                            driver.GetElement(EventDetailsModalLocators.Tabs.LocationList).Click();
                            return IsTabSelected(EventDetailsModalLocators.Tabs.ActiveTab) && driver.IsElementPresent(EventDetailsModalLocators.Body.LocationBody);
                        }

                        break;

                    case ("audit history"):

                        if (driver.IsElementPresent(EventDetailsModalLocators.Tabs.AuditHistory, TimeSpan.FromMilliseconds(3000)))
                        {
                            driver.GetElement(EventDetailsModalLocators.Tabs.AuditHistory).Click();
                            return IsTabSelected(EventDetailsModalLocators.Tabs.ActiveTab) && driver.IsElementPresent(EventDetailsModalLocators.Body.AuditHistoryBody);
                        }
                        break;

                    default:
                        throw new InvalidDataException("Invalid Data Input");
                }
                return true;
            }
            catch (ElementNotInteractableException)
            {
                return false;
            }
        }

        public void ClickCloseButton()
        {
            driver.GetElement(EventDetailsModalLocators.Button.CloseButton).Click();
        }

        public void ClickJobHeader()
        {
            driver.GetElement(EventDetailsModalLocators.Button.ArrowRight).Click();
            Thread.Sleep(1000);
            driver.GetElement(EventDetailsModalLocators.Tabs.JobsList).Click();
        }

        public String GetRowJobID(int rowToClick = 0)
        {
            Table EventDetailsTable = new Table(driver.GetElement(EventDetailsModalLocators.Table.JobsDetails), driver);

            return EventDetailsTable.GetCellValue("Job Id", rowToClick);
        }

        protected override bool EvaluateLoadedStatus()
        {
            bool IsEventListPresent = driver.IsElementPresent(EventDetailsModalLocators.Header.EventDetails);

            return IsEventListPresent;
        }

        public bool IsJobIDEqual(string headerID)
        {
            return GetRowJobID() == headerID;
        }

        private bool IsTabSelected(By TabName)
        {
            return driver.GetElement(TabName).GetAttribute("aria-selected").Equals("true");
        }

        public bool IsEventModalBodyDisplayed()
        {
            return driver.IsElementPresent(EventDetailsModalLocators.Body.EventBody);
        }

        public bool IsEventMapBodyDisplayed()
        {
            return driver.IsElementPresent(EventDetailsModalLocators.Map.MapBody);
        }
    }
}