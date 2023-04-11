using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;
using Datacom.TestAutomation.Common;

namespace BNZ.TestAutomation.Sample.Model
{
    public class ExtentReporterService
    {
        private static readonly Lazy<ExtentReports> extent = new(() => new ExtentReports());

        public static ExtentReports Instance { get { return extent.Value; } }

        static ExtentReporterService()
        {
            var reporter = new ExtentHtmlReporter(GetTestDirectory());
            reporter.Config.Theme = Theme.Dark;
            Instance.AttachReporter(reporter);
        }

        public static string GetTestDirectory()
        {
            return TestSettingsService.Instance.Load<ReportSettings>("ReportSettings").Path!;
        }

        private ExtentReporterService()
        {
        }
    }
}
