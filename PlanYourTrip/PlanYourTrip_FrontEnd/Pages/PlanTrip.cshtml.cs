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
        public string IsPublic { get; set; } = "false";

        [BindProperty]
        public string PlanDesc { get; set; }

        [BindProperty]
        public string PageOnExit { get; set; }

        public async Task<IActionResult> OnGet()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(SessionKeys.CurrentUser)))
            {
                return new RedirectToPageResult("/Login");
            }

            string url = Request.Headers["Referer"].ToString();

            int planId = Convert.ToInt32(HttpContext.Request.Query["plan"]);

            if (planId != 0)
            {
                try
                {
                    TripPlan = await _tripPlanProcessor.GetPlan(planId);
                    IsPublic = (TripPlan.Publiczny) ? "true" : "false";
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
            TripPlans newPlan = new TripPlans
            {
                Nazwa = TripPlan.Nazwa,
                Opis = TripPlan.Opis,
                PunktyJSON = TripPlan.PunktyJSON,
                DataUtworzenia = DateTime.UtcNow.AddHours(2),
                OstatniaAktualizacja = DateTime.UtcNow.AddHours(2),
                AutorId = Convert.ToInt32(HttpContext.Session.GetString(SessionKeys.CurrentUser)),
                Publiczny = (Request.Form["planVisibility"] == "true") ? true : false
            };
            try
            {
                if (planId == 0)
                {
                    await _tripPlanProcessor.AddTripPlan(newPlan);
                }
                else
                {
                    TripPlans currentPlan = await _tripPlanProcessor.GetPlan(planId);

                    if (Convert.ToInt32(HttpContext.Session.GetString(SessionKeys.CurrentUser)) != currentPlan.AutorId)
                    {
                        newPlan.DataUtworzenia = DateTime.UtcNow.AddHours(2);
                        await _tripPlanProcessor.AddTripPlan(newPlan);
                    }
                    else
                    {
                        newPlan.TripPlanId = currentPlan.TripPlanId;
                        newPlan.DataUtworzenia = currentPlan.DataUtworzenia;
                        await _tripPlanProcessor.UpdateTripPlan(newPlan);
                    }
                }

                if (PageOnExit == "stay")
                {
                    return Page();
                }
                else if (PageOnExit == "exit")
                {
                    return new RedirectToPageResult("/MyTripsList");
                }
                else
                {
                    return new RedirectToPageResult("/Error");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
