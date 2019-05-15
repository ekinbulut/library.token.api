namespace Library.Authentication.Service.Settings
{
    public class TokenManagerConfig
    {
        public virtual string Secret { get; set; }
        public virtual string Audience { get; set; }
        public virtual string Issuer { get; set; }
    }
}