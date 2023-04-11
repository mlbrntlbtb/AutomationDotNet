using System.Diagnostics.CodeAnalysis;

namespace Datacom.TestAutomation.Web.Selenium
{
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)]
    public class WebSettings
    {
        public string Browser { get; set; } = "chrome";
        public int CommandTimeoutSeconds { get; set; } = 60;
        public int DefaultTimeoutSeconds { get; set; } = 30;
        public string DownloadDirectory { get; set; } = string.Empty;
        public string DriverPath { get; set; } = string.Empty;
        public bool EnableEventFiringWebDriver { get; set; } = false;
        public bool Headless { get; set; } = false;
        public string? RemoteHubServer { get; set; }
        public bool ReuseWebDriver { get; set; } = false;
        public string? SessionID { get; set; }
        public Uri? ExecutorURL { get; set; }
        public bool CloseBrowserAfterRun { get; set; } = true;
    }
}
