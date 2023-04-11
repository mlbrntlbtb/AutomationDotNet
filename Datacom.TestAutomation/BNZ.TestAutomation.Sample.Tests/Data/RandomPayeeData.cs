using Bogus;

namespace BNZ.TestAutomation.Sample.Tests
{
    public class RandomPayeeData : Faker<PayeeDetails>
    {
        public RandomPayeeData()
        {
            RuleFor(p => p.Name, f => f.Name.FullName());
            RuleFor(p => p.Account, f => new RandomAccountData().Generate());
        }
    }
}
