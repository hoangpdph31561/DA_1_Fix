using BaseSolution.BlazorServer.Data.DataTransferObjects.Acount;
using BaseSolution.BlazorServer.Data.DataTransferObjects.Acount.Request;

namespace BaseSolution.BlazorServer.Respository.Interfaces
{
    public interface ILogin
    {
         Task<ViewLoginInput> Login(LoginInputRequest request);
    }
}
