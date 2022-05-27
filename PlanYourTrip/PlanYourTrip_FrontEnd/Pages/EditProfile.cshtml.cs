using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PlanYourTrip_ClassLibrary.Classes;
using PlanYourTrip_ClassLibrary.KeysStorage;
using PlanYourTrip_FrontEnd.ApiLogic;
using System.ComponentModel.DataAnnotations;

namespace PlanYourTrip_FrontEnd.Pages
{
    public class EditProfileModel : PageModel, ILogout
    {
        private readonly UserProcessor _userProcessor;

        public EditProfileModel(UserProcessor userProcessor) =>
            _userProcessor = userProcessor;

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

        //Second form
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
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(SessionKeys.CurrentUser)))
            {
                return new RedirectToPageResult("/Login");
            }

            Users currentUser = await _userProcessor.GetUserById(Convert.ToInt32(HttpContext.Session.GetString(SessionKeys.CurrentUser)));
            if(currentUser == null)
            {
                return new RedirectToPageResult("/Error");
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
