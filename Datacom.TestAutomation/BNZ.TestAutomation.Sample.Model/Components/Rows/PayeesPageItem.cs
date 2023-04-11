using Datacom.TestAutomation.Web.Selenium;
using OpenQA.Selenium;

namespace BNZ.TestAutomation.Sample.Model
{
    public class PayeesPageItem : Loadable<PayeesPageItem>, ILoadable<PayeesPageItem>
    {
        private readonly IWebElement payeeItem;

        public PayeesPageItem(IWebElement payeeItem)
        {
            this.payeeItem = payeeItem;
        }

        public string GetName()
        {
            return payeeItem.GetElement(PayeesPageLocators.Tables.PayeeTable.Names).Text;
        }

        public string GetAccount()
        {
            return payeeItem.GetElement(PayeesPageLocators.Tables.PayeeTable.Accounts).Text;
        }

        protected override bool EvaluateLoadedStatus()
        {
            return true;
        }

        protected override void ExecuteLoad()
        {
            
        }
    }
}
