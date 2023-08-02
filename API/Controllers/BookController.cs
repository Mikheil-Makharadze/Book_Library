using API.DTO;
using API.Response;
using AutoMapper;
using Core.Entities;
using Core.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// Crud for Books
    /// </summary>
    public class BookController : BaseApiContoller
    {
        private readonly IBookService _bookService;
        private readonly IAuthorBookService _authorBookService;
        private readonly IMapper _mapper;

        /// <summary>
        /// injecting services for Book
        /// </summary>
        /// <param name="bookService"></param>
        /// <param name="authorBookService"></param>
        /// <param name="mapper"></param>
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
            return Ok(new APIResponse(_mapper.Map<BookDTO>(await _bookService.GetByIdDetailsAsync(Id))));
        }

        /// <summary>
        /// Get All Books
        /// </summary>
        /// <returns>Books</returns>
        [HttpGet]
        public async Task<ActionResult<APIResponse>> GetAllBooks()
        {
            return Ok(new APIResponse(_mapper.Map<List<BookDTO>>(await _bookService.GetAllAsync())));
        }

        /// <summary>
        /// Get All Books With Author Info
        /// </summary>
        /// <returns>Books with author details</returns>
        [HttpGet("Details")]
        public async Task<ActionResult<APIResponse>> GetAllBooksDetails(SearchString searchString)
        {
            return Ok(new APIResponse(_mapper.Map<List<BookDTO>>(await _bookService.GetAllDetailsAsync(searchString.Search))));
        }

        /// <summary>
        /// Create New Book
        /// </summary>
        /// <param name="bookCreate"></param>
        /// <returns>newly created book id</returns>
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<APIResponse>> CreateBook(BookCreateDTO bookCreate)
        {
            var book = _mapper.Map<Book>(bookCreate);
            await _bookService.AddAsync(book);

            if(bookCreate.AuthorsId != null)
            {
                foreach (var authorId in bookCreate.AuthorsId)
                {
                    await _authorBookService.CreateAuthorBook(authorId, book.Id);
                }
            }                

            return Ok(new APIResponse( book.Id));
        }

        /// <summary>
        /// Delete The Book Via Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>newly deleted book id</returns>
        /// <response code="404">If the book is was not found</response>

        [HttpDelete("{Id:int}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> DeleteBook(int Id)
        {
            return Ok(new APIResponse(await _bookService.GetByIdAsync(Id)));
        }

        /// <summary>
        /// Update The Book Via Id
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="bookUpdate"></param>
        /// <returns>newly updated book id</returns>
        /// <response code="404">If the book is was not found</response>
        [HttpPut("{Id:int}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> UpdateProduct(int Id, BookCreateDTO bookUpdate)
        {
            var book = await _bookService.GetByIdAsync(Id);

            _mapper.Map(bookUpdate, book);                
            await _bookService.UpdateAsync(book);

            await _authorBookService.RemoveByBookId(book.Id);
            if (bookUpdate.AuthorsId != null)
            {
                foreach (var authorId in bookUpdate.AuthorsId)
                {
                    await _authorBookService.CreateAuthorBook(authorId, book.Id);
                }
            }

            return Ok(new APIResponse(book.Id));
        }
    }
}
