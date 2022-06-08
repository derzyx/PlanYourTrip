using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PlanYourTrip_ClassLibrary.Classes;
using PlanYourTrip_ClassLibrary.KeysStorage;
using PlanYourTrip_FrontEnd.ApiLogic;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace PlanYourTrip_FrontEnd.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly UserProcessor _userProcessor;

        public RegisterModel(UserProcessor userProcessor) =>
            _userProcessor = userProcessor;

        [BindProperty]
        [Required]
        [StringLength(30, MinimumLength = 4)]
        public string Nick { get; set; }
        [BindProperty]
        public string? Name { get; set; }
        [BindProperty]
        public string? Surname { get; set; }
        [BindProperty]
        [Required]
        public string Email { get; set; }
        [BindProperty]
        [Required]
        [StringLength(30, MinimumLength = 8)]
        public string Password { get; set; }
        [BindProperty]
        public string? Desc { get; set; }


        [BindProperty]
        public string ErrorMessage { get; set; }

        [BindProperty]
        public string SuccessMessage { get; set; }

        public void OnGet()
        {

        }

        public void OnGetResponseMessage()
        {
            if(HttpContext.Request.Query["c"] == HttpStatusCode.OK.ToString())
            {
                SuccessMessage = HttpContext.Request.Query["m"];
            }
            else
            {
                ErrorMessage = HttpContext.Request.Query["m"];
            }
        }

        public async Task<IActionResult> OnPost()
        {
            Users newUser = new Users
            {
                Nick = Nick,
                Imie = Name,
                Nazwisko = Surname,
                Email = Email,
                Haslo = Password,
                Opis = Desc
            };

            HttpResponseMessage message = await _userProcessor.AddUser(newUser);

            return new RedirectToPageResult("/Register", "ResponseMessage", new { c = message.StatusCode, m = message.Content.ReadAsStringAsync().Result });
        }
    }
}
