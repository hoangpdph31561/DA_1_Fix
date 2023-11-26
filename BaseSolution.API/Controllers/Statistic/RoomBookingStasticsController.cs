using AutoMapper;
using BaseSolution.Application.DataTransferObjects.Statistic.RoomBooking;
using BaseSolution.Application.DataTransferObjects.Statistic.RoomBooking.Request;
using BaseSolution.Application.Interfaces.Repositories.ReadOnly;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Infrastructure.Implements.Repositories.ReadOnly;
using BaseSolution.Infrastructure.Implements.Services;
using BaseSolution.Infrastructure.ViewModels.Amenity;
using BaseSolution.Infrastructure.ViewModels.Statistic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BaseSolution.API.Controllers.Statistic
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomBookingStasticsController : ControllerBase
    {
        private readonly IRoombookingStatisticReadOnlyRepository _roombookingStatisticReadOnlyRepository;
        private readonly IMapper _mapper;
        private readonly ILocalizationService _localizationService;

        public RoomBookingStasticsController(IRoombookingStatisticReadOnlyRepository roombookingStatisticReadOnlyRepository , IMapper mapper, ILocalizationService localizationService)
        {
            _roombookingStatisticReadOnlyRepository = roombookingStatisticReadOnlyRepository;
            _mapper = mapper;
            _localizationService = localizationService;
        }
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] ViewRoomBookingWithPaginationRequest request, CancellationToken cancellationToken)
        {
            GetRoomBookingStasticViewModel vm = new(_roombookingStatisticReadOnlyRepository, _localizationService);
            await vm.HandleAsync(request, cancellationToken);
            if (vm.Success)
            {
                List<RoomBookingStatisticDto> result = (List<RoomBookingStatisticDto>)vm.Data;
                return Ok(result);
            }
            return BadRequest(vm);
        }
    }
}
