namespace BNZ.TestAutomation.Sample.Tests
{
    [TestClass]
    public class AddPayeesTests : BaseTest
    {

        [TestMethod]
        public void VerifyAddPayee()
        {
            PayeeDetails payee = new RandomPayeeData().Generate();

            Logger!.LogInformation(Test!, "Navigate to Payees page");
            PayeesPage payeesPage = ComponentFactory!.GetComponent<PayeesPage>().Load();

            Logger!.LogInformation(Test!, $"Add new payee: {payee.ToString("Name: {Name} | Account: {Account.BankCode}-{Account.BranchCode}-{Account.AccountNumber}-{Account.Suffix}")}");
            payeesPage.AddPayee(payee);

            Logger!.LogInformation(Test!, $"Notification is displayed and equals to 'Payee added'");
            payeesPage.GetNotification().Should().Be("Payee added");

            PayeesPageItem? item = payeesPage.GetPayee(e => e.GetName().IsValueEqualsTo(payee.Name!));

            Logger!.LogInformation(Test!, $"{payee.Name} is added in the list");
            item.Should().NotBeNull();

            Logger!.LogInformation(Test!, $"Name is equal to {payee.Name}");
            item!.GetName().Should().Be(payee.Name);

            Logger!.LogInformation(Test!, $"Account number is equal to {payee.Account?.GetAccount()}");
            item!.GetAccount().Should().Be(payee.Account!.GetAccount());
            
            Logger!.LogDebug("{name}:{account}", item.GetName(), item.GetAccount());
        }
    }
}
