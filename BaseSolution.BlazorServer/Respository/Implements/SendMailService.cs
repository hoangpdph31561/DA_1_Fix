using BaseSolution.BlazorServer.Respository.Interfaces;
using System.Net.Mail;
using System.Text.Json;

namespace BaseSolution.BlazorServer.Respository.Implements
{
    public class SendMailService : ISendMailService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<SendMailService> logger;

        public SendMailService(ILogger<SendMailService> _logger, HttpClient httpClient)
        {
            logger = _logger;
            logger.LogInformation("Create SendMailService");
            _httpClient = httpClient;
        }

        public async Task SendMail(string mailAddress)
        {
            var emailAddress = JsonSerializer.Serialize(mailAddress);
            await _httpClient.PostAsJsonAsync("api/Customers/sendGmail", emailAddress);
            logger.LogInformation("send mail to " + mailAddress);
        }
        public async Task<bool> ConfirmCode(string code)
        {
            var verifyCode = JsonSerializer.Serialize(code);
            var result = await _httpClient.PostAsJsonAsync("api/Customers/verifyCode", verifyCode);
            if (result.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }     
        }
    }
}
