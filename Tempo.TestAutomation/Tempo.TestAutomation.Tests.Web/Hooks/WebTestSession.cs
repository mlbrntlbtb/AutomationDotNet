using AventStack.ExtentReports;
using Datacom.TestAutomation.Common;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium;

namespace Tempo.TestAutomation.Tests.Web
{
    public class WebTestSession : TestSession<WebTestSession>
    {
        public override void Start()
        {
            base.Start();

            if (!Resolve<WebSettings>().ReuseWebDriver)
            {
                Logger!.LogDebug("SessionID: {sessionId}", Resolve<IWebDriver>().GetSessionId());
                Logger!.LogDebug("ExecutorURL: {sxecutorURL}", Resolve<IWebDriver>().GetExecutorURL());
            }
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

        public override string GetTestName()
        {
            return TestContext.CurrentContext.Test.Name;
        }

        public override TestContext GetContext()
        {
            return TestContext.CurrentContext;
        }

        public override ExtentReports GetReporter()
        {
            return ExtentReporterService.Instance;
        }
    }
}