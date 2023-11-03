using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Application.DataTransferObjects.Building.Request
{
    public class BuildingCreateRequest
    {
        public string Name { get; set; } = null!;
        public Guid? CreatedBy { get; set; }
    }
}
