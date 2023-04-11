using System.Drawing;
using System.Globalization;
using Datacom.TestAutomation.Common;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace Datacom.TestAutomation.Web.Selenium
{
    public static class WebElementExtensions
    {
        /// <summary>
        /// Converts generic IWebElement into specified web element (Checkbox, Table, etc.) .
        /// </summary>
        public static T As<T>(this IWebElement webElement)
            where T : class, IWebElement
        {
            var constructor = typeof(T).GetConstructor(new[] { typeof(IWebElement) });

            if (constructor is not null)
            {
                return (constructor.Invoke(new object[] { webElement }) as T)!;
            }

            throw new ArgumentNullException(string.Format(CultureInfo.CurrentCulture, "Constructor for type {0} is null.", typeof(T)));
        }

        public static IEnumerable<string> GetClasses(this IWebElement webElement)
        {
            try
            {
                return webElement.GetAttribute("class").Split(' ').ToList();
            }
            catch
            {
                return Enumerable.Empty<string>();
            }
        }

        /// <summary>
        /// Returns the textual content of the specified node, and all its descendants regardless element is visible or not.
        /// </summary>
        public static string GetTextContent(this IWebElement webElement)
        {
            var javascript = webElement.ToDriver() as IJavaScriptExecutor;
            if (javascript == null)
            {
                throw new ArgumentException("Element must wrap a web driver that supports javascript execution");
            }

            var textContent = (string)javascript.ExecuteScript("return arguments[0].textContent", webElement);
            return textContent;
        }

        /// <summary>
        /// Returns the value of the specified node
        /// </summary>
        public static string GetValue(this IWebElement webElement)
        {
            return webElement.GetAttribute("value");
        }

        public static bool HasClass(this IWebElement webElement, string value)
        {
            return webElement.GetClasses().Any(c => c.IsValueEqualsTo(value));
        }

        public static void Hover(this IWebElement element, IWebDriver driver, bool ignoreStaleElementException = true)
        {
            try
            {
                Actions action = new(driver);
                action.MoveToElement(element).Perform();
            }
            catch (StaleElementReferenceException)
            {
                if (!ignoreStaleElementException)
                    throw;
            }
        }

        /// <summary>
        /// Verify if actual element text equals to expected.
        /// </summary>
        public static bool IsElementTextContains(this IWebElement webElement, string text, bool ignoreCase = true)
        {
            if (ignoreCase)
            {
                webElement.Text.Contains(text, StringComparison.InvariantCultureIgnoreCase);
            }

            return webElement.Text.Contains(text);
        }

        /// <summary>
        /// Verify if actual element text equals to expected.
        /// </summary>
        public static bool IsElementTextEqualsTo(this IWebElement webElement, string text, bool ignoreCase = true)
        {
            if (ignoreCase)
            {
                webElement.Text.Equals(text, StringComparison.InvariantCultureIgnoreCase);
            }

            return webElement.Text.Equals(text);
        }

        /// <summary>
        /// Click on element using javascript.
        /// </summary>
        public static void JavaScriptClick(this IWebElement webElement)
        {
            if (webElement.ToDriver() is not IJavaScriptExecutor javascript)
            {
                throw new ArgumentException("Element must wrap a web driver that supports javascript execution");
            }

            javascript.ExecuteScript("arguments[0].click();", webElement);
        }

        public static void SendKeys(this IWebElement webElement, string value, bool clear = true)
        {
            if (clear)
            {
                webElement.Clear();
            }

            webElement.SendKeys(value);
        }

        /// <summary>
        /// Set element attribute using javascript.
        /// </summary>
        public static void SetAttribute(this IWebElement webElement, string attribute, string attributeValue)
        {
            if (webElement.ToDriver() is not IJavaScriptExecutor javascript)
            {
                throw new ArgumentException("Element must wrap a web driver that supports javascript execution");
            }

            javascript.ExecuteScript(
                "arguments[0].setAttribute(arguments[1], arguments[2])",
                webElement,
                attribute,
                attributeValue);
        }

        /// <summary>
        /// Checks whether element has attribute
        /// </summary>
        public static bool HasAttribute(this IWebElement webElement, string attribute)
        {
            try
            {
                return webElement.GetAttribute(attribute).IsNotNullOrEmpty();
            }
            catch (Exception)
            {
                return false;
            }
        }

        #region Relative Positions

        public static Func<Point> Above(this IWebElement outer, IWebElement inner) =>
            () =>
            {
                int x = outer.Size.Width / 2;
                int y = (inner.Location.Y - outer.Location.Y) / 2;
                return new Point(x, y);
            };

        public static Func<Point> Below(this IWebElement outer, IWebElement inner) =>
            () =>
            {
                int x = outer.Size.Width / 2;
                var outerEnd = outer.Location + outer.Size;
                var innerEnd = inner.Location + inner.Size;
                int dBelow = outerEnd.Y - innerEnd.Y;
                int y = innerEnd.Y + dBelow / 2;
                return new Point(x, y);
            };

        public static Func<Point> LeftTo(this IWebElement outer, IWebElement inner) =>
            () =>
            {
                int x = (inner.Location.X - outer.Location.X) / 2;
                int y = outer.Size.Height / 2;
                return new Point(x, y);
            };

        public static Func<Point> RightTo(this IWebElement outer, IWebElement inner) =>
            () =>
            {
                var outerEnd = outer.Location + outer.Size;
                var innerEnd = inner.Location + inner.Size;
                int dRight = outerEnd.X - innerEnd.X;
                int x = innerEnd.X + dRight / 2;
                int y = outer.Size.Height / 2;
                return new Point(x, y);
            };

        #endregion Relative Positions
    }
}