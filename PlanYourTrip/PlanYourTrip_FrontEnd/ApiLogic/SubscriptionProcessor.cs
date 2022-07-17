

using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PlanYourTrip_ClassLibrary.Classes;
using PlanYourTrip_ClassLibrary.ClassesDTO;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace PlanYourTrip_FrontEnd.ApiLogic
{
    public class SubscriptionProcessor
    {
        private readonly HttpClient _httpClient;

        public SubscriptionProcessor(HttpClient httpClient)
        {
            _httpClient = httpClient;

            _httpClient.BaseAddress = new Uri("https://planyourtrip-backendapp.azurewebsites.net/api/");
            //_httpClient.BaseAddress = new Uri("https://localhost:7224/api/");
        }

        //Get/All
        //Get/UserSubsIds/{userId}
        //Get/IsSubscribed
        //
        //Add
        //Remove/{subId}


        public async Task<List<int>> GetUserSubscriptions(int id) =>
            await _httpClient.GetFromJsonAsync<List<int>>($"Subscription/Get/UserSubsIds/{id}");

        public async Task<HttpResponseMessage> GetSubscription(SubscriptionDTO subscription)
        {
            HttpResponseMessage httpResponseMessage =
                await _httpClient.GetAsync($"Subscription/Get/All");

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var subJson = new StringContent(
                    JsonConvert.SerializeObject(subscription),
                    Encoding.UTF8,
                    Application.Json);

                return await _httpClient.PostAsync("Subscription/Get/IsSubscribed", subJson);
            }
            else
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
            }
        }

        public async Task<HttpResponseMessage> AddSub(Subscription subscription)
        {
            HttpResponseMessage httpResponseMessage =
                await _httpClient.GetAsync($"Subscription/Get/All");

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var subJson = new StringContent(
                    JsonConvert.SerializeObject(subscription),
                    Encoding.UTF8,
                    Application.Json);

                return await _httpClient.PostAsync("Subscription/Add", subJson);
            }
            else
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
            }
        }

        public async Task<HttpResponseMessage> RemoveSub(int subId)
        {
            return await _httpClient.DeleteAsync($"Subscription/Remove/{subId}");
        }
    }
}
