using AventStack.ExtentReports;
using OpenQA.Selenium;

namespace BNZ.TestAutomation.Sample.Tests
{
    public class WebTestSession : TestSession<WebTestSession>
    {
        private readonly TestContext testContext;

        public WebTestSession(TestContext testContext)
        {
            this.testContext = testContext;
        }

        public override TestContext GetContext()
        {
            return testContext;
        }

        public override ExtentReports GetReporter()
        {
            return ExtentReporterService.Instance;
        }

        public override string GetTestName()
        {
            return testContext.TestName!;
        }

        public override void Stop()
        {
            Logger!.LogDebug("Generate report");
            GetReporter().Flush();

            if (Resolve<WebSettings>().CloseBrowserAfterRun)
            {
                Logger!.LogDebug("Stop webdriver");
                Resolve<IWebDriver>().Quit();
                Resolve<IWebDriver>().Dispose();
                base.Stop();
            }
        }
    }
}
