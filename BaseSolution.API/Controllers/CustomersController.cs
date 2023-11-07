using AutoMapper;
using BaseSolution.Application.DataTransferObjects.Customer.Request;
using BaseSolution.Application.Interfaces.Repositories.ReadOnly;
using BaseSolution.Application.Interfaces.Repositories.ReadWrite;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Infrastructure.ViewModels.Customer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BaseSolution.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        public readonly ICustomerReadOnlyRepository _CustomerReadOnlyRepository;
        public readonly ICustomerReadWriteRepository _CustomerReadWriteRepository;
        private readonly ILocalizationService _localizationService;
        private readonly IMapper _mapper;

        public CustomersController(ICustomerReadOnlyRepository CustomerReadOnlyRepository, ICustomerReadWriteRepository CustomerReadWriteRepository, ILocalizationService localizationService, IMapper mapper)
        {
            _CustomerReadOnlyRepository = CustomerReadOnlyRepository;
            _CustomerReadWriteRepository = CustomerReadWriteRepository;
            _localizationService = localizationService;
            _mapper = mapper;
        }

        // GET api/<CustomerController>/5
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] ViewCustomerWithPaginationRequest request, CancellationToken cancellationToken)
        {
            CustomerListWithPaginationViewModel vm = new(_CustomerReadOnlyRepository, _localizationService);

            await vm.HandleAsync(request, cancellationToken);

            return Ok(vm);
        }

        // GET api/<CustomerController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
        {
            CustomerViewModel vm = new(_CustomerReadOnlyRepository, _localizationService);

            await vm.HandleAsync(id, cancellationToken);

            return Ok(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CustomerCreateRequest request, CancellationToken cancellationToken)
        {
            CustomerCreateViewModel vm = new(_CustomerReadOnlyRepository, _CustomerReadWriteRepository, _localizationService, _mapper);

            await vm.HandleAsync(request, cancellationToken);

            return Ok(vm);
        }

        [HttpPut]
        public async Task<IActionResult> Put(CustomerUpdateRequest request, CancellationToken cancellationToken)
        {
            CustomerUpdateViewModel vm = new(_CustomerReadWriteRepository, _localizationService, _mapper);

            await vm.HandleAsync(request, cancellationToken);

            return Ok(vm);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(CustomerDeleteRequest request, CancellationToken cancellationToken)
        {
            CustomerDeleteViewModel vm = new(_CustomerReadWriteRepository, _localizationService, _mapper);

            await vm.HandleAsync(request, cancellationToken);

            return Ok(vm);
        }
    }
}
