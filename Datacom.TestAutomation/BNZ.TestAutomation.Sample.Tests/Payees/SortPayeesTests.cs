namespace BNZ.TestAutomation.Sample.Tests
{
    [TestClass]
    public class SortPayeesTests : BaseTest
    {

        [TestMethod]
        public void VerifySortPayee()
        {
            PayeeDetails payee = new RandomPayeeData().Generate();

            Logger!.LogInformation(Test!, $"Add new payee: {payee.ToString("Name: {Name} | Account: {Account.BankCode}-{Account.BranchCode}-{Account.AccountNumber}-{Account.Suffix}")}");
            PayeesPage payeesPage = ComponentFactory!.GetComponent<PayeesPage>().Load();
            payeesPage.Load().AddPayee(payee).GetNotification();

            IEnumerable<PayeesPageItem> payees = payeesPage.GetPayees();
            IEnumerable<string> names = payees.Select(e => e.GetName());

            Logger!.LogInformation(Test!, $"Default order should be 'Ascending' order");
            payeesPage.GetSortOrder().Should().Be(SortOrder.Ascending); 
            names.Should().BeInAscendingOrder();
            
            Logger!.LogInformation(Test!, $"The order should change to 'Descending' if you click on the 'Name' column");
            payeesPage.SortPayeesByName().Should().Be(SortOrder.Descending);
            payees = payeesPage.GetPayees();
            names = payees.Select(e => e.GetName());
            names.Should().BeInDescendingOrder();
        }
    }
}
