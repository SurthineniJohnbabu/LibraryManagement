namespace LibraryManagement.API.Core.Models
{
    public class UserBookModel
    {
        public int UserId { get; set; }
        public string BookName { get; set; }
        public bool IsFavorite { get; set; }
        public bool IsRead { get; set; }
        public string BookReview { get; set; }
    }
}
