using BaseSolution.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Application.DataTransferObjects.Building
{
    public class BuildingDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public EntityStatus Status { get; set; }
        public int NumberOfFloors { get; set; }
    }
}
