using AutoMapper;
using BaseSolution.Application.DataTransferObjects.Services;
using BaseSolution.Application.DataTransferObjects.Services.Request;
using BaseSolution.Application.Interfaces.Repositories.ReadOnly;
using BaseSolution.Application.Interfaces.Repositories.ReadWrite;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Application.ValueObjects.Pagination;
using BaseSolution.Infrastructure.ViewModels.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BaseSolution.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        public readonly IServiceReadOnlyRepository _ServiceReadOnlyRepository;
        public readonly IServicesReadWriteRepository _ServiceReadWriteRepository;
        private readonly ILocalizationService _localizationService;
        private readonly IMapper _mapper;

        public ServicesController(IServiceReadOnlyRepository ServiceReadOnlyRepository, IServicesReadWriteRepository ServiceReadWriteRepository, ILocalizationService localizationService, IMapper mapper)
        {
            _ServiceReadOnlyRepository = ServiceReadOnlyRepository;
            _ServiceReadWriteRepository = ServiceReadWriteRepository;
            _localizationService = localizationService;
            _mapper = mapper;
        }

        // GET api/<ServiceController>/5
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] ViewServiceWithPaginationRequest request, CancellationToken cancellationToken)
        {
            ServiceListWithPaginationViewModel vm = new(_ServiceReadOnlyRepository, _localizationService);

            await vm.HandleAsync(request, cancellationToken);
            if (vm.Success)
            {
                PaginationResponse<ServiceDTO> result = (PaginationResponse<ServiceDTO>)vm.Data;
                return Ok(result);
            }

            return BadRequest(vm);
        }

        [HttpGet("getServiceAsync")]
        public async Task<IActionResult> GetServiceAsync([FromQuery] ViewServiceWithPaginationRequest request, CancellationToken cancellationToken)
        {
            GetServiceViewModel vm = new(_ServiceReadOnlyRepository, _localizationService);

            await vm.HandleAsync(request, cancellationToken);
            if (vm.Success)
            {
                List<ServiceDTO> result = (List<ServiceDTO>)vm.Data;
                return Ok(result);
            }

            return BadRequest(vm);
        }


        // GET api/<ServiceController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
        {
            ServiceViewModel vm = new(_ServiceReadOnlyRepository, _localizationService);

            await vm.HandleAsync(id, cancellationToken);
            if (vm.Success)
            {
                return Ok((ServiceDTO)vm.Data);
            }
            return BadRequest(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Post(ServiceCreateRequest request, CancellationToken cancellationToken)
        {
            ServiceCreateViewModel vm = new(_ServiceReadOnlyRepository, _ServiceReadWriteRepository, _localizationService, _mapper);

            await vm.HandleAsync(request, cancellationToken);
            if (vm.Success)
            {
                return Ok(vm);
            }
            return BadRequest(vm);
        }

        [HttpPut]
        public async Task<IActionResult> Put(ServiceUpdateRequest request, CancellationToken cancellationToken)
        {
            ServiceUpdateViewModel vm = new(_ServiceReadWriteRepository, _localizationService, _mapper);

            await vm.HandleAsync(request, cancellationToken);
            if (vm.Success)
            {
                return Ok(vm);
            }
            return BadRequest(vm);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] ServiceDeleteRequest request, CancellationToken cancellationToken)
        {
            ServiceDeleteViewModel vm = new(_ServiceReadWriteRepository, _localizationService, _mapper);

            await vm.HandleAsync(request, cancellationToken);
            if (vm.Success)
            {
                return Ok(vm);
            }
            return BadRequest(vm);
        }
    }
}
