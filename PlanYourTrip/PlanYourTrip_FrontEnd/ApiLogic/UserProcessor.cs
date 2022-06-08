using Microsoft.AspNetCore.Mvc;
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

            _httpClient.BaseAddress = new Uri("https://planyourtrip-backendapp.azurewebsites.net/api/");
            //_httpClient.BaseAddress = new Uri("https://localhost:7224/api/");
        }

        //Get/All
        //Get/ById/{userId}
        //Get/ByEmail/{email}
        //Get/PublicData/One/{userId}
        //Get/PublicData/Many
        //Get/Nicks
        //
        //Add
        //Login
        //Update/{userId}
        //PatchPassword/{userId}
        //Remove/{userId}

        public async Task<List<Users>> GetUsers()
        {
            List<Users> user = null;

            HttpResponseMessage httpResponseMessage =
                await _httpClient.GetAsync($"User");

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                user = await _httpClient.GetFromJsonAsync<List<Users>>($"User/Get/All");
            }

            return user;
        }
        public async Task<Users> GetUserById(int userId)
        {
            Users user = null;

            HttpResponseMessage httpResponseMessage =
                await _httpClient.GetAsync($"User/Get/ById/{userId}");

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                user = await _httpClient.GetFromJsonAsync<Users>($"User/Get/ById/{userId}");
            }

            return user;
        }
        public async Task<Users> GetUserByEmail(string email)
        {
            Users user = null;

            HttpResponseMessage httpResponseMessage =
                await _httpClient.GetAsync($"User/Get/ByEmail/{email}");

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                user = await _httpClient.GetFromJsonAsync<Users>($"User/Get/ByEmail/{email}");
            }

            return user;
        }
        public async Task<UserPublicDataDTO> GetUserPublicData(int userId)
        {
            UserPublicDataDTO user = null;

            HttpResponseMessage httpResponseMessage =
                await _httpClient.GetAsync($"User/Get/ById/{userId}");

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                user = await _httpClient.GetFromJsonAsync<UserPublicDataDTO>($"User/Get/PublicData/One/{userId}");
            }

            return user;
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

                return await _httpClient.PostAsync($"User/Get/PublicData/Many", usersIdsJson);
            }
            else
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
            }
        }
        public async Task<HttpResponseMessage> GetUsersNicks(List<int> usersIds)
        {
            HttpResponseMessage httpResponseMessage =
                await _httpClient.GetAsync($"User/Get/All");

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var usersIdsJson = new StringContent(
                    JsonConvert.SerializeObject(usersIds),
                    Encoding.UTF8,
                    Application.Json);

                return await _httpClient.PostAsync($"User/Get/Nicks", usersIdsJson);
            }
            else
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
            }
        }
            


        public async Task<HttpResponseMessage> AddUser(Users user)
        {
            var userJson = new StringContent(
                JsonConvert.SerializeObject(user),
                Encoding.UTF8,
                Application.Json);

            return await _httpClient.PostAsync("User/Add", userJson);
        }
        public async Task<HttpResponseMessage> LoginUser(UsersDTO user)
        {
            var userJson = new StringContent(
                JsonConvert.SerializeObject(user),
                Encoding.UTF8,
                Application.Json);

            return await _httpClient.PostAsync("User/Login", userJson);
        }
        public async Task<HttpResponseMessage> UpdateUser(Users user)
        {
            var userJson = new StringContent(
                JsonConvert.SerializeObject(user),
                Encoding.UTF8,
                Application.Json);

            return await _httpClient.PutAsync($"User/Update/{user.Id}", userJson);
        }
        public async Task<HttpResponseMessage> UpdatePassword(PasswordDTO passwords)
        {
            var userJson = new StringContent(
                JsonConvert.SerializeObject(passwords),
                Encoding.UTF8,
                Application.Json);

            return await _httpClient.PatchAsync($"User/PatchPassword/{passwords.UserId}", userJson);
        }
        public async Task<HttpResponseMessage> DeleteUser(int userId)
        {
            return await _httpClient.DeleteAsync($"User/Remove/{userId}");
        }
    }
}
