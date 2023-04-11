using AventStack.ExtentReports;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;

namespace Tempo.TestAutomation.Model.Interface
{ 
    public class ScreenCaptureService : IScreenCaptureService
    {
        private readonly IWebDriver driver;
        public ScreenCaptureService(IWebDriver driver)
        {
            this.driver = driver;
        }
        public MediaEntityModelProvider CaptureScreenImage()
        {
           return MediaEntityBuilder.CreateScreenCaptureFromBase64String(driver.TakeScreenshot().ToString()).Build();
        }

    }
}
