namespace Tempo.TestAutomation.Model.DTOs
{
    public class CustomersDetails
    {
        public Customerdetails? CustomerDetails { get; set; }

        public class Customerdetails
        {
            public string? Customer { get; set; }
        }
    }
}