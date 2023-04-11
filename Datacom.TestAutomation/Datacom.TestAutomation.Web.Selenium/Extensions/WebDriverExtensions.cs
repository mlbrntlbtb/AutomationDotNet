using System.Globalization;
using System.Reflection;
using Datacom.TestAutomation.Common;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;

namespace Datacom.TestAutomation.Web.Selenium
{
    public static class WebDriverExtensions
    {
        public static Actions Actions(this IWebDriver driver)
        {
            return new Actions(driver);
        }

        public static void DragAndDropJs(this IWebDriver driver, IWebElement source, IWebElement destination)
        {
            var script =
                "function createEvent(typeOfEvent) { " +
                   "var event = document.createEvent(\"CustomEvent\"); " +
                   "event.initCustomEvent(typeOfEvent, true, true, null); " +
                   "event.dataTransfer = { " +
                            "data: { }, " +
                        "setData: function(key, value) { " +
                                "this.data[key] = value; " +
                            "}, " +
                        "getData: function(key) { " +
                               "return this.data[key]; " +
                            "} " +
                        "}; " +
                    "return event; " +
                        "} " +
                        "function dispatchEvent(element, event, transferData) { " +
                            "if (transferData !== undefined)" +
                            "{" +
                        "event.dataTransfer = transferData;" +
                        "}" +
                    "if (element.dispatchEvent) {" +
                        "element.dispatchEvent(event);" +
                        "} else if (element.fireEvent) {" +
                        "element.fireEvent(\"on\" + event.type, event);" +
                        "}" +
                    "}" +
                    "function simulateHTML5DragAndDrop(element, target)" +
                    "{" +
                        "var dragStartEvent = createEvent('dragstart');" +
                        "dispatchEvent(element, dragStartEvent);" +
                        "var dropEvent = createEvent('drop');" +
                        "dispatchEvent(target, dropEvent, dragStartEvent.dataTransfer);" +
                        "var dragEndEvent = createEvent('dragend');" +
                        "dispatchEvent(element, dragEndEvent, dropEvent.dataTransfer);" +
                    "} simulateHTML5DragAndDrop(arguments[0], arguments[1])";

            driver.ExecuteJavaScript(script, source, destination);
        }

        public static Uri GetExecutorURL(this IWebDriver driver)
        {
            WebDriver? webDriver;
            var isEventDriver = TestSettingsService.Instance.Load<WebSettings>().EnableEventFiringWebDriver;
            if (isEventDriver)
            {
                EventFiringDriver eventDriver = (driver as EventFiringDriver)!;
                IWrapsDriver wrapperAccess = eventDriver!;
                webDriver = (WebDriver)wrapperAccess.WrappedDriver;
            }
            else
            {
                webDriver = (WebDriver)driver;
            }

            FieldInfo executorField = typeof(WebDriver).GetField("executor",
                                                                  BindingFlags.NonPublic
                                                                  | BindingFlags.Instance)!;
            object executor = executorField!.GetValue(webDriver)!;

            FieldInfo internalExecutorField = executor.GetType()
                                                      .GetField("internalExecutor",
                                                                BindingFlags.NonPublic
                                                                | BindingFlags.Instance)!;
            object internalExecutor = internalExecutorField!.GetValue(executor)!;

            FieldInfo remoteServerUriField = internalExecutor.GetType()
                                                             .GetField("remoteServerUri",
                                                                       BindingFlags.NonPublic
                                                                       | BindingFlags.Instance)!;
            Uri remoteServerUri = (remoteServerUriField.GetValue(internalExecutor) as Uri)!;

            return remoteServerUri;
        }

        public static string GetSessionId(this IWebDriver driver)
        {
            WebDriver? webDriver;
            bool isEventDriver = TestSettingsService.Instance.Load<WebSettings>().EnableEventFiringWebDriver;
            if (isEventDriver)
            {
                EventFiringDriver eventDriver = (driver as EventFiringDriver)!;
                IWrapsDriver wrapperAccess = eventDriver!;
                webDriver = (WebDriver)wrapperAccess.WrappedDriver;
            }
            else
            {
                webDriver = (WebDriver)driver;
            }

            string sessionID = webDriver.SessionId.ToString();

            return sessionID;
        }

