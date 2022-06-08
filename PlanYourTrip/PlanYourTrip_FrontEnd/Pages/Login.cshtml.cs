using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PlanYourTrip_ClassLibrary.Classes;
using PlanYourTrip_ClassLibrary.ClassesDTO;
using PlanYourTrip_ClassLibrary.KeysStorage;
using PlanYourTrip_FrontEnd.ApiLogic;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace PlanYourTrip_FrontEnd.Pages
{
    public class LoginModel : PageModel
    {
        private readonly UserProcessor _userProcessor;

        public LoginModel(UserProcessor userProcessor) =>
            _userProcessor = userProcessor;

        [BindProperty]
        [Required]
        public string Email { get; set; }

        [BindProperty]
        [Required]
        public string Password { get; set; }

        [BindProperty]
        public string ErrorMessage { get; set; }


        private string badRequestCode = HttpStatusCode.BadRequest.ToString();
        private string notFoundCode = HttpStatusCode.NotFound.ToString();
        private string OKCode = HttpStatusCode.OK.ToString();

        public IActionResult OnGet()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString(SessionKeys.CurrentUser)))
            {
                return new RedirectToPageResult("/Login");
            }

            return Page();
        }

        public void OnGetResponseMessage()
        {
            if (HttpContext.Request.Query["c"] == badRequestCode || HttpContext.Request.Query["c"] == notFoundCode)
            {
                ErrorMessage = HttpContext.Request.Query["m"];
            }
        }

        public async Task<IActionResult> OnPost()
        {
            UsersDTO user = new UsersDTO { Email = Email, Haslo = Password };

            HttpResponseMessage message = await _userProcessor.LoginUser(user);

            if (message.IsSuccessStatusCode)
            {
                Users currentUser = await _userProcessor.GetUserByEmail(Email);
                HttpContext.Session.SetString(SessionKeys.CurrentUser, currentUser.Id.ToString());
                return new RedirectToPageResult("/Index");
            }
            else
            {
                return new RedirectToPageResult("/Login", "ResponseMessage", new { c = message.StatusCode, m = message.Content.ReadAsStringAsync().Result });
            }
        }
    }
}
