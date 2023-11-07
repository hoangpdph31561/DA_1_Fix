namespace BaseSolution.Application.DataTransferObjects.Role.Request
{
    public class RoleDeleteRequest
    {
        public Guid Id { get; set; }
        public Guid? DeletedBy { get; set; }
    }
}
