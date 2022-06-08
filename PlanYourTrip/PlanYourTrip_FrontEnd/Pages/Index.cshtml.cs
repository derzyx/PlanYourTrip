using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PlanYourTrip_ClassLibrary.Classes;
using PlanYourTrip_ClassLibrary.KeysStorage;
using PlanYourTrip_FrontEnd.ApiLogic;

namespace PlanYourTrip_FrontEnd.Pages
{
    public class IndexModel : PageModel, ILogout
    {
        private readonly TripPlanProcessor _tripPlanProcessor;
        private readonly UserProcessor _userProcessor;

        public IndexModel(TripPlanProcessor tripPlanProcessor, UserProcessor userProcessor) {
            _tripPlanProcessor = tripPlanProcessor;
            _userProcessor = userProcessor;
        }

        private const int quantity = 6;

        [BindProperty]
        public List<TripPlans> Plans { get; set; }

        [BindProperty]
        public List<string> AuthorsNicks { get; set; }

        public List<int> userIds = new List<int>();

        public async Task<IActionResult> OnGet()
        {
            Plans = await _tripPlanProcessor.GetTopPlans(quantity);
            foreach(TripPlans plan in Plans)
            {
                userIds.Add(plan.AutorId);
            }

            HttpResponseMessage response = await _userProcessor.GetUsersNicks(userIds);
            AuthorsNicks = await response.Content.ReadFromJsonAsync<List<string>>();
            return Page();
        }

        public ActionResult OnPostLogout()
        {
            HttpContext.Session.Remove(SessionKeys.CurrentUser);
            return new RedirectToPageResult("/Index");
        }
    }
}