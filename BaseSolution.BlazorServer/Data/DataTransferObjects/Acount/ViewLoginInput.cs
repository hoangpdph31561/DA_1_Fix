namespace BaseSolution.BlazorServer.Data.DataTransferObjects.Acount
{
    public class ViewLoginInput
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Guid UserRoleId { get; set; }
        public string RoleCode { get; set; }
        public string Name { get; set; }
    }
}
