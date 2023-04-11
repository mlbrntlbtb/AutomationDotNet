using OpenQA.Selenium;

namespace Tempo.TestAutomation.Model.Web.Locators.Modal
{
    public class AddJobModalLocators
    {
        public static class DeliveryDetailForm
        {
            public static class TextBox
            {
                public static By Address => By.CssSelector("form[name='deliverDetailForm'] input[name='address']");
                public static By EmailAddress => By.CssSelector("form[name='deliverDetailForm'] input[name='contactEmail']");
            }
        }

        public static class Footer
        {
            public static class Button
            {
                public static By Save => By.CssSelector(".modalPopupFooter button[type='submit']");
            }
        }

        public static class Header
        {
            public static By AddJob => By.XPath("//job-edit[@job-id]//div[contains(@class, 'head_inner')]/span[contains(text(),'Add Job')]");
        }

        public static class JobForm
        {
            public static class Checkbox
            {
                public static By CreateOrder => By.CssSelector(".create_order_checkbox");
            }

            public static class Listbox
            {
                public static By Jobtype => By.CssSelector("label[for='jobType'] ~ span[role='listbox'] span.k-select");
            }

            public static class OrderForm
            {
                public static class ListBox
                {
                    public static By Customer => By.CssSelector("div.k-list-scroller > ul");
                    public static By Service => By.CssSelector("div.k-list-scroller > ul");
                }

                public static class TextBox
                {
                    public static By Customer => By.CssSelector("#lookupOrderCustomer input");
                    public static By Reference => By.CssSelector("input[name='orderReference']");
                    public static By Service => By.XPath("//label[@for='orderServiceDropDown']/parent::div/following-sibling::span//span[contains(@class,'k-input')]");
                }
            }

            public static class TextBox
            {
                public static By ActionBranch = By.CssSelector("input[name='actionBranch']");
                public static By ScheduledDate = By.CssSelector("input[name='scheduledDate']");
                public static By ItemCount => By.CssSelector("input[name='itemCount']");
                public static By OrderInternalNotes => By.CssSelector("textarea[name='orderNotes']");
                public static By Reference => By.CssSelector("input[name='jobReference']");
                public static By Route => By.CssSelector("#routeAutoComplete");
                public static By SpecialInstruction => By.CssSelector("textarea[name='details']");
            }
        }

        public static class OrderPricingForm
        {
            public static class Navbar
            {
                public static class Button
                {
                    public static By AddSurcharge => By.CssSelector("button[data-tooltip='Add Surcharge']");
                }
            }
        }

        public static class PickUpDetailForm
        {
            public static class TextBox
            {
                public static By Address => By.CssSelector("form[name='pickupDetailForm'] input[name='address']");
                public static By EmailAddress => By.CssSelector("form[name='pickupDetailForm'] input[name='contactEmail']");
                public static By Suburb => By.CssSelector("input[name='suburb']");
            }

            public static class Dropdown
            {
                public static By FirstAddress => By.XPath("//div[@class='multi-line-list-item']");
            }
        }
    }
}