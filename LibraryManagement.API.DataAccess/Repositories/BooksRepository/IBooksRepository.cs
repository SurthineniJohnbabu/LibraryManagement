using LibraryManagement.API.Core.Models;
using System.Data;
using System.Threading.Tasks;

namespace LibraryManagement.API.DataAccess.Repositories.BooksRepository
{
    public interface IBooksRepository
    {
        Task DeleteBookEntry(string bookName);
        Task<DataSet> GetBookReviews(string bookName);
        Task<DataSet> GetBooksDetails(string bookName = null);
        Task InsertBookEntry(BookModel bookModel);
        Task<DataSet> UpdateBookDetails(BookModel bookModel);
        Task<DataSet> UpdateBookReview(UserBookModel userBook);
        Task InsertUserBookMappingEntry(UserBookModel userBook);
    }
}
