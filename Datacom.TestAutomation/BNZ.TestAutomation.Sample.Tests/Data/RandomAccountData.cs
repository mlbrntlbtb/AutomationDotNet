using Bogus;

namespace BNZ.TestAutomation.Sample.Tests
{
    public class RandomAccountData : Faker<AccountDetails> 
    {
        public RandomAccountData()
        {
            RuleFor(a => a.BankCode, f => f.PickRandom(Constants.BANK_CODES));
            RuleFor(a => a.BranchCode, f => f.Random.ReplaceNumbers("####"));
            RuleFor(a => a.AccountNumber, f => f.Random.ReplaceNumbers("#######"));
            RuleFor(a => a.Suffix, f => f.PickRandom(new string[] {"00", "01", "25"}));
        }
    }
}
