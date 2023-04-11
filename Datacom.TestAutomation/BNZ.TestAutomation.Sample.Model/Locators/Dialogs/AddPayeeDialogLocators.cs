using OpenQA.Selenium;

namespace BNZ.TestAutomation.Sample.Model
{
    public static class AddPayeeDialogLocators
    {
        public static class Fields
        {
            public static By PayeeName => By.CssSelector("#ComboboxInput-apm-name");
            public static By BankCode => By.CssSelector("[data-rv-value='account.bankCode']");
            public static By BranchCode => By.CssSelector("[data-rv-value='account.branchCode']");
            public static By AccountNumber => By.CssSelector("[data-rv-value='account.accountNumber']");
            public static By Suffix => By.CssSelector("[data-rv-value='account.suffix']");
        }

        public static class Buttons
        {
            public static By SomeoneNew => By.CssSelector("[data-cb-type='new-payee'] .item");
            public static By Add => By.CssSelector(".js-submit");
        }

        public static class Text
        {
            public static By ErrorHeader => By.CssSelector(".error-header");
            public static By ErrorToolTip => By.CssSelector(".text.js-tooltip-text");
        }
    }
}
