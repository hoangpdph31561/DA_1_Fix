using BaseSolution.BlazorServer.Data.ValueObjects.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.BlazorServer.Data.DataTransferObjects.Customer.Request
{
    public class CustomerUpdateRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string IdentificationNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string? ApprovedCode { get; set; }
        public CustomerType CustomerType { get; set; }
        public EntityStatus Status { get; set; } 
        public Guid? ModifiedBy { get; set; }

    }
}
