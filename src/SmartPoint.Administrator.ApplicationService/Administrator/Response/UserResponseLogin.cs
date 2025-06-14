namespace SmartPoint.Administrator.ApplicationService.Administrator.Response
{
    public class UserResponseLogin
    {
        public required string AccessToken { get; set; }
        public double ExpiresIn { get; set; }
        public required UserToken UserToken { get; set; }
    }
}