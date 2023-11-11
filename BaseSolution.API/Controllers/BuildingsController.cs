using AutoMapper;
using BaseSolution.Application.DataTransferObjects.Building;
using BaseSolution.Application.DataTransferObjects.Building.Request;

using BaseSolution.Application.Interfaces.Repositories.ReadOnly;
using BaseSolution.Application.Interfaces.Repositories.ReadWrite;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Application.ValueObjects.Pagination;
using BaseSolution.Infrastructure.Implements.Repositories.ReadOnly;
using BaseSolution.Infrastructure.Implements.Repositories.ReadWrite;
using BaseSolution.Infrastructure.ViewModels.Building;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BaseSolution.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuildingsController : ControllerBase
    {
        private readonly IBuildingReadOnlyRespository _buildingReadOnlyRespository;
        private readonly IBuildingReadWriteRespository _buildingReadWriteRespository;
        private readonly IMapper _mapper;
        private readonly ILocalizationService _localizationService;
        public BuildingsController(IBuildingReadOnlyRespository buildingReadOnlyRespository, IBuildingReadWriteRespository buildingReadWriteRespository, IMapper mapper, ILocalizationService localizationService)
        {
            _buildingReadOnlyRespository = buildingReadOnlyRespository;
            _buildingReadWriteRespository = buildingReadWriteRespository;
            _mapper = mapper;
            _localizationService = localizationService;
        }
        [HttpGet("getBuildingsByAdmin")]
        public async Task<IActionResult> GetListBuildingByAdmin([FromQuery] ViewBuildingWithPaginationRequest request, CancellationToken cancellationToken)
        {
            BuildingListWithPaginationByAdminViewModel vm = new(_buildingReadOnlyRespository, _localizationService);
            await vm.HandleAsync(request, cancellationToken);
            if(vm.Success)
            {
                PaginationResponse<BuildingDTO> result = new();
                result = (PaginationResponse<BuildingDTO>)vm.Data;
                return Ok(result);
            }
            return BadRequest(vm);
        }
        [HttpGet("getBuildingsByOther")]
        public async Task<IActionResult> GetListBuildingByOther([FromQuery] ViewBuildingWithPaginationRequest request, CancellationToken cancellationToken)
        {
            BuildingListWithPaginationByOtherViewModel vm = new(_buildingReadOnlyRespository, _localizationService);
            await vm.HandleAsync(request, cancellationToken);
            if (vm.Success)
            {
                PaginationResponse<BuildingDTO> result = new();
                result = (PaginationResponse<BuildingDTO>)vm.Data;
                return Ok(result);
            }
            return BadRequest(vm);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBuildingById(Guid id, CancellationToken cancellationToken)
        {
            BuildingViewModel vm = new(_buildingReadOnlyRespository, _localizationService);
            await vm.HandleAsync(id, cancellationToken);
            return Ok(vm);
        }
        [HttpPost]
        public async Task<IActionResult> CreateNewBuilding(BuildingCreateRequest request, CancellationToken cancellationToken)
        {
            BuildingCreateViewModel vm = new(_buildingReadOnlyRespository, _buildingReadWriteRespository, _mapper, _localizationService);
            await vm.HandleAsync(request, cancellationToken);

            return Ok(vm);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBuilding(BuildingUpdateRequest request, CancellationToken cancellationToken)
        {
            BuildingUpdateViewModel vm = new(_buildingReadWriteRespository, _mapper, _localizationService);
            await vm.HandleAsync(request, cancellationToken);

            return Ok(vm);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBuilding([FromQuery]BuildingDeleteRequest request, CancellationToken cancellationToken)
        {
            BuildingDeleteViewModel vm = new(_buildingReadWriteRespository, _mapper, _localizationService);

            await vm.HandleAsync(request, cancellationToken);

            return Ok(vm);
        }

    }
}
