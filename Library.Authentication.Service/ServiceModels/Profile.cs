using System;

namespace Library.Authentication.Service.ServiceModels
{
    public abstract class Profile
    {
        public int Id { get; set; }
        public DateTime TimeStamp { get; set; } = DateTime.Now;
    }
}