using OpenQA.Selenium;

namespace Tempo.TestAutomation.Model.Web.Locators.Modals
{
    public class EventDetailsModalLocators
    {
        public static class Body
        {
            public static By AttachmentsBody => By.CssSelector(".image-gallery");

            public static By AuditHistoryBody => By.CssSelector("audit-history-view[class='ng-isolate-scope']");

            public static By EndorsementsBody => By.CssSelector("div[ng-repeat='endorsement in eventEndorsements']");

            public static By EventBody => By.CssSelector("#kendoTabStrip-1");

            public static By ItemsBody => By.CssSelector("div[id = 'kendoTabStrip-2'] div[class='k-grid k-widget']");

            public static By JobsBody => By.CssSelector("table[role='treegrid'] thead[role='rowgroup']");

            public static By LocationBody => By.CssSelector("div[id='eventLocationMap'] div[class='k-widget k-header k-shadow k-navigator']");

            public static By SignaturesBody => By.CssSelector("#canvas");
        }

        public static class Button
        {
            public static By ArrowLeft => By.CssSelector(".k-button.k-button-icon.k-button-bare.k-tabstrip-prev");

            public static By ArrowRight => By.CssSelector("span[class='k-button k-button-icon k-button-bare k-tabstrip-next'] span[class='k-icon k-i-arrow-60-right']");

            public static By CloseButton => By.CssSelector("span[class='k-icon k-i-close']");
        }

        public static class Header
        {
            public static By EventDetails => By.CssSelector("span#kendoWindow_wnd_title");
        }

        public static class Tabs
        {
            public static By ActiveTab => By.CssSelector(".k-item.k-state-default.k-tab-on-top.k-state-active");

            public static By AttachmentsList => By.CssSelector("li[ng-show='eventAttachments != null']");

            public static By AuditHistory => By.CssSelector("li[aria-controls='auditHistoryTab']");

            public static By EndorsementsList => By.CssSelector("li[ng-show='eventEndorsements != null']");

            public static By EventDetailsList => By.CssSelector("li[class='k-state-active k-item k-tab-on-top k-state-default k-first']");

            public static By ItemsList => By.CssSelector("li[ng-show='hasEventItems'] span[class='k-link']");

            public static By JobsList => By.CssSelector("li[ng-show='hasEventJobs']");

            public static By LocationList => By.CssSelector("li[ng-show='event.location != null']");

            public static By SignaturesList => By.CssSelector("li[ng-show='eventSignature != null']");
        }

        public static class Table
        {
            public static By JobsDetails => By.CssSelector("div[k-options='eventJobsGridOptions'] > table");
        }

        public static class Map
        {
            public static By MapBody => By.CssSelector("#eventMapLarge");
        }
    }
}