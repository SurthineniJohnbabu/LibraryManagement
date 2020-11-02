using LibraryManagement.API.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryManagement.API.BusinessLogic.Books
{
    public interface IBooksManager
    {
        Task DeleteBookEntry(string bookName);
        Task<List<BookReviewModel>> GetBookReviews(string bookName);
        Task<List<BookModel>> GetBooksDetails(string bookName = null);
        Task InsertBookEntry(BookModel bookModel);
        Task<BookModel> UpdateBookDetails(BookModel bookModel);
        Task<BookReviewModel> UpdateBookReview(UserBookModel userBook);
        Task InsertUserBookMappingEntry(UserBookModel userBook);
    }
}
