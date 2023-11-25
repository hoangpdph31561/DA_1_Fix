using AutoMapper;
using BaseSolution.Application.DataTransferObjects.ServiceOrder;
using BaseSolution.Application.DataTransferObjects.ServiceOrder.Request;
using BaseSolution.Application.Interfaces.Repositories.ReadOnly;
using BaseSolution.Application.Interfaces.Repositories.ReadWrite;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Application.ValueObjects.Pagination;
using BaseSolution.Infrastructure.ViewModels.ServiceOrder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BaseSolution.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceOrdersController : ControllerBase
    {
        private readonly IServiceOrderReadOnlyRespository _serviceOrderReadOnly;
        private readonly ILocalizationService _localizationService;
        private readonly IMapper _mapper;
        private readonly IServiceOrderReadWriteRespository _serviceOrderReadWrite;
        public ServiceOrdersController(IServiceOrderReadOnlyRespository serviceOrderReadOnly, IServiceOrderReadWriteRespository serviceOrderReadWrite, IMapper mapper, ILocalizationService localizationService)
        {
            _serviceOrderReadOnly = serviceOrderReadOnly;
            _serviceOrderReadWrite = serviceOrderReadWrite;
            _mapper = mapper;
            _localizationService = localizationService;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetServiceOrderById(Guid id, CancellationToken cancellationToken)
        {
            ServiceOrderViewModel vm = new(_serviceOrderReadOnly, _localizationService);
            await vm.HandleAsync(id, cancellationToken);
            return Ok(vm);
        }
        [HttpGet("serviceOrdersByAdmin")]
        public async Task<IActionResult> GetServiceOrdersByAdmin([FromQuery]ViewServiceOrderWithPaginationRequest request, CancellationToken cancellationToken)
        {
            ServiceOrderListWithPaginationViewModelByAdmin vm = new(_serviceOrderReadOnly, _localizationService);
            await vm.HandleAsync(request, cancellationToken);
            return Ok(vm);
        }
        [HttpGet("serviceOrdersByOther")]
        public async Task<IActionResult> GetServiceOrdersByOther([FromQuery]ViewServiceOrderWithPaginationRequest request, CancellationToken cancellationToken)
        {
            ServiceOrderListWithPaginationViewModelByOther vm = new(_serviceOrderReadOnly, _localizationService);
            await vm.HandleAsync(request, cancellationToken);
            if(vm.Success)
            {
                PaginationResponse<ServiceOrderDTO> result = (PaginationResponse<ServiceOrderDTO>)vm.Data;
                return Ok(result);
            }
            return BadRequest(vm);
        }
        [HttpGet("ServiceOrdersByIdCustomer")]
        public async Task<IActionResult> GetServiceOrdersByIdCustomer(Guid idCustomer, CancellationToken cancellationToken)
        {
            ServiceOrderListWithPaginationByIdCustomerViewModel vm = new(_serviceOrderReadOnly, _localizationService);
            await vm.HandleAsync(idCustomer, cancellationToken);
            if(vm.Success)
            {
                List<ServiceOrderDTO> result = (List<ServiceOrderDTO>)vm.Data;
                return Ok(result);
            }
            return BadRequest(vm);
        }
        [HttpPost]
        public async Task<IActionResult> CreateNewServiceOrder(ServiceOrderCreateRequest request, CancellationToken cancellationToken)
        {
            ServiceOrderCreateViewModel vm = new(_serviceOrderReadOnly, _serviceOrderReadWrite, _mapper, _localizationService);
            await vm.HandleAsync(request, cancellationToken);
            return Ok(vm);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateServiceOrder (ServiceOrderUpdateRequest request, CancellationToken cancellationToken)
        {
            ServiceOrderUpdateViewModel vm = new(_serviceOrderReadWrite, _mapper, _localizationService);
            await vm.HandleAsync (request, cancellationToken);
            return Ok(vm);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteServiceOrder(ServiceOrderDeleteRequest request, CancellationToken cancellationToken)
        {
            ServiceOrderDeleteViewModel vm = new(_serviceOrderReadWrite, _localizationService);
            await vm.HandleAsync(request, cancellationToken);
            return Ok(vm);
        }

    }
}
