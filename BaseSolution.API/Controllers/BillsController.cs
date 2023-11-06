using AutoMapper;
using BaseSolution.Application.DataTransferObjects.Bill.Request;
using BaseSolution.Application.Interfaces.Repositories.ReadOnly;
using BaseSolution.Application.Interfaces.Repositories.ReadWrite;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Infrastructure.ViewModels.Bill;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BaseSolution.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillsController : ControllerBase
    {
        private readonly IBillReadOnlyRespository _billReadOnlyRespository;
        private readonly IBillReadWriteRespository _billReadWriteRespository;
        private readonly IMapper _mapper;
        private readonly ILocalizationService _localizationService;
        public BillsController(IBillReadOnlyRespository billReadOnlyRespository, IBillReadWriteRespository billReadWriteRespository, ILocalizationService localizationService, IMapper mapper)
        {
            _billReadOnlyRespository = billReadOnlyRespository;
            _billReadWriteRespository = billReadWriteRespository;
            _mapper = mapper;
            _localizationService = localizationService;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBillById(Guid id, CancellationToken cancellationToken)
        {
            BillViewModel vm = new(_billReadOnlyRespository, _localizationService);
            await vm.HandleAsync(id, cancellationToken);
            return Ok(vm);
        }
        [HttpGet("getBillsByAdmin")]
        public async Task<IActionResult> GetBillsByAdmin([FromQuery]ViewBillWithPaginationRequest request, CancellationToken cancellationToken)
        {
            BillListWithPaginationByAdminViewModel vm = new(_billReadOnlyRespository, _localizationService);
            await vm.HandleAsync(request, cancellationToken);
            return Ok(vm);
        }
        [HttpGet("getBillByOther")]
        public async Task<IActionResult> GetBillsByOther([FromQuery]ViewBillWithPaginationRequest request, CancellationToken cancellationToken)
        {
            BillListWithPaginationByOtherViewModel vm = new(_billReadOnlyRespository, _localizationService);
            await vm.HandleAsync(request, cancellationToken);
            return Ok(vm);
        }
        [HttpPost]
        public async Task<IActionResult> CreateBill(BillCreateRequest request, CancellationToken cancellationToken)
        {
            BillCreateViewModel vm = new(_billReadOnlyRespository, _billReadWriteRespository, _localizationService, _mapper);
            await vm.HandleAsync(request, cancellationToken);
            return Ok(vm);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateBill(BillUpdateRequest request, CancellationToken cancellationToken)
        {
            BillUpdateViewModel vm = new(_billReadWriteRespository, _mapper, _localizationService);
            await vm.HandleAsync(request, cancellationToken);
            return Ok(vm);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteBill(BillDeleteRequest request, CancellationToken cancellationToken)
        {
            BillDeleteViewModel vm = new(_billReadWriteRespository, _localizationService);
            await vm.HandleAsync(request, cancellationToken);
            return Ok(vm);
        }
    }
}
