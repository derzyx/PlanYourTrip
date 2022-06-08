using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PlanYourTrip_ClassLibrary.Classes;
using PlanYourTrip_ClassLibrary.KeysStorage;
using PlanYourTrip_FrontEnd.ApiLogic;

namespace PlanYourTrip_FrontEnd.Pages
{
    public class MySubscriptionsModel : PageModel, ILogout
    {
        private readonly TripPlanProcessor _tripPlanProcessor;
        private readonly UserProcessor _userProcessor;
        private readonly SubscriptionProcessor _subscriptionProcessor;

        public MySubscriptionsModel(TripPlanProcessor tripPlanProcessor, UserProcessor userProcessor, SubscriptionProcessor subscriptionProcessor)
        {
            _tripPlanProcessor = tripPlanProcessor;
            _userProcessor = userProcessor;
            _subscriptionProcessor = subscriptionProcessor;
        }

        [BindProperty]
        public List<int> UserSubs { get; set; }
        [BindProperty]
        public List<string> SubsNicks { get; set; }
        [BindProperty]
        public List<TripPlans> SubsLatestPlans { get; set; }

        public async Task<IActionResult> OnGet()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(SessionKeys.CurrentUser)))
            {
                return new RedirectToPageResult("/Login");
            }

            UserSubs = await _subscriptionProcessor.GetUserSubscriptions(Convert.ToInt32(HttpContext.Session.GetString(SessionKeys.CurrentUser)));
            if(UserSubs.Count > 0)
            {
                HttpResponseMessage nicksResponse = await _userProcessor.GetUsersNicks(UserSubs);
                SubsNicks = await nicksResponse.Content.ReadFromJsonAsync<List<string>>();

                HttpResponseMessage plansResponse = await _tripPlanProcessor.GetSubsLatestPlans(UserSubs, 3);
                SubsLatestPlans = await plansResponse.Content.ReadFromJsonAsync<List<TripPlans>>();

                List<TripPlans> userPlans = SubsLatestPlans.Where(plan => plan.AutorId == UserSubs[0]).ToList();
            }

            return Page();
        }

        public ActionResult OnPostLogout()
        {
            HttpContext.Session.Remove(SessionKeys.CurrentUser);
            return new RedirectToPageResult("/Index");
        }

    }
}
