using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tempo.TestAutomation.Model.DTOs.CustomerPortal.AddNewConsignment
{
    public class AddItemDetails
    {
        public string? PackageType { get; set; }

        public string? Quantity { get; set; }

        public string? Weight { get; set; }

        public string? Length { get; set; }

        public string? Width { get; set; }

        public string? Height { get; set; }

        public string? Volume { get; set; }
    }
}
