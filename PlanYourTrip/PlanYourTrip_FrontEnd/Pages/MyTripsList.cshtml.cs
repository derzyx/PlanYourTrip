using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PlanYourTrip_ClassLibrary.Classes;
using PlanYourTrip_ClassLibrary.KeysStorage;
using PlanYourTrip_FrontEnd.ApiLogic;

namespace PlanYourTrip_FrontEnd.Pages
{
    public class MyTripsListModel : PageModel, ILogout
    {
        private readonly TripPlanProcessor _tripPlanProcessor;

        public MyTripsListModel(TripPlanProcessor tripPlanProcessor) =>
            _tripPlanProcessor = tripPlanProcessor;

        [BindProperty]
        public List<TripPlans> Plans { get; set; }
        [BindProperty]
        public string PlanToRemove { get; set; }



        public int Quantity = 23;

        public int MaxGroup = 5;

        public int PlansToLoad { get {
                if (Quantity >= MaxGroup)
                {
                    return MaxGroup;
                }
                else
                {
                    return Quantity;
                }
            } }


        public async Task<IActionResult> OnGet()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(SessionKeys.CurrentUser)))
            {
                return new RedirectToPageResult("/Login");
            }

            Plans = await _tripPlanProcessor.GetUserPlans(Convert.ToInt32(HttpContext.Session.GetString(SessionKeys.CurrentUser)));
            return Page();
        }

        public async Task<IActionResult> OnPostRemovePlan()
        {
            await _tripPlanProcessor.DeleteTripPlan(Convert.ToInt32(PlanToRemove));
            return new RedirectToPageResult("/MyTripsList");
        }

        public ActionResult OnPostLogout()
        {
            HttpContext.Session.Remove(SessionKeys.CurrentUser);
            return new RedirectToPageResult("/Index");
        }
    }
}
