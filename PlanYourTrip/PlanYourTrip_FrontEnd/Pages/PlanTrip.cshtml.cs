using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PlanYourTrip_ClassLibrary.Classes;
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
                    return new TripPlans() { Nazwa = "Nowy plan", PunktyJSON = ""};
                }
                else
                {
                    return _tripPlan;
                }
            } set { _tripPlan = value; } } 

        [BindProperty]
        public string PlanName { get; set; }
        [BindProperty]
        public string TripString { get; set; }

        public async Task OnGet()
        {
            int planId = Convert.ToInt32(HttpContext.Request.Query["plan"]);

            if (planId != 0)
            {
                try
                {
                    TripPlan = await _tripPlanProcessor.GetPlan(planId);
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            //TripString = System.IO.File.ReadAllText(@"C:\Users\mkrau\source\VS2022_repos\PlanYourTrip\PlanYourTrip\PlanYourTrip_FrontEnd\wwwroot\js\TEMPTripString.txt");
            //string tripString = TripString;
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
                        AutorId = 1
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
                        //Contributors = TripPlan.Contributors,
                        //Users = TripPlan.Users
                    };
                    await _tripPlanProcessor.UpdateTripPlan(newPlan);
                }

                return new RedirectToPageResult("/MyTripList");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
