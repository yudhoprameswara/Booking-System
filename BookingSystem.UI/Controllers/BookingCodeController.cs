using BookingSystem.DataModel.Master;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace BookingSystem.UI.Controllers
{

    public class BookingCodeController : Controller
    {
     
        private readonly IHttpClientFactory _httpClientFactory;


        public BookingCodeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = new IndexBCVM();
            var client = _httpClientFactory.CreateClient();

            var response = await client.GetAsync("http://localhost:5196/api/BookingCode");

            if (response.IsSuccessStatusCode) {

                var responseString = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,

                };
                try {
                    model = JsonSerializer.Deserialize<IndexBCVM>(responseString, options);
                }
                catch (JsonException e) {
                    return View("Error", e.Message);
                }
            }
            return View(model);

        }
        [HttpGet]
        public async Task<IActionResult> UpsertPage(int? id)
        {
            var model = new CreateEditBCVM();
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"http://localhost:5196/api/BookingCode/GetSingle?id={id}");

            if (id != null)
            {
                if (response.IsSuccessStatusCode)
                {

                    var responseString = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,

                    };
                    try
                    {
                        model = JsonSerializer.Deserialize<CreateEditBCVM>(responseString, options);
                    }
                    catch (JsonException e)
                    {
                        return View("Error", e.Message);
                    }
                }
                return View(model);

            }
            return View();
        }

        [HttpPost("Upsert")]
        public async Task<IActionResult> Upsert(CreateEditBCVM model)
        {

            //if (!ModelState.IsValid)
            //{
            //    return View(model);
            //}

            var client = _httpClientFactory.CreateClient();
            var content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");

            var response = await client.PostAsync("http://localhost:5196/api/BookingCode", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            var error = await response.Content.ReadAsStringAsync();
            ModelState.AddModelError(string.Empty, $"Server error: {error}");
            return View(model);
        }

        [HttpGet, HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.DeleteAsync($"http://localhost:5196/api/BookingCode?id={id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            var error = await response.Content.ReadAsStringAsync();
            ModelState.AddModelError(string.Empty, $"Server error: {error}");

            return RedirectToAction("Index");
        }
    }
}
