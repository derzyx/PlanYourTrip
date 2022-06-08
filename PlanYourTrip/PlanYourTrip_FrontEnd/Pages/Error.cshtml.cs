using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PlanYourTrip_ClassLibrary.KeysStorage;
using PlanYourTrip_FrontEnd.ApiLogic;
using System.Diagnostics;

namespace PlanYourTrip_FrontEnd.Pages
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    [IgnoreAntiforgeryToken]
    public class ErrorModel : PageModel, ILogout
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        private readonly ILogger<ErrorModel> _logger;

        public ErrorModel(ILogger<ErrorModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
        }

        public ActionResult OnPostLogout()
        {
            HttpContext.Session.Remove(SessionKeys.CurrentUser);
            return new RedirectToPageResult("/Index");
        }
    }
}