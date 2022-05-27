using Newtonsoft.Json;
using PlanYourTrip_ClassLibrary.Classes;
using PlanYourTrip_ClassLibrary.ClassesDTO;
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
        public async Task<List<Users>> GetUsers()
        {
            List<Users> user = null;

            HttpResponseMessage httpResponseMessage =
                await _httpClient.GetAsync($"User");

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                user = await _httpClient.GetFromJsonAsync<List<Users>>($"User");
            }

            return user;
        }

        // Get user by ID
        public async Task<Users> GetUserById(int id)
        {
            Users user = null;

            HttpResponseMessage httpResponseMessage =
                await _httpClient.GetAsync($"User/id/{id}");

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                user = await _httpClient.GetFromJsonAsync<Users>($"User/id/{id}");
            }

            return user;
        }

        // Get user by Email
        public async Task<Users> GetUserByEmail(string email)
        {
            Users user = null;

            HttpResponseMessage httpResponseMessage =
                await _httpClient.GetAsync($"User/email/{email}");

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                user = await _httpClient.GetFromJsonAsync<Users>($"User/email/{email}");
            }

            return user;
        }

        public async Task<HttpResponseMessage> LoginUser(UsersDTO user)
        {
            var userJson = new StringContent(
                JsonConvert.SerializeObject(user),
                Encoding.UTF8,
                Application.Json);

            using var httpResponseMessage =
                await _httpClient.PostAsync("User/login", userJson);

            return httpResponseMessage;
        }
        

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
