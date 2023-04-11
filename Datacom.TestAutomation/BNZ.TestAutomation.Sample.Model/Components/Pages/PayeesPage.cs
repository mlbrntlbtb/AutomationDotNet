using System.Diagnostics.Metrics;
using System.Linq;
using System.Text.RegularExpressions;
using Datacom.TestAutomation.Web.Selenium;
using OpenQA.Selenium;

namespace BNZ.TestAutomation.Sample.Model
{
    public class PayeesPage : BasePage<PayeesPage>, ILoadable<PayeesPage>
    {
        private readonly IComponentFactory pageFactory;
        private readonly AppSettings settings;
        private readonly IWebDriver driver;

        public PayeesPage(IComponentFactory pageFactory, AppSettings settings, IWebDriver driver)
            : base(driver)
        {
            this.pageFactory = pageFactory;
            this.settings = settings;
            this.driver = driver;
        }

        public PayeesPage AddPayee(PayeeDetails payee)
        {
            driver.GetElement(PayeesPageLocators.Buttons.Add).Click();
            pageFactory.GetComponent<AddPayeeDialog>().AddPayee(payee);
            return this;
        }

        public string GetNotification()
        {
            return driver.GetElement(PayeesPageLocators.Texts.Notification).Text;
        }

        public string GetHeader()
        {
            return driver.GetElement(PayeesPageLocators.Texts.PayeesHeader).Text;
        }

        public SortOrder GetSortOrder()
        {
            string label = driver.GetElement(PayeesPageLocators.Tables.PayeeTable.NameColumn).GetAttribute("aria-label");

            Regex regx = new(@"(?s)(?<=name ).+?(?= selected)", RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);

            MatchCollection matches = regx.Matches(label);

            if (matches.Count > 0)
            {
                return matches?.FirstOrDefault()?.Value switch
                {
                    "A to Z" => SortOrder.Ascending,
                    "Z to A" => SortOrder.Descending,
                    _ => SortOrder.None,
                };
            }

            return SortOrder.None;
        }

        public SortOrder SortPayeesByName()
        {
            driver.GetElement(PayeesPageLocators.Tables.PayeeTable.NameColumn).Click();
            return GetSortOrder();
        }

        public IEnumerable<PayeesPageItem> GetPayees()
        {
            return driver.GetElements(PayeesPageLocators.Tables.PayeeTable.Items).Select(payee => new PayeesPageItem(payee));
        }

        public PayeesPageItem? GetPayee(Func<PayeesPageItem, bool> condition)
        {
            return GetPayees().FirstOrDefault(condition);
        }

        protected override bool EvaluateLoadedStatus()
        {
            return driver.IsElementPresent(PayeesPageLocators.Texts.PayeesHeader)
                && driver.IsElementPresent(PayeesPageLocators.Buttons.Add);
        }

        protected override void ExecuteLoad()
        {
            driver.NavigateTo(settings.BaseUrl + "payees");
        }

    }
}
