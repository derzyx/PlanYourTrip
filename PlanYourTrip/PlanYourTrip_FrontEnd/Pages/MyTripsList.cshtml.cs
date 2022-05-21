using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PlanYourTrip_ClassLibrary.Classes;
using PlanYourTrip_FrontEnd.ApiLogic;

namespace PlanYourTrip_FrontEnd.Pages
{
    public class MyTripsListModel : PageModel
    {
        private readonly TripPlanProcessor _tripPlanProcessor;

        public MyTripsListModel(TripPlanProcessor tripPlanProcessor) =>
            _tripPlanProcessor = tripPlanProcessor;

        [BindProperty]
        public List<TripPlans> Plans { get; set; }
        [BindProperty]
        public string PostToRemove { get; set; }



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



        public async Task OnGet()
        {
            Plans = await _tripPlanProcessor.GetUserPlans(1);
        }

        public async Task<ActionResult> OnPost()
        {
            await _tripPlanProcessor.DeleteTripPlan(Convert.ToInt32(PostToRemove));
            return RedirectToPage("/MyTripsList");
        }
    }
}
