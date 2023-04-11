using OpenQA.Selenium;

namespace Tempo.TestAutomation.Model.Web.Locators.Pages.CustomerPortal
{
    public static class AddNewConsignmentPageLocators
    {
        public static class Label
        {
            public static By Header => By.CssSelector("div[class*='heading']");
        }

        public static class ServicePane
        {
            public static class DropDownList
            {
                public static By Service => By.CssSelector("kendo-dropdownlist[formcontrolname='serviceId']");
            }

            public static class TextBox
            {
                public static By Reference => By.CssSelector("input[name='referenceNameEdit']");

                public static By ReadyTime => By.CssSelector("kendo-datepicker[name='dateTimeEdit'] input");

                public static By SpecialInstructions => By.CssSelector("input[formcontrolname='details']");
            }
        }

        public static class PickUpDetails
        {
            public static class DropDownField
            {
                public static By Search => By.CssSelector("div[formgroupname='pickupDetail'] kendo-searchbar input");
            }

            public static class TextBox
            {
                public static By ContactName => By.CssSelector("div[formgroupname='pickupDetail'] input[formcontrolname='contactName']");

                public static By Company => By.CssSelector("div[formgroupname='pickupDetail'] input[formcontrolname='companyName']");

                public static By Address => By.CssSelector("div[formgroupname='pickupDetail'] input[formcontrolname='address']");

                public static By Email => By.CssSelector("div[formgroupname='pickupDetail'] input[formcontrolname='contactEmail']");

                public static By Phone => By.CssSelector("div[formgroupname='pickupDetail'] input[formcontrolname='contactPhone']");
            }

            public static class CheckBox
            {
                public static By SaveNewContact => By.CssSelector("label[for='savePickupContact']");
            }
        }

        public static class DeliveryDetails
        {
            public static class DropDownField
            {
                public static By Search => By.CssSelector("div[formgroupname='deliverDetail'] kendo-searchbar input");
            }

            public static class TextBox
            {
                public static By ContactName => By.CssSelector("div[formgroupname='deliverDetail'] input[formcontrolname='contactName']");

                public static By Company => By.CssSelector("div[formgroupname='deliverDetail'] input[formcontrolname='companyName']");

                public static By Address => By.CssSelector("div[formgroupname='deliverDetail'] input[formcontrolname='address']");

                public static By Email => By.CssSelector("div[formgroupname='deliverDetail'] input[formcontrolname='contactEmail']");

                public static By Phone => By.CssSelector("div[formgroupname='deliverDetail'] input[formcontrolname='contactPhone']");
            }

            public static class CheckBox
            {
                public static By SaveNewContact => By.CssSelector("label[for='saveDeliverContact']");
            }
        }

        public static class AddItemDetails
        {
            public static class DropDownList
            {
                public static By PackageType => By.CssSelector("kendo-dropdownlist[formcontrolname='packageType']");
            }

            public static class DropDownField
            {
                public static By UNCode => By.CssSelector("kendo-combobox[formcontrolname='unNumberCode'] input");
            }

            public static class TextBox
            {
                public static By Reference => By.CssSelector("input[formcontrolname='reference']");

                public static By Description => By.CssSelector("input[formcontrolname='description']");

                public static By Quantity => By.CssSelector("kendo-numerictextbox[name='itemCount'] input");

                public static By Weight => By.CssSelector("kendo-numerictextbox[name='weight'] input");

                public static By Length => By.CssSelector("kendo-numerictextbox[name='length'] input");

                public static By Width => By.CssSelector("kendo-numerictextbox[name='width'] input");

                public static By Height => By.CssSelector("kendo-numerictextbox[name='height'] input");

                public static By Volume => By.CssSelector("kendo-numerictextbox[name='volume'] input");

                public static By TotalWeight => By.CssSelector("kendo-numerictextbox[name='totalWeight'] input");

                public static By TotalVolume => By.CssSelector("kendo-numerictextbox[name='totalVolume'] input");
            }
        }

        public static class Button
        {
            public static By AddItem => By.CssSelector("button[class*='small button']");

            public static By Reset => By.CssSelector("button[class*='danger button']");

            public static By Submit => By.CssSelector("button[class*='primary button']");
        }

        public static class ConsignmentConfirmation
        {
            public static class Label
            {
                public static By Message => By.CssSelector("p[class='medium heading']");

                public static By Instruction => By.CssSelector("div[class='centered text']");
            }

            public static class Link
            {
                public static By ConsignmentNote => By.LinkText("Consignment Note");

                public static By Labels100x150 => By.LinkText("Labels (100x150)");

                public static By LabelsA4 => By.LinkText("Labels (A4)");
            }

            public static class Button
            {
                public static By AddAnotherConsignment => By.CssSelector("button[class*='primary']");
            }
        }
    }
}