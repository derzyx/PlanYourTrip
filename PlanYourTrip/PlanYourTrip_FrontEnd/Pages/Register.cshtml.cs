using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace PlanYourTrip_FrontEnd.Pages
{
    public class RegisterModel : PageModel
    {
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
        [Required]
        [StringLength(30, MinimumLength = 8)]
        public string Password { get; set; }
        [BindProperty]
        public string Desc { get; set; }

        public void OnGet()
        {
        }

        public void OnPostAsync()
        {

        }
    }
}
