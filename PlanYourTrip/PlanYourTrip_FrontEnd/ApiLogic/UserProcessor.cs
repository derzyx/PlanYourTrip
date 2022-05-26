using Newtonsoft.Json;
using PlanYourTrip_ClassLibrary.Classes;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace PlanYourTrip_FrontEnd.ApiLogic
{
    public class UserProcessor
    {
        private readonly HttpClient _httpClient;

        public UserProcessor(HttpClient httpClient)
        {
            _httpClient = httpClient;

            //_httpClient.BaseAddress = new Uri("https://planyourtrip-backendapp.azurewebsites.net/api/");
            _httpClient.BaseAddress = new Uri("https://localhost:7224/api/");
        }

        // Get all users
        public async Task<List<Users>> GetUsers() =>
            await _httpClient.GetFromJsonAsync<List<Users>>("User");

        // Get user by ID
        public async Task<Users> GetUser(int id) =>
            await _httpClient.GetFromJsonAsync<Users>($"User/{id}");

        // Add new user
        public async Task AddUser(Users user)
        {
            var userJson = new StringContent(
                JsonConvert.SerializeObject(user),
                Encoding.UTF8,
                Application.Json);

            using var httpResponseMessage =
                await _httpClient.PostAsync("User", userJson);

            httpResponseMessage.EnsureSuccessStatusCode();
        }

        // Update user

        public async Task UpdateUser(Users user)
        {
            var userJson = new StringContent(
                JsonConvert.SerializeObject(user),
                Encoding.UTF8,
                Application.Json);

            using var httpResponseMessage =
                await _httpClient.PostAsync($"User/{user.Id}", userJson);

            httpResponseMessage.EnsureSuccessStatusCode();
        }

        // Remove user
        public async Task DeleteUser(int id)
        {
            using var httpResponseMessage =
                await _httpClient.DeleteAsync($"User/{id}");

            httpResponseMessage.EnsureSuccessStatusCode();
        }
    }
}
