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
    /// Crud For Author
    /// </summary>
    public class AuthorController : BaseApiContoller
    {
        private readonly IAuthorService _authorService;
        private readonly IAuthorBookService _authorBookService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Injecting Services
        /// </summary>
        /// <param name="authorService"></param>
        /// <param name="authorBookService"></param>
        /// <param name="mapper"></param>
        public AuthorController(IAuthorService authorService, IAuthorBookService authorBookService, IMapper mapper)
        {
            _authorService = authorService;
            _authorBookService = authorBookService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get The Author Via Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>author with books details</returns>
        /// <response code="404">If the author was not found</response>
        [HttpGet(("{Id:int}"))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetAuthor(int Id)
        {
            return Ok(new APIResponse(_mapper.Map<AuthorDTO>(await _authorService.GetByIdDetailsAsync(Id))));
        }

        /// <summary>
        /// Get All Authors
        /// </summary>
        /// <returns>authors</returns>
        [HttpGet]
        public async Task<ActionResult<APIResponse>> GetAllAuthors()
        {
            return Ok(new APIResponse(_mapper.Map<List<AuthorDTO>>(await _authorService.GetAllAsync())));
        }

        /// <summary>
        /// Get All Authors With Books Info
        /// </summary>
        /// <returns>authors with books details</returns>
        [HttpGet("Details")]
        public async Task<ActionResult<APIResponse>> GetAllAuthorsDetails(SearchString searchString)
        {
            return Ok(new APIResponse(_mapper.Map<List<AuthorDTO>>(await _authorService.GetAllDetailsAsync(searchString.Search))));
        }

        /// <summary>
        /// Create New Author
        /// </summary>
        /// <param name="authorCreate"></param>
        /// <returns>newly created author id</returns>
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<APIResponse>> CreateAuthors(AuthorCreateDTO authorCreate)
        {
            var author = _mapper.Map<Author>(authorCreate);
            await _authorService.AddAsync(author);

            if(authorCreate.BooksId != null)
            {
                foreach (var bookId in authorCreate.BooksId)
                {
                    await _authorBookService.CreateAuthorBook(author.Id, bookId);
                }
            }                

            return Ok(new APIResponse(author.Id));
        }

        /// <summary>
        /// Delete The Author Via Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>newly deleted author id</returns>
        /// <response code="404">If the author is was not found</response>
        [HttpDelete("{Id:int}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> DeleteAuthor(int Id)
        {
            return Ok(new APIResponse(await _authorService.DeleteAsync(Id)));
        }

        /// <summary>
        /// Update The Author Via Id
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="authorUpdate"></param>
        /// <returns>newly updated author id</returns>
        /// <response code="404">If the author is was not found</response>
        [HttpPut("{Id:int}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> UpdateProduct(int Id, AuthorCreateDTO authorUpdate)
        {
            var author = await _authorService.GetByIdAsync(Id);

            _mapper.Map(authorUpdate, author);
            await _authorService.UpdateAsync(author);

            await _authorBookService.RemoveByAuthorId(author.Id);

            if (authorUpdate.BooksId != null)
            {
                foreach (var bookId in authorUpdate.BooksId)
                {
                    await _authorBookService.CreateAuthorBook(author.Id, bookId);
                }
            }

            return Ok(new APIResponse(author.Id));
        }
    }
}
