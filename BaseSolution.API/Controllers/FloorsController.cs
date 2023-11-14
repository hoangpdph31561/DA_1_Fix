using AutoMapper;
using BaseSolution.Application.DataTransferObjects.Floor;
using BaseSolution.Application.DataTransferObjects.Floor.Request;
using BaseSolution.Application.Interfaces.Repositories.ReadOnly;
using BaseSolution.Application.Interfaces.Repositories.ReadWrite;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Application.ValueObjects.Pagination;
using BaseSolution.Infrastructure.ViewModels.Floor;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BaseSolution.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FloorsController : ControllerBase
    {
        private readonly IFloorReadOnlyRespository _floorReadOnlyRespository;
        private readonly IFloorReadWriteRespository _floorReadWriteRespository;
        private readonly IMapper _mapper;
        private readonly ILocalizationService _localizationService;
        public FloorsController(IFloorReadOnlyRespository floorReadOnlyRespository, IFloorReadWriteRespository floorReadWriteRespository, IMapper mapper, ILocalizationService localizationService)
        {
            _floorReadOnlyRespository = floorReadOnlyRespository;
            _floorReadWriteRespository = floorReadWriteRespository;
            _mapper = mapper;
            _localizationService = localizationService;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFloorById(Guid id, CancellationToken cancellationToken)
        {
            FloorViewModel vm = new(_floorReadOnlyRespository, _localizationService);
            await vm.HandleAsync(id, cancellationToken);
            if(vm.Success)
            {
                FloorDTO result = (FloorDTO)vm.Data;
                return Ok(result);
            }
            return BadRequest(vm);
        }
        [HttpGet("getFloorsByAdmin")]
        public async Task<IActionResult> GetFloorByAdmin([FromQuery]ViewFloorWithPaginationRequest request, CancellationToken cancellationToken)
        {
            FloorListWithPaginationByAdminViewModel vm = new(_floorReadOnlyRespository, _localizationService);
            await vm.HandleAsync(request, cancellationToken);
            return Ok(vm);
        }
        [HttpGet]
        public async Task<IActionResult> GetFloorByOther([FromQuery]ViewFloorWithPaginationRequest request, CancellationToken cancellationToken)
        {
            FloorListWithPaginationByOtherViewModel vm = new(_floorReadOnlyRespository, _localizationService);
            await vm.HandleAsync(request, cancellationToken);
            if(vm.Success)
            {
                PaginationResponse<FloorDTO> result = (PaginationResponse < FloorDTO >) vm.Data;
                return Ok(result);
            }
            return BadRequest(vm);
        }
        [HttpPost]
        public async Task<IActionResult> CreateNewFloor(FloorCreateRequest request, CancellationToken cancellationToken)
        {
            FloorCreateViewModel vm = new(_floorReadOnlyRespository, _floorReadWriteRespository,_mapper, _localizationService);
            await vm.HandleAsync(request,cancellationToken); 
            return Ok(vm);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateFloor(FloorUpdateRequest request, CancellationToken cancellationToken)
        {
            FloorUpdateViewModel vm = new(_floorReadWriteRespository, _mapper, _localizationService);
            await vm.HandleAsync(request, cancellationToken);
            return Ok(vm);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteFloor([FromQuery]FloorDeleteRequest request, CancellationToken cancellationToken)
        {
            FloorDeleteViewModel vm = new(_floorReadWriteRespository, _localizationService);
            await vm.HandleAsync(request, cancellationToken);
            return Ok(vm);
        }
    }
}
