using AutoMapper;
using BaseSolution.Application.DataTransferObjects.ServiceType.Request;
using BaseSolution.Application.Interfaces.Repositories.ReadOnly;
using BaseSolution.Application.Interfaces.Repositories.ReadWrite;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Infrastructure.ViewModels.ServiceType;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BaseSolution.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceTypesController : ControllerBase
    {
        public readonly IServiceTypeReadOnlyRepository _ServiceTypeReadOnlyRepository;
        public readonly IServiceTypeReadWriteRepository _ServiceTypeReadWriteRepository;
        private readonly ILocalizationService _localizationService;
        private readonly IMapper _mapper;

        public ServiceTypesController(IServiceTypeReadOnlyRepository ServiceTypeReadOnlyRepository, IServiceTypeReadWriteRepository ServiceTypeReadWriteRepository, ILocalizationService localizationService, IMapper mapper)
        {
            _ServiceTypeReadOnlyRepository = ServiceTypeReadOnlyRepository;
            _ServiceTypeReadWriteRepository = ServiceTypeReadWriteRepository;
            _localizationService = localizationService;
            _mapper = mapper;
        }

        // GET api/<ServiceTypeController>/5
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] ViewServiceTypeWithPaginationRequest request, CancellationToken cancellationToken)
        {
            ServiceTypeListWithPaginationViewModel vm = new(_ServiceTypeReadOnlyRepository, _localizationService);

            await vm.HandleAsync(request, cancellationToken);

            return Ok(vm);
        }

        // GET api/<ServiceTypeController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
        {
            ServiceTypeViewModel vm = new(_ServiceTypeReadOnlyRepository, _localizationService);

            await vm.HandleAsync(id, cancellationToken);

            return Ok(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Post(ServiceTypeCreateRequest request, CancellationToken cancellationToken)
        {
            ServiceTypeCreateViewModel vm = new(_ServiceTypeReadOnlyRepository, _ServiceTypeReadWriteRepository, _localizationService, _mapper);

            await vm.HandleAsync(request, cancellationToken);

            return Ok(vm);
        }

        [HttpPut]
        public async Task<IActionResult> Put(ServiceTypeUpDateRequest request, CancellationToken cancellationToken)
        {
            ServiceTypeUpdateViewModel vm = new(_ServiceTypeReadWriteRepository, _localizationService, _mapper);

            await vm.HandleAsync(request, cancellationToken);

            return Ok(vm);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(ServiceTypeDeleteRequest request, CancellationToken cancellationToken)
        {
            ServiceTypeDeleteViewModel vm = new(_ServiceTypeReadWriteRepository, _localizationService, _mapper);

            await vm.HandleAsync(request, cancellationToken);

            return Ok(vm);
        }
    }
}
