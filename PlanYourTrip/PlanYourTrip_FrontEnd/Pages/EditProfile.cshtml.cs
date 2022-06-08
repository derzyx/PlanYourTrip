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
    public class EditProfileModel : PageModel, ILogout
    {
        private readonly UserProcessor _userProcessor;

        public EditProfileModel(UserProcessor userProcessor) =>
            _userProcessor = userProcessor;

        //First form
        [BindProperty]
        public string Nick { get; set; }
        [BindProperty]
        public string? Name { get; set; }
        [BindProperty]
        public string? Surname { get; set; }
        [BindProperty]
        public string Email { get; set; }
        [BindProperty]
        public string? Desc { get; set; }

        //Second form
        [BindProperty]
        public string CurrentPassword { get; set; }
        [BindProperty]
        public string NewPassword { get; set; }
        [BindProperty]
        public string NewPasswordRepeat { get; set; }

        [BindProperty]
        public Users CurrentUser { get; set; }

        [BindProperty]
        public string SuccessMessage { get; set; }

        [BindProperty]
        public string ErrorMessage { get; set; }

        [BindProperty]
        public string DeleteConfirmText { get; set; } = "USUWAM";
        [BindProperty]
        public string DeleteConfirmInput { get; set; }

        public async Task<IActionResult> OnGet()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(SessionKeys.CurrentUser)))
            {
                return new RedirectToPageResult("/Login");
            }

            CurrentUser = await _userProcessor.GetUserById(Convert.ToInt32(HttpContext.Session.GetString(SessionKeys.CurrentUser)));
            if(CurrentUser == null)
            {
                return new RedirectToPageResult("/Error");
            }
            Desc = CurrentUser.Opis;

            //PUT method returns 204 No Content if success
            if (HttpContext.Request.Query["c"] == HttpStatusCode.OK.ToString())
            {
                SuccessMessage = HttpContext.Request.Query["m"];
            }
            else
            {
                ErrorMessage = HttpContext.Request.Query["m"];
            }

            return Page();
        }

        public async Task<ActionResult> OnPostUserData()
        {
            string errorMessage = "";
            if (Nick == null || Nick.Length < 4 || Nick.Length > 30)
            {
                errorMessage += "Nick powinien mieæ od 4 do 30 znaków <br/>";
            }
            if(Email == null)
            {
                errorMessage += "Email jest wymagany <br/>";
            }

            if (errorMessage.Length > 0)
            {
                HttpStatusCode code = HttpStatusCode.BadRequest;
                return new RedirectToPageResult("/EditProfile", new {c = code, m = errorMessage});
            }

            Users currentUser = await _userProcessor.GetUserById(Convert.ToInt32(HttpContext.Session.GetString(SessionKeys.CurrentUser)));

            Users userToUpdate = new Users
            {
                Id = currentUser.Id,
                Nick = Nick,
                Imie = Name,
                Nazwisko = Surname,
                Email = Email,
                Haslo = currentUser.Haslo,
                Opis = Desc,
                IdAvatar = currentUser.IdAvatar,
                TripPlans = currentUser.TripPlans
            };

            HttpResponseMessage response = await _userProcessor.UpdateUser(userToUpdate);
            return new RedirectToPageResult("/EditProfile", new { c = response.StatusCode, m = response.Content.ReadAsStringAsync().Result });
        }

        public async Task<ActionResult> OnPostPassword()
        {
            string errorMessage = "";
            if (CurrentPassword == null || NewPassword == null || NewPasswordRepeat == null)
            {
                errorMessage += "Nie wype³niono wymaganego pola <br/>";
            }
            else
            {
                if (NewPassword != NewPasswordRepeat)
                {
                    errorMessage += "Nowe has³a nie s¹ takie same <br/>";
                }

                if (NewPassword.Length < 8 || NewPassword.Length > 30 ||
                    NewPasswordRepeat.Length < 8 || NewPasswordRepeat.Length > 30)
                {
                    errorMessage += "Has³o musi mieæ od 8 do 30 znaków <br/>";
                }
            }

            if(errorMessage.Length > 0)
            {
                return new RedirectToPageResult("/EditProfile", new { c = HttpStatusCode.BadRequest, m = errorMessage });
            }

            HttpResponseMessage response = await _userProcessor.UpdatePassword(new PasswordDTO
            {
                UserId = Convert.ToInt32(HttpContext.Session.GetString(SessionKeys.CurrentUser)),
                OldPassword = CurrentPassword,
                NewPassword = NewPassword,
                NewPasswordRepeat = NewPasswordRepeat
            });
            return new RedirectToPageResult("/EditProfile", new { c = response.StatusCode, m = response.Content.ReadAsStringAsync().Result });
        }

        public async Task<ActionResult> OnPostDeleteUser()
        {
            if (DeleteConfirmInput != DeleteConfirmText)
            {
                return new RedirectToPageResult("/EditProfile", new { c = HttpStatusCode.BadRequest, m = "Wprowadzone s³owo jest inne <br/>" });
            }
            else
            {
                HttpResponseMessage response = await _userProcessor.DeleteUser(Convert.ToInt32(HttpContext.Session.GetString(SessionKeys.CurrentUser)));
                if(response.StatusCode == HttpStatusCode.OK)
                {
                    HttpContext.Session.Remove(SessionKeys.CurrentUser);
                    return new RedirectToPageResult("/Index");
                }

                return new RedirectToPageResult("/EditProfile", new { c = response.StatusCode, m = response.Content.ReadAsStringAsync().Result });
            }
        }

        public ActionResult OnPostLogout()
        {
            HttpContext.Session.Remove(SessionKeys.CurrentUser);
            return new RedirectToPageResult("/Index");
        }
    }
}
