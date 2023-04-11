using Datacom.TestAutomation.Common;
using Datacom.TestAutomation.Web.Selenium;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using Tempo.TestAutomation.Model.Common;
using Tempo.TestAutomation.Model.Web.Locators.Page;

namespace Tempo.TestAutomation.Model.Web
{
    public class LoginPage : TempoBasePage<LoginPage>, ILoadable<LoginPage>
    {
        private readonly IWebDriver driver;
        private readonly ILogger<LoginPage> logger;
        private readonly AppSettings settings;

        public LoginPage(ILogger<LoginPage> logger, AppSettings settings, IWebDriver driver)
            : base(driver)
        {
            this.logger = logger;
            this.settings = settings;
            this.driver = driver;
        }

        public void LoginUser(UserDetails user)
        {
            SetUsername(user.Username);
            if (user.Username == "administrator")
            {
                SetPassword(Environment.GetEnvironmentVariable("TEMPO_PW")!.DecryptXOR(Environment.GetEnvironmentVariable("TEMPO_KEY")!));
                ClickLogin();
            }
            else if (user.Username == "Aut001")
            {
                SetPassword(Environment.GetEnvironmentVariable("TEMPO_ROUTE_USER_PW")!.DecryptXOR(Environment.GetEnvironmentVariable("TEMPO_KEY")!));
                ClickLogin();
            }
        }

        protected override bool EvaluateLoadedStatus()
        {
            bool isLoginPresent = driver.IsElementPresent(LoginPageLocators.Button.Login);
            bool isUsernamePresent = driver.IsElementPresent(LoginPageLocators.Field.Username);
            bool isPasswordPresent = driver.IsElementPresent(LoginPageLocators.Field.Password);

            return isLoginPresent && isUsernamePresent && isPasswordPresent;
        }

        protected override void ExecuteLoad()
        {
            string url = settings.Web!.BaseUrl!;
            logger.LogInformation("Navigate to {url}", url);
            driver.NavigateTo(url);
        }

        private LoginPage ClickLogin()
        {
            driver.GetElement(LoginPageLocators.Button.Login).Click();
            return this;
        }

        private LoginPage SetPassword(string? password)
        {
            if (password.IsNotNullOrEmpty())
                driver.GetElement(LoginPageLocators.Field.Password)
                      .SendKeys(password);

            return this;
        }

        private LoginPage SetUsername(string? username)
        {
            if (username.IsNotNullOrEmpty())
                driver.GetElement(LoginPageLocators.Field.Username).Clear();
            driver.GetElement(LoginPageLocators.Field.Username)
                  .SendKeys(username);

            return this;
        }
    }
}