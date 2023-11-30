using AutoMapper;
using BaseSolution.Application.DataTransferObjects.RoomBookingDetail.Request;
using BaseSolution.Application.DataTransferObjects.ServiceOrder;
using BaseSolution.Application.DataTransferObjects.ServiceOrderDetail;
using BaseSolution.Application.DataTransferObjects.ServiceOrderDetail.Request;
using BaseSolution.Application.Interfaces.Repositories.ReadOnly;
using BaseSolution.Application.Interfaces.Repositories.ReadWrite;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Infrastructure.ViewModels.RoomBookingDetail;
using BaseSolution.Infrastructure.ViewModels.ServiceOrderDetail;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BaseSolution.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceOrderDetailsController : ControllerBase
    {
        private readonly IServiceOrderDetailReadOnlyRespository _serviceOrderDetailReadOnly;
        private readonly ILocalizationService _localizationService;
        private readonly IMapper _mapper;
        private readonly IServiceOrderDetailReadWriteRespository _serviceOrderDetailReadWrite;
        public ServiceOrderDetailsController(IServiceOrderDetailReadOnlyRespository serviceOrderDetailReadOnly, IServiceOrderDetailReadWriteRespository serviceOrderDetailReadWrite, IMapper mapper, ILocalizationService localizationService)
        {
            _serviceOrderDetailReadWrite = serviceOrderDetailReadWrite;
            _localizationService = localizationService;
            _mapper = mapper;
            _serviceOrderDetailReadOnly = serviceOrderDetailReadOnly;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetServiceOrderDetailById(Guid id, CancellationToken cancellationToken)
        {
            ServiceOrderDetailViewModel vm = new(_serviceOrderDetailReadOnly, _localizationService);
            await vm.HandleAsync(id, cancellationToken);
            return Ok(vm);
        }
        [HttpGet("getServiceOrderDetailByAdmin")]
        public async Task<IActionResult> GetServiceOrderDetailsByAdmin([FromQuery]ViewServiceOrderDetailWithPaginationRequest request, CancellationToken cancellationToken)
        {
            ServiceOrderDetailListWithPaginationViewModelByAdmin vm = new(_serviceOrderDetailReadOnly, _localizationService);
            await vm.HandleAsync(request,cancellationToken);
            return Ok(vm);
        }
        [HttpGet("getServiceOrderDetailByOther")]
        public async Task<IActionResult> GetServiceOrderDetailsByOther([FromQuery]ViewServiceOrderDetailWithPaginationRequest request, CancellationToken cancellationToken)
        {
            ServiceOrderDetailListWithPaginationViewModelByOther vm = new(_serviceOrderDetailReadOnly, _localizationService);
            await vm.HandleAsync(request, cancellationToken);
            return Ok(vm);
        }

         [HttpGet("getServiceOrderDetailByIdServiceOrder")]
        public async Task<IActionResult> GetServiceOrderDetailByIdServiceOrder([FromQuery] ViewServiceOrderDetailByIdServiceOderRequest request, CancellationToken cancellationToken)
        {
            ServiceOrderDetailViewModelByServiceOrderId vm = new(_serviceOrderDetailReadOnly, _localizationService);
            await vm.HandleAsync(request, cancellationToken);
            if(vm.Success)
            {
                List<ServiceOrderDetailDTO> result = (List<ServiceOrderDetailDTO>)vm.Data;
                return Ok(result);
            }
            return BadRequest(vm);
        }
        [HttpPost]
        public async Task<IActionResult> CreateNewServiceOrderDetail(ServiceOrderDetailCreateRequest request, CancellationToken cancellationToken)
        {
            ServiceOrderDetailCreateViewModel vm = new(_serviceOrderDetailReadOnly, _serviceOrderDetailReadWrite, _mapper, _localizationService);
            await vm.HandleAsync(request, cancellationToken);
            return Ok(vm);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateServiceOrderDetail(ServiceOrderDetailUpdateRequest request, CancellationToken cancellationToken)
        {
            ServiceOrderDetailUpdateViewModel vm = new(_serviceOrderDetailReadWrite, _mapper, _localizationService);
            await vm.HandleAsync(request, cancellationToken);
            return Ok(vm);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteServiceOrderDetail(ServiceOrderDetailDeleteRequest request, CancellationToken cancellationToken)
        {
            ServiceOrderDetailDeleteViewModel vm = new(_serviceOrderDetailReadWrite, _localizationService);
            await vm.HandleAsync(request,cancellationToken);
            return Ok(vm);
        }
    }
}
