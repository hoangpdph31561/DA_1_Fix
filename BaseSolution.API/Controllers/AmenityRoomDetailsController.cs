using AutoMapper;
using BaseSolution.Application.DataTransferObjects.AmenityRoomDetail;
using BaseSolution.Application.DataTransferObjects.AmenityRoomDetail.Request;
using BaseSolution.Application.Interfaces.Repositories.ReadOnly;
using BaseSolution.Application.Interfaces.Repositories.ReadWrite;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Application.ValueObjects.Pagination;
using BaseSolution.Infrastructure.ViewModels.AmenityRoomDetail;
using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
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
        private readonly IValidator<AmenityRoomDetailCreateRequest> _validator;
        private readonly IValidator<AmenityRoomDetailUpdateRequest> _validatorUpdate;

        public AmenityRoomDetailsController(IAmenityRoomDetailReadOnlyRepository AmenityRoomDetailReadOnlyRepository, IAmenityRoomDetailReadWriteRepository AmenityRoomDetailReadWriteRepository, IMapper mapper, ILocalizationService localizationService,
            IValidator<AmenityRoomDetailCreateRequest> validator, IValidator<AmenityRoomDetailUpdateRequest> validatorUpdate
            )
        {
            _AmenityRoomDetailReadOnlyRespository = AmenityRoomDetailReadOnlyRepository;
            _AmenityRoomDetailReadWriteRespository = AmenityRoomDetailReadWriteRepository;
            _mapper = mapper;
            _localizationService = localizationService;
            _validator = validator;
            _validatorUpdate = validatorUpdate;
        }
        [HttpGet]
        public async Task<IActionResult> GetListAmenityRoomDetailByAdmin([FromQuery] ViewAmenityRoomDetailWithPaginationRequest request, CancellationToken cancellationToken)
        {
            AmenityRoomDetailListWithPaginationViewModel vm = new(_AmenityRoomDetailReadOnlyRespository, _localizationService);
            await vm.HandleAsync(request, cancellationToken);
            if (vm.Success)
            {
                PaginationResponse<AmenityRoomDetailDTO> result = new();
                result = (PaginationResponse<AmenityRoomDetailDTO>)vm.Data;
                return Ok(result);
            }
            return BadRequest(vm);
        }
        [HttpGet("getAmenityRoomDetailByAmenityId")]
        public async Task<IActionResult> GetListAmenityRoomDetailByAmenityId([FromQuery] ViewAmenityRoomDetailWithPaginationRequestAndAmenityId request, CancellationToken cancellationToken)
        {
            AmenityRoomDetailListWithPaginationAndAmenityIdViewModel vm = new(_AmenityRoomDetailReadOnlyRespository, _localizationService);
            await vm.HandleAsync(request, cancellationToken);
            if (vm.Success)
            {
                PaginationResponse<AmenityRoomDetailDTO> result = new();
                result = (PaginationResponse<AmenityRoomDetailDTO>)vm.Data;
                return Ok(result);
            }
            return BadRequest(vm);
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
            ValidationResult validate = await _validator.ValidateAsync(request);
            if (!validate.IsValid)
            {
                validate.AddToModelState(this.ModelState);
                return BadRequest(ModelState);
            }
            AmenityRoomDetailCreateViewModel vm = new(_AmenityRoomDetailReadOnlyRespository, _AmenityRoomDetailReadWriteRespository, _mapper, _localizationService);
            await vm.HandleAsync(request, cancellationToken);
            return Ok(vm);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAmenityRoomDetail(AmenityRoomDetailUpdateRequest request, CancellationToken cancellationToken)
        {
            ValidationResult validate = await _validatorUpdate.ValidateAsync(request);
            if (!validate.IsValid)
            {
                validate.AddToModelState(this.ModelState);
                return BadRequest(ModelState);
            }
            AmenityRoomDetailUpdateViewModel vm = new(_AmenityRoomDetailReadWriteRespository, _mapper, _localizationService);
            await vm.HandleAsync(request, cancellationToken);
            return Ok(vm);
        }
        [HttpPut("createUpdateDeleteAmenityRoomDetail")]
        public async Task<IActionResult> CreateUpdateDeleteAmenityRoomDetail(List<AmenityCreateUpdateDeleteRequest> request, CancellationToken cancellationToken)
        {
            AmenityRoomDetailCreateUpdateDeleteViewModel vm = new(_AmenityRoomDetailReadWriteRespository, _localizationService);
            await vm.HandleAsync(request, cancellationToken);
            if(vm.Success)
            {
                return Ok(vm);
            }
            return BadRequest(vm);
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
