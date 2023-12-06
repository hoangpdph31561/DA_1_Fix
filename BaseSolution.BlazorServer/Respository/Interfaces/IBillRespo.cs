﻿using BaseSolution.BlazorServer.Data.DataTransferObjects.Bill;
using BaseSolution.BlazorServer.Data.DataTransferObjects.Bill.Request;
using BaseSolution.BlazorServer.Data.ValueObjects.Pagination;

namespace BaseSolution.BlazorServer.Respository.Interfaces
{
    public interface IBillRespo
    {
        Task<PaginationResponse<BillDTO>> GetAllBill(ViewBillWithPaginationRequest request);
        Task<BillDTO> GetBillByIdCustomer(Guid idCustomer);
        Task<bool> CreateNewBill(BillCreateRequest request);
        Task<BillDTO> GetBillById(Guid id);
        Task<bool> UpdateBill(BillUpdateRequest request);
    }
}
