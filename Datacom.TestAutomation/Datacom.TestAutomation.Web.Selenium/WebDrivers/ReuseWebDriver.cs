using System.Reflection;
using OpenQA.Selenium;

namespace Datacom.TestAutomation.Web.Selenium
{
    public class ReuseWebDriver : WebDriver
    {
        private readonly string sessionId;
        public ReuseWebDriver(string sessionId, ICommandExecutor commandExecutor, DriverOptions options) : base(commandExecutor, options.ToCapabilities())
        {
            this.sessionId = sessionId;
            var sessionIdBase = GetType().BaseType!
                                         .GetField("sessionId", BindingFlags.Instance |
                                                                BindingFlags.NonPublic)!;
            sessionIdBase.SetValue(this, new SessionId(sessionId));
        }

        protected override Response Execute(string driverCommandToExecute, Dictionary<string, object> parameters)
        {
            if (driverCommandToExecute == DriverCommand.NewSession)
            {
                var resp = new Response
                {
                    Status = WebDriverResult.Success,
                    SessionId = sessionId,
                    Value = new Dictionary<string, object>()
                };
                return resp;
            }
            var respBase = base.Execute(driverCommandToExecute, parameters);
            return respBase;
        }
    }
}

