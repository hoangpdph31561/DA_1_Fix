using AutoMapper;
using BaseSolution.Application.DataTransferObjects.Customer.Request;
using BaseSolution.Application.Interfaces.Repositories.ReadOnly;
using BaseSolution.Application.Interfaces.Repositories.ReadWrite;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Infrastructure.Extensions;
using BaseSolution.Infrastructure.ViewModels.Customer;
using MailKit.Security;
using Microsoft.AspNetCore.Mvc;
using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.AspNetCore.Authorization;
using BaseSolution.Application.DataTransferObjects.Customer;

namespace BaseSolution.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        public readonly ICustomerReadOnlyRepository _CustomerReadOnlyRepository;
        public readonly ICustomerReadWriteRepository _CustomerReadWriteRepository;
        private readonly ILocalizationService _localizationService;
        private readonly IMapper _mapper;

        public CustomersController(ICustomerReadOnlyRepository CustomerReadOnlyRepository, ICustomerReadWriteRepository CustomerReadWriteRepository, ILocalizationService localizationService, IMapper mapper)
        {
            _CustomerReadOnlyRepository = CustomerReadOnlyRepository;
            _CustomerReadWriteRepository = CustomerReadWriteRepository;
            _localizationService = localizationService;
            _mapper = mapper;
        }

        // GET api/<CustomerController>/5
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] ViewCustomerWithPaginationRequest request, CancellationToken cancellationToken)
        {
            CustomerListWithPaginationViewModel vm = new(_CustomerReadOnlyRepository, _localizationService);

            await vm.HandleAsync(request, cancellationToken);

            return Ok(vm);
        }

        // GET api/<CustomerController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
        {
            CustomerViewModel vm = new(_CustomerReadOnlyRepository, _localizationService);

            await vm.HandleAsync(id, cancellationToken);

            return Ok(vm);
        }
        [HttpGet("{identification}/details")]
        public async Task<IActionResult> GetIdentificationNumber(string identification, CancellationToken cancellationToken)
        {
            CustomerIdentificationViewModel vm = new(_CustomerReadOnlyRepository, _localizationService);

            await vm.HandleAsync(identification, cancellationToken);
            return Ok(vm);
        }
        [AllowAnonymous]
        [HttpPost("sendGmail")]
        public async Task<IActionResult> SendGmailAsync([FromBody]string emailAddress)
        {
            var code = UtilityExtensions.GenerateRandomString(6);
            if (EmailVerification.SetCodeForEmail(emailAddress, code))
            {
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse("hainhph35304@fpt.edu.vn"));
                email.To.Add(MailboxAddress.Parse(emailAddress));
                email.Subject = "Mã đăng nhập";

                var body = new TextPart("html")
                {
                    Text = "<h1><strong>" + code + "</strong></h1>"
                };
                email.Body = body;

                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                    await client.AuthenticateAsync("hainhph35304@fpt.edu.vn", "jumm lvtt uavb kebq");
                    await client.SendAsync(email);
                    client.Disconnect(true);
                }
            }
            return Ok();
        }
        [AllowAnonymous]
        [HttpPost("verifyCode")]
        public async Task<IActionResult> VerifyCode([FromBody] string code)
        {
            if (EmailVerification.ConfirmCode(code))
            {
                return Ok();
            }
            else
            {
                return BadRequest("Mã xác nhận không đúng vui lòng kiểm tra lại");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Post(CustomerCreateRequest request, CancellationToken cancellationToken)
        {
            CustomerCreateViewModel vm = new(_CustomerReadOnlyRepository, _CustomerReadWriteRepository, _localizationService, _mapper);

            await vm.HandleAsync(request, cancellationToken);

            return Ok(vm);
        }

        [HttpPut]
        public async Task<IActionResult> Put(CustomerUpdateRequest request, CancellationToken cancellationToken)
        {
            CustomerUpdateViewModel vm = new(_CustomerReadWriteRepository, _localizationService, _mapper);

            await vm.HandleAsync(request, cancellationToken);

            return Ok(vm);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(CustomerDeleteRequest request, CancellationToken cancellationToken)
        {
            CustomerDeleteViewModel vm = new(_CustomerReadWriteRepository, _localizationService, _mapper);

            await vm.HandleAsync(request, cancellationToken);

            return Ok(vm);
        }
    }
}
