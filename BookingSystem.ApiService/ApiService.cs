using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class ApiService
{
    private readonly HttpClient _client;

    public ApiService(HttpClient client)
    {
        _client = client;
    }

    public async Task<List<BookingCodeModel>> GetBookingCodesAsync()
    {
        _client.DefaultRequestHeaders.Accept.Clear();
        _client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

        HttpResponseMessage response = await _client.GetAsync("api/index"); // Adjust to your API endpoint
        response.EnsureSuccessStatusCode();

        string responseData = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<List<BookingCodeModel>>(responseData);
    }
}
