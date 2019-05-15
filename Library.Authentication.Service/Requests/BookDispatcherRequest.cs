namespace Library.Authentication.Service.Requests
{
    public class BookDispatcherRequest
    {
        public int UserId { get; set; }
        public int Offset { get; set; }
        public int Limit { get; set; }
    }
}