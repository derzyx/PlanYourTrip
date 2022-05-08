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
        
        

        [BindProperty]
        public TripPlans TripPlan { get; set; } 
        [BindProperty]
        public string PlanName { get; set; }
        [BindProperty]
        public string TripString { get; set; }


        public async Task OnGet()
        {
            try
            {
                TripPlan = await _tripPlanProcessor.GetPlan(4);
            }
            catch (Exception ex)
            {
                throw;
            }
            //TripString = System.IO.File.ReadAllText(@"C:\Users\mkrau\source\VS2022_repos\PlanYourTrip\PlanYourTrip\PlanYourTrip_FrontEnd\wwwroot\js\TEMPTripString.txt");
            //string tripString = TripString;
        }

        public async Task<IActionResult> OnPost()
        {
            //string planName = PlanName;
            //string tripString = TripString;
            try
            {
                var currentPlan = await _tripPlanProcessor.GetPlan(4);

                TripPlans newPlan = new TripPlans {
                    TripPlanId = Convert.ToInt32(currentPlan.TripPlanId),
                    Nazwa = PlanName,
                    Opis = currentPlan.Opis,
                    PunktyJSON = TripString,
                    AutorId = Convert.ToInt32(currentPlan.AutorId),
                    Contributors = currentPlan.Contributors,
                    Users = currentPlan.Users
                };

                await _tripPlanProcessor.UpdateTripPlan(newPlan);

                return new RedirectToPageResult("/Index");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
