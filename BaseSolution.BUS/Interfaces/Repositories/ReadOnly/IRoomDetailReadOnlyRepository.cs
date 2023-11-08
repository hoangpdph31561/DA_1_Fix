﻿using BaseSolution.Application.DataTransferObjects.RoomDetail.Request;
using BaseSolution.Application.DataTransferObjects.RoomDetail;
using BaseSolution.Application.ValueObjects.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseSolution.Application.ValueObjects.Pagination;

namespace BaseSolution.Application.Interfaces.Repositories.ReadOnly
{
    public interface IRoomDetailReadOnlyRepository 
    {
        Task<RequestResult<RoomDetailDto?>> GetRoomDetailByIdAsync(Guid idRoomDetail, CancellationToken cancellationToken);
        Task<RequestResult<PaginationResponse<RoomDetailDto>>> GetRoomDetailWithPaginationByAdminAsync(
            ViewRoomDetailWithPaginationRequest request, CancellationToken cancellationToken);
    }
}