using Datacom.TestAutomation.Common;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Datacom.TestAutomation.Web.Selenium
{
    public static class SearchContextExtensions
    {
        public static IWebElement GetElement(this ISearchContext searchContext, By locator, Func<IWebElement, bool> condition, TimeSpan? customTimeout = null, string? customMessage = null)
        {
            IWebDriver driver = searchContext.ToDriver();
            TimeSpan timeout = customTimeout ?? TestSettingsService.Instance.Load<WebSettings>().DefaultTimeoutSeconds.Seconds();
            WebDriverWait wait = new(driver, timeout) { Message = customMessage ?? $"Get element => {locator}" };
            wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException));
            wait.Until(
                    driver =>
                    {
                        return condition(searchContext.FindElement(@locator));
                    });
            return searchContext.FindElement(@locator);
        }

        public static IWebElement GetElement(this ISearchContext searchContext, By locator, TimeSpan? customTimeout = null, string? customMessage = null)
        {
            return searchContext.GetElement(locator, e => e.Displayed && e.Enabled, customTimeout, customMessage);
        }

        public static IList<IWebElement> GetElements(this ISearchContext searchContext, By locator, Func<IWebElement, bool> condition, int minNumberOfElements, TimeSpan? customTimeout = null, string? customMessage = null)
        {
            IWebDriver driver = searchContext.ToDriver();
            TimeSpan timeout = customTimeout ?? TestSettingsService.Instance.Load<WebSettings>().DefaultTimeoutSeconds.Seconds();
            WebDriverWait wait = new(driver, timeout) { Message = customMessage ?? $"Get elements => {locator}" };
            wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException),
                                      typeof(InvalidOperationException),
                                      typeof(NoSuchElementException));
            try
            {
                wait.Until(
                    driver =>
                    {
                        return searchContext.FindElements(@locator).Where(condition).Count() >= minNumberOfElements;
                    });

                return searchContext.FindElements(@locator).Where(condition).ToList();
            }
            catch
            {
                return new List<IWebElement>();
            }
        }

        public static IList<IWebElement> GetElements(this ISearchContext searchContext, By locator, int minNumberOfElements, TimeSpan? customTimeout = null, string? customMessage = null)
        {
            return searchContext.GetElements(locator, e => e.Displayed && e.Enabled, minNumberOfElements, customTimeout, customMessage);
        }

        public static IList<IWebElement> GetElements(this ISearchContext searchContext, By locator, Func<IWebElement, bool> condition, TimeSpan? customTimeout = null, string? customMessage = null)
        {
            IWebDriver driver = searchContext.ToDriver();
            TimeSpan timeout = customTimeout ?? TestSettingsService.Instance.Load<WebSettings>().DefaultTimeoutSeconds.Seconds();
            WebDriverWait wait = new(driver, timeout) { Message = customMessage ?? $"Get elements => {locator}" };
            wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException),
                                      typeof(InvalidOperationException));
            wait.Until(
                    driver =>
                    {
                        return searchContext.FindElements(@locator).Any(condition);
                    });
            return searchContext.FindElements(@locator).Where(condition).ToList();
        }

        public static IList<IWebElement> GetElements(this ISearchContext searchContext, By locator, TimeSpan? customTimeout = null, string? customMessage = null)
        {
            return searchContext.GetElements(locator, e => e.Displayed && e.Enabled, customTimeout, customMessage);
        }

        public static bool IsElementPresent(this ISearchContext searchContext, By locator, TimeSpan? customTimeout = null)
        {
            return searchContext.IsElementPresent(locator, e => e.Displayed, customTimeout);
        }

        public static bool IsElementPresent(this ISearchContext searchContext, By locator, Func<IWebElement, bool> condition, TimeSpan? customTimeout = null)
        {
            try
            {
                searchContext.GetElement(locator, condition, customTimeout);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }

        public static bool IsElementEnabled(this ISearchContext searchContext, By locator, TimeSpan? customTimeout = null)
        {
            return searchContext.IsElementEnabled(locator, e => e.Displayed && e.Enabled, customTimeout);
        }

        public static bool IsElementEnabled(this ISearchContext searchContext, By locator, Func<IWebElement, bool> condition, TimeSpan? customTimeout = null)
        {
            try
            {
                searchContext.GetElement(locator, condition, customTimeout);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }

        public static IWebDriver ToDriver(this ISearchContext webElement)
        {
            if (webElement is IWrapsDriver wrappedElement)
            {
                return wrappedElement.WrappedDriver;
            }
            return (IWebDriver)webElement;
        }

        public static void WaitAvailability(this ISearchContext searchContext, By locator, int seconds = 5)
        {
            IWebDriver driver = searchContext.ToDriver();
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, seconds));
            var element = wait.Until(condition =>
            {
                try
                {
                    var elementToBeDisplayed = driver.FindElement(locator);
                    return elementToBeDisplayed.Displayed;
                }
                catch (StaleElementReferenceException)
                {
                    return false;
                }
                catch (NoSuchElementException)
                {
                    return false;
                }
            });
        }

        public static void WaitAvailability(this ISearchContext searchContext, By locator, string textValue, int seconds = 5)
        {
            IWebDriver driver = searchContext.ToDriver();
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, seconds));
            var element = wait.Until(condition =>
            {
                try
                {
                    var elementToBeDisplayed = driver.FindElement(locator);
                    return elementToBeDisplayed.Displayed && elementToBeDisplayed.Text.Trim().Equals(textValue);
                }
                catch (StaleElementReferenceException)
                {
                    return false;
                }
                catch (NoSuchElementException)
                {
                    return false;
                }
            });
        }

        public static void WaitUntilElementAttributeChanged(this ISearchContext searchContext, By locator, string attribute, string initialValue, TimeSpan? customTimeout = null)
        {
            IWebDriver driver = searchContext.ToDriver();
            TimeSpan timeout = customTimeout ?? TestSettingsService.Instance.Load<WebSettings>().DefaultTimeoutSeconds.Seconds();

            WebDriverWait wait = new(driver, timeout);
            wait.Until(driver =>
            {
                return !searchContext.GetElement(locator).GetAttribute(attribute).IsValueEqualsTo(initialValue);
            });
        }

        public static void WaitUntilElementIsNotPresent(this ISearchContext searchContext, By locator, TimeSpan? customTimeout = null)
        {
            IWebDriver driver = searchContext.ToDriver();
            TimeSpan timeout = customTimeout ?? TestSettingsService.Instance.Load<WebSettings>().DefaultTimeoutSeconds.Seconds();
            WebDriverWait wait = new(driver, timeout);
            wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException), typeof(NoSuchElementException));
            wait.Until(driver => !searchContext.IsElementPresent(@locator, 500.Milliseconds()));
        }

        public static void WaitUntilElementIsPresent(this ISearchContext searchContext, By locator, TimeSpan? customTimeout = null)
        {
            IWebDriver driver = searchContext.ToDriver();
            TimeSpan timeout = customTimeout ?? TestSettingsService.Instance.Load<WebSettings>().DefaultTimeoutSeconds.Seconds();
            WebDriverWait wait = new(driver, timeout);
            wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException), typeof(NoSuchElementException));
            wait.Until(driver => !searchContext.IsElementPresent(@locator, 500.Milliseconds()));
        }
    }
}