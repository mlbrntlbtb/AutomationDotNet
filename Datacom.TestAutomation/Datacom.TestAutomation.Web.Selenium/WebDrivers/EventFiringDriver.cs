using System.Globalization;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Events;

namespace Datacom.TestAutomation.Web.Selenium
{
    public class EventFiringDriver : EventFiringWebDriver
    {
        protected ILogger Logger;
        public EventFiringDriver(ILogger logger, IWebDriver driver) : base(driver)
        {
            Logger = logger;
        }

        protected override void OnElementClicking(WebElementEventArgs e)
        {
            Logger.LogTrace("Clicking: {element}", ToStringElement(e));
            base.OnElementClicking(e);
        }

        protected override void OnElementClicked(WebElementEventArgs e)
        {
            Logger.LogTrace("Clicked: {element}", ToStringElement(e));
            base.OnElementClicked(e);
        }

        protected override void OnElementValueChanged(WebElementValueEventArgs e)
        {
            Logger.LogTrace("On Element Value Changed: {element}", ToStringElement(e));
            base.OnElementValueChanging(e);
        }

        protected override void OnElementValueChanging(WebElementValueEventArgs e)
        {
            Logger.LogTrace("On Element Value Changing: {element}", ToStringElement(e));
            base.OnElementValueChanging(e);
        }

        protected override void OnFindingElement(FindElementEventArgs e)
        {
            Logger.LogTrace("On Finding Element: {element}", e.FindMethod);
            base.OnFindingElement(e);
        }

        protected override void OnFindElementCompleted(FindElementEventArgs e)
        {
            Logger.LogTrace("Found Element: {element}", e.FindMethod);
            base.OnFindElementCompleted(e);
        }

        protected override void OnNavigating(WebDriverNavigationEventArgs e)
        {
            Logger.LogTrace("Navigating to: {url}", e.Url);
            base.OnNavigating(e);
        }
        protected override void OnScriptExecuted(WebDriverScriptEventArgs e)
        {
            Logger.LogTrace("On Script Executed: {script}", e.Script);
            base.OnScriptExecuted(e);
        }

        protected override void OnScriptExecuting(WebDriverScriptEventArgs e)
        {
            Logger.LogTrace("On Script Executing: {script}", e.Script);
            base.OnScriptExecuting(e);
        }
        private static string AppendAttribute(WebElementEventArgs e, string attribute)
        {
            var attrValue = attribute == "text" ? e.Element.Text : e.Element.GetAttribute(attribute);
            return string.IsNullOrEmpty(attrValue) ? string.Empty : string.Format(CultureInfo.CurrentCulture, " {0}='{1}' ", attribute, attrValue);
        }

        private static string ToStringElement(WebElementEventArgs e)
        {
            return string.Format(
                CultureInfo.CurrentCulture,
                "{0}{{{1}{2}{3}{4}{5}{6}{7}{8}}}",
                e.Element.TagName,
                AppendAttribute(e, "id"),
                AppendAttribute(e, "name"),
                AppendAttribute(e, "value"),
                AppendAttribute(e, "class"),
                AppendAttribute(e, "type"),
                AppendAttribute(e, "role"),
                AppendAttribute(e, "text"),
                AppendAttribute(e, "href"));
        }
    }
}
