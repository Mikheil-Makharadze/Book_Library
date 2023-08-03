using AutoMapper;
using Client.Exceptions;
using Client.Models.DTO;
using Clinet.Models.API;
using Clinet.Models.DTO;
using Clinet.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Controllers
{
    public class BookController : Controller
    {
        public readonly IBookService _bookService;
        public readonly IAuthorService _authorService;
        private readonly IMapper _mapper;

        public BookController(IBookService bookService, IAuthorService authorService, IMapper mapper)
        {
            _bookService = bookService;
            _authorService = authorService;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index(string? searchString)
        {
            var books = await _bookService.GetAllAsync(searchString, GetToken());

            return View(books);
        }

        public async Task<IActionResult> Details(int id)
        {
            var book = await _bookService.GetByIdAsync(id, GetToken());

            return View(book);
        }


        [Authorize]
        public async Task<IActionResult> Create()
        {
            await UpdataAuthorViewBag();

            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookCreateDTO book)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _bookService.AddAsync(book, GetToken());

                    return RedirectToAction("Index");
                }
                catch (APIException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }                
            }

            await UpdataAuthorViewBag();

            return View(book);
        }

        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            await UpdataAuthorViewBag();

            var book = _mapper.Map<BookUpdateDTO>(await _bookService.GetByIdAsync(id, GetToken()));

            return View(book);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BookUpdateDTO book)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _bookService.UpdateAsync(book.Id, book, GetToken());

                    return RedirectToAction("Index");
                }
                catch (APIException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }                
            }

            await UpdataAuthorViewBag();

            return View(book);
        }

        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var book = await _bookService.GetByIdAsync(id, GetToken());

            return View(book);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _bookService.DeleteAsync(id, GetToken());

            return RedirectToAction("Index");
        }

        #region PrivateMethods
        private string GetToken()
        {
            return HttpContext.Session.GetString(SD.SessionToken)!;
        }

        private async Task UpdataAuthorViewBag()
        {
            var Authors = await _authorService.GetAllSelectorItemsAsync(GetToken());

            ViewBag.Authors = new SelectList(Authors, "Id", "Name");
        }
        #endregion
    }
}
