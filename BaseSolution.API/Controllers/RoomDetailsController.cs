using AutoMapper;
using BaseSolution.Application.DataTransferObjects.RoomDetail;
using BaseSolution.Application.DataTransferObjects.RoomDetail.Request;
using BaseSolution.Application.Interfaces.Repositories.ReadOnly;
using BaseSolution.Application.Interfaces.Repositories.ReadWrite;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Application.ValueObjects.Pagination;
using BaseSolution.Infrastructure.Implements.Repositories.ReadOnly;
using BaseSolution.Infrastructure.Implements.Repositories.ReadWrite;
using BaseSolution.Infrastructure.ViewModels.RoomDetail;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BaseSolution.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomDetailsController : ControllerBase
    {
        public readonly IRoomDetailReadOnlyRepository _RoomDetailReadOnlyRepository;
        public readonly IRoomDetailReadWriteRepository _RoomDetailReadWriteRepository;
        private readonly ILocalizationService _localizationService;
        private readonly IMapper _mapper;


        public RoomDetailsController(IRoomDetailReadOnlyRepository RoomDetailReadOnlyRepository, IRoomDetailReadWriteRepository RoomDetailReadWriteRepository, ILocalizationService localizationService, IMapper mapper)
        {
            _RoomDetailReadOnlyRepository = RoomDetailReadOnlyRepository;
            _RoomDetailReadWriteRepository = RoomDetailReadWriteRepository;
            _localizationService = localizationService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetListRoomDetailByAdmin([FromQuery] ViewRoomDetailWithPaginationRequest request, CancellationToken cancellationToken)
        {
            RoomDetailListWithPaginationViewModel vm = new(_RoomDetailReadOnlyRepository, _localizationService);

            await vm.HandleAsync(request, cancellationToken);
            if(vm.Success)
            {
                PaginationResponse<RoomDetailDto> result = (PaginationResponse<RoomDetailDto>)vm.Data;
                return Ok(result);
            }

            return BadRequest(vm);
        }

          [HttpGet("getRoomBookingDetailByStatus")]
        public async Task<IActionResult> GetListRoomDetailByStatus([FromQuery] ViewRoomDetailWithPaginationRequest request, CancellationToken cancellationToken)
        {
            RoomDetailWithPaginationByStatusViewModel vm = new(_RoomDetailReadOnlyRepository, _localizationService);

            await vm.HandleAsync(request, cancellationToken);
            if(vm.Success)
            {
                PaginationResponse<RoomDetailDto> result = (PaginationResponse<RoomDetailDto>)vm.Data;
                return Ok(result);
            }

            return BadRequest(vm);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoomDetailById(Guid id, CancellationToken cancellationToken)
        {
            RoomDetailViewModel vm = new(_RoomDetailReadOnlyRepository, _localizationService);

            await vm.HandleAsync(id, cancellationToken);

            return Ok(vm);
        }
        [HttpGet("idRoomType")]
        public async Task<List<RoomDetailDto>> GetRoomDetailByIdRoomType( Guid idRoomType, CancellationToken cancellationToken)
        {
            RoomDetailViewModelByRoomTypeId vm = new(_RoomDetailReadOnlyRepository, _localizationService);

            await vm.HandleAsync(idRoomType, cancellationToken);
            if (vm.Success)
            {
                List<RoomDetailDto> result = (List<RoomDetailDto>)vm.Data;
                return result;
            }

            return new List<RoomDetailDto>();
        }
        [HttpPost]
        public async Task<IActionResult> Post(RoomDetailCreateRequest request, CancellationToken cancellationToken)
        {
            RoomDetailCreateViewModel vm = new(_RoomDetailReadOnlyRepository, _RoomDetailReadWriteRepository, _localizationService, _mapper);

            await vm.HandleAsync(request, cancellationToken);
            return Ok(vm);
        }

        [HttpPut]
        public async Task<IActionResult> Put(RoomDetailUpdateRequest request, CancellationToken cancellationToken)
        {
            RoomDetailUpdateViewModel vm = new(_RoomDetailReadWriteRepository, _localizationService, _mapper);

            await vm.HandleAsync(request, cancellationToken);
            return Ok(vm);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(RoomDetailDeleteRequest request, CancellationToken cancellationToken)
        {
            RoomDetailDeleteViewModel vm = new(_RoomDetailReadWriteRepository, _localizationService, _mapper);

            await vm.HandleAsync(request, cancellationToken);

            return Ok(vm);
        }
    }
}
