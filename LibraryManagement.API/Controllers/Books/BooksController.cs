using System;
using System.Threading.Tasks;
using LibraryManagement.API.BusinessLogic.Books;
using LibraryManagement.API.Core.Constants;
using LibraryManagement.API.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.API.Controllers.Books
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase
    {
        public readonly IBooksManager bookManager;
        public BooksController(IBooksManager bookManager)
        {
            this.bookManager = bookManager;
        }

        /// <summary>
        /// This action method is used for to retrieve the list of books
        /// </summary>
        /// <returns>Returns Books List</returns>
        [HttpGet("BooksList")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<ActionResult> Get(string bookName = null)
        {
            try
            {
                var getBooksList = await this.bookManager.GetBooksDetails(bookName);
                return Ok(getBooksList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// This action method is used for to insert book entry
        /// </summary>
        /// <returns>Create entry in Books list</returns>
        [Authorize(Roles = Role.Admin)]
        [HttpPost("CreateBook")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<ActionResult> Post(BookModel bookModel)
        {
            // only allow admins to create Book entries
            if (!User.IsInRole(Role.Admin))
                return Forbid();

            try
            {
                await this.bookManager.InsertBookEntry(bookModel);
                return Accepted();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// This action method is used for to update book entry
        /// </summary>
        /// <returns>Update entry in Books list</returns>
        [Authorize(Roles = Role.Admin)]
        [HttpPut("UpdateBook")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<ActionResult> Put(BookModel bookModel)
        {
            // only allow admins to Update Book entries
            if (!User.IsInRole(Role.Admin))
                return Forbid();

            try
            {
                var bookUpdatedDetails = await this.bookManager.UpdateBookDetails(bookModel);
                return Ok(bookUpdatedDetails);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// This action method is used for to delete book entry
        /// </summary>
        /// <returns>delete entry in Books list</returns>
        [Authorize(Roles = Role.Admin)]
        [HttpDelete("DeleteBook")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<ActionResult> Delete(string bookName)
        {
            // only allow admins to delet Book entry
            if (!User.IsInRole(Role.Admin))
                return Forbid();

            try
            {
                await this.bookManager.DeleteBookEntry(bookName);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// This action method is used for to insert user and book mapping
        /// </summary>
        /// <returns>Create entry in User Book mapping list</returns>
        [HttpPost("ReadFavoriteBook")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<ActionResult> Post(UserBookModel userBook)
        {
            try
            {
                userBook.UserId = int.Parse(User.Identity.Name);
                await this.bookManager.InsertUserBookMappingEntry(userBook);
                return Accepted();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// This action method is used for to get all reviews
        /// </summary>
        /// <returns>Returns Books reviews</returns>
        [Authorize(Roles = Role.Admin)]
        [HttpGet("Reviews/{bookName}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<ActionResult> GetBookReviews(string bookName)
        {
            // only allow admins to see all book reviews
            if (!User.IsInRole(Role.Admin))
                return Forbid();

            try
            {
                var getBooksReviews = await this.bookManager.GetBookReviews(bookName);
                return Ok(getBooksReviews);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// This action method is used for to create book review
        /// </summary>
        /// <returns>Returns Book reviews</returns>
        [HttpPut("WriteBookReview")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<ActionResult> Put(UserBookModel userBook)
        {
            try
            { 
                userBook.UserId = int.Parse(User.Identity.Name);
                var getBooksReviews = await this.bookManager.UpdateBookReview(userBook);

                return Ok(getBooksReviews);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}