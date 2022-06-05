using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PlanYourTrip_ClassLibrary.Classes;
using PlanYourTrip_ClassLibrary.ClassesDTO;
using PlanYourTrip_ClassLibrary.KeysStorage;
using PlanYourTrip_FrontEnd.ApiLogic;
using System.ComponentModel.DataAnnotations;

namespace PlanYourTrip_FrontEnd.Pages
{
    public class AllTripPlansModel : PageModel, ILogout
    {
        private readonly TripPlanProcessor _tripPlanProcessor;
        private readonly UserProcessor _userProcessor;

        public AllTripPlansModel(TripPlanProcessor tripPlanProcessor, UserProcessor userProcessor)
        {
            _tripPlanProcessor = tripPlanProcessor;
            _userProcessor = userProcessor;
        }

        private const int quantity = 6;

        [BindProperty]
        public string SortType { get; set; }

        [BindProperty]
        public string DateMin { get; set; }
        [BindProperty]
        public string DateMax { get; set;}


        [BindProperty]
        public string ErrorMessage { get; set; }

        [BindProperty]
        public string EndOfListMessage { get; set; } = "To ju¿ wszystkie posty.";

        [BindProperty]
        public List<TripPlans> Plans { get; set; }

        [BindProperty]
        public List<string> AuthorsNicks { get; set; }

        public List<int> userIds = new List<int>();

        public async Task<IActionResult> OnGet()
        {
            if (!string.IsNullOrEmpty(HttpContext.Request.Query["e"]))
            {
                ErrorMessage = HttpContext.Request.Query["e"];
            }

            Plans = await _tripPlanProcessor.GetTopPlans(quantity);
            foreach (TripPlans plan in Plans)
            {
                userIds.Add(plan.AutorId);
            }
            HttpResponseMessage response = await _userProcessor.GetUsersNicks(userIds);
            AuthorsNicks = await response.Content.ReadFromJsonAsync<List<string>>();
            return Page();
        }

        public async Task<IActionResult> OnGetFiltered()
        {
            PlansFilterDTO filterDTO = new PlansFilterDTO
            {
                SortType = HttpContext.Request.Query["sort"],
                DateMin = HttpContext.Request.Query["mindate"],
                DateMax = HttpContext.Request.Query["maxdate"]
            };

            DateMin = (!string.IsNullOrEmpty(HttpContext.Request.Query["mindate"])) ? HttpContext.Request.Query["mindate"] : "";
            DateMax = (!string.IsNullOrEmpty(HttpContext.Request.Query["maxdate"])) ? HttpContext.Request.Query["maxdate"] : "";


            if(!string.IsNullOrEmpty(HttpContext.Request.Query["mindate"]) && !string.IsNullOrEmpty(HttpContext.Request.Query["maxdate"]) && DateOnly.Parse(DateMin) > DateOnly.Parse(DateMax))
            {
                return new RedirectToPageResult("AllTripPlans", new {e = "Data 'DO' jest starsza od daty 'OD'"});
            }

            HttpResponseMessage plansResponse = await _tripPlanProcessor.GetFilteredPlans(filterDTO);
            Plans = await plansResponse.Content.ReadFromJsonAsync<List<TripPlans>>();

            EndOfListMessage = (Plans.Count > 0) ? "To ju¿ wszystkie plany." : "Nie znaleziono ¿adnych planów.";

            foreach (TripPlans plan in Plans)
            {
                userIds.Add(plan.AutorId);
            }
            HttpResponseMessage nicksResponse = await _userProcessor.GetUsersNicks(userIds);
            AuthorsNicks = await nicksResponse.Content.ReadFromJsonAsync<List<string>>();
            return Page();
        }

        public async Task<IActionResult> OnPostFilter()
        {
            PlansFilterDTO filters = new PlansFilterDTO
            {
                SortType = Request.Form["sortType"],
                DateMin = DateMin,
                DateMax = DateMax
            };

            Console.WriteLine(filters);

            return new RedirectToPageResult("AllTripPlans", "Filtered", new {mindate = filters.DateMin, maxdate = filters.DateMax, sort = filters.SortType});
        }

        public ActionResult OnPostLogout()
        {
            HttpContext.Session.Remove(SessionKeys.CurrentUser);
            return new RedirectToPageResult("/Index");
        }
    }
}
