using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Application.DataTransferObjects.Floor.Request
{
    public class FloorUpdateRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public int? NumberOfRoom { get; set; }
        public Guid? BuildingId { get; set; }
        public Guid? ModifiedBy { get; set; }
    }
}
