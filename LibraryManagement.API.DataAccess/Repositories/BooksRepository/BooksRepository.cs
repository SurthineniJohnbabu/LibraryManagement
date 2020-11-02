using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using LibraryManagement.API.Core.Models;
using LibraryManagement.API.DataAccess.UnitOfWork;

namespace LibraryManagement.API.DataAccess.Repositories.BooksRepository
{
    public class BooksRepository : BaseRepository, IBooksRepository
    {
        public BooksRepository(IUnitOfWorkFactory unitOfWorkFactory) : base(unitOfWorkFactory)
        {
        }
        public async Task DeleteBookEntry(string bookName)
        {
            var parameters = new SqlParameter[]
            {
                new SqlParameter(Constants.Parameters.BOOK_NAME, bookName)
            };

            await base.ExecuteStoredProcedure(Constants.Procedures.LM_DELETE_BOOK, parameters);
        }

        public async Task<DataSet> GetBooksDetails(string bookName = null)
        {
            var parameters = new SqlParameter[]
            {
                new SqlParameter(Constants.Parameters.BOOK_NAME, bookName)
            };

            try
            {
                return await base.ReadFromStoredProcedure(Constants.Procedures.LM_GET_LIST_OF_BOOKS, parameters);
            }
            catch(Exception ex)
            { throw ex; }
        }

        public async Task<DataSet> GetBookReviews(string bookName)
        {
            var parameters = new SqlParameter[]
            {
                new SqlParameter(Constants.Parameters.BOOK_NAME, bookName)
            };

            return await base.ReadFromStoredProcedure(Constants.Procedures.LM_GET_ALL_USER_REVIEWS, parameters);
        }

        public async Task InsertBookEntry(BookModel bookModel)
        {
            var parameters = new SqlParameter[]
            {
                new SqlParameter(Constants.Parameters.BOOK_NAME, bookModel.BookName),
                new SqlParameter(Constants.Parameters.BOOK_AUTHOR, bookModel.BookAuthor),
                new SqlParameter(Constants.Parameters.IS_ACTIVE, bookModel.IsActive),
                new SqlParameter(Constants.Parameters.PAGE_COUNT, bookModel.BookPageCount),
                new SqlParameter(Constants.Parameters.BOOK_CATEGORY, bookModel.BookCategory)
            };

            try
            {
                await base.ExecuteStoredProcedure(Constants.Procedures.LM_INSERT_UPDATE_BOOK_MASTER, parameters);
            }
            catch(Exception ex)
            { }
        }

        public async Task<DataSet> UpdateBookDetails(BookModel bookModel)
        {
            var parameters = new SqlParameter[]
            {
                new SqlParameter(Constants.Parameters.BOOK_NAME, bookModel.BookName),
                new SqlParameter(Constants.Parameters.BOOK_AUTHOR, bookModel.BookAuthor),
                new SqlParameter(Constants.Parameters.IS_ACTIVE, bookModel.IsActive),
                new SqlParameter(Constants.Parameters.PAGE_COUNT, bookModel.BookPageCount),
                new SqlParameter(Constants.Parameters.BOOK_CATEGORY, bookModel.BookCategory)
            };

            await base.ExecuteStoredProcedure(Constants.Procedures.LM_INSERT_UPDATE_BOOK_MASTER, parameters);

            var parameter = new SqlParameter[]
            {
                new SqlParameter(Constants.Parameters.BOOK_NAME, bookModel.BookName)
            };

            return await base.ReadFromStoredProcedure(Constants.Procedures.LM_GET_LIST_OF_BOOKS, parameter);
        }

        public async Task<DataSet> UpdateBookReview(UserBookModel userBook)
        {
            var parameters = new SqlParameter[]
            {
                new SqlParameter(Constants.Parameters.USER_ID, userBook.UserId),
                new SqlParameter(Constants.Parameters.BOOK_NAME, userBook.BookName),
                new SqlParameter(Constants.Parameters.BOOK_REVIEW, userBook.BookReview)
            };

            await base.ExecuteStoredProcedure(Constants.Procedures.LM_UPDATE_REVIEW_BOOK, parameters);

            var parameter = new SqlParameter[]
            {
                new SqlParameter(Constants.Parameters.BOOK_NAME, userBook.BookName)
            };

            return await base.ReadFromStoredProcedure(Constants.Procedures.LM_GET_ALL_USER_REVIEWS, parameter);
        }

        public async Task InsertUserBookMappingEntry(UserBookModel userBook)
        {
            var parameters = new SqlParameter[]
            {
                new SqlParameter(Constants.Parameters.USER_ID, userBook.UserId),
                new SqlParameter(Constants.Parameters.BOOK_NAME, userBook.BookName),
                new SqlParameter(Constants.Parameters.IS_FAVORITE, userBook.IsFavorite),
                new SqlParameter(Constants.Parameters.IS_READ, userBook.IsRead),
                new SqlParameter(Constants.Parameters.BOOK_REVIEW, (object)userBook.BookReview ?? DBNull.Value)
            };

            await base.ExecuteStoredProcedure(Constants.Procedures.LM_INSERT_UPDATE_USER_BOOK_MAPPING, parameters);
        }

        private static class Constants
        {
            public static class Parameters
            {
                public const string BOOK_NAME = "@BookName";
                public const string BOOK_AUTHOR = "@BookAuthor";
                public const string IS_ACTIVE = "@IsActive";
                public const string PAGE_COUNT = "@PageCount";
                public const string BOOK_CATEGORY = "@Category";
                public const string USER_ID = "@UserId";
                public const string IS_FAVORITE = "@IsFavorite";
                public const string IS_READ = "@IsRead";
                public const string BOOK_REVIEW = "@BookReview";
            }
            public static class Procedures
            {
                public const string LM_DELETE_BOOK = "LM_DeleteBook";
                public const string LM_GET_ALL_USER_REVIEWS = "LM_GetAllUserReviews";
                public const string LM_GET_LIST_OF_BOOKS = "LM_GetListOfBooks";
                public const string LM_INSERT_UPDATE_BOOK_MASTER = "LM_InsertUpdate_BookMaster";
                public const string LM_INSERT_UPDATE_USER_BOOK_MAPPING = "LM_InsertUpdate_UserBookMapping";
                public const string LM_UPDATE_REVIEW_BOOK = "LM_Update_Review_Book";
            }
        }
    }
}
