using BookingSystem.DataModel.Master;
using BookingSystem.DataModel.Master.Dropdown;
using BookingSystem.DataModel.Master.Room;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace BookingSystem.UI.Controllers
{
    public class RoomController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public RoomController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = new IndexRoom();
            var client = _httpClientFactory.CreateClient();

            var response = await client.GetAsync("http://localhost:5196/api/Room");

            if (response.IsSuccessStatusCode)
            {

                var responseString = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,

                };
                try
                {
                    model = JsonSerializer.Deserialize<IndexRoom>(responseString, options);
                }
                catch (JsonException e)
                {
                    return View("Error", e.Message);
                }
            }
            return View(model);

        }


        [HttpGet]
        public async Task<IActionResult> UpsertPage(int? id)
        {
            var model = new CreateEditRoomVM();
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"http://localhost:5196/api/room/GetSingle?id={id}");
            ListDropdown? locationList = null;
            var locationResponse = await client.GetAsync("http://localhost:5196/api/room/dropdown");

            if (locationResponse.IsSuccessStatusCode)
            {
                var locationResponseString = await locationResponse.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                try
                {
                    locationList = JsonSerializer.Deserialize<ListDropdown>(locationResponseString, options);
                    model.locationDropdowns = locationList?.locationDropdowns;
                }
                catch (JsonException e)
                {
                    return View("Error", e.Message);
                }
            }

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
                        model = JsonSerializer.Deserialize<CreateEditRoomVM>(responseString, options);
                        model.locationDropdowns = locationList?.locationDropdowns;
                    }
                    catch (JsonException e)
                    {
                        return View("Error", e.Message);
                    }
                }
                

                return View(model);

            }
            return View(model);
        }



        [HttpGet, HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.DeleteAsync($"http://localhost:5196/api/room?id={id}");

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
