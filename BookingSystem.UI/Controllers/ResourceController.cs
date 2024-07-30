using BookingSystem.DataModel.Master;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;
using System.Net.Http.Headers;
using System.Net.Http;

namespace BookingSystem.UI.Controllers
{
    public class ResourceController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;


        public ResourceController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        }
        public async Task<IActionResult> Index()
        {
            var model = new IndexResource();
            var client = _httpClientFactory.CreateClient();

            var response = await client.GetAsync("http://localhost:5196/api/Resource");

            if (response.IsSuccessStatusCode)
            {

                var responseString = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                try
                {
                    model = JsonSerializer.Deserialize<IndexResource>(responseString, options);
                }
                catch (JsonException ex)
                {
                    return View("Error", ex.Message);
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> UpsertPage(int? id)
        {
            var model = new CreateEditResourceVM();
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"http://localhost:5196/api/Resource/GetSingle?id={id}");

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
                        model = JsonSerializer.Deserialize<CreateEditResourceVM>(responseString, options);
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


        //[HttpPost("UpsertResource")]
        //public async Task<IActionResult> UpsertResource(CreateEditResourceVM dto)
        //{
        //    try
        //    {
        //        var client = _httpClientFactory.CreateClient();
        //        var url = $"http://localhost:5196/Api/Resource";

        //        using (var multipartContent = new MultipartFormDataContent())
        //        {
        //            multipartContent.Add(new StringContent(dto.Id.ToString()), nameof(dto.Id));
        //            multipartContent.Add(new StringContent(dto.Name), nameof(dto.Name));
        //            multipartContent.Add(new StringContent(dto.Status.ToString()), nameof(dto.Status));
        //            multipartContent.Add(new StringContent(dto.Icon ?? string.Empty), nameof(dto.Icon));

        //            if (dto.IconFile != null)
        //            {
        //                var fileContent = new StreamContent(dto.IconFile.OpenReadStream());
        //                fileContent.Headers.ContentType = new MediaTypeHeaderValue(dto.IconFile.ContentType);
        //                multipartContent.Add(fileContent, nameof(dto.IconFile), dto.IconFile.FileName);
        //            }

        //            var response = await client.PostAsync(url, multipartContent);

        //            if (response.IsSuccessStatusCode)
        //            {
        //                return RedirectToAction("Index");
        //            }

        //            // Log response content if needed
        //            var responseContent = await response.Content.ReadAsStringAsync();
        //            ModelState.AddModelError("", $"Error occurred: {responseContent}");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log exception details
        //        ModelState.AddModelError("", $"Exception occurred: {ex.Message}");
        //    }

        //    return View(dto);
        //}
        [HttpPost]
        public async Task<IActionResult> UpsertResource(CreateEditResourceVM dto)
        {
            var client = _httpClientFactory.CreateClient();
            var url = $"http://localhost:5196/Api/Resource";

            using (var multipartContent = new MultipartFormDataContent())
            {
                multipartContent.Add(new StringContent(dto.Id.ToString()), nameof(dto.Id));
                multipartContent.Add(new StringContent(dto.Name), nameof(dto.Name));
                multipartContent.Add(new StringContent(dto.Status.ToString()), nameof(dto.Status));
                multipartContent.Add(new StringContent(dto.Icon ?? string.Empty), nameof(dto.Icon));

                if (dto.IconFile != null)
                {
                    var fileContent = new StreamContent(dto.IconFile.OpenReadStream());
                    fileContent.Headers.ContentType = new MediaTypeHeaderValue(dto.IconFile.ContentType);
                    multipartContent.Add(fileContent, nameof(dto.IconFile), dto.IconFile.FileName);
                }

                // Add the list of codes
                if (dto.code != null)
                {
                    for (int i = 0; i < dto.code.Count; i++)
                    {
                        multipartContent.Add(new StringContent(dto.code[i].Id.ToString()), $"code[{i}].Id");
                        multipartContent.Add(new StringContent(dto.code[i].status.ToString()), $"code[{i}].Status");
                        multipartContent.Add(new StringContent(dto.code[i].ResourceCode), $"code[{i}].ResourceCode");
                    }
                }

                var response = await client.PostAsync(url, multipartContent);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(dto);
        }


        [HttpGet, HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.DeleteAsync($"http://localhost:5196/api/Resource?id={id}");

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
