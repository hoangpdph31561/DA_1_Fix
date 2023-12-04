using AutoMapper;
using BaseSolution.Application.DataTransferObjects.Account;
using BaseSolution.Application.DataTransferObjects.Account.request;
using BaseSolution.Application.Interfaces.Login;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Infrastructure.Implements.Login;
using BaseSolution.Infrastructure.Implements.Services;
using BaseSolution.Infrastructure.ViewModels.Login;
using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BaseSolution.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginsController : ControllerBase
    {
        private readonly ILoginService _loginService;
        private readonly ILocalizationService _localizationService;
        private readonly IMapper _mapper;
        private readonly IValidator<LoginInputRequest> _validator;

        public LoginsController(ILoginService loginService,ILocalizationService localizationService,IMapper mapper, IValidator<LoginInputRequest> validator)
        {
            _loginService = loginService;
            _localizationService = localizationService;
            _mapper = mapper;
            _validator = validator;
        }
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginInputRequest request,CancellationToken cancellationToken)
        {
            ValidationResult validate = await _validator.ValidateAsync(request);
            if (!validate.IsValid)
            {
                validate.AddToModelState(this.ModelState);
                return BadRequest(ModelState);
            }
           
            LoginViewModel vm = new(_loginService, _localizationService);
            await vm.HandleAsync(request,cancellationToken);
            if(vm.Success)
            {
                ViewLoginInput result = (ViewLoginInput)vm.Data;
                return Ok(result);
            }
            return BadRequest(vm);
        }
    }
}
