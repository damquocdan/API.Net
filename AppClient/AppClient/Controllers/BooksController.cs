using AppClient.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace AppClient.Controllers
{
    public class BooksController : Controller
    {
        private readonly HttpClient _httpClient;

        // Injecting IHttpClientFactory to create HttpClient instance
        public BooksController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            var books = await _httpClient.GetFromJsonAsync<List<Book>>("Books");
            return View(books);
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(long id)
        {
            var book = await _httpClient.GetFromJsonAsync<Book>($"books/{id}");
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Book book)
        {
            if (ModelState.IsValid)
            {
                // Call API to create the book
                var response = await _httpClient.PostAsJsonAsync("books", book);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error occurred while creating the book.");
                }
            }
            return View(book);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(long id)
        {
            var book = await _httpClient.GetFromJsonAsync<Book>($"books/{id}");
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // POST: Books/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, Book book)
        {
            if (id != book.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var response = await _httpClient.PutAsJsonAsync($"books/{id}", book);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error occurred while updating the book.");
                }
            }
            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"books/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index"); // Redirect to the list of books
            }

            // Handle failure (optional)
            TempData["ErrorMessage"] = "Failed to delete the book.";
            return RedirectToAction("Index");
        }




        // POST: Books/Search
        public async Task<IActionResult> Search(string keyword)
        {
            List<Book> books;

            if (string.IsNullOrEmpty(keyword))
            {
                // Nếu từ khóa tìm kiếm rỗng, lấy tất cả sách
                books = await _httpClient.GetFromJsonAsync<List<Book>>("books");
            }
            else
            {
                // Nếu có từ khóa tìm kiếm, gọi API tìm kiếm
                books = await _httpClient.GetFromJsonAsync<List<Book>>($"books/search?keyword={keyword}");
            }

            return View("Index", books); // Trả về view Index với danh sách sách tìm được
        }
        public async Task<IActionResult> GetBookDetails(string id)
        {
            var response = await _httpClient.GetAsync($"books/{id}");
            if (response.IsSuccessStatusCode)
            {
                var book = await response.Content.ReadAsAsync<Book>();
                return PartialView("_BookDetails", book); // Render partial view
            }
            return NotFound();
        }

    }
}
