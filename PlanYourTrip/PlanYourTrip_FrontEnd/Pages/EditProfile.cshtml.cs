using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PlanYourTrip_ClassLibrary.Classes;
using System.ComponentModel.DataAnnotations;

namespace PlanYourTrip_FrontEnd.Pages
{
    public class EditProfileModel : PageModel
    {
        private readonly TripPlanProcessor _tripPlanProcessor;

        public PlanTripModel(TripPlanProcessor tripPlanProcessor) =>
            _tripPlanProcessor = tripPlanProcessor;

        //First form
        [BindProperty]
        [Required]
        [StringLength(30, MinimumLength = 4)]
        public string Nick { get; set; }
        [BindProperty]
        public string Name { get; set; }
        [BindProperty]
        public string Surname { get; set; }
        [BindProperty]
        [Required]
        public string Email { get; set; }
        [BindProperty]
        public string Desc { get; set; }

        //Secont form
        [BindProperty]
        [Required]
        [StringLength(30, MinimumLength = 8)]
        public string CurrentPassword { get; set; }
        [BindProperty]
        [Required]
        [StringLength(30, MinimumLength = 8)]
        public string NewPassword { get; set; }
        [BindProperty]
        [Required]
        [StringLength(30, MinimumLength = 8)]
        public string NewPasswordRepeat { get; set; }

        [BindProperty]
        public Users CurrentUser { get; set; }

        public async Task<IActionResult> OnGet()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("_CurrentUser")))
            {
                return new RedirectToPageResult("/Login");
            }
            else
            {
                
            }

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {

        }
    }
}
