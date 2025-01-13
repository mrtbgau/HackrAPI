using System.Net.Http;
using System.Text.Json;

namespace API.Services.Mail
{
    public class MailService(HttpClient httpClient, IConfiguration configuration)
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly string _apiKey = configuration["Hunter:Key"];
        public async Task<bool> VerifyEmailExistenceAsync(string email)
        {
            var url = $"https://api.hunter.io/v2/email-verifier?email={email}&api_key={_apiKey}";

            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Échec de la validation de l'email");
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            using var jsonDoc = JsonDocument.Parse(responseContent);

            var result = jsonDoc.RootElement
                                .GetProperty("data")
                                .GetProperty("result")
                                .GetString();

            return result == "deliverable";
        } 
    }

}
