namespace Tempo.TestAutomation.Model.DTOs
{
    public class FreightData
    {
        public string? ConsignmentNumber { get; set; }
        public string? ColumnName { get; set; }
        public Parentmenu? ParentMenu { get; set; }
        public Childmenu? ChildMenu { get; set; }
        public Menulist? MenuList { get; set; }

        public class Parentmenu
        {
            public string? Columns { get; set; }
        }

        public class Childmenu
        {
            public string? ItemID { get; set; }
        }

        public class Menulist
        {
            public string? SaveLayout { get; set; }
            public string? ClearLayout { get; set; }
            public string? AddConsignment { get; set; }
        }
    }
}