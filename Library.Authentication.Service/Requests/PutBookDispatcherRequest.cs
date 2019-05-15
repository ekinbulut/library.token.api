using System;

namespace Library.Authentication.Service.Requests
{
    public class PutBookDispatcherRequest
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string AuthorId { get; set; }
        public int PublisherId { get; set; }
        public string Tag { get; set; } //genre
        public int No { get; set; }
        public DateTime PublisherDate { get; set; }
        public int SkinType { get; set; }
        public string LibraryId { get; set; }
        public int Id { get; set; }
        public int ShelfId { get; set; }
    }
}