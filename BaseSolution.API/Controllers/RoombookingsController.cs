using AutoMapper;
using BaseSolution.Application.DataTransferObjects.Roombooking;
using BaseSolution.Application.DataTransferObjects.Roombooking.Request;
using BaseSolution.Application.Interfaces.Repositories.ReadOnly;
using BaseSolution.Application.Interfaces.Repositories.ReadWrite;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Application.ValueObjects.Pagination;
using BaseSolution.Infrastructure.ViewModels.Roombooking;
using Microsoft.AspNetCore.Mvc;

namespace BaseSolution.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RoombookingsController : ControllerBase
{
    private readonly IRoombookingReadOnlyRepository _roombookingrReadOnlyRespository;
    private readonly IRoombookingReadWriteRepository _roombookingReadWriteRespository;
    private readonly IMapper _mapper;
    private readonly ILocalizationService _localizationService;
    public RoombookingsController(IRoombookingReadOnlyRepository roombookingReadOnlyRepository, IRoombookingReadWriteRepository roombookingReadWriteRepository, IMapper mapper, ILocalizationService localizationService)
    {
        _roombookingrReadOnlyRespository = roombookingReadOnlyRepository;
        _roombookingReadWriteRespository = roombookingReadWriteRepository;
        _mapper = mapper;
        _localizationService = localizationService;
    }
    [HttpGet("getRoomBookingByDetail")]
    public async Task<IActionResult> GetListRoomBookingDetailByAdmin([FromQuery] ViewRoombookingWithPaginationRequest request, CancellationToken cancellationToken)
    {
        RoombookingListWithPaginationViewModel vm = new(_roombookingrReadOnlyRespository, _localizationService);
        await vm.HandleAsync(request, cancellationToken);
        if (vm.Success)
        {
            PaginationResponse<RoombookingDTO> result = (PaginationResponse<RoombookingDTO>)vm.Data;
            return Ok(result);
        }
        return BadRequest(vm);

    }
      [HttpGet("getRoomBookingByOther")]
    public async Task<IActionResult> GetListRoomBookingDetailByOther([FromQuery] ViewRoombookingWithPaginationRequest request, CancellationToken cancellationToken)
    {
        RoomBookingListWithPaginationByOtherViewModel vm = new(_roombookingrReadOnlyRespository, _localizationService);
        await vm.HandleAsync(request, cancellationToken);

        if(vm.Success)

        {
            PaginationResponse<RoombookingDTO> result = (PaginationResponse<RoombookingDTO>)vm.Data;
            return Ok(result);
        }
        return BadRequest(vm);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetRoomBookingDetailByAdmin(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            RoombookingViewModel vm = new(_roombookingrReadOnlyRespository, _localizationService);
            await vm.HandleAsync(id, cancellationToken);
            if(vm.Success)
            {
                RoombookingDTO result = (RoombookingDTO)vm.Data;
                return Ok(result);
            }
            return BadRequest(vm);
        }
        catch (Exception)
        {
            throw;
        }
       
    }
    [HttpGet("{idCustomer}/details")]
    public async Task<IActionResult> GetRoomBookingDetailByIdCustomer(Guid idCustomer, CancellationToken cancellationToken)
    {
        try
        {
            RoomBookingByCustomerIdViewModel vm = new(_roombookingrReadOnlyRespository, _localizationService);
            await vm.HandleAsync(idCustomer, cancellationToken);
            if (vm.Success)
            {
                List<RoombookingDTO> result = (List<RoombookingDTO>)vm.Data;
                return Ok(result);
            }
            return BadRequest(vm);
        }
        catch (Exception)
        {
            throw;
        }
    }
    [HttpPost]
    public async Task<IActionResult> Post(RoombookingCreateRequest request, CancellationToken cancellationToken)
    {
        RoombookingCreateViewModel vm = new(_roombookingrReadOnlyRespository, _roombookingReadWriteRespository, _mapper, _localizationService);
        await vm.HandleAsync(request, cancellationToken);

        return Ok(vm.Data);
    }

    [HttpPut]
    public async Task<IActionResult> Put(RoombookingUpdateRequest request, CancellationToken cancellationToken)
    {
        RoombookingUpdateViewModel vm = new(_roombookingReadWriteRespository, _mapper, _localizationService);
        await vm.HandleAsync(request, cancellationToken);

        return Ok(vm);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(RoombookingDeleteRequest request, CancellationToken cancellationToken)
    {
        RoombookingDeleteViewModel vm = new(_roombookingReadWriteRespository, _localizationService, _mapper);

        await vm.HandleAsync(request, cancellationToken);

        return Ok(vm);
    }
}
