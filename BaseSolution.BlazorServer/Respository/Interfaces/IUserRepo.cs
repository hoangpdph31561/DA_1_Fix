namespace BaseSolution.BlazorServer.Respository.Interfaces
{
    public interface IUserRepo
    {
        Task<bool> ConfirmAccountAsync(string username, string password);
    }
}
