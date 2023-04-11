namespace BNZ.TestAutomation.Sample.Model
{
    public class AccountDetails
    {
        public string? AccountNumber { get; set; }
        public string? BankCode { get; set; }
        public string? BranchCode { get; set; }
        public float? CurrentBalance { get; set; }
        public string? FormattedOtherCurrencyAvailableBalance { get; set; }
        public string? FormattedOtherCurrencyBalance { get; set; }
        public string? Nickname { get; set; }
        public string? ProductCode { get; set; }
        public string? ProductName { get; set; }
        public string? Suffix { get; set; }
        public string? Type { get; set; }
        public string GetAccount()
        {
            return $"{BankCode}-{BranchCode}-{AccountNumber}-{Suffix}";
        }
    }
}
