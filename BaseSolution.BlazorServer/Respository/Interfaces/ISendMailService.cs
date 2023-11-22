
namespace BaseSolution.BlazorServer.Respository.Interfaces
{
    public interface ISendMailService
    {
        Task SendMail(string email);
        Task<bool> ConfirmCode(string code);
    }
}
