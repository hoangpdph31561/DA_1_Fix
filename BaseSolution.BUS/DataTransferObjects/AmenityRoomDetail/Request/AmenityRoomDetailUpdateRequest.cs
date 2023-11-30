﻿using BaseSolution.Domain.Enums;

namespace BaseSolution.Application.DataTransferObjects.AmenityRoomDetail.Request
{
    public class AmenityRoomDetailUpdateRequest
    {
        public Guid Id { get; set; }
        public Guid AmenityId { get; set; }
        public Guid RoomTypeId { get; set; }
        public int Amount { get; set; }
        public EntityStatus Status { get; set; } 
        public Guid? ModifiedBy { get; set; }
    }
}
