using System.Diagnostics.CodeAnalysis;

namespace Library.Common.Contracts.Events.BookEvents
{
    [ExcludeFromCodeCoverage]
    public class BookCreated
    {
        public int BookId { get; set; }
        public string AuthorId { get; set; }
        public int PublisherId { get; set; }
        public string LibraryId { get; set; }
        public int ShelfId { get; set; }
    }
}