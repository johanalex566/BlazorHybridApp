using CoffeeBrowser.Library.Data;
using System.Net.Http;
using System.Net.Http.Json;

namespace CoffeeBrowser.wpf.Data;

public class CoffeeService : ICoffeeService
{

    private readonly HttpClient _httpClient = new();

    public async Task<IEnumerable<Coffee>?> LoadCoffesAsync()
    {
        var coffees = await _httpClient.GetFromJsonAsync<IEnumerable<Coffee?>>("https://fake-coffee-api.vercel.app/api");

        return coffees;
    }

}
