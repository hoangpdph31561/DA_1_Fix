using BaseSolution.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Application.DataTransferObjects.Building.Request
{
    public class BuildingUpdateRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public EntityStatus Status { get; set; }
        public Guid? ModifiedBy { get; set; }
    }
}
