namespace BaseSolution.BlazorServer.Data.DataTransferObjects.Acount.Request
{
    public class LoginInputRequest
    {
        
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public Guid? UserRoleId { get; set; }
    }
}
