using API.DTO;
using API.Response;
using AutoMapper;
using Core.Entities;
using Core.Interface;
using Infrastructure.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    public class AuthorController : BaseApiContoller
    {
        private readonly IAuthorService _authorService;
        private readonly IAuthorBookService _authorBookService;
        private readonly IMapper _mapper;
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
            try
            {
                var author = await _authorService.GetByIdDetailsAsync(Id);

                if (author == null)
                {
                    return NotFound(new APIResponse
                    {
                        StatusCode = HttpStatusCode.NotFound,
                        IsSuccess = false,
                        ErrorMessages = new List<string> { "Author was not found" }
                    });
                }

                return Ok(new APIResponse
                {
                    StatusCode = HttpStatusCode.OK,
                    IsSuccess = true,
                    Result = _mapper.Map<AuthorDTO>(author)
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
        /// Get All Authors
        /// </summary>
        /// <returns>authors</returns>
        [HttpGet]
        public async Task<ActionResult<APIResponse>> GetAllAuthors()
        {
            try
            {
                var authors = await _authorService.GetAllAsync();

                return Ok(new APIResponse
                {
                    StatusCode = HttpStatusCode.OK,
                    IsSuccess = true,
                    Result = _mapper.Map<List<AuthorDTO>>(authors)
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
        /// Get All Authors With Books Info
        /// </summary>
        /// <returns>authors with books details</returns>
        [HttpGet("Details")]
        public async Task<ActionResult<APIResponse>> GetAllAuthorsDetails(SearchString searchString)
        {
            try
            {
                var authors = await _authorService.GetAllDetailsAsync(searchString.Search);

                return Ok(new APIResponse
                {
                    StatusCode = HttpStatusCode.OK,
                    IsSuccess = true,
                    Result = _mapper.Map<List<AuthorDTO>>(authors)
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
        /// Create New Author
        /// </summary>
        /// <param name="authorCreate"></param>
        /// <returns>newly created author id</returns>
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<APIResponse>> CreateAuthors(AuthorCreateDTO authorCreate)
        {
            try
            {
                Author author = _mapper.Map<Author>(authorCreate);
                await _authorService.AddAsync(author);

                if(authorCreate.booksId.Count() > 0)
                {
                    foreach (var bookId in authorCreate.booksId)
                    {
                        await _authorBookService.CreateAuthorBook(author.Id, bookId);
                    }
                }                

                return Ok(new APIResponse
                {
                    StatusCode = HttpStatusCode.OK,
                    IsSuccess = true,
                    Result = author.Id
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
            try
            {
                var author = await _authorService.GetByIdAsync(Id);
                if (author == null)
                {
                    return NotFound(new APIResponse
                    {
                        StatusCode = HttpStatusCode.NotFound,
                        IsSuccess = false,
                        ErrorMessages = new List<string> { "Author was not found" }
                    });
                }
                await _authorService.DeleteAsync(author);

                return Ok(new APIResponse
                {
                    StatusCode = HttpStatusCode.OK,
                    IsSuccess = true,
                    Result = author.Id
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
            try
            {
                var author = await _authorService.GetByIdAsync(Id);
                if (author == null)
                {
                    return NotFound(new APIResponse
                    {
                        StatusCode = HttpStatusCode.NotFound,
                        IsSuccess = false,
                        ErrorMessages = new List<string> { "Author was not found" }
                    });
                }

                _mapper.Map(authorUpdate, author);
                await _authorService.UpdateAsync(author);

                await _authorBookService.RemoveByAuthorId(author.Id);

                if (authorUpdate.booksId.Count() > 0)
                {
                    foreach (var bookId in authorUpdate.booksId)
                    {
                        await _authorBookService.CreateAuthorBook(author.Id, bookId);
                    }
                }

                return Ok(new APIResponse
                {
                    StatusCode = HttpStatusCode.NoContent,
                    IsSuccess = true,
                    Result = author.Id
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
