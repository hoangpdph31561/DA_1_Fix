using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Application.DataTransferObjects.Statistic.RoomBooking
{
    public class RoomBookingStatisticDto
    {
        public int Month { get; set; } // các tháng đặt phòng
        public string NameRoom { get; set; }
        public int BookingCount { get; set; }
    }

}
