using Newtonsoft.Json;
using PlanYourTrip_ClassLibrary.Classes;
using PlanYourTrip_ClassLibrary.ClassesDTO;
using System.Text;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;

namespace PlanYourTrip_FrontEnd.ApiLogic
{
    public class TripPlanProcessor
    {
        private readonly HttpClient _httpClient;

        public TripPlanProcessor(HttpClient httpClient)
        {
            _httpClient = httpClient;

            _httpClient.BaseAddress = new Uri("https://planyourtrip-backendapp.azurewebsites.net/api/");
            //_httpClient.BaseAddress = new Uri("https://localhost:7224/api/");
        }

        // Plans
        // Plans/{id}
        // Plans/{userId}
        // Plans/{userId}/Public
        // Plans/Filter
        // Plans/Latest/{quantity}
        // Plans/Latest/{userId}/{quantity}
        // Plans/SubsLatest
        //
        // Put {id}
        // Post
        // Delete {id}


        public async Task<List<TripPlans>> GetPlans() =>
            await _httpClient.GetFromJsonAsync<List<TripPlans>>("TripPlan/Plans");
        public async Task<TripPlans> GetPlan(int id) =>
            await _httpClient.GetFromJsonAsync<TripPlans>($"TripPlan/Plans/{id}");
        public async Task<List<TripPlans>> GetUserPlans(int userId) =>
            await _httpClient.GetFromJsonAsync<List<TripPlans>>($"TripPlan/Plans/User/{userId}");
        public async Task<List<TripPlans>> GetUserPublicPlans(int userId) =>
            await _httpClient.GetFromJsonAsync<List<TripPlans>>($"TripPlan/Plans/User/{userId}/Public");
        public async Task<HttpResponseMessage> GetFilteredPlans(PlansFilterDTO filters)
        {
            HttpResponseMessage httpResponseMessage =
                await _httpClient.GetAsync($"TripPlan/Plans");

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var filtersJson = new StringContent(
                    JsonConvert.SerializeObject(filters),
                    Encoding.UTF8,
                    Application.Json);

                return await _httpClient.PostAsync($"TripPlan/Plans/Filter", filtersJson);
            }
            else
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
            }
        }
        public async Task<List<TripPlans>> GetTopPlans(int quantity) =>
            await _httpClient.GetFromJsonAsync<List<TripPlans>>($"TripPlan/Plans/Latest/{quantity}");
        public async Task<List<TripPlans>> GetUserTopPlans(int userId, int quantity) =>
            await _httpClient.GetFromJsonAsync<List<TripPlans>>($"TripPlan/Plans/Latest/{userId}/{quantity}");
        public async Task<HttpResponseMessage> GetSubsLatestPlans(List<int> userIds, int quantity)
        {
            userIds.Add(quantity);

            HttpResponseMessage httpResponseMessage =
                await _httpClient.GetAsync($"TripPlan/Plans");

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var userIdsJson = new StringContent(
                    JsonConvert.SerializeObject(userIds),
                    Encoding.UTF8,
                    Application.Json);

                return await _httpClient.PostAsync($"TripPlan/Plans/SubsLatest", userIdsJson);
            }
            else
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
            }

        }


        public async Task UpdateTripPlan(TripPlans plan)
        {
            var tripPlanJson = new StringContent(
                JsonConvert.SerializeObject(plan),
                Encoding.UTF8,
                Application.Json);

            using var httpResponseMessage =
                await _httpClient.PutAsync($"TripPlan/{plan.TripPlanId}", tripPlanJson);

            httpResponseMessage.EnsureSuccessStatusCode();
        }
        public async Task AddTripPlan(TripPlans plan)
        {
            var tripPlanJson = new StringContent(
                JsonConvert.SerializeObject(plan),
                Encoding.UTF8,
                Application.Json);

            using var httpResponseMessage =
                await _httpClient.PostAsync($"TripPlan", tripPlanJson);

            httpResponseMessage.EnsureSuccessStatusCode();
        }
        public async Task<HttpResponseMessage> DeleteTripPlan(int id)
        {
            return await _httpClient.DeleteAsync($"TripPlan/{id}");
        }
    }
}
