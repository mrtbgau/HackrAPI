using Bogus;
using System.Text.Json;

namespace API.Services.Identity
{
    public class IdentityService(HttpClient httpClient, IConfiguration configuration) : IIdentityService
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly string? _apiKey = configuration["SerpApi:Key"];
        public async Task<object> GenerateIdentityAsync()
        {
            var faker = new Faker();
            var fakeIdentity = new
            {
                FullName = faker.Name.FullName(),
                Gender = faker.PickRandom(new[] { "Male", "Female", "Non-binary" }),
                Address = faker.Address.FullAddress(),
                Email = faker.Internet.Email(),
                Phone = faker.Phone.PhoneNumber(),
                BirthDate = faker.Date.Past(30, DateTime.Now.AddYears(-18)).ToString("yyyy-MM-dd"),
                PhotoUrl = "https://thispersondoesnotexist.com"
            };

            return await Task.FromResult(fakeIdentity);
        }

        public async Task<object> SearchPersonAsync(string firstName, string lastName)
        {
            var query = $"{firstName} {lastName}";
            var url = $"https://serpapi.com/search.json?q={query}&hl=fr&api_key={_apiKey}";

            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Failed to query SerpApi: {response.ReasonPhrase}");
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<JsonElement>(jsonResponse);

            var links = new List<string>();
            if (result.TryGetProperty("organic_results", out var organicResults))
            {
                foreach (var resultItem in organicResults.EnumerateArray())
                {
                    if (resultItem.TryGetProperty("link", out var link))
                    {
                        links.Add(link.GetString());
                    }
                }
            }

            return new
            {
                Query = query,
                Links = links
            };
        }
    }
}
