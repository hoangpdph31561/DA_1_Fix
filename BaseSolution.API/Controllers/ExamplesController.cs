using AutoMapper;
using BaseSolution.Application.DataTransferObjects.Example.Request;
using BaseSolution.Application.Interfaces.Repositories.ReadOnly;
using BaseSolution.Application.Interfaces.Repositories.ReadWrite;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Infrastructure.ViewModels.Example;
using Microsoft.AspNetCore.Mvc;

namespace BaseSolution.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamplesController : ControllerBase
    {
        public readonly IExampleReadOnlyRepository _exampleReadOnlyRepository;
        public readonly IExampleReadWriteRepository _exampleReadWriteRepository;
        private readonly ILocalizationService _localizationService;
        private readonly IMapper _mapper;

        public ExamplesController(IExampleReadOnlyRepository exampleReadOnlyRepository, IExampleReadWriteRepository exampleReadWriteRepository, ILocalizationService localizationService, IMapper mapper)
        {
            _exampleReadOnlyRepository = exampleReadOnlyRepository;
            _exampleReadWriteRepository = exampleReadWriteRepository;
            _localizationService = localizationService;
            _mapper = mapper;
        }

        // GET api/<ExampleController>/5
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] ViewExampleWithPaginationRequest request, CancellationToken cancellationToken)
        {
            ExampleListWithPaginationViewModel vm = new(_exampleReadOnlyRepository, _localizationService);

            await vm.HandleAsync(request, cancellationToken);

            return Ok(vm);
        }

        // GET api/<ExampleController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
        {
            ExampleViewModel vm = new(_exampleReadOnlyRepository, _localizationService);

            await vm.HandleAsync(id, cancellationToken);

            return Ok(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Post(ExampleCreateRequest request, CancellationToken cancellationToken)
        {
            ExampleCreateViewModel vm = new(_exampleReadOnlyRepository, _exampleReadWriteRepository, _localizationService, _mapper);

            await vm.HandleAsync(request, cancellationToken);

            return Ok(vm);
        }

        [HttpPut]
        public async Task<IActionResult> Put(ExampleUpdateRequest request, CancellationToken cancellationToken)
        {
            ExampleUpdateViewModel vm = new(_exampleReadWriteRepository, _localizationService, _mapper);

            await vm.HandleAsync(request, cancellationToken);

            return Ok(vm);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(ExampleDeleteRequest request, CancellationToken cancellationToken)
        {
            ExampleDeleteViewModel vm = new(_exampleReadWriteRepository, _localizationService, _mapper);

            await vm.HandleAsync(request, cancellationToken);

            return Ok(vm);
        }
    }
}