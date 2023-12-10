
﻿namespace BaseSolution.BlazorServer.Data.DataTransferObjects.RoomBookingDetail.Request
{
    public class RoomBookingDetailUpdateRequest
    {
        public Guid Id { get; set; }
        public DateTimeOffset CheckInBooking { get; set; }
        public DateTimeOffset CheckOutReality { get; set; }
    }
}
