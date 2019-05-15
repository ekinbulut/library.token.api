using System;

namespace Library.Authentication.Service.ServiceModels.Books
{
    public class BookModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PublisherId { get; set; }
        public string AuthorId { get; set; }
        public DateTime PublishDate { get; set; }
        public string Tag { get; set; }
        public int No { get; set; }
        public int SkinType { get; set; }
        public string LibraryId { get; set; }
        public int ShelfId { get; set; }
    }
}