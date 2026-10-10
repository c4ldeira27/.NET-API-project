using Dima.Core.Handlers;
using Dima.Core.Requests;
using Dima.Core.Requests.Account;
using Dima.Core.Responses;
using System.Net.Http.Json;
using System.Text;

namespace Dima.Pwa.Handlers
{
    public class AccountHandler(IHttpClientFactory httpClientFactory) : IAccountHandler
    {
        private readonly HttpClient _client = httpClientFactory.CreateClient(Configuration.HttpClientName);
        public async Task<Response<string>> LoginAsync(LoginRequest request)
        {
            var result = await _client.PostAsJsonAsync("v1/identity/login?useCookies=true", request);
            return result.IsSuccessStatusCode 
                ? new Response<string>("Login successful", 200, "Login successful") 
                : new Response<string>(null, 400, "Invalid username or password");
        }

        public async Task LogoutAsync()
        {
            var emptyContent = new StringContent("{}", Encoding.UTF8, "application/json");
           await _client.PostAsJsonAsync("v1/identity/register", emptyContent);
        }

        public async Task<Response<string>> RegisterAsync(RegisterRequest request)
        {
            var result = await _client.PostAsJsonAsync("v1/identity/register", request);
            return result.IsSuccessStatusCode
                ? new Response<string>("Registration successful", 201, "Registration successful")
                : new Response<string>(null, 400, "Invalid username or password");
        }
    }
}
