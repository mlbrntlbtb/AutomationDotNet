using AventStack.ExtentReports;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;

namespace BNZ.TestAutomation.Sample.Tests
{
    [TestClass]
    public class BaseTest
    {
        protected WebTestSession? TestSession;
        protected ExtentTest? Test;
        protected IComponentFactory? ComponentFactory;
        protected ILogger<BaseTest>? Logger;
        public TestContext? TestContext { get; set; }

        [TestInitialize]
        public void Setup()
        {
            TestSession = new WebTestSession(TestContext!);
            TestSession.Start();

            ComponentFactory = TestSession!.Resolve<IComponentFactory>()!;
            Logger = TestSession!.Resolve<ILogger<BaseTest>>();

            Test = TestSession.GetReporter().CreateTest($"{TestSession.GetTestName()}");
            Logger.LogInformation("Start {name}", TestSession.GetTestName());
        }

        [TestCleanup]
        public void CleanUp()
        {
            if (TestContext!.CurrentTestOutcome != UnitTestOutcome.Passed)
            {
                Test!.Fail($"{TestSession!.GetTestName()} failed",
                    MediaEntityBuilder.CreateScreenCaptureFromBase64String(TestSession!.Resolve<IWebDriver>().TakeScreenshot().ToString()).Build());
            }
            else
            {
                Test!.Pass($"{TestSession!.GetTestName()} passed",
                    MediaEntityBuilder.CreateScreenCaptureFromBase64String(TestSession!.Resolve<IWebDriver>().TakeScreenshot().ToString()).Build());
            }

            TestSession!.Stop();

            Logger!.LogInformation("Test Directory: {path}", ExtentReporterService.GetTestDirectory());
        }
    }
}