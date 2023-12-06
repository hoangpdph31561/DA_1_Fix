using BaseSolution.BlazorServer.Respository.Interfaces;

namespace BaseSolution.BlazorServer.Respository.Implements
{
    public class CustomerId : ICustomerId
    {
        public Guid Id { get ; set; }
    }
}
