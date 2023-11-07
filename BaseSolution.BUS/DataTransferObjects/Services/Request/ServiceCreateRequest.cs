using BaseSolution.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Application.DataTransferObjects.Services.Request
{
    public class ServiceCreateRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public UnitType Unit { get; set; }
        public Guid ServiceTypeId { get; set; }
        public Guid? CreatedBy { get; set; }
    }
}
