using Masny.Client.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

namespace Masny.Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClient;

        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Secret()
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Baerer {token}");
            var serverResponse = await _httpClient.GetAsync($"https://localhost:44318/secret/index?access_token={token}");
            var apiResponse = await _httpClient.GetAsync("https://localhost:44317/api/secret");

            var secretViewModel = new SecretViewModel
            {
                ApiMessage = await serverResponse.Content.ReadAsStringAsync(),
                ServerMessage = await apiResponse.Content.ReadAsStringAsync()
            };

            return View(secretViewModel);
        }
    }
}
