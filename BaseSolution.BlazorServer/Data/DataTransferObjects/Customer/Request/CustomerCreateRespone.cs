using Newtonsoft.Json;

namespace BaseSolution.BlazorServer.Data.DataTransferObjects.Customer.Request
{
    public class IdCustomer
    {
        public Guid Id { get; set; }

        private static IdCustomer instance;
        public static IdCustomer Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new IdCustomer();
                }
                return instance;
            }
        }
    }
}
