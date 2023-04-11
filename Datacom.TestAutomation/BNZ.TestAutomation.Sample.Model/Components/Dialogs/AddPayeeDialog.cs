using Datacom.TestAutomation.Common;
using Datacom.TestAutomation.Web.Selenium;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium;

namespace BNZ.TestAutomation.Sample.Model
{
    public class AddPayeeDialog : Loadable<AddPayeeDialog>, ILoadable<AddPayeeDialog>
    {
        private readonly ILogger<AddPayeeDialog> logger;
        private readonly IWebDriver driver;

        public AddPayeeDialog(ILogger<AddPayeeDialog> logger, IWebDriver driver)
        {
            this.logger = logger;
            this.driver = driver;
        }

        public AddPayeeDialog AddPayee(PayeeDetails payee)
        {
            logger.LogInformation("Add payee: {payee}", payee);

            if (payee.Name.IsNotNullOrEmpty())
            {
                driver.GetElement(AddPayeeDialogLocators.Fields.PayeeName).SendKeys(payee?.Name);
                driver.GetElement(AddPayeeDialogLocators.Buttons.SomeoneNew).Click();
            }

            if (payee?.Account is not null)
            {
                driver.GetElement(AddPayeeDialogLocators.Fields.BankCode).SendKeys(payee?.Account?.BankCode);
                driver.GetElement(AddPayeeDialogLocators.Fields.BranchCode).SendKeys(payee?.Account?.BranchCode);
                driver.GetElement(AddPayeeDialogLocators.Fields.AccountNumber).SendKeys(payee?.Account?.AccountNumber);
                driver.GetElement(AddPayeeDialogLocators.Fields.Suffix).SendKeys(payee?.Account?.Suffix);
            }

            driver.GetElement(AddPayeeDialogLocators.Buttons.Add).Click();

            return this;
        }

        protected override bool EvaluateLoadedStatus()
        {
            return driver.WaitForPageToLoad() && driver.IsElementPresent(AddPayeeDialogLocators.Fields.PayeeName);
        }

        protected override void ExecuteLoad()
        {
            driver.WaitForPageToLoad();
        }
    }
}
