using AutoMapper;
using BaseSolution.Application.DataTransferObjects.Amenity;
using BaseSolution.Application.DataTransferObjects.Amenity.Request;
using BaseSolution.Application.Interfaces.Repositories.ReadOnly;
using BaseSolution.Application.Interfaces.Repositories.ReadWrite;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Application.ValueObjects.Pagination;
using BaseSolution.Infrastructure.ViewModels.Amenity;
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
        public AmenitiesController(IAmenityReadOnlyRepository amenityReadOnlyRepository, IAmenityReadWriteRepository amenityReadWriteRepository, IMapper mapper, ILocalizationService localizationService)
        {
            _amenityReadOnlyRespository = amenityReadOnlyRepository;
            _amenityReadWriteRespository = amenityReadWriteRepository;
            _mapper = mapper;
            _localizationService = localizationService;
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
            AmenityCreateViewModel vm = new(_amenityReadOnlyRespository, _amenityReadWriteRespository, _mapper, _localizationService);
            await vm.HandleAsync(request, cancellationToken);
            return Ok(vm);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAmenity(AmenityUpdateRequest request, CancellationToken cancellationToken)
        {
            AmenityUpdateViewModel vm = new(_amenityReadWriteRespository, _mapper, _localizationService);
            await vm.HandleAsync(request, cancellationToken);
            return Ok(vm);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAmenity([FromQuery]AmenityDeleteRequest request, CancellationToken cancellationToken)
        {
            AmenityDeleteViewModel vm = new(_amenityReadWriteRespository, _localizationService, _mapper);

            await vm.HandleAsync(request, cancellationToken);

            return Ok(vm);
        }
    }
}
