using AutoMapper;
using BaseSolution.Application.DataTransferObjects.Statistic.Bill;
using BaseSolution.Application.DataTransferObjects.Statistic.Bill.Request;
using BaseSolution.Application.DataTransferObjects.Statistic.RoomBooking;
using BaseSolution.Application.Interfaces.Repositories.ReadOnly;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Infrastructure.ViewModels.Statistic;
using Microsoft.AspNetCore.Mvc;

namespace BaseSolution.API.Controllers.Statistic
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillStatisticsController : ControllerBase
    {
        private readonly IBillStatisticReadOnlyRespository _billStatisticReadOnly;
        private readonly ILocalizationService _localizationService;

        public BillStatisticsController(IBillStatisticReadOnlyRespository billStatisticReadOnly , ILocalizationService localizationService)
        {
            _billStatisticReadOnly = billStatisticReadOnly;
            _localizationService = localizationService;
        }
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] BillBillStatisticRequest request, CancellationToken cancellationToken)
        {
            GetBillStatisticViewModel vm = new(_billStatisticReadOnly, _localizationService);
            await vm.HandleAsync(request, cancellationToken);
            if (vm.Success)
            {
                List<BillStatisticDto> result = (List<BillStatisticDto>)vm.Data;
                return Ok(result);
            }
            return BadRequest(vm);
        }
    }
}
