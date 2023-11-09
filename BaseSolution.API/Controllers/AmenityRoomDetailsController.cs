using AutoMapper;
using BaseSolution.Application.DataTransferObjects.AmenityRoomDetail.Request;
using BaseSolution.Application.Interfaces.Repositories.ReadOnly;
using BaseSolution.Application.Interfaces.Repositories.ReadWrite;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Infrastructure.ViewModels.AmenityRoomDetail;
using Microsoft.AspNetCore.Mvc;

namespace BaseSolution.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AmenityRoomDetailsController : ControllerBase
    {
        private readonly IAmenityRoomDetailReadOnlyRepository _AmenityRoomDetailReadOnlyRespository;
        private readonly IAmenityRoomDetailReadWriteRepository _AmenityRoomDetailReadWriteRespository;
        private readonly IMapper _mapper;
        private readonly ILocalizationService _localizationService;
        public AmenityRoomDetailsController(IAmenityRoomDetailReadOnlyRepository AmenityRoomDetailReadOnlyRepository, IAmenityRoomDetailReadWriteRepository AmenityRoomDetailReadWriteRepository, IMapper mapper, ILocalizationService localizationService)
        {
            _AmenityRoomDetailReadOnlyRespository = AmenityRoomDetailReadOnlyRepository;
            _AmenityRoomDetailReadWriteRespository = AmenityRoomDetailReadWriteRepository;
            _mapper = mapper;
            _localizationService = localizationService;
        }
        [HttpGet]
        public async Task<IActionResult> GetListAmenityRoomDetailByAdmin([FromQuery] ViewAmenityRoomDetailWithPaginationRequest request, CancellationToken cancellationToken)
        {
            AmenityRoomDetailListWithPaginationViewModel vm = new(_AmenityRoomDetailReadOnlyRespository, _localizationService);
            await vm.HandleAsync(request, cancellationToken);
            return Ok(vm);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAmenityRoomDetailById(Guid id, CancellationToken cancellationToken)
        {
            AmenityRoomDetailViewModel vm = new(_AmenityRoomDetailReadOnlyRespository, _localizationService);
            await vm.HandleAsync(id, cancellationToken);
            return Ok(vm);
        }
        [HttpPost]
        public async Task<IActionResult> CreateNewAmenityRoomDetail(AmenityRoomDetailCreateRequest request, CancellationToken cancellationToken)
        {
            AmenityRoomDetailCreateViewModel vm = new(_AmenityRoomDetailReadOnlyRespository, _AmenityRoomDetailReadWriteRespository, _mapper, _localizationService);
            await vm.HandleAsync(request, cancellationToken);
            return Ok(vm);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAmenityRoomDetail(AmenityRoomDetailUpdateRequest request, CancellationToken cancellationToken)
        {
            AmenityRoomDetailUpdateViewModel vm = new(_AmenityRoomDetailReadWriteRespository, _mapper, _localizationService);
            await vm.HandleAsync(request, cancellationToken);
            return Ok(vm);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAmenityRoomDetail(AmenityRoomDetailDeleteRequest request, CancellationToken cancellationToken)
        {
            AmenityRoomDetailDeleteViewModel vm = new(_AmenityRoomDetailReadWriteRespository, _localizationService, _mapper);

            await vm.HandleAsync(request, cancellationToken);

            return Ok(vm);
        }
    }
}
