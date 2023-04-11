using Datacom.TestAutomation.Web.Selenium;
using OpenQA.Selenium;
using Tempo.TestAutomation.Model.Web.Components.Object;
using Tempo.TestAutomation.Model.Web.Components.PageContainers;
using Tempo.TestAutomation.Model.Web.Locators.Modals;

namespace Tempo.TestAutomation.Model.Web.Components.Modals
{
    public class SetPasswordModal : TempoBasePage<SetPasswordModal>, ILoadable<SetPasswordModal>
    {
        private readonly IWebDriver driver;
        private readonly DropDownListContainer dropDownList;
        private readonly LoadingWheel loadingWheel;

        public SetPasswordModal(IWebDriver driver, DropDownListContainer dropDownList, LoadingWheel loadingWheel)
            : base(driver)
        {
            this.driver = driver;
            this.dropDownList = dropDownList;
            this.loadingWheel = loadingWheel;
        }

        public void FillResetPasswordForm()
        {
            driver.GetElement(SetPasswordModalLocators.ResetPasswordForm.TextBox.NewPassword).SendKeys(Environment.GetEnvironmentVariable("TEMPO_RESET_PW_USER_PW")!);
            driver.GetElement(SetPasswordModalLocators.ResetPasswordForm.TextBox.RepeatNewPassword).SendKeys(Environment.GetEnvironmentVariable("TEMPO_RESET_PW_USER_PW")!);
        }

        public void ClickSave()
        {
            driver.GetElement(SetPasswordModalLocators.ResetPasswordForm.Button.Save).Click();
            loadingWheel.WaitToDisappear();
        }

        protected override bool EvaluateLoadedStatus()
        {
            bool IsNewPasswordPresent = driver.IsElementPresent(SetPasswordModalLocators.ResetPasswordForm.TextBox.NewPassword);
            bool IsNewPasswordEnabled = driver.GetElement(SetPasswordModalLocators.ResetPasswordForm.TextBox.NewPassword).Enabled;

            return IsNewPasswordPresent && IsNewPasswordEnabled;
        }
    }
}