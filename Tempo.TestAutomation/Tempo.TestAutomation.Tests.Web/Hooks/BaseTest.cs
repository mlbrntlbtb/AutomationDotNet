using AventStack.ExtentReports;
using Datacom.TestAutomation.Common;
using Microsoft.Extensions.Logging;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using Tempo.TestAutomation.Model.Common;
using Tempo.TestAutomation.Model.Interface;

namespace Tempo.TestAutomation.Tests.Web
{
    [Parallelizable(ParallelScope.Fixtures)]
    public class BaseTest
    {
        protected WebTestSession? TestSession;
        protected ExtentTest? Test;
        protected IComponentFactory? PageFactory;
        protected ILogger<BaseTest>? Logger;
        protected IScreenCaptureService? ScreenCaptureService;
        protected UserDetails? AdminUser;
        protected WebSettings? WebSettings;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            TestSession = new WebTestSession();
            TestSession.Start();

            PageFactory = TestSession!.Resolve<IComponentFactory>();
            Logger = TestSession!.Resolve<ILogger<BaseTest>>();
            ScreenCaptureService = TestSession!.Resolve<IScreenCaptureService>();
            WebSettings = TestSession!.Resolve<WebSettings>();

            //========================================================================================
            //Load Test Data
            //========================================================================================
            AdminUser = TestDataService.Instance.Load<UserDetails>("TestUsers:Admin");
        }

        [SetUp]
        public void SetUp()
        {
            Test = TestSession!.GetReporter().CreateTest($"{TestSession.GetTestName()}");
            Logger!.LogInformation("Start {name}", TestSession.GetTestName());
        }

        [TearDown]
        public void TearDown()
        {
            if (TestSession!.GetContext().Result.Outcome != ResultState.Success)
            {
                Logger!.LogFail(Test!, $"Test failed due to {TestSession.GetContext().Result.Message}",
                    MediaEntityBuilder.CreateScreenCaptureFromBase64String(TestSession!.Resolve<IWebDriver>().TakeScreenshot().ToString()).Build());
            }

            TestSession!.Stop();

            Logger!.LogDebug("Test Directory: {path}", ExtentReporterService.GetTestDirectory());
            TestContext.WriteLine(ExtentReporterService.GetTestDirectory());
        }
    }
}