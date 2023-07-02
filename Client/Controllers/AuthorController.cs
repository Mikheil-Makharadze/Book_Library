using AutoMapper;
using Client.Models.DTO;
using Clinet.Models.API;
using Clinet.Models.DTO;
using Clinet.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Controllers
{
    public class AuthorController : Controller
    {
        public readonly IBookService _bookService;
        public readonly IAuthorService _authorService;
        private readonly IMapper _mapper;

        public AuthorController(IBookService bookService, IAuthorService authorService, IMapper mapper)
        {
            _bookService = bookService;
            _authorService = authorService;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index(string? searchString)
        {
            var authors = await _authorService.GetAllDetailsAsync(searchString, GetToken());

            return View(authors);
        }

        public async Task<IActionResult> Details(int id)
        {
            var author = await _authorService.GetByIdAsync(id, GetToken());

            return View(author);
        }

        [Authorize]
        public async Task<IActionResult> Create()
        {
            await UpdataBookViewBag();

            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AuthorCreateDTO author)
        {
            if (ModelState.IsValid)
            {
                await _authorService.AddAsync(author, GetToken());

                return RedirectToAction("Index");
            }

            await UpdataBookViewBag();

            return View(author);
        }


        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            await UpdataBookViewBag();

            var author = _mapper.Map<AuthorUpdateDTO>(await _authorService.GetByIdAsync(id, GetToken()));

            return View(author);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AuthorUpdateDTO author)
        {
            if (ModelState.IsValid)
            {
                await _authorService.UpdateAsync(author.Id, author, GetToken());

                return RedirectToAction("Index");
            }

            await UpdataBookViewBag();

            return View(author);
        }


        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var author = await _authorService.GetByIdAsync(id, GetToken());

            return View(author);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _authorService.DeleteAsync(id, GetToken());

            return RedirectToAction("Index");
        }

        #region PrivateMethods
        private string GetToken()
        {
            return HttpContext.Session.GetString(SD.SessionToken);
        }

        private async Task UpdataBookViewBag()
        {
            var Books = await _bookService.GetAllAsync(GetToken());

            ViewBag.Books = new SelectList(Books, "Id", "Title");
        }
        #endregion
    }
}
