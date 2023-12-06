using AutoMapper;
using BaseSolution.Application.DataTransferObjects.Amenity;
using BaseSolution.Application.DataTransferObjects.Amenity.Request;
using BaseSolution.Application.Interfaces.Repositories.ReadOnly;
using BaseSolution.Application.Interfaces.Repositories.ReadWrite;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Application.ValueObjects.Pagination;
using BaseSolution.Infrastructure.ViewModels.Amenity;
using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc; 

namespace BaseSolution.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AmenitiesController : ControllerBase
    {
        private readonly IAmenityReadOnlyRepository _amenityReadOnlyRespository;
        private readonly IAmenityReadWriteRepository _amenityReadWriteRespository;
        private readonly IMapper _mapper;
        private readonly ILocalizationService _localizationService;
        private readonly IValidator<AmenityCreateRequest> _validator;
        private readonly IValidator<AmenityUpdateRequest> _validatorUpdate;
        private readonly IValidator<AmenityDeleteRequest> _validatorDetete;

        public AmenitiesController(IAmenityReadOnlyRepository amenityReadOnlyRepository, IAmenityReadWriteRepository amenityReadWriteRepository, IMapper mapper,
            ILocalizationService localizationService, IValidator<AmenityCreateRequest> validator, IValidator<AmenityUpdateRequest> validatorUpdate,
            IValidator<AmenityDeleteRequest> validatorDetete
            )
        {
            _amenityReadOnlyRespository = amenityReadOnlyRepository;
            _amenityReadWriteRespository = amenityReadWriteRepository;
            _mapper = mapper;
            _localizationService = localizationService;
            _validator = validator;
            _validatorUpdate = validatorUpdate;
            _validatorDetete = validatorDetete;
        }
        [HttpGet]
        public async Task<IActionResult> GetListAmenityByAdmin([FromQuery] ViewAmenityWithPaginationRequest request, CancellationToken cancellationToken)
        {
            AmenityListWithPaginationViewModel vm = new(_amenityReadOnlyRespository, _localizationService);
            await vm.HandleAsync(request, cancellationToken);
            if(vm.Success)
            {
                PaginationResponse<AmenityDTO> result = (PaginationResponse<AmenityDTO>)vm.Data;
                return Ok(result);
            }
            return BadRequest(vm);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAmenityById(Guid id, CancellationToken cancellationToken)
        {
            AmenityViewModel vm = new(_amenityReadOnlyRespository, _localizationService);
            await vm.HandleAsync(id, cancellationToken);
            if(vm.Success)
            {
                AmenityDTO result = (AmenityDTO)vm.Data;
                return Ok(result);
            }
            return BadRequest(vm);
        }
        [HttpPost]
        public async Task<IActionResult> CreateNewAmenity(AmenityCreateRequest request, CancellationToken cancellationToken)
        {
            ValidationResult validate = await _validator.ValidateAsync(request);
            if (!validate.IsValid)
            {
                validate.AddToModelState(this.ModelState);
                return BadRequest(ModelState);
            }
            AmenityCreateViewModel vm = new(_amenityReadOnlyRespository, _amenityReadWriteRespository, _mapper, _localizationService);
            await vm.HandleAsync(request, cancellationToken);
            if(vm.Success)
            {
                return Ok(vm);
            }
            return BadRequest(vm);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAmenity(AmenityUpdateRequest request, CancellationToken cancellationToken)
        {
            ValidationResult validate = await _validatorUpdate.ValidateAsync(request);
            if (!validate.IsValid)
            {
                validate.AddToModelState(this.ModelState);
                return BadRequest(ModelState);
            }
            AmenityUpdateViewModel vm = new(_amenityReadWriteRespository, _mapper, _localizationService);
            await vm.HandleAsync(request, cancellationToken);
            if(vm.Success)
            {
                return Ok(vm);
            }
            return BadRequest(vm);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAmenity([FromQuery]AmenityDeleteRequest request, CancellationToken cancellationToken)
        {
            ValidationResult validate = await _validatorDetete.ValidateAsync(request);
            if (!validate.IsValid)
            {
                validate.AddToModelState(this.ModelState);
                return BadRequest(ModelState);
            }
            AmenityDeleteViewModel vm = new(_amenityReadWriteRespository, _localizationService, _mapper);

            await vm.HandleAsync(request, cancellationToken);
            if(vm.Success) 
            {
                return Ok(vm);
            }

            return BadRequest(vm);
        }
    }
}
