using Datacom.TestAutomation.Common;
using Datacom.TestAutomation.Web.Selenium;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using Tempo.TestAutomation.Model.Common;
using Tempo.TestAutomation.Model.Web.Locators.Pages.CustomerPortal;

namespace Tempo.TestAutomation.Model.Web.Components.Pages.CustomerPortal
{
    public class LoginPage : TempoBasePage<LoginPage>, ILoadable<LoginPage>
    {
        private readonly IWebDriver driver;
        private readonly ILogger<LoginPage> logger;
        private readonly AppSettings settings;

        public LoginPage(ILogger<LoginPage> logger, AppSettings settings, IWebDriver driver) : base(driver)
        {
            this.logger = logger;
            this.settings = settings;
            this.driver = driver;
        }

        public void LoginUserCustomer(UserDetails userDetails)
        {
            SetUsername(userDetails.Username);
            string? decryptedPW;
            switch (userDetails.Username)
            {
                case "perf050":
                    decryptedPW = Environment.GetEnvironmentVariable("TEMPO_CUSTOMER_PORTAL_PW")!.DecryptXOR(Environment.GetEnvironmentVariable("TEMPO_KEY")!);
                    break;

                default:
                    throw new Exception("User customer details not recognized.");
            }
            SetPassword(decryptedPW);
            ClickLogin();
        }

        protected override bool EvaluateLoadedStatus()
        {
            bool isLoginPresent = driver.IsElementPresent(LoginPageLocators.Button.Login);
            bool isUsernamePresent = driver.IsElementPresent(LoginPageLocators.TextBox.Username);
            bool isPasswordPresent = driver.IsElementPresent(LoginPageLocators.TextBox.Password);
            bool isInstructionsPresent = driver.IsElementPresent(LoginPageLocators.Label.Instructions);

            return isLoginPresent & isUsernamePresent & isPasswordPresent & isInstructionsPresent;
        }

        protected override void ExecuteLoad()
        {
            string url = settings.Web!.CustomerPortalUrl!;
            logger.LogInformation("Navigate to {url}", url);
            driver.NavigateTo(url);
        }

        private void ClickLogin()
        {
            driver.GetElement(LoginPageLocators.Button.Login).Click();
        }

        private void SetPassword(string? password)
        {
            if (password.IsNotNullOrEmpty())
            {
                IWebElement Password = driver.GetElement(LoginPageLocators.TextBox.Password);
                Password.Clear();
                Password.SendKeys(password);
            }
        }

        private void SetUsername(string? username)
        {
            if (username.IsNotNullOrEmpty())
            {
                IWebElement Username = driver.GetElement(LoginPageLocators.TextBox.Username);
                Username.Clear();
                Username.SendKeys(username);
            }
        }
    }
}