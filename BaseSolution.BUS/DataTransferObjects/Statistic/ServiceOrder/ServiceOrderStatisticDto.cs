using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Application.DataTransferObjects.Statistic.ServiceOrder
{
    public class ServiceOrderStatisticDto
    {
        public int Month { get; set; } 
        public string NameService { get; set; }
        public int OrderCount { get; set; }
    }
}
