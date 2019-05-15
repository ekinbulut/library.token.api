namespace Library.Authentication.Service.ServiceModels
{
    public class LoginUserModel : Profile
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }

        public UserInformation Information { get; set; } = new UserInformation();
    }
}