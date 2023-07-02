using API.DTO;
using API.Response;
using AutoMapper;
using Core.Entities;
using Core.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{

    public class BookController : BaseApiContoller
    {
        private readonly IBookService _bookService;
        private readonly IAuthorBookService _authorBookService;
        private readonly IMapper _mapper;
        public BookController(IBookService bookService, IAuthorBookService authorBookService, IMapper mapper)
        {
            _bookService = bookService;
            _authorBookService = authorBookService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get The Book Via Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>Book</returns>
        /// <response code="404">If the book is was not found</response>
        [HttpGet(("{Id:int}"))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetBook(int Id)
        {
            try
            {
                var book = await _bookService.GetByIdDetailsAsync(Id);

                if (book == null)
                {
                    return NotFound(new APIResponse
                    {
                        StatusCode = HttpStatusCode.NotFound,
                        IsSuccess = false,
                        ErrorMessages = new List<string> { "Book was not found" }
                    });
                }

                return Ok(new APIResponse
                {
                    StatusCode = HttpStatusCode.OK,
                    IsSuccess = true,
                    Result = _mapper.Map<BookDTO>(book)
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new APIResponse
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    IsSuccess = false,
                    ErrorMessages = new List<string> { ex.ToString() }
                });
            }
        }

        /// <summary>
        /// Get All Books
        /// </summary>
        /// <returns>Books</returns>
        [HttpGet]
        public async Task<ActionResult<APIResponse>> GetAllBooks()
        {
            try
            {
                var books = await _bookService.GetAllAsync();

                return Ok(new APIResponse
                {
                    StatusCode = HttpStatusCode.OK,
                    IsSuccess = true,
                    Result = _mapper.Map<List<BookDTO>>(books)
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new APIResponse
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    IsSuccess = false,
                    ErrorMessages = new List<string> { ex.ToString() }
                });
            }
        }

        /// <summary>
        /// Get All Books With Author Info
        /// </summary>
        /// <returns>Books with author details</returns>
        [HttpGet("Details")]
        public async Task<ActionResult<APIResponse>> GetAllBooksDetails(SearchString searchString)
        {
            try
            {
                var books = await _bookService.GetAllDetailsAsync(searchString.Search);



                return Ok(new APIResponse
                {
                    StatusCode = HttpStatusCode.OK,
                    IsSuccess = true,
                    Result = _mapper.Map<List<BookDTO>>(books)
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new APIResponse
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    IsSuccess = false,
                    ErrorMessages = new List<string> { ex.ToString() }
                });
            }
        }

        /// <summary>
        /// Create New Book
        /// </summary>
        /// <param name="bookCreate"></param>
        /// <returns>newly created book id</returns>
        [HttpPost]
        public async Task<ActionResult<APIResponse>> CreateBook(BookCreateDTO bookCreate)
        {
            try
            {
                Book book = _mapper.Map<Book>(bookCreate);
                await _bookService.AddAsync(book);

                if(bookCreate.AuthorsId.Count() > 0)
                {
                    foreach (var authorId in bookCreate.AuthorsId)
                    {
                        await _authorBookService.CreateAuthorBook(authorId, book.Id);
                    }
                }
                

                return Ok(new APIResponse
                {
                    StatusCode = HttpStatusCode.OK,
                    IsSuccess = true,
                    Result = book.Id
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new APIResponse
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    IsSuccess = false,
                    ErrorMessages = new List<string> { ex.ToString() }
                });
            }
        }

        /// <summary>
        /// Delete The Book Via Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>newly deleted book id</returns>
        /// <response code="404">If the book is was not found</response>

        [HttpDelete("{Id:int}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> DeleteBook(int Id)
        {
            try
            {
                var book = await _bookService.GetByIdAsync(Id);
                await _bookService.DeleteAsync(book);

                if (book == null)
                {
                    return NotFound(new APIResponse
                    {
                        StatusCode = HttpStatusCode.NotFound,
                        IsSuccess = false,
                        ErrorMessages = new List<string> { "Book was not found" }
                    });
                }

                return Ok(new APIResponse
                {
                    StatusCode = HttpStatusCode.OK,
                    IsSuccess = true,
                    Result = book.Id
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new APIResponse
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    IsSuccess = false,
                    ErrorMessages = new List<string> { ex.ToString() }
                });
            }
        }

        /// <summary>
        /// Update The Book Via Id
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="bookUpdate"></param>
        /// <returns>newly updated book id</returns>
        /// <response code="404">If the book is was not found</response>
        [HttpPut("{Id:int}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> UpdateProduct(int Id, BookCreateDTO bookUpdate)
        {
            try
            {
                var book = await _bookService.GetByIdAsync(Id);
                if (book == null)
                {
                    return NotFound(new APIResponse
                    {
                        StatusCode = HttpStatusCode.NotFound,
                        IsSuccess = false,
                        ErrorMessages = new List<string> { "Book was not found" }
                    });
                }

                _mapper.Map(bookUpdate, book);                
                await _bookService.UpdateAsync(book);

                await _authorBookService.removeByBookId(book.Id);
                if (bookUpdate.AuthorsId.Count() > 0)
                {
                    foreach (var authorId in bookUpdate.AuthorsId)
                    {
                        await _authorBookService.CreateAuthorBook(authorId, book.Id);
                    }
                }

                return Ok(new APIResponse
                {
                    StatusCode = HttpStatusCode.NoContent,
                    IsSuccess = true,
                    Result = book.Id
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new APIResponse
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    IsSuccess = false,
                    ErrorMessages = new List<string> { ex.ToString() }
                });
            }
        }
    }
}
