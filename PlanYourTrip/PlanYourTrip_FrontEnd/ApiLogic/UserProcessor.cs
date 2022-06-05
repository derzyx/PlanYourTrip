using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PlanYourTrip_ClassLibrary.Classes;
using PlanYourTrip_ClassLibrary.ClassesDTO;
using PlanYourTrip_ClassLibrary.Repository;
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

        public async Task<UserPublicDataDTO> GetUserPublicData(int id)
        {
            UserPublicDataDTO user = null;

            HttpResponseMessage httpResponseMessage =
                await _httpClient.GetAsync($"User/PublicData/{id}");

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                user = await _httpClient.GetFromJsonAsync<UserPublicDataDTO>($"User/PublicData/{id}");
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




        // this is a POST method
        public async Task<HttpResponseMessage> GetUsersNicks(List<int> usersIds)
        {
            HttpResponseMessage httpResponseMessage =
                await _httpClient.GetAsync($"User");

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var usersIdsJson = new StringContent(
                    JsonConvert.SerializeObject(usersIds),
                    Encoding.UTF8,
                    Application.Json);

                return await _httpClient.PostAsync($"User/Nicks", usersIdsJson);
            }
            else
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
            }
        }

        public async Task<HttpResponseMessage> GetUsersPublicDataList(List<int> usersIds)
        {

            HttpResponseMessage httpResponseMessage =
                await _httpClient.GetAsync($"User");

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var usersIdsJson = new StringContent(
                    JsonConvert.SerializeObject(usersIds),
                    Encoding.UTF8,
                    Application.Json);

                return await _httpClient.PostAsync($"User/PublicDataList", usersIdsJson);
            }
            else
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
            }
        }

        public async Task<List<Subscription>> GetUserSubscriptions(int id) =>
            await _httpClient.GetFromJsonAsync<List<Subscription>>($"User/{id}/Subscriptions");
            
        // Add new user
        public async Task<HttpResponseMessage> AddUser(Users user)
        {
            var userJson = new StringContent(
                JsonConvert.SerializeObject(user),
                Encoding.UTF8,
                Application.Json);

            return await _httpClient.PostAsync("User", userJson);
        }
        public async Task<HttpResponseMessage> LoginUser(UsersDTO user)
        {
            var userJson = new StringContent(
                JsonConvert.SerializeObject(user),
                Encoding.UTF8,
                Application.Json);

            return await _httpClient.PostAsync("User/login", userJson);
        }
        


        // Update user
        public async Task<HttpResponseMessage> UpdateUser(Users user)
        {
            var userJson = new StringContent(
                JsonConvert.SerializeObject(user),
                Encoding.UTF8,
                Application.Json);

            return await _httpClient.PutAsync($"User/{user.Id}", userJson);
        }

        // Update users password
        public async Task<HttpResponseMessage> UpdatePassword(PasswordDTO passwords)
        {
            var userJson = new StringContent(
                JsonConvert.SerializeObject(passwords),
                Encoding.UTF8,
                Application.Json);

            return await _httpClient.PatchAsync($"User/{passwords.UserId}", userJson);
        }

        // Remove user
        public async Task<HttpResponseMessage> DeleteUser(int id)
        {
            return await _httpClient.DeleteAsync($"User/{id}");
        }
    }
}
