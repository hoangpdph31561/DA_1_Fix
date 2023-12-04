using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Application.DataTransferObjects.Building.Request
{
    public class BuildingDeleteRequest
    {
        public Guid Id { get; set; }
        public Guid? DeletedBy { get; set; }

    }
}
