namespace SmartPoint.Administrator.ApplicationService.Administrator.Response
{
    public class UserToken
    {
        public required string Id { get; set; }
        public required string Email { get; set; }
        public IEnumerable<UserClaim>? Claims { get; set; }
    }
}