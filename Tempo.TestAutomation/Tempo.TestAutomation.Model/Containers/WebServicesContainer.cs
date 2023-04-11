using Datacom.TestAutomation.Common;
using Datacom.TestAutomation.Web.Selenium;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Tempo.TestAutomation.Model.Interface;
using Tempo.TestAutomation.Model.Web.Components.Elements;
using Tempo.TestAutomation.Model.Web.Components.Object;
using Tempo.TestAutomation.Model.Web.Components.PageContainers;

namespace Tempo.TestAutomation.Model
{
    public class WebServicesContainer : IServiceContainer
    {
        public void Register(IServiceCollection collection)
        {
            collection
                .AddOptions()
                .AddLogging(builder => builder.AddSimpleConsole(options =>
                {
                    options.TimestampFormat = "HH:mm:ss ";
                    options.SingleLine = true;
                    options.IncludeScopes = true;
                }))
                .AddLogging(builder => builder.AddDebug().AddConfiguration())
                .AddLogging(logger => logger.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace))
                .AddScoped(typeof(ILogger<>), typeof(Logger<>))
                .UseTestSettings<AppSettings>("AppSettings")
                .UseTestSettings<WebSettings>()
                .AddWebDriverFactories()
                .AddScoped(provider =>
                {
                    WebSettings settings = provider.GetRequiredService<WebSettings>();
                    ILogger<EventFiringDriver> logger = provider.GetRequiredService<ILogger<EventFiringDriver>>();

                    if (settings.EnableEventFiringWebDriver)
                    {
                        return new EventFiringDriver(logger, new WebDriverFactory(provider).Create());
                    }
                    else
                    {
                        return new WebDriverFactory(provider).Create();
                    }
                })
                .AddScoped(provider =>
                {
                    IWebDriver driver = provider.GetRequiredService<IWebDriver>();
                    WebSettings settings = provider.GetRequiredService<WebSettings>();
                    WebDriverWait wait = new(driver, settings.DefaultTimeoutSeconds.Seconds());
                    wait.IgnoreExceptionTypes(typeof(NoSuchElementException),
                                              typeof(StaleElementReferenceException),
                                              typeof(ElementClickInterceptedException),
                                              typeof(ElementNotInteractableException),
                                              typeof(ElementNotVisibleException),
                                              typeof(InvalidOperationException));
                    return wait;
                })
                .AddScoped(provider =>
                {
                    IWebDriver driver = provider.GetRequiredService<IWebDriver>();
                    WebSettings settings = provider.GetRequiredService<WebSettings>();
                    WebDriverWait wait = new TryWebDriverWait(driver, settings.DefaultTimeoutSeconds.Seconds());
                    wait.IgnoreExceptionTypes(typeof(NoSuchElementException),
                                              typeof(StaleElementReferenceException),
                                              typeof(ElementClickInterceptedException),
                                              typeof(ElementNotInteractableException),
                                              typeof(ElementNotVisibleException),
                                              typeof(InvalidOperationException));
                    return wait;
                })
                .AddLoadableComponents()
                .AddScoped<IComponentFactory, ComponentFactory>()
                .AddTransient<IScreenCaptureService, ScreenCaptureService>()
                .AddScoped<DropDownListContainer>()
                .AddScoped<LoadingWheel>()
                .AddScoped<TableFilter>()
                .AddScoped<ToastMessage>()
                .AddScoped<NavigationBar>()
                .AddScoped<LoadingProgressBar>()
                .AddScoped<Tab>()
                .AddScoped<CheckBox>();
        }
    }
}