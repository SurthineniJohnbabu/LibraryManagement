using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Transactions;
using LibraryManagement.API.Core.Models;
using LibraryManagement.API.DataAccess.Repositories.BooksRepository;
using System.Linq;

namespace LibraryManagement.API.BusinessLogic.Books
{
    public class BooksManager : IBooksManager
    {
        private readonly IBooksRepository bookRepository;

        public BooksManager(IBooksRepository bookRepository)
        {
            this.bookRepository = bookRepository;
        }

        public async Task<List<BookModel>> GetBooksDetails(string bookName = null)
        {
            try
            {
                DataSet bookDetails = await this.bookRepository.GetBooksDetails(bookName);
                var mapBookDetails = MapBookDetails(bookDetails.Tables[0]);

                return mapBookDetails;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task InsertBookEntry(BookModel bookModel)
        {
            await this.bookRepository.InsertBookEntry(bookModel);
        }

        public async Task InsertUserBookMappingEntry(UserBookModel userBook)
        {
            await this.bookRepository.InsertUserBookMappingEntry(userBook);
        }

        public async Task<BookModel> UpdateBookDetails(BookModel bookModel)
        {
            try
            { 
                var bookDetails = await this.bookRepository.UpdateBookDetails(bookModel);
                var updatedBookDetails = MapUpdatedBookDetails(bookDetails.Tables[0]);

                return updatedBookDetails;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task DeleteBookEntry(string bookName)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, TimeSpan.FromMinutes(5.0), TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    await bookRepository.DeleteBookEntry(bookName);
                    scope.Complete();
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
            }
        }

        public async Task<List<BookReviewModel>> GetBookReviews(string bookName)
        {
            try
            { 
                DataSet bookReviews = await this.bookRepository.GetBookReviews(bookName);
                var mapBookReviewsList = MapBookReviews(bookReviews.Tables[0]);

                return mapBookReviewsList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
}

        public async Task<BookReviewModel> UpdateBookReview(UserBookModel userBook)
        {
            try
            { 
                var bookReview = await this.bookRepository.UpdateBookReview(userBook);
                var userBookReview = MapBookReview(bookReview.Tables[0]);

                return userBookReview;
            }
            catch (Exception ex)
            {
                throw ex;
            }
}

        private List<BookReviewModel> MapBookReviews(DataTable bookReviews)
        {
            return bookReviews.AsEnumerable().Select(review => new BookReviewModel
            {
                UserName = review.Field<string>(Constants.Parameters.USER_NAME),
                BookName = review.Field<string>(Constants.Parameters.BOOK_NAME),
                BookReviews = review.Field<string>(Constants.Parameters.BOOK_REVIEW)
            }).ToList();
        }

        private BookModel MapUpdatedBookDetails(DataTable bookDetails)
        {
            return bookDetails.AsEnumerable().Select(book => new BookModel
            {
                BookName = book.Field<string>(Constants.Parameters.BOOK_NAME),
                BookAuthor = book.Field<string>(Constants.Parameters.BOOK_AUTHOR),
                IsActive = book.Field<bool>(Constants.Parameters.IS_ACTIVE),
                BookPageCount = book.Field<int>(Constants.Parameters.PAGE_COUNT),
                BookCategory = book.Field<string>(Constants.Parameters.BOOK_CATEGORY)
            }).FirstOrDefault();
        }

        private List<BookModel> MapBookDetails(DataTable bookDetails)
        {
            return bookDetails.AsEnumerable().Select(book => new BookModel
            {
                BookName = book.Field<string>(Constants.Parameters.BOOK_NAME),
                BookAuthor = book.Field<string>(Constants.Parameters.BOOK_AUTHOR),
                IsActive = book.Field<bool>(Constants.Parameters.IS_ACTIVE),
                BookPageCount = book.Field<int>(Constants.Parameters.PAGE_COUNT),
                BookCategory = book.Field<string>(Constants.Parameters.BOOK_CATEGORY)
            }).ToList();
        }

        private BookReviewModel MapBookReview(DataTable bookReview)
        {
            return bookReview.AsEnumerable().Select(review => new BookReviewModel
            {
                BookName = review.Field<string>(Constants.Parameters.BOOK_NAME),
                UserName = review.Field<string>(Constants.Parameters.USER_NAME),
                BookReviews = review.Field<string>(Constants.Parameters.BOOK_REVIEW)
            }).FirstOrDefault();
        }

        private static class Constants
        {
            public static class Parameters
            {
                public const string USER_NAME = "UserName";
                public const string BOOK_NAME = "BookName";
                public const string BOOK_AUTHOR = "BookAuther";
                public const string IS_ACTIVE = "IsActive";
                public const string PAGE_COUNT = "PageCount";
                public const string BOOK_CATEGORY = "Category";
                public const string BOOK_REVIEW = "BookReview";
            }
        }
    }
}
