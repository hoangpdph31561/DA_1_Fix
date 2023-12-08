using AutoMapper;
using BaseSolution.Application.DataTransferObjects.Statistic.ServiceOrder;
using BaseSolution.Application.DataTransferObjects.Statistic.ServiceOrder.Request;
using BaseSolution.Application.Interfaces.Repositories.ReadOnly;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Infrastructure.ViewModels.Statistic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BaseSolution.API.Controllers.Statistic
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceOrderStatisticsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IServiceOrderStatisticReadOnlyRespository _serviceStatisticReadOnlyRepository;
        private readonly ILocalizationService _localizationService;
        public ServiceOrderStatisticsController(IServiceOrderStatisticReadOnlyRespository serviceStatisticReadOnlyRepository, ILocalizationService localizationService,IMapper mapper)
        {
            _serviceStatisticReadOnlyRepository = serviceStatisticReadOnlyRepository;
            _localizationService = localizationService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] ViewServiceOrderWithPaginationRequest request, CancellationToken cancellationToken)
        {
            GetServiceOrderStatisticViewModel vm = new(_serviceStatisticReadOnlyRepository, _localizationService);
            await vm.HandleAsync(request, cancellationToken);
            if (vm.Success)
            {
                List<ServiceOrderStatisticDto> result = (List<ServiceOrderStatisticDto>)vm.Data;
                return Ok(result);
            }
            return BadRequest(vm);
        }
    }
}
