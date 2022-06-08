using Microsoft.AspNetCore.Mvc;

namespace PlanYourTrip_FrontEnd.ApiLogic
{
    public interface ILogout
    {
        public ActionResult OnPostLogout();
    }
}
