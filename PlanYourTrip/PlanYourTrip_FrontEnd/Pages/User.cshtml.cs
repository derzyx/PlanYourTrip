using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PlanYourTrip_ClassLibrary.Classes;
using PlanYourTrip_ClassLibrary.ClassesDTO;
using PlanYourTrip_ClassLibrary.KeysStorage;
using PlanYourTrip_FrontEnd.ApiLogic;

namespace PlanYourTrip_FrontEnd.Pages
{
    public class UserModel : PageModel, ILogout
    {
        public enum SubscriptionType
        {
            Subscribed,
            NotSubscribed,
            Self,
            NotLogged
        }

        private readonly TripPlanProcessor _tripPlanProcessor;
        private readonly UserProcessor _userProcessor;
        private readonly SubscriptionProcessor _subscriptionProcessor;

        public UserModel(TripPlanProcessor tripPlanProcessor, UserProcessor userProcessor, SubscriptionProcessor subscriptionProcessor)
        {
            _tripPlanProcessor = tripPlanProcessor;
            _userProcessor = userProcessor;
            _subscriptionProcessor = subscriptionProcessor;
        }

        [BindProperty]
        public UserPublicDataDTO ViewedUser { get; set; }

        [BindProperty]
        public List<TripPlans> Plans { get; set; }

        [BindProperty]
        public SubscriptionType SubType { get; set; }

        [BindProperty]
        public string ViewedUserId { get; set; }

        public async Task<IActionResult> OnGet()
        {
            List<TripPlans> AllPlans = new List<TripPlans>();

            ViewedUserId = Request.Query["id"];
            if(ViewedUserId == null)
            {
                return NotFound();
            }

            ViewedUser = await _userProcessor.GetUserPublicData(Convert.ToInt32(ViewedUserId));
            Plans = await _tripPlanProcessor.GetUserPublicPlans(Convert.ToInt32(ViewedUserId));

            AllPlans = await _tripPlanProcessor.GetPlans();

            if (string.IsNullOrEmpty(HttpContext.Session.GetString(SessionKeys.CurrentUser)))
            {
                SubType = SubscriptionType.NotLogged;
            }
            else
            {
                if (HttpContext.Session.GetString(SessionKeys.CurrentUser) == ViewedUserId)
                {
                    SubType = SubscriptionType.Self;
                }
                else
                {
                    HttpResponseMessage responseMessage = 
                        await _subscriptionProcessor.GetSubscription(new SubscriptionDTO
                    {
                        SubscriberId = Convert.ToInt32(HttpContext.Session.GetString(SessionKeys.CurrentUser)),
                        ObservedId = Convert.ToInt32(ViewedUserId)
                    });

                    SubType = (responseMessage.IsSuccessStatusCode) ? SubscriptionType.Subscribed : SubscriptionType.NotSubscribed;
                }
            }

            return Page();
        }

        public async Task<IActionResult> OnPostRemoveSub()
        {
            int userId = Convert.ToInt32(ViewedUserId);
            int currentUserId = Convert.ToInt32(HttpContext.Session.GetString(SessionKeys.CurrentUser));

            HttpResponseMessage response = await _subscriptionProcessor.GetSubscription(new SubscriptionDTO
            {
                SubscriberId = currentUserId,
                ObservedId = userId
            });
            Subscription sub = await response.Content.ReadFromJsonAsync<Subscription>();

            HttpResponseMessage deleteResponse = await _subscriptionProcessor.RemoveSub(sub.ObservedId);
            if (deleteResponse.IsSuccessStatusCode)
            {
                return new RedirectToPageResult("/User", new { id = sub.ObservedId });
            }
            else
            {
                return new RedirectToPageResult("/Error");
            }
        }

        public async Task<IActionResult> OnPostAddSub()
        {
            int viewedUserId = Convert.ToInt32(ViewedUserId);
            int currentUserId = Convert.ToInt32(HttpContext.Session.GetString(SessionKeys.CurrentUser));

            HttpResponseMessage message = await _subscriptionProcessor.AddSub(new Subscription
            {
                SubscriberId = currentUserId,
                ObservedId = viewedUserId
            });

            if (message.IsSuccessStatusCode)
            {
                return new RedirectToPageResult("/User", new {id = viewedUserId });
            }
            else
            {
                return new RedirectToPageResult("/Error");
            }
        }

        public ActionResult OnPostLogout()
        {
            HttpContext.Session.Remove(SessionKeys.CurrentUser);
            return new RedirectToPageResult("/Index");
        }
    }
}