        public static bool IsPageTitle(this IWebDriver driver, string pageTitle, TimeSpan? customTimeout = null)
        {
            TimeSpan timeout = customTimeout ?? TestSettingsService.Instance.Load<WebSettings>().DefaultTimeoutSeconds.Seconds();
            WebDriverWait wait = new(driver, timeout);

            try
            {
                return wait.Until(d => d.Title.Equals(pageTitle, StringComparison.InvariantCultureIgnoreCase));
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }

        public static JavaScriptAlert JavaScriptAlert(this IWebDriver driver)
        {
            return new JavaScriptAlert(driver);
        }

        public static IWebDriver LastWindow(this IWebDriver driver)
            => driver.SwitchTo().Window(driver.WindowHandles.Last());

        public static void NavigateTo(this IWebDriver driver, Uri url)
        {
            driver.Navigate().GoToUrl(url);
        }

        public static void NavigateTo(this IWebDriver driver, string url)
        {
            driver.NavigateTo(new Uri(url));
        }

        public static bool PageSourceContainsText(this IWebDriver driver, string text, TimeSpan? customTimeout = null, bool isCaseSensitive = true)
        {
            TimeSpan timeout = customTimeout ?? TestSettingsService.Instance.Load<WebSettings>().DefaultTimeoutSeconds.Seconds();

            Func<IWebDriver, bool> condition;

            if (isCaseSensitive)
            {
                condition = drv => drv.PageSource.Contains(text);
            }
            else
            {
                condition = drv => drv.PageSource.ToUpperInvariant().Contains(text.ToUpperInvariant());
            }

            if (timeout.TotalMilliseconds >= 0)
            {
                var wait = new WebDriverWait(driver, timeout);
                return wait.Until(condition);
            }

            return condition.Invoke(driver);
        }

        public static void ScrollIntoMiddle(this IWebDriver driver, By locator)
        {
            var element = driver.ToDriver().GetElement(locator);
            var height = driver.Manage().Window.Size.Height;
            var hoverItem = (ILocatable)element;
            var locationY = hoverItem.LocationOnScreenOnceScrolledIntoView.Y;
            driver.ExecuteJavaScript(string.Format(CultureInfo.InvariantCulture, "javascript:window.scrollBy(0,{0})", locationY - (height / 2)));
        }

        public static IWebDriver SwitchToFirstWindow(this IWebDriver driver)
            => driver.SwitchTo().Window(driver.WindowHandles.First());

        public static IWebDriver SwitchToLastWindow(this IWebDriver driver)
            => driver.SwitchTo().Window(driver.WindowHandles.Last());

        public static void SwitchToWindowUsingUrl(this IWebDriver driver, Uri url, TimeSpan? customTimeout = null)
        {
            TimeSpan timeout = customTimeout ?? TestSettingsService.Instance.Load<WebSettings>().DefaultTimeoutSeconds.Seconds();
            WebDriverWait wait = new(driver, timeout);
            wait.Until(
                driver =>
                {
                    foreach (var handle in driver.WindowHandles)
                    {
                        driver.SwitchTo().Window(handle);
                        if (driver.Url.Equals(url.ToString()))
                        {
                            return true;
                        }
                    }
                    return false;
                });
        }

        public static void WaitForAjax(this IWebDriver driver, TimeSpan? customTimeout = null)
        {
            try
            {
                TimeSpan timeout = customTimeout ?? TestSettingsService.Instance.Load<WebSettings>().DefaultTimeoutSeconds.Seconds();
                WebDriverWait wait = new(driver, timeout);
                wait.Until(
                    driver =>
                    {
                        return driver.ExecuteJavaScript<bool>("return jQuery.active == 0");
                    });
            }
            catch (InvalidOperationException)
            {
                //Ignore exception
            }
        }

        public static void WaitForAngular(this IWebDriver driver, TimeSpan? customTimeout = null)
        {
            try
            {
                TimeSpan timeout = customTimeout ?? TestSettingsService.Instance.Load<WebSettings>().DefaultTimeoutSeconds.Seconds();
                WebDriverWait wait = new(driver, timeout);
                wait.Until(
                    driver =>
                    {
                        return driver.ExecuteJavaScript<bool>(
                            "return window.angular != undefined && window.angular.element(document.body).injector().get('$http').pendingRequests.length == 0");
                    });
            }
            catch (InvalidOperationException)
            {
                //Ignore exception
            }
        }

        public static bool WaitForPageToLoad(this IWebDriver driver, TimeSpan? customTimeout = null)
        {
            string state = string.Empty;
            try
            {
                TimeSpan timeout = customTimeout ?? TestSettingsService.Instance.Load<WebSettings>().DefaultTimeoutSeconds.Seconds();
                WebDriverWait wait = new(driver, timeout);

                //Checks every 500 ms whether predicate returns true if returns exit otherwise keep trying till it returns ture
                wait.Until(d =>
                {
                    try
                    {
                        state = driver.ExecuteJavaScript<string>(@"return document.readyState");
                    }
                    catch (InvalidOperationException)
                    {
                        //Ignore
                    }
                    catch (NoSuchWindowException)
                    {
                        //when popup is closed, switch to last windows
                        driver.LastWindow();
                    }

                    //In IE7 there are chances we may get state as loaded instead of complete
                    return (state!.Equals("complete", StringComparison.InvariantCultureIgnoreCase));
                });
            }
            catch (TimeoutException)
            {
                //sometimes Page remains in Interactive mode and never becomes Complete, then we can still try to access the controls
                if (!state!.Equals("interactive", StringComparison.InvariantCultureIgnoreCase))
                    throw;
            }
            catch (NullReferenceException)
            {
                //sometimes Page remains in Interactive mode and never becomes Complete, then we can still try to access the controls
                if (!state!.Equals("interactive", StringComparison.InvariantCultureIgnoreCase))
                    throw;
            }
            catch (WebDriverException)
            {
                if (driver.WindowHandles.Count == 1)
                {
                    driver.SwitchTo().Window(driver.WindowHandles[0]);
                }

                state = driver.ExecuteJavaScript<string>(@"return document.readyState");
                if (!(state.Equals("complete", StringComparison.InvariantCultureIgnoreCase) || state.Equals("loaded", StringComparison.InvariantCultureIgnoreCase)))
                    throw;
            }

            return true;
        }

        public static object? WaitForScript(this IWebDriver driver, string script, TimeSpan? customTimeout = null)
        {
            TimeSpan timeout = customTimeout ?? TestSettingsService.Instance.Load<WebSettings>().DefaultTimeoutSeconds.Seconds();
            WebDriverWait wait = new(driver, timeout);

            wait.Until(d =>
            {
                try
                {
                    object returnValue = driver.ExecuteJavaScript<object>(script);

                    return returnValue;
                }
                catch (InvalidOperationException)
                {
                    return null;
                }
                catch (WebDriverException)
                {
                    return null;
                }
            });

            return null;
        }
    }
}