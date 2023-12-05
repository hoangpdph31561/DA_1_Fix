﻿using BaseSolution.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Application.DataTransferObjects.ServiceOrder
{
    public class ServiceOrderDTO
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; } // số lượng 
        public decimal Price { get; set; }
        public decimal TotalAmount { get; set; }
        public EntityStatus Status { get; set; }
        public DateTimeOffset CreatedTime { get; set; }
        // base on 
        public string CustomerName { get; set; }  // Tên khách hàng
        public Guid CustomerId { get; set; }
        public Guid ServiceId { get; set; }
        public string ServiceName { get; set; }
        public Guid? RoomBookingDetailId { get; set; } // đặt dịch vụ cho phòng  
    }
}
