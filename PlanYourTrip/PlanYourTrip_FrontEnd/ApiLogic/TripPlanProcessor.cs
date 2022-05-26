using Newtonsoft.Json;
using PlanYourTrip_ClassLibrary.Classes;
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

            //_httpClient.BaseAddress = new Uri("https://planyourtrip-backendapp.azurewebsites.net/api/");
            _httpClient.BaseAddress = new Uri("https://localhost:7224/api/");
        }

        // Get all plans
        public async Task<List<TripPlans>> GetPlans() =>
            await _httpClient.GetFromJsonAsync<List<TripPlans>>("TripPlan");

        // Get plan by id
        public async Task<TripPlans> GetPlan(int id) =>
            await _httpClient.GetFromJsonAsync<TripPlans>($"TripPlan/{id}");

        public async Task<List<TripPlans>> GetUserPlans(int userId) =>
            await _httpClient.GetFromJsonAsync<List<TripPlans>>($"TripPlan/MyPlans/{userId}");

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

        public async Task DeleteTripPlan(int id)
        {
            using var httpResponseMessage =
                await _httpClient.DeleteAsync($"TripPlan/{id}");

            httpResponseMessage.EnsureSuccessStatusCode();
        }
    }
}
