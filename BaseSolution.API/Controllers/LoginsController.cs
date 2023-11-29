using AutoMapper;
using BaseSolution.Application.DataTransferObjects.Account.request;
using BaseSolution.Application.Interfaces.Login;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Infrastructure.Implements.Login;
using BaseSolution.Infrastructure.Implements.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BaseSolution.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginsController : ControllerBase
    {
        private readonly ILoginService _loginService;
        private readonly ILocalizationService _localizationService;
        private readonly IMapper _mapper;

        public LoginsController(ILoginService loginService,ILocalizationService localizationService,IMapper mapper)
        {
            _loginService = loginService;
            _localizationService = localizationService;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginInputRequest request)
        {
            var result = await _loginService.Login(request);
            return Ok(result);
        }
    }
}
