using Datacom.TestAutomation.Web.Selenium;

namespace BNZ.TestAutomation.Sample.Model
{
    public class PayeesWorkflow
    {
        private readonly PayeeDetails payee;
        private readonly PayeesPage payeesPage;
        public PayeesWorkflow (IComponentFactory factory, PayeeDetails payee)
        {
            payeesPage = factory.GetComponent<PayeesPage>();
            this.payee = payee;
        }

        public void AddPayee()
        {
            payeesPage.Load().AddPayee(payee);
        }
    }
}
