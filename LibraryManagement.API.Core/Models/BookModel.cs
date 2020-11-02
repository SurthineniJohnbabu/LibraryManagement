namespace LibraryManagement.API.Core.Models
{
    public class BookModel
    {
        public string BookName { get; set; }
        public string BookAuthor { get; set; }
        public bool IsActive { get; set; }
        public string BookCategory { get; set; }
        public int BookPageCount { get; set; }
    }
}
