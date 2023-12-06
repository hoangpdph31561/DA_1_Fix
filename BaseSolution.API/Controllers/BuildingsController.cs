using AutoMapper;
using BaseSolution.Application.DataTransferObjects.Building;
using BaseSolution.Application.DataTransferObjects.Building.Request;
using BaseSolution.Application.Interfaces.Repositories.ReadOnly;
using BaseSolution.Application.Interfaces.Repositories.ReadWrite;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Application.ValueObjects.Pagination;
using BaseSolution.Infrastructure.ViewModels.Building;
using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
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
        private readonly IValidator<BuildingCreateRequest> _validator;
        private readonly IValidator<BuildingUpdateRequest> _validatorUpdate;

        public BuildingsController(IBuildingReadOnlyRespository buildingReadOnlyRespository, IBuildingReadWriteRespository buildingReadWriteRespository, IMapper mapper, ILocalizationService localizationService,
               IValidator<BuildingCreateRequest> validator, IValidator<BuildingUpdateRequest> validatorUpdate
            )
        {
            _buildingReadOnlyRespository = buildingReadOnlyRespository;
            _buildingReadWriteRespository = buildingReadWriteRespository;
            _mapper = mapper;
            _localizationService = localizationService;
            _validator = validator;
            _validatorUpdate = validatorUpdate;

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
            if (vm.Success)
            {
                BuildingDTO result = (BuildingDTO)vm.Data;
                return Ok(result);
            }
            return BadRequest(vm);
        }
        [HttpPost]
        public async Task<IActionResult> CreateNewBuilding(BuildingCreateRequest request, CancellationToken cancellationToken)
        {
            ValidationResult validate = await _validator.ValidateAsync(request);
            if (!validate.IsValid)
            {
                validate.AddToModelState(this.ModelState);
                return BadRequest(ModelState);
            }
            BuildingCreateViewModel vm = new(_buildingReadOnlyRespository, _buildingReadWriteRespository, _mapper, _localizationService);
            await vm.HandleAsync(request, cancellationToken);
            if (vm.Success)
            {
                return Ok(vm);
            }

            return BadRequest(vm);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBuilding(BuildingUpdateRequest request, CancellationToken cancellationToken)
        {
            ValidationResult validate = await _validatorUpdate.ValidateAsync(request);
            if (!validate.IsValid)
            {
                validate.AddToModelState(this.ModelState);
                return BadRequest(ModelState);
            }
            BuildingUpdateViewModel vm = new(_buildingReadWriteRespository, _mapper, _localizationService);
            await vm.HandleAsync(request, cancellationToken);
            if(vm.Success)
            {
                return Ok(vm);
            }
            return BadRequest(vm);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBuilding([FromQuery]BuildingDeleteRequest request, CancellationToken cancellationToken)
        {
            BuildingDeleteViewModel vm = new(_buildingReadWriteRespository, _mapper, _localizationService);
            await vm.HandleAsync(request, cancellationToken);
            if(vm.Success)
            {
                return Ok(vm);
            }
            return BadRequest(vm);
        }

    }
}
