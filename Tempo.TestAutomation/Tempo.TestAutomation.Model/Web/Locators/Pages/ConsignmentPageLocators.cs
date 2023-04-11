using OpenQA.Selenium;

namespace Tempo.TestAutomation.Model.Web.Locators.Pages
{
    public class ConsignmentPageLocators
    {
        public static class Button
        {
            public static By Cancel => By.CssSelector("button[ng-click='controller.reset()']");
            public static By EquipmentAdd => By.CssSelector("#consignmentEquipmentGrid a");
            public static By Save => By.CssSelector("button[ng-click='controller.save()']");
        }

        public static class Message
        {
            public static By NotificationMessage => By.CssSelector("span[ng-bind-html='message.content']");
        }

        public static class Table
        {
            public static By Body => By.TagName("body");
            public static By Consignment => By.CssSelector("div[class='flex_panel'] div[class='head_inner ng-binding']");
            public static By ConsignmentEquipment => By.CssSelector("#consignmentEquipmentGrid");
        }

        public static class TextBox
        {
            public static By ConsignmentNotes => By.CssSelector("input[ng-disabled='isSaved']");
            public static By Customer => By.CssSelector(".autocomplete-field.k-input[ng-name='customerLookup']");
            public static By DeliveryAddress => By.CssSelector("pickup-deliver-detail-view[title='Deliver Detail'] input[placeholder='Start Typing...']");
            public static By DeliveryAddressLookup => By.CssSelector("pickup-deliver-detail-view[title='Deliver Detail'] customer-address-lookup input[placeholder='Start typing...']");
            public static By Description => By.CssSelector("textarea[ng-model='consignment.details']");
            public static By PaperworkRequired => By.CssSelector("#IsPaperworkRequired");
            public static By Quantity => By.CssSelector(".k-textbox.ng-pristine.ng-untouched.ng-invalid.ng-invalid-required");
            public static By Reference => By.CssSelector("input[ng-model='consignment.order.reference']");
            public static By ScannedItems => By.CssSelector("#TotalScannedItems");
            public static By Service => By.CssSelector(".autocomplete-field.k-input[ng-name='serviceLookup']");
            public static By Volume => By.CssSelector("input[ng-model='consignment.volume']");
            public static By Weight => By.CssSelector("input[ng-model='consignment.weight']");
        }
    }
}