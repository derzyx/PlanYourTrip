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

        public void OnGet()
        {
            if (!string.IsNullOrEmpty(HttpContext.Request.Query["e"]))
            {
                ErrorMessage = "Nieprawid³owe dane logowania";
            }
        }

        public async Task<IActionResult> OnPost()
        {
            UsersDTO user = new UsersDTO { Email = Email, Haslo = Password };

            using var httpResponseMessage =
                await _userProcessor.LoginUser(user);

            HttpStatusCode statusCode = httpResponseMessage.StatusCode;

            switch (statusCode)
            {
                case HttpStatusCode.OK:
                    Users currentUser = await _userProcessor.GetUserByEmail(Email);
                    HttpContext.Session.SetString(SessionKeys.CurrentUser, currentUser.Id.ToString());
                    return new RedirectToPageResult("/Index");
                case HttpStatusCode.BadRequest:
                    return new RedirectToPageResult("/Login", new { e = HttpStatusCode.BadRequest });
                case HttpStatusCode.NotFound:
                    return new RedirectToPageResult("/Login", new { e = HttpStatusCode.NotFound });
                default:
                    return new RedirectToPageResult("/Login", new { e = HttpStatusCode.BadRequest });
            }
        }
    }
}
