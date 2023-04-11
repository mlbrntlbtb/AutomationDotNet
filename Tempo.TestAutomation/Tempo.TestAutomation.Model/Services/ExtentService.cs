using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;
using Datacom.TestAutomation.Common;

namespace Tempo.TestAutomation.Model
{
    public class ExtentReporterService
    {
        private static readonly Lazy<ExtentReports> extent = new(() => new ExtentReports());

        public static ExtentReports Instance { get { return extent.Value; } }

        static ExtentReporterService()
        {
            var reporter = new ExtentV3HtmlReporter(GetTestDirectory());
            reporter.Config.Theme = Theme.Dark;
            Instance.AttachReporter(reporter);
        }

        public static string GetTestDirectory()
        {
            return TestSettingsService.Instance.Load<ReportSettings>("ReportSettings").Path + DateUtilities.GetCurrentDateTime("yyyyMMdd_HHmm") + ".html" ?? "/TestResults";
        }

        private ExtentReporterService()
        {
        }
    }
}
