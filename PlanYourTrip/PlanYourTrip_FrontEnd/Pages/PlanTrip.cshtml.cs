using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PlanYourTrip_ClassLibrary.Classes;
using PlanYourTrip_ClassLibrary.KeysStorage;
using PlanYourTrip_FrontEnd.ApiLogic;

namespace PlanYourTrip_FrontEnd.Pages
{
    public class PlanTripModel : PageModel
    {
        private readonly TripPlanProcessor _tripPlanProcessor;

        public PlanTripModel(TripPlanProcessor tripPlanProcessor) =>
            _tripPlanProcessor = tripPlanProcessor;
        
        
        private TripPlans _tripPlan { get; set; }

        //Current trip plan
        [BindProperty]
        public TripPlans TripPlan { get { 
                if(_tripPlan == null)
                {
                    return new TripPlans() { PunktyJSON = "" };
                }
                else
                {
                    return _tripPlan;
                }
            } set { 
                _tripPlan = value; 
            } } 


        [BindProperty]
        public string PlanName { get; set; }
        [BindProperty]
        public string TripString { get; set; }
        [BindProperty]
        public bool IsPublic { get; set; }

        public async Task<IActionResult> OnGet()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(SessionKeys.CurrentUser)))
            {
                return new RedirectToPageResult("/Login");
            }

            int planId = Convert.ToInt32(HttpContext.Request.Query["plan"]);

            if (planId != 0)
            {
                try
                {
                    TripPlan = await _tripPlanProcessor.GetPlan(planId);
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            int planId = Convert.ToInt32(HttpContext.Request.Query["plan"]);
            TripPlans newPlan;
            try
            {
                if(planId == 0)
                {
                    newPlan = new TripPlans
                    {
                        Nazwa = PlanName,
                        Opis = "",
                        PunktyJSON = TripString,
                        DataUtworzenia = DateTime.UtcNow,
                        OstatniaAktualizacja = DateTime.UtcNow,
                        AutorId = 1,
                        Publiczny = IsPublic
                    };

                    await _tripPlanProcessor.AddTripPlan(newPlan);
                }
                else
                {
                    TripPlans currentPlan = await _tripPlanProcessor.GetPlan(planId);

                    newPlan = new TripPlans
                    {
                        TripPlanId = currentPlan.TripPlanId,
                        Nazwa = PlanName,
                        Opis = TripPlan.Opis,
                        PunktyJSON = TripString,
                        DataUtworzenia = currentPlan.DataUtworzenia,
                        OstatniaAktualizacja = DateTime.UtcNow,
                        AutorId = Convert.ToInt32(currentPlan.AutorId),
                        Publiczny = IsPublic
                    };
                    await _tripPlanProcessor.UpdateTripPlan(newPlan);
                }

                return new RedirectToPageResult("/MyTripsList");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
