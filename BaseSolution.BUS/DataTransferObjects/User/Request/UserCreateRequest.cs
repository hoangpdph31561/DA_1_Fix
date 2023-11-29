﻿using BaseSolution.Domain.Enums;

namespace BaseSolution.Application.DataTransferObjects.User.Request
{
    public class UserCreateRequest
    {
        public string UserName { get; set; } 
        public string Name { get; set; } 
        public string Email { get; set; }
        public string PhoneNumber { get; set; } 
        public Guid UserRoleId { get; set; }
    }
}
