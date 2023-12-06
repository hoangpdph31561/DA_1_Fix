using AutoMapper;
using BaseSolution.Application.DataTransferObjects.Amenity.Request;
using BaseSolution.Application.DataTransferObjects.Role.Request;
using BaseSolution.Application.DataTransferObjects.User;
using BaseSolution.Application.DataTransferObjects.User.Request;
using BaseSolution.Application.Interfaces.Repositories.ReadOnly;
using BaseSolution.Application.Interfaces.Repositories.ReadWrite;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Application.ValueObjects.Pagination;
using BaseSolution.Infrastructure.Implements.Repositories.ReadOnly;
using BaseSolution.Infrastructure.Implements.Repositories.ReadWrite;
using BaseSolution.Infrastructure.ViewModels.Amenity;
using BaseSolution.Infrastructure.ViewModels.User;
using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IValidator<UserCreateRequest> _validator;
        private readonly IValidator<UserUpdateRequest> _validatorUpdate;
        private readonly IValidator<UserDeleteRequest> _validatorDelete;

        public UsersController(IUserReadOnlyRepository userReadOnlyRepository, IUserReadWriteRepository userReadWriteRepository,
            IMapper mapper, ILocalizationService localizationService, IValidator<UserCreateRequest> validator,
            IValidator<UserUpdateRequest> validatorUpdate, IValidator<UserDeleteRequest> validatorDelete)
        {
            _userReadOnlyRespository = userReadOnlyRepository;
            _userReadWriteRespository = userReadWriteRepository;
            _mapper = mapper;
            _localizationService = localizationService;
            _validator = validator;
            _validatorUpdate = validatorUpdate;
            _validatorDelete = validatorDelete;

        }
        [HttpGet]
        public async Task<IActionResult> GetListUserByAdmin([FromQuery] ViewUserWithPaginationRequest request, CancellationToken cancellationToken)
        {
            UserListWithPaginationViewModel vm = new(_userReadOnlyRespository, _localizationService);
            await vm.HandleAsync(request, cancellationToken);

            if (vm.Success)
            {
                PaginationResponse<UserDTO> result = (PaginationResponse<UserDTO>)vm.Data!;

                return Ok(result);
            }
            return BadRequest(vm);
        }
        [AllowAnonymous]
        [HttpGet("confirmAccount")]
        public async Task<IActionResult> ConfirmAccount(string username, string password, CancellationToken cancellationToken)
        {
            var getUser = await _userReadOnlyRespository.GetUserByUserNameAsync(username, cancellationToken);
            if (getUser.Data?.UserName != username || getUser.Data?.Password != password)
            {
                return BadRequest("Sai tài khoản người dùng hoặc sai mật khẩu!");
            }
            else
            {
                return Ok("Đăng nhập thành công!");
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(Guid id, CancellationToken cancellationToken)
        {
            UserViewModel vm = new(_userReadOnlyRespository, _localizationService);
            await vm.HandleAsync(id, cancellationToken);

            if (vm.Success)
            {
                UserDTO result = (UserDTO)vm.Data!;


                return Ok(result);
            }
            return BadRequest(vm);
        }
        [HttpPost]
        public async Task<IActionResult> CreateNewUser(UserCreateRequest request, CancellationToken cancellationToken)
        {
            ValidationResult validate = await _validator.ValidateAsync(request);
            if (!validate.IsValid)
            {
                validate.AddToModelState(this.ModelState);
                return BadRequest(ModelState);
            }
            UserCreateViewModel vm = new(_userReadOnlyRespository, _userReadWriteRespository, _mapper, _localizationService);
            await vm.HandleAsync(request, cancellationToken);
            if (vm.Success)
            {
                return Ok(vm);
            }

            return BadRequest(vm);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser(UserUpdateRequest request, CancellationToken cancellationToken)
        {
            ValidationResult validate = await _validatorUpdate.ValidateAsync(request);
            if (!validate.IsValid)
            {
                validate.AddToModelState(this.ModelState);
                return BadRequest(ModelState);
            }
            UserUpdateViewModel vm = new(_userReadWriteRespository, _mapper, _localizationService);
            await vm.HandleAsync(request, cancellationToken);
            if (vm.Success)
            {
                return Ok(vm);
            }

            return BadRequest(vm);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteUser([FromQuery] UserDeleteRequest request, CancellationToken cancellationToken)
        {
            ValidationResult validate = await _validatorDelete.ValidateAsync(request);
            if (!validate.IsValid)
            {
                validate.AddToModelState(this.ModelState);
                return BadRequest(ModelState);
            }
            UserDeleteViewModel vm = new(_userReadWriteRespository, _localizationService, _mapper);

            await vm.HandleAsync(request, cancellationToken);

            if (vm.Success)
            {
                return Ok(vm);
            }

            return BadRequest(vm);
        }

    }
}
