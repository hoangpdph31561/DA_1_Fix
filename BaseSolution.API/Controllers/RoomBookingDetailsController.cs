using AutoMapper;
using BaseSolution.Application.DataTransferObjects.RoomBookingDetail.Request;
using BaseSolution.Application.Interfaces.Repositories.ReadOnly;
using BaseSolution.Application.Interfaces.Repositories.ReadWrite;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Infrastructure.ViewModels.RoomBookingDetail;
using Microsoft.AspNetCore.Mvc;

namespace BaseSolution.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomBookingDetailsController : ControllerBase
    {
        private readonly IRoomBookingDetailReadOnlyRepository _roomBookingDetailReadOnlyRepository;
        private readonly IRoomBookingDetailReadWriteRepository _roomBookingDetailReadWriteRepository;
        private readonly IMapper _mapper;
        private readonly ILocalizationService _localizationService;
        public RoomBookingDetailsController(IRoomBookingDetailReadOnlyRepository roomBookingDetailReadOnlyRepository, IRoomBookingDetailReadWriteRepository RoomBookingDetailReadWriteRepository,
            IMapper mapper, ILocalizationService localizationService)
        {
            _roomBookingDetailReadOnlyRepository = roomBookingDetailReadOnlyRepository;
            _roomBookingDetailReadWriteRepository = RoomBookingDetailReadWriteRepository;
            _mapper = mapper;
            _localizationService = localizationService;
        }
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] ViewRoomBookingDetailWithPaginationRequest request, CancellationToken cancellationToken)
        {
            RoomBookingDetailListWithPaginationViewModel vm = new(_roomBookingDetailReadOnlyRepository, _localizationService);
            await vm.HandleAsync(request, cancellationToken);

            return Ok(vm);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
        {
            RoomBookingDetailViewModel vm = new(_roomBookingDetailReadOnlyRepository, _localizationService);
            await vm.HandleAsync(id, cancellationToken);
            return Ok(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Post(RoomBookingDetailCreateRequest request, CancellationToken cancellationToken)
        {
            RoomBookingDetailCreateViewModel vm = new(_roomBookingDetailReadOnlyRepository, _roomBookingDetailReadWriteRepository, _mapper, _localizationService);
            await vm.HandleAsync(request, cancellationToken);

            return Ok(vm);
        }

        [HttpPut]
        public async Task<IActionResult> Put(RoomBookingDetailUpdateRequest request, CancellationToken cancellationToken)
        {
            RoomBookingDetailUpdateViewModel vm = new(_roomBookingDetailReadWriteRepository, _mapper, _localizationService);
            await vm.HandleAsync(request, cancellationToken);

            return Ok(vm);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(RoomBookingDetailDeleteRequest request, CancellationToken cancellationToken)
        {
            RoomBookingDetailDeleteViewModel vm = new(_roomBookingDetailReadWriteRepository, _localizationService, _mapper);

            await vm.HandleAsync(request, cancellationToken);

            return Ok(vm);
        }
    }
}
