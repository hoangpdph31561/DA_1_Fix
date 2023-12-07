using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Domain.Enums
{
    public enum RoomStatus
    {
        Vacant = 1,
        OutOfOrder = 2,
        Deleted = 3,
        Occupied = 4,
        Reserved = 5,
        Cleaned = 6,
        Dirty = 7,
        Inspected = 8,
        DoNotDisturb = 9,
        CheckIn = 10,
        CheckOut = 11,
        AwaitingConfirmation = 12
    }
}
