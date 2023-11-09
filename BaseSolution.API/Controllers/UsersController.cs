using AutoMapper;
using BaseSolution.Application.DataTransferObjects.Amenity.Request;
using BaseSolution.Application.DataTransferObjects.User.Request;
using BaseSolution.Application.Interfaces.Repositories.ReadOnly;
using BaseSolution.Application.Interfaces.Repositories.ReadWrite;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Infrastructure.Implements.Repositories.ReadWrite;
using BaseSolution.Infrastructure.ViewModels.Amenity;
using BaseSolution.Infrastructure.ViewModels.User;
using Microsoft.AspNetCore.Mvc;

namespace BaseSolution.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserReadOnlyRepository _userReadOnlyRespository;
        private readonly IUserReadWriteRepository _userReadWriteRespository;
        private readonly IMapper _mapper;
        private readonly ILocalizationService _localizationService;
        public UsersController(IUserReadOnlyRepository userReadOnlyRepository, IUserReadWriteRepository userReadWriteRepository,
            IMapper mapper, ILocalizationService localizationService)
        {
            _userReadOnlyRespository = userReadOnlyRepository;
            _userReadWriteRespository = userReadWriteRepository;
            _mapper = mapper;
            _localizationService = localizationService;
        }
        [HttpGet]
        public async Task<IActionResult> GetLisUserByAdmin([FromQuery] ViewUserWithPaginationRequest request, CancellationToken cancellationToken)
        {
            UserListWithPaginationViewModel vm = new(_userReadOnlyRespository, _localizationService);
            await vm.HandleAsync(request, cancellationToken);
            return Ok(vm);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(Guid id, CancellationToken cancellationToken)
        {
            UserViewModel vm = new(_userReadOnlyRespository, _localizationService);
            await vm.HandleAsync(id, cancellationToken);
            return Ok(vm);
        }
        [HttpPost]
        public async Task<IActionResult> CreateNewUser(UserCreateRequest request, CancellationToken cancellationToken)
        {
            UserCreateViewModel vm = new(_userReadOnlyRespository, _userReadWriteRespository, _mapper, _localizationService);
            await vm.HandleAsync(request, cancellationToken);
            return Ok(vm);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser(UserUpdateRequest request, CancellationToken cancellationToken)
        {
            UserUpdateViewModel vm = new(_userReadWriteRespository, _mapper, _localizationService);
            await vm.HandleAsync(request, cancellationToken);
            return Ok(vm);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteUser(UserDeleteRequest request, CancellationToken cancellationToken)
        {
            UserDeleteViewModel vm = new(_userReadWriteRespository, _localizationService, _mapper);

            await vm.HandleAsync(request, cancellationToken);

            return Ok(vm);
        }

    }
}
